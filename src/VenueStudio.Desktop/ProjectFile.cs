using Newtonsoft.Json;

namespace Aurelia.VenueStudio;

/// <summary>Description of one installation: zones, amplifiers, sources and the control logic.</summary>
public sealed class ProjectFile
{
    public string Name { get; set; } = "";
    public List<Zone> Zones { get; set; } = new();
    public List<string> Libraries { get; set; } = new();

    /// <summary>Reads a project description. Project files are exchanged between planners.</summary>
    public static ProjectFile Load(string path) =>
        JsonConvert.DeserializeObject<ProjectFile>(File.ReadAllText(path)) ?? new ProjectFile();

    public void Save(string path) =>
        File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
}

public sealed class Zone
{
    public string Name { get; set; } = "";
    public List<string> Amplifiers { get; set; } = new();
}
