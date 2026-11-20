using ICSharpCode.SharpZipLib.Zip;

namespace Aurelia.VenueStudio;

/// <summary>
/// Project archives (*.venuepkg) bundle a project file with its logic and media. Planners and
/// integrators send them to each other, so an archive is input from outside.
/// </summary>
public static class ProjectArchive
{
    public static void Extract(string archivePath, string targetDirectory)
    {
        // FastZip extracts every entry of the archive below targetDirectory.
        new FastZip().ExtractZip(archivePath, targetDirectory, fileFilter: null);
    }

    public static void Create(string archivePath, string sourceDirectory)
    {
        new FastZip().CreateZip(archivePath, sourceDirectory, recurse: true, fileFilter: null);
    }
}
