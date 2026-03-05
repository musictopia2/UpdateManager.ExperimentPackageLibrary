namespace UpdateManager.ExperimentPackageLibrary;
public class UpdateExperimentClass(IExperimentContext context, INugetPacker packer, INugetUploader uploader)
{
    public async Task UpdateAsync(BuildHookArgs arguments)
    {
        string csprojPath = Path.Combine(arguments.ProjectDir, arguments.ProjectFileName);
        CsProjEditor editor = new(csprojPath);
        if (editor.GetFeedType() is not null)
        {
            Console.WriteLine("This was already a local or public feed.  Therefore, can't update it");
            return;
        }

        if (editor.IsLibraryProject() == false)
        {
            Console.WriteLine("This is not a library.  Therefore, can't update it");
            return;
        }
        ExperimentPackageModel? experiment = await context.UpsertExperimentAsync(arguments, csprojPath);
        if (experiment is null)
        {
            Console.WriteLine("Failed to update because most likely archived");
            return;
        }
        bool created = await packer.CreateNugetPackageAsync(experiment, true);
        if (!created)
        {
            throw new CustomBasicException("Failed to create nuget package.");
        }

        if (!Directory.Exists(experiment.NugetPackagePath))
        {
            throw new CustomBasicException($"NuGet package path does not exist: {experiment.NugetPackagePath}");
        }
        var files = ff1.FileList(experiment.NugetPackagePath);
        files.RemoveAllOnly(x => !x.EndsWith(".nupkg", StringComparison.OrdinalIgnoreCase));
        if (files.Count != 1)
        {
            throw new CustomBasicException($"Error: Expected 1 .nupkg file, but found {files.Count}.");
        }
        string nugetFile = ff1.FullFile(files.Single());
        //its no longer local.

        bool uploaded = await uploader.UploadNugetPackageAsync(nugetFile);
        if (uploaded == false)
        {
            throw new CustomArgumentException("Failed to upload package to desired server");
        }


    }
}