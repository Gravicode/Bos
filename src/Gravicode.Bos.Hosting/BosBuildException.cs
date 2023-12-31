﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.Serialization;

namespace Gravicode.Bos.Hosting
{
    [Serializable]
    internal class BosBuildException : Exception
    {
        public BosBuildException()
        {
        }

        public BosBuildException(string? message) : base(message)
        {
        }

        public BosBuildException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BosBuildException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
