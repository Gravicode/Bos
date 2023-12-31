﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Bos;
using Bos.Serialization;
using YamlDotNet.Serialization;

namespace Gravicode.Bos.ConfigModel
{
    // General design note re: nullability - we use [Required] to validate for non-null
    // so we use = default! to reflect that. Code that creates a ConfigApplication should
    // validate it before dereferencing the properties.
    public class ConfigApplication
    {
        // This gets set by all of the code paths that read the application
        [YamlIgnore]
        public FileInfo Source { get; set; } = default!;

        public string? Name { get; set; }

        public int? DashboardPort { get; set; }

        public string? BuildSolution { get; set; }

        public string? Namespace { get; set; }

        public ConfigRegistry? Registry { get; set; }

        public ContainerEngineType? ContainerEngineType { get; set; }

        public string? Network { get; set; }

        public List<Dictionary<string, object>> Extensions { get; set; } = new List<Dictionary<string, object>>();

        public List<ConfigService> Services { get; set; } = new List<ConfigService>();

        public List<ConfigIngress> Ingress { get; set; } = new List<ConfigIngress>();

        public void Validate()
        {
            var config = this;

            var context = new ValidationContext(config);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(config, context, results, validateAllProperties: true))
            {
                throw new BosYamlException(
                    "Configuration validation failed." + Environment.NewLine +
                    string.Join(Environment.NewLine, results.Select(r => r.ErrorMessage)));
            }

            foreach (var extension in config.Extensions)
            {
                if (!extension.TryGetValue("name", out var name) || string.IsNullOrWhiteSpace(name as string))
                {
                    throw new BosYamlException(CoreStrings.ExtensionMustProvideAName);
                }
            }

            if (config.Registry != null)
            {
                if (!Validator.TryValidateObject(config.Registry, new ValidationContext(config.Registry), results, validateAllProperties: true))
                {
                    throw new BosYamlException(
                        "Registry validation failed." + Environment.NewLine +
                        string.Join(Environment.NewLine, results.Select(r => r.ErrorMessage)));
                }
            }

            foreach (var service in config.Services)
            {
                context = new ValidationContext(service);
                if (!Validator.TryValidateObject(service, context, results, validateAllProperties: true))
                {
                    throw new BosYamlException(
                        $"Service '{service.Name}' validation failed." + Environment.NewLine +
                        string.Join(Environment.NewLine, results.Select(r => r.ErrorMessage)));
                }

                if (config.Services.Count(o => o.Name == service.Name) > 1)
                {
                    throw new BosYamlException(CoreStrings.ServiceMustHaveUniqueNames);
                }

                if (service.Project != null && service.Image != null)
                {
                    throw new BosYamlException(CoreStrings.FormatProjectImageExecutableExclusive("project", "image"));
                }

                if (service.Project != null && service.Executable != null)
                {
                    throw new BosYamlException(CoreStrings.FormatProjectImageExecutableExclusive("project", "executable"));
                }

                if (service.Image != null && service.Executable != null)
                {
                    throw new BosYamlException(CoreStrings.FormatProjectImageExecutableExclusive("image", "executable"));
                }

                foreach (var binding in service.Bindings)
                {
                    context = new ValidationContext(binding);
                    if (!Validator.TryValidateObject(binding, context, results, validateAllProperties: true))
                    {
                        throw new BosYamlException(
                            $"Binding '{binding.Name}' of service '{service.Name}' validation failed." + Environment.NewLine +
                            string.Join(Environment.NewLine, results.Select(r => r.ErrorMessage)));
                    }

                    if (string.IsNullOrEmpty(binding.Name) && service.Bindings.Count > 1)
                    {
                        throw new BosYamlException(CoreStrings.FormatMultipleBindingWithoutName("service"));
                    }

                    if (service.Bindings.Count(o => o.Name == binding.Name) > 1)
                    {
                        throw new BosYamlException(CoreStrings.FormatMultipleBindingWithSameName("service"));
                    }

                    if (service.Bindings.Count(o => o.Port != null && o.Port == binding.Port) > 1)
                    {
                        throw new BosYamlException(CoreStrings.FormatMultipleBindingWithSamePort("service"));
                    }
                }

                foreach (var envVar in service.Configuration)
                {
                    context = new ValidationContext(envVar);
                    if (!Validator.TryValidateObject(envVar, context, results, validateAllProperties: true))
                    {
                        throw new BosYamlException(
                            $"Environment variable '{envVar.Name}' of service '{service.Name}' validation failed." + Environment.NewLine +
                            string.Join(Environment.NewLine, results.Select(r => r.ErrorMessage)));
                    }
                }

                foreach (var volume in service.Volumes)
                {
                    context = new ValidationContext(volume);
                    if (!Validator.TryValidateObject(volume, context, results, validateAllProperties: true))
                    {
                        throw new BosYamlException(
                            $"Volume '{volume.Source}' of service '{service.Name}' validation failed." + Environment.NewLine +
                            string.Join(Environment.NewLine, results.Select(r => r.ErrorMessage)));
                    }
                }

                var probes = new[] { (Name: "liveness", Probe: service.Liveness), (Name: "readiness", Probe: service.Readiness) }.Where(p => p.Probe != null).ToArray();
                foreach (var probe in probes)
                {
                    context = new ValidationContext(probe.Probe!);
                    if (!Validator.TryValidateObject(probe.Probe!, context, results, validateAllProperties: true))
                    {
                        throw new BosYamlException(
                            $"Probe '{probe.Name}' in service '{service.Name}' validation failed." + Environment.NewLine +
                            string.Join(Environment.NewLine, results.Select(r => r.ErrorMessage)));
                    }

                    // right now only http is supported, so it must be set
                    if (probe.Probe!.Http == null)
                    {
                        throw new BosYamlException(CoreStrings.FormatProberRequired(probe.Name));
                    }

                    context = new ValidationContext(probe.Probe!.Http);
                    if (!Validator.TryValidateObject(probe.Probe!.Http, context, results, validateAllProperties: true))
                    {
                        throw new BosYamlException(
                            $"Http in probe '{probe.Name}' in service '{service.Name}' validation failed." + Environment.NewLine +
                            string.Join(Environment.NewLine, results.Select(r => r.ErrorMessage)));
                    }

                    if (probe.Name == "liveness" && probe.Probe!.SuccessThreshold != 1)
                    {
                        throw new BosYamlException(CoreStrings.FormatSuccessThresholdMustBeOne(probe.Name));
                    }
                }
            }

