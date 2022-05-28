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
    /// Defines the properties and methods for a <see cref="FileManagementProducer"/>.
    /// </summary>
    internal sealed class FileManagementProducer : IFileManagementProducer
    {
        private readonly ILogger<FileManagementProducer> _logger;
        private readonly FileManagerOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileManagementProducer"/> class.
        /// </summary>
        /// <param name="logger">A logger instance.</param>
        /// <param name="options">An options instance.</param>
        /// <exception cref="ArgumentNullException">Thrown when a required ctor parameter is null.</exception>
        public FileManagementProducer(ILogger<FileManagementProducer> logger, IOptions<FileManagerOptions> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        /// <inheritdoc/>
        public Task ScanDirectory(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
