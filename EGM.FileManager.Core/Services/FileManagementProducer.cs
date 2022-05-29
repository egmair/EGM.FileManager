using EGM.FileManager.Core.Abstractions.Channels;
using EGM.FileManager.Core.Abstractions.Services;
using EGM.FileManager.Core.Options;
using EGM.FileManager.Core.Primitives;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        private const int TIMER_PERIOD_MS = -1;
        private uint TIME_SPAN;

        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<FileManagementProducer> _logger;
        private readonly FileManagerOptions _options;
        private readonly IQueue<ManagedFile> _fileQueue;

        private Timer? _timer;
        private CancellationTokenSource? _cancellationTokenSource;
        private Task? _consumerTask;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileManagementProducer"/> class.
        /// </summary>
        /// <param name="logger">A logger instance.</param>
        /// <param name="options">An options instance.</param>
        /// <exception cref="ArgumentNullException">Thrown when a required ctor parameter is null.</exception>
        public FileManagementProducer(ILogger<FileManagementProducer> logger,
            IOptionsMonitor<FileManagerOptions> optionsMonitor,
            IOptions<FileManagerOptions> options,
            IQueue<ManagedFile> fileQueue,
            IHostApplicationLifetime appLifetime,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _fileQueue = fileQueue ?? throw new ArgumentNullException(nameof(fileQueue));
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _appLifetime = appLifetime ?? throw new ArgumentNullException(nameof(appLifetime));
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));

            TIME_SPAN = optionsMonitor.CurrentValue.EnqueueDelay ?? 1000;

            optionsMonitor.OnChange(cfg =>
            {
                TIME_SPAN = cfg.EnqueueDelay ?? 1000;
            });
        }

        /// <inheritdoc/>
        public Task ScanDirectory(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