            foreach (var ingress in config.Ingress)
            {
                context = new ValidationContext(ingress);
                if (!Validator.TryValidateObject(ingress, context, results, validateAllProperties: true))
                {
                    throw new BosYamlException(
                        $"Ingress '{ingress.Name}' validation failed." + Environment.NewLine +
                        string.Join(Environment.NewLine, results.Select(r => r.ErrorMessage)));
                }

                foreach (var binding in ingress.Bindings)
                {
                    context = new ValidationContext(binding);
                    if (!Validator.TryValidateObject(binding, context, results, validateAllProperties: true))
                    {
                        throw new BosYamlException(
                            $"Binding '{binding.Name}' of ingress '{ingress.Name}' validation failed." + Environment.NewLine +
                            string.Join(Environment.NewLine, results.Select(r => r.ErrorMessage)));
                    }
                    if (string.IsNullOrEmpty(binding.Name) && ingress.Bindings.Count > 1)
                    {
                        throw new BosYamlException(CoreStrings.FormatMultipleBindingWithoutName("ingress"));
                    }
                    if (ingress.Bindings.Count(o => o.Name == binding.Name) > 1)
                    {
                        throw new BosYamlException(CoreStrings.FormatMultipleBindingWithSameName("ingress"));
                    }
                    if (binding.Protocol != "http" && binding.Protocol != "https" && binding.Protocol != null)
                    {
                        throw new BosYamlException(CoreStrings.IngressBindingMustBeHttpOrHttps);
                    }
                    if (ingress.Bindings.Count(o => o.Port == binding.Port) > 1)
                    {
                        throw new BosYamlException(CoreStrings.FormatMultipleBindingWithSamePort("ingress"));
                    }
                }

                // Make sure all ingress rules have an associated service
                foreach (var rule in ingress.Rules)
                {
                    context = new ValidationContext(rule);
                    if (!Validator.TryValidateObject(rule, context, results, validateAllProperties: true))
                    {
                        throw new BosYamlException(
                            $"Rule '{rule.Path}' of ingress '{ingress.Name}' validation failed." + Environment.NewLine +
                            string.Join(Environment.NewLine, results.Select(r => r.ErrorMessage)));
                    }

                    if (config.Services.Count(o => o.Name == rule.Service) != 1)
                    {
                        throw new BosYamlException(CoreStrings.IngressRuleMustReferenceService);
                    }
                }
            }
        }
    }
}