﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gravicode.Bos.Hosting.Model.V1
{
    public class V1DockerVolume
    {
        public string? Name { get; set; }
        public string? Source { get; set; }
        public string? Target { get; set; }
    }
}
