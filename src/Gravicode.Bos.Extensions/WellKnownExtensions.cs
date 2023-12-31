// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Gravicode.Bos.Extensions.Dapr;
using Gravicode.Bos.Extensions.Elastic;
using Gravicode.Bos.Extensions.Seq;
using Gravicode.Bos.Extensions.Zipkin;

namespace Gravicode.Bos.Extensions
{
    public static class WellKnownExtensions
    {
        public static IReadOnlyDictionary<string, Extension> Extensions = new Dictionary<string, Extension>(StringComparer.InvariantCultureIgnoreCase)
        {
            { "dapr", new DaprExtension() },
            { "elastic", new ElasticStackExtension() },
            { "seq", new SeqExtension() },
            { "zipkin", new ZipkinExtension() },
        };
    }
}
