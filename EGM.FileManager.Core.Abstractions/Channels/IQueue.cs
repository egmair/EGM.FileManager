using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EGM.FileManager.Core.Abstractions.Channels
{
    /// <summary>
    /// Defines a contract for a <see cref="IQueue{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of item being queued.</typeparam>
    public interface IQueue<T> where T : class
    {
        /// <summary>
        /// Attempts to add an item to the queue.
        /// </summary>
        /// <param name="item">The item to be queued.</param>
        /// <returns>A <see cref="bool"/> value - <c>true</c> if the item was queued successfully.</returns>
        bool TryQueue(T item);

        /// <summary>
        /// Attempts to read an item from the queue.
        /// </summary>
        /// <param name="item">The item read from the queue.</param>
        /// <returns>A <typeparamref name="T"/> value.</returns>
        bool TryReadQueue(out T? item);

        /// <summary>
        /// Asynchronously reads all items from the queue.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>An <see cref="IAsyncEnumerable{T}"/> instance.</returns>
        IAsyncEnumerable<T>? ReadAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Stops items from being added to the queue.
        /// </summary>
        void CloseQueue();

        /// <summary>
        /// Waits to read an item from the queue.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns></returns>
        ValueTask<bool> WaitToReadQueueAsync(CancellationToken cancellationToken = default);
    }
}
