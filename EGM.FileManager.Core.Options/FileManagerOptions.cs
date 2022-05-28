using System.Collections.Generic;

namespace EGM.FileManager.Core.Options
{
    public sealed class FileManagerOptions
    {
        public string? SourceDirectory { get; set; }

        public Dictionary<string, string> TargetDirectoryBindings { get; set; } = new Dictionary<string, string>();
    }
}
