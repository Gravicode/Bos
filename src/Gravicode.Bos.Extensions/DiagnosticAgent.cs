// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Linq;

namespace Gravicode.Bos
{
    public static class DiagnosticAgent
    {
        public static SidecarBuilder GetOrAddSidecar(DotnetProjectServiceBuilder project)
        {
            // Bring your rain boots.
            project.RelocateDiagnosticsDomainSockets = true;

            var sidecar = project.Sidecars.FirstOrDefault(s => s.Name == "bos-diag-agent");
            if (sidecar != null)
            {
                return sidecar;
            }

            sidecar = new SidecarBuilder("bos-diag-agent", "rynowak/bos-diag-agent", "0.1")
            {
                Args =
                {
                    "--kubernetes=true",
                    $"--service={project.Name}",
                    $"--assemblyName={project.AssemblyName}",
                },
            };
            project.Sidecars.Add(sidecar);
            return sidecar;
        }
    }
}
