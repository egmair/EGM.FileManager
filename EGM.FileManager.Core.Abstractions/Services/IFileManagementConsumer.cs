using System.Threading;
using System.Threading.Tasks;

namespace EGM.FileManager.Core.Abstractions.Services
{
    /// <summary>
    /// Defines a contract for a <see cref="IFileManagementConsumer"/>.
    /// </summary>
    public interface IFileManagementConsumer
    {
        /// <summary>
        /// Processes files in the produced queue.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>A <see cref="Task"/> representing the operation.</returns>
        Task ProcessFiles(CancellationToken cancellationToken = default);
    }
}
