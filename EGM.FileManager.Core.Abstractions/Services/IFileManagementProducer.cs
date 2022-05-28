using System;
using System.Threading;
using System.Threading.Tasks;

namespace EGM.FileManager.Core.Abstractions.Services
{
    /// <summary>
    /// Defines a contract for a <see cref="IFileManagementProducer"/>.
    /// </summary>
    public interface IFileManagementProducer
    {
        /// <summary>
        /// Scans a target directory for files to be processed.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>A <see cref="Task"/>, representing the operation.</returns>
        Task ScanDirectory(CancellationToken cancellationToken = default);
    }
}
