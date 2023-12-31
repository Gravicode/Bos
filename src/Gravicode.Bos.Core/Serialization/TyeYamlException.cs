// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using YamlDotNet.Core;

namespace Bos.Serialization
{
    public class BosYamlException : Exception
    {
        public BosYamlException(string message)
            : base(message)
        {
        }

        public BosYamlException(Mark start, string message)
            : this(start, message, null)
        {
        }

        public BosYamlException(Mark start, string message, Exception? innerException)
            : base($"Error parsing YAML: ({start.Line}, {start.Column}): {message}", innerException)
        {
        }

        public BosYamlException(Mark start, string message, Exception? innerException, FileInfo fileInfo)
            : base($"Error parsing '{fileInfo.Name}': ({start.Line}, {start.Column}): {message}", innerException)
        {
        }

        public BosYamlException(string message, Exception? inner)
            : base(message, inner)
        {
        }
    }
}
