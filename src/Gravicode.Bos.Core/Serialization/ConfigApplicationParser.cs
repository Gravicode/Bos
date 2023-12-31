﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Gravicode.Bos.ConfigModel;
using YamlDotNet.RepresentationModel;

namespace Bos.Serialization
{
    public static class ConfigApplicationParser
    {
        public static void HandleConfigApplication(YamlMappingNode yamlMappingNode, ConfigApplication app)
        {
            foreach (var child in yamlMappingNode.Children)
            {
                var key = YamlParser.GetScalarValue(child.Key);

                switch (key)
                {
                    case "name":
                        app.Name = YamlParser.GetScalarValue(key, child.Value);
                        break;
                    case "solution":
                        app.BuildSolution = YamlParser.GetScalarValue(key, child.Value);
                        break;
                    case "namespace":
                        app.Namespace = YamlParser.GetScalarValue(key, child.Value);
                        break;
                    case "network":
                        app.Network = YamlParser.GetScalarValue(key, child.Value);
                        break;
                    case "registry":
                        app.Registry = ConfigRegistryParser.HandleRegistry(key, child.Value);
                        break;
                    case "containerEngine":
                        string engine = YamlParser.GetScalarValue(key, child.Value);
                        if (engine.Equals("docker", StringComparison.InvariantCultureIgnoreCase))
                        {
                            app.ContainerEngineType = ContainerEngineType.Docker;
                        }
                        else if (engine.Equals("podman", StringComparison.InvariantCultureIgnoreCase))
                        {
                            app.ContainerEngineType = ContainerEngineType.Podman;
                        }
                        else
                        {
                            throw new BosYamlException($"Unknown container engine: \"{engine}\"");
                        }
                        break;
                    case "dashboardPort":
                        if (int.TryParse(YamlParser.GetScalarValue(key, child.Value), out var dashboardPort))
                        {
                            app.DashboardPort = dashboardPort;
                        }
                        else
                        {
                            throw new BosYamlException(child.Key.Start, CoreStrings.FormatMustBeAnInteger(key));
                        }
                        break;
                    case "ingress":
                        YamlParser.ThrowIfNotYamlSequence(key, child.Value);
                        ConfigIngressParser.HandleIngress((child.Value as YamlSequenceNode)!, app.Ingress);
                        break;
                    case "services":
                        YamlParser.ThrowIfNotYamlSequence(key, child.Value);
                        ConfigServiceParser.HandleServiceMapping((child.Value as YamlSequenceNode)!, app.Services, app);
                        break;
                    case "extensions":
                        YamlParser.ThrowIfNotYamlSequence(key, child.Value);
                        ConfigExtensionsParser.HandleExtensionsMapping((child.Value as YamlSequenceNode)!, app.Extensions);
                        break;
                    default:
                        throw new BosYamlException(child.Key.Start, CoreStrings.FormatUnrecognizedKey(key));
                }
            }
        }
    }
}
