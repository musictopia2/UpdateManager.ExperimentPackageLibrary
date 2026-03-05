namespace UpdateManager.ExperimentPackageLibrary;
public class ExperimentPackageModel : INugetModel
{
    public string CsProjPath { get; set; } = "";
    public string NugetPackagePath { get; set; } = "";
    public bool Development { get; set; } = true; //this should always show in development since its experimental.
    public string PackageName { get; set; } = "";
    public string PrefixForPackageName { get; set; } = "";
    public string Version { get; set; } = "";
    public BasicList<string> ArchivedLocations { get; set; } = [];
}