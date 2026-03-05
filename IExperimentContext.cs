namespace UpdateManager.ExperimentPackageLibrary;
public interface IExperimentContext
{
    Task<BasicList<ExperimentPackageModel>> GetExperimentsAsync();

    // Updates only the version of the package identified by its name.
    Task UpdateExperimentVersionAsync(string packageName, string version);

    // Updates the properties of a package with the provided model.
    Task UpdateExperimentAsync(ExperimentPackageModel package);

    //actually chose just to add the experiment period now.  the updater has to figure out if it can or not.
    Task AddExperimentAsync(BuildHookArgs args);
    //may need to save an entirely new list (like if i had to add another field).
    Task SaveCompleteListAsync(BasicList<ExperimentPackageModel> packages);
}
