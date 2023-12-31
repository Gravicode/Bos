﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(web =>
                {
                    web.UseStartup<Startup>()
                       .ConfigureKestrel(options =>
                       {
                           options.ConfigureEndpointDefaults(o => o.Protocols = HttpProtocols.Http2);
                       });
                });
    }
}
