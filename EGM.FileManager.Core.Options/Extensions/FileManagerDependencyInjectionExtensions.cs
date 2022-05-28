using EGM.FileManager.Core.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    // TODO: Move this extension to an implementation library. It's current location is not appropriate.
    public static class FileManagerDependencyInjectionExtensions
    {
        public static void AddFileManagement(this IServiceCollection services, Action<FileManagerOptions>? options = null)
        {
            // TODO: Register services.
        }
    }
}
