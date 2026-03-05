namespace UpdateManager.ExperimentPackageLibrary;
public class FileExperimentContext : IExperimentContext
{
    readonly string _packagePath = bb1.Configuration!.RequiredNuGetExperimentPath;
    async Task<ExperimentPackageModel?> IExperimentContext.UpsertExperimentAsync(BuildHookArgs args, string csProj)
    {
        BasicList<ExperimentPackageModel> packages;
        
        if (ff1.FileExists(_packagePath) == false)
        {
            packages = [];
        }
        else
        {
            packages = await js1.RetrieveSavedObjectAsync<BasicList<ExperimentPackageModel>>(_packagePath);
        }
        string nugetPath = Path.Combine(args.ProjectDir, "bin", "Release");
        ExperimentPackageModel? existing = packages.SingleOrDefault(x => x.PackageName == args.ProjectName);
        if (existing is null)
        {
            existing = new();
            existing.PackageName = args.ProjectName;
            existing.Version = "1.0.0";
            existing.CsProjPath = csProj;
            //no archived locations yet.
            existing.NugetPackagePath = nugetPath;
            packages.Add(existing);
            await js1.SaveObjectAsync(_packagePath, packages);

            return existing; //this means approved because its brand new.
        }


        if (existing.ArchivedLocations.Any(x => x == csProj))
        {
            return null; //this was archived
        }
        if (existing.CsProjPath != csProj)
        {
            existing.ArchivedLocations.Add(existing.CsProjPath);
            existing.CsProjPath = csProj;
            existing.NugetPackagePath = nugetPath;
        }
        existing.Version.IncrementMinorVersion();
        await js1.SaveObjectAsync(_packagePath, packages);
        return existing;
    }
}