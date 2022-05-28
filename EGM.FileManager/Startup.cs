using EGM.FileManager.Core.Extensions;
using EGM.FileManager.Core.Options;

namespace EGM.FileManager
{
    internal sealed class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var fileManagerConfigSection = Configuration.GetSection(FileManagerOptions.FileManager);
            var fileManagerConfig = fileManagerConfigSection.Get<FileManagerOptions>();
            services.AddFileManagement(opts =>
            {
                opts.DefaultDirectory = fileManagerConfig.DefaultDirectory;
                opts.SourceDirectory = fileManagerConfig.SourceDirectory;
                opts.TargetDirectoryBindings = fileManagerConfig.TargetDirectoryBindings;
                opts.ProcessUnsupportedFileTypes = fileManagerConfig.ProcessUnsupportedFileTypes;
            });
            services.AddHostedService<Worker>();
        }
    }
}
