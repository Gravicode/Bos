﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gravicode.Bos.Hosting.Model
{
    public class IngressRunInfo : RunInfo
    {
        public IngressRunInfo(List<IngressRule> rules)
        {
            Rules = rules;
        }

        public List<IngressRule> Rules { get; }
    }
}
