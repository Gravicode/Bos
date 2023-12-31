﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gravicode.Bos.Hosting.Model
{
    public class DockerVolume
    {
        public DockerVolume(string? source, string? name, string target, bool readOnly = false)
        {
            Source = source;
            Name = name;
            Target = target;
            ReadOnly = readOnly;
        }

        public string? Name { get; }
        public string? Source { get; }
        public string Target { get; }
        public bool ReadOnly { get; }
    }
}
