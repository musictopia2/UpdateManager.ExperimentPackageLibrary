namespace UpdateManager.ExperimentPackageLibrary;
public class FileExperimentContext : IExperimentContext
{
    Task<bool> IExperimentContext.AddExperimentAsync(BuildHookArgs args)
    {
        throw new NotImplementedException();
    }

    Task<BasicList<ExperimentPackageModel>> IExperimentContext.GetExperimentsAsync()
    {
        throw new NotImplementedException();
    }

    Task IExperimentContext.SaveCompleteListAsync(BasicList<ExperimentPackageModel> packages)
    {
        throw new NotImplementedException();
    }

    Task IExperimentContext.UpdateExperimentAsync(ExperimentPackageModel package)
    {
        throw new NotImplementedException();
    }

    Task IExperimentContext.UpdateExperimentVersionAsync(string packageName, string version)
    {
        throw new NotImplementedException();
    }
}