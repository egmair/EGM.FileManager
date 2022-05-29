using EGM.FileManager.Core.Abstractions.Services;
using EGM.FileManager.Core.Services;
using EGM.FileManager.Core.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using EGM.FileManager.Core.Abstractions.Channels;
using EGM.FileManager.Core.Primitives;
using EGM.FileManager.Core.Channels;

namespace EGM.FileManager.Core.Extensions
{
    public static class FileManagerDependencyInjectionExtensions
    {
        public static void AddFileManagement(this IServiceCollection services, Action<FileManagerOptions>? options = null)
        {
            services.AddSingleton<IQueue<ManagedFile>, ManagedFileQueue>()
                .AddSingleton<IFileManagementConsumer, FileManagementConsumer>()
                .AddSingleton<IFileManagementProducer, FileManagementProducer>()
                .Configure(options);
        }
    }
}
