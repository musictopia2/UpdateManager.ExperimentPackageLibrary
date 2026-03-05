namespace UpdateManager.ExperimentPackageLibrary;
public class ImportProcesses
{
    public static async Task ImportAsync(BuildHookArgs build)
    { 
        try
        {
            CustomBasicException.ThrowIfNull(bb1.Configuration);
            CsProjEditor editor = new(build.ProjectFileName);
            if (editor.GetFeedType() is not null)
            {
                Console.WriteLine("This was already a local or public feed");
                return;
            }
            if (editor.IsLibraryProject() == false)
            {
                Console.WriteLine("This is not a library");
                return;
            }
            if (editor.HasPostBuildTarget())
            {
                Console.WriteLine("This already has a post build program.  Experiments can't have more than one.  Maybe already done");
                return;
            }
            string path = bb1.Configuration.ExperimentPostBuildFeedProcessorProgram;
            editor.AddPostBuildCommand(path, false);
            editor.SaveChanges();
            Console.WriteLine("Added the post build program to experiments.  Do a build in order to publish");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Had this error {ex.Message}  The file was {build.ProjectFileName}");
            return;
        }
    }
}