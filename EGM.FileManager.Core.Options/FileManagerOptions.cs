using System.Collections.Generic;

namespace EGM.FileManager.Core.Options
{
    /// <summary>
    /// Defines the properties and methods for a <see cref="FileManagerOptions"/> class.
    /// </summary>
    public sealed class FileManagerOptions
    {
        public const string FileManager = "FileManager";

        /// <summary>
        /// The source directory to monitor for incoming files.
        /// </summary>
        public string? SourceDirectory { get; set; }

        /// <summary>
        /// Whether the file manager should process unsupported file types.
        /// </summary>
        public bool ProcessUnsupportedFileTypes { get; set; } = false;

        /// <summary>
        /// The default directory to place unsupported file types.
        /// </summary>
        public string? DefaultDirectory { get; set; }

        /// <summary>
        /// The collection of bindings to place particular file types into certain directories.
        /// </summary>
        public Dictionary<string, string> TargetDirectoryBindings { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// The maximum number of files to hold in the processing queue.
        /// </summary>
        public int QueueLimit { get; set; } = 10;

        /// <summary>
        /// A delay on adding items to the queue.
        /// </summary>
        public uint EnqueueDelay { get; set; } = 1000;

        /// <summary>
        /// Whether the file manager should create a target directory if it does not exist.
        /// </summary>
        public bool CreateNonExistantTargetDirectories { get; set; } = false;
    }
}
