using EGM.FileManager.Core.Abstractions.Services;
using EGM.FileManager.Core.Services;
using EGM.FileManager.Core.Options;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EGM.FileManager.Core.Extensions
{
    public static class FileManagerDependencyInjectionExtensions
    {
        public static void AddFileManagement(this IServiceCollection services, Action<FileManagerOptions>? options = null)
        {
            services.AddSingleton<IFileManagementProducer, FileManagementProducer>()
                .AddSingleton<IFileManagementConsumer, FileManagementConsumer>()
                .Configure(options);
        }
    }
}
