using System.IO;
using Gravicode.Bos.ConfigModel;
using Gravicode.Bos.Serialization;

namespace Gravicode.Bos
{
    public static class InitHost
    {
        public static string CreateBosFile(FileInfo? path, bool force)
        {
            var (content, outputFilePath) = CreateBosFileContent(path, force);

            File.WriteAllText(outputFilePath, content);

            return outputFilePath;
        }

        public static (string, string) CreateBosFileContent(FileInfo? path, bool force)
        {
            if (path is FileInfo && path.Exists && !force)
            {
                ThrowIfBosFilePresent(path, "bos.yml");
                ThrowIfBosFilePresent(path, "bos.yaml");
            }

            if (force)
            {
                // Don't use existing bos.yaml if we are force creating it again.
                // path prior is pointing to the bos.yaml file still, so refind another file that isn't the bos.yaml
                var hasViableFileType = ConfigFileFinder.TryFindSupportedFile(path?.DirectoryName ?? ".",
                    out var filePath,
                    out var errorMessage,
                    new string[] { "*.csproj", "*.fsproj", "*.sln" });

                if (!hasViableFileType)
                {
                    throw new CommandException(errorMessage!);
                }

                path = new FileInfo(filePath!);
            }

            var template = @"
# bos application configuration file
# read all about it at https://github.com/dotnet/bos
#
# when you've given us a try, we'd love to know what you think:
#    https://aka.ms/AA7q20u
#
# define global settings here
# name: exampleapp # application name
# registry: exampleuser # dockerhub username or container registry hostname

# define multiple services here
services:
- name: myservice
  # project: app.csproj # msbuild project path (relative to this file)
  # executable: app.exe # path to an executable (relative to this file)
  # args: --arg1=3 # arguments to pass to the process
  # replicas: 5 # number of times to launch the application
  # env: # array of environment variables
  #  - name: key
  #    value: value
  # bindings: # optional array of bindings (ports, connection strings)
    # - port: 8080 # number port of the binding
".TrimStart();

            // Output in the current directory unless an input file was provided, then
            // output next to the input file.
            var outputFilePath = "bos.yaml";

            if (path is FileInfo && path.Exists)
            {
                var application = ConfigFactory.FromFile(path);
                var serializer = YamlSerializer.CreateSerializer();

                var extension = path.Extension.ToLowerInvariant();
                var directory = path.Directory;

                // Clear all bindings if any for solutions and project files
                if (extension == ".sln" || extension == ".csproj" || extension == ".fsproj")
                {
                    // If the input file is a project or solution then use that as the name
                    application.Extensions = null!;
                    application.Ingress = null!;

                    foreach (var service in application.Services)
                    {
                        service.Bindings = null!;
                        service.Configuration = null!;
                        service.Volumes = null!;
                        service.Project = service.Project!.Substring(directory!.FullName.Length).TrimStart('/');
                    }

                    // If the input file is a sln/project then place the config next to it
                    outputFilePath = Path.Combine(directory!.FullName, "bos.yaml");
                }
                else
                {
                    // If the input file is a yaml, then replace it.
                    outputFilePath = path.FullName;
                }

                template = @"
# bos application configuration file
# read all about it at https://github.com/dotnet/bos
#
# when you've given us a try, we'd love to know what you think:
#    https://aka.ms/AA7q20u
#
".TrimStart() + serializer.Serialize(application);
            }

            return (template, outputFilePath);
        }

        private static void ThrowIfBosFilePresent(FileInfo? path, string yml)
        {
            var bosYaml = Path.Combine(path!.DirectoryName!, yml);
            if (File.Exists(bosYaml))
            {
                throw new CommandException($"File '{bosYaml}' already exists. Use --force to override the {yml} file if desired.");
            }
        }
    }
}
