using EGM.FileManager.Core.Abstractions.Services;
using EGM.FileManager.Core.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="FileManagementConsumer"/> class.
        /// </summary>
        /// <param name="logger">A logger instance.</param>
        /// <param name="options">An options instance.</param>
        /// <exception cref="ArgumentNullException">Thrown when a required ctor parameter is null.</exception>
        public FileManagementConsumer(ILogger<FileManagementConsumer> logger, IOptions<FileManagerOptions> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        /// <inheritdoc/>
        public Task ProcessFiles(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
