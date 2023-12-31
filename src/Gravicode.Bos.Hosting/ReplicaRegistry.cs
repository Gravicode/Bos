﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Gravicode.Bos.Hosting
{
    public class ReplicaRegistry : IDisposable
    {
        private readonly ILogger _logger;
        private readonly ConcurrentDictionary<string, object> _fileWriteLocks;
        private readonly string _bosFolderPath;

        public ReplicaRegistry(string directory, ILogger logger)
        {
            _logger = logger;
            _fileWriteLocks = new ConcurrentDictionary<string, object>();
            _bosFolderPath = Path.Join(directory, ".bos");
        }

        public bool WriteReplicaEvent(string storeName, IDictionary<string, string> replicaRecord)
        {
            if (!Directory.Exists(_bosFolderPath))
            {
                Directory.CreateDirectory(_bosFolderPath);
            }

            var filePath = Path.Join(_bosFolderPath, GetStoreFile(storeName));
            var contents = JsonSerializer.Serialize(replicaRecord, new JsonSerializerOptions { WriteIndented = false });
            var lockObj = GetLockForStore(storeName);

            lock (lockObj)
            {
                try
                {
                    File.AppendAllText(filePath, contents + Environment.NewLine);
                    return true;
                }
                catch (DirectoryNotFoundException ex)
                {
                    _logger.LogWarning(ex, "bos folder is not found. file: {file}", filePath);
                    return false;
                }
            }
        }

        public bool DeleteStore(string storeName)
        {
            var filePath = Path.Join(_bosFolderPath, GetStoreFile(storeName));

            if (!File.Exists(filePath))
            {
                return false;
            }

            try
            {
                File.Delete(filePath);
                return true;
            }
            catch (DirectoryNotFoundException ex)
            {
                _logger.LogWarning(ex, "bos folder is not found. file: {file}", filePath);
                return false;
            }
        }

        public async ValueTask<IList<IDictionary<string, string>>> GetEvents(string storeName)
        {
            var filePath = Path.Join(_bosFolderPath, GetStoreFile(storeName));

            if (!File.Exists(filePath))
            {
                return Array.Empty<IDictionary<string, string>>();
            }

            var contents = await File.ReadAllTextAsync(filePath);
            var events = contents.Split(Environment.NewLine);

            return events.Where(e => !string.IsNullOrEmpty(e.Trim()))
                .Select(e => JsonSerializer.Deserialize<IDictionary<string, string>>(e))
                .ToList()!;
        }

        private object GetLockForStore(string storeName)
        {
            return _fileWriteLocks.GetOrAdd(storeName, _ => new object());
        }

        private string GetStoreFile(string storeName) => $"{storeName}_store";

        public void Dispose()
        {
            if (Directory.Exists(_bosFolderPath))
            {
                try
                {
                    DirectoryExtensions.DeleteDirectory(_bosFolderPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
