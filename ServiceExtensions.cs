namespace UpdateManager.ExperimentPackageLibrary;
public static class ServiceExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection RegisterUpdateServices()
        {
            //this is when you register the import processes
            //actually don't register the packer when you are importing.
            services.AddSingleton<IExperimentContext, FileExperimentContext>()
                .AddSingleton<INugetPacker, NugetPacker>()
                
                ;
            return services; 
        }
        
    }
}