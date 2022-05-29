using EGM.FileManager.Core.Abstractions.Channels;
using EGM.FileManager.Core.Options;
using EGM.FileManager.Core.Primitives;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EGM.FileManager.Core.Channels
{
    /// <summary>
    /// Defines the properties and methods for a <see cref="ManagedFileQueue"/> class.
    /// </summary>
    internal sealed class ManagedFileQueue : IQueue<ManagedFile>
    {
        private readonly ILogger<ManagedFileQueue> _logger;
        private Channel<ManagedFile> _channel;

        public ManagedFileQueue(ILogger<ManagedFileQueue> logger, IOptions<FileManagerOptions> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _channel = Channel.CreateBounded<ManagedFile>(options.Value.QueueLimit ?? 10);
        }

        /// <inheritdoc/>
        public void CloseQueue()
            => _channel.Writer.Complete();

        /// <inheritdoc/>
        public IAsyncEnumerable<ManagedFile> ReadAllAsync(CancellationToken cancellationToken = default)
            => _channel.Reader.ReadAllAsync(cancellationToken);

        /// <inheritdoc/>
        public bool TryQueue(ManagedFile item)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool TryReadQueue(out ManagedFile? item)
        {
            item = null;

            if (_channel.Reader.TryRead(out var managedFile))
            {
                item = managedFile;
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public async ValueTask<bool> WaitToReadQueueAsync(CancellationToken cancellationToken = default)
            => await _channel.Reader.WaitToReadAsync(cancellationToken);
    }
}
