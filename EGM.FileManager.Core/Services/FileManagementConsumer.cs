using EGM.FileManager.Core.Abstractions.Channels;
using EGM.FileManager.Core.Abstractions.Services;
using EGM.FileManager.Core.Options;
using EGM.FileManager.Core.Primitives;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EGM.FileManager.Core.Services
{
    /// <summary>
    /// Defines the properties and methods for a <see cref="FileManagementConsumer"/>.
    /// </summary>
    internal sealed class FileManagementConsumer : IFileManagementConsumer
    {
        private readonly ILogger<FileManagementConsumer> _logger;
        private readonly FileManagerOptions _options;
        private readonly IQueue<ManagedFile> _fileQueue;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileManagementConsumer"/> class.
        /// </summary>
        /// <param name="logger">A logger instance.</param>
        /// <param name="fileQueue">A queue instance.</param>
        /// <param name="options">An options instance.</param>
        /// <exception cref="ArgumentNullException">Thrown when a required ctor parameter is null.</exception>
        public FileManagementConsumer(ILogger<FileManagementConsumer> logger,
            IQueue<ManagedFile> fileQueue,
            IOptions<FileManagerOptions> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _fileQueue = fileQueue ?? throw new ArgumentNullException(nameof(fileQueue));
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        /// <inheritdoc/>
        public async Task ProcessFiles(CancellationToken cancellationToken = default)
        {
            await foreach (var file in _fileQueue.ReadAllAsync(cancellationToken))
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning(Properties.logMessages.CancellationRequested);
                }

                _logger.LogInformation(Properties.logMessages.ReadFileFromQueue, file.FilePath);

                if (!File.Exists(file.FilePath))
                {
                    _logger.LogError(Properties.logMessages.FileNotFound, file.FilePath);
                    continue;
                }

                if (!_options.TargetDirectoryBindings.ContainsKey(file.FilePath) && !_options.ProcessUnsupportedFileTypes)
                {
                    _logger.LogError(Properties.logMessages.FileTypeNotSupported, file.FilePath);
                    continue;
                }

                var targetDir = _options.TargetDirectoryBindings[file.FileType];

                if (!Directory.Exists(targetDir) && !_options.CreateNonExistantTargetDirectories)
                {
                    _logger.LogError(Properties.logMessages.TargetDirNotFound, targetDir);
                    continue;
                }
                else if (!Directory.Exists(targetDir) && _options.CreateNonExistantTargetDirectories)
                {
                    Directory.CreateDirectory(targetDir);
                    _logger.LogError(Properties.logMessages.CreatedTargetDir, targetDir);
                }

                try
                {
                    var fileInfo = new FileInfo(file.FilePath);
                    File.Move(file.FilePath, Path.Combine(targetDir, fileInfo.Name));
                    _logger.LogInformation(Properties.logMessages.FileMovedSuccessfully, file.FilePath, targetDir);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, Properties.logMessages.ErrorOccurredMovingFile, file.FilePath, targetDir, e.Message);
                    continue;
                }
            }
        }
    }
}
