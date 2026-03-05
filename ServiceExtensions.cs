
namespace UpdateManager.ExperimentPackageLibrary;
public static class ServiceExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection RegisterImportServices()
        {
            //this is when you register the import processes
            //actually don't register the packer when you are importing.
            services.AddSingleton<IExperimentContext, FileExperimentContext>();
                ;
            return services; 
        }
    }
}