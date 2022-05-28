using System.Collections.Generic;

namespace EGM.FileManager.Core.Options
{
    /// <summary>
    /// Defines the properties and methods for a <see cref="FileManagerOptions"/> class.
    /// </summary>
    public sealed class FileManagerOptions
    {
        /// <summary>
        /// The source directory to monitor for incoming files.
        /// </summary>
        public string? SourceDirectory { get; set; }

        /// <summary>
        /// The collection of bindings to place particular file types into certain directories.
        /// </summary>
        public Dictionary<string, string> TargetDirectoryBindings { get; set; } = new Dictionary<string, string>();
    }
}
