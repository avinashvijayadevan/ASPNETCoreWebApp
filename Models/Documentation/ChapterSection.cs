namespace ASPNETCoreWebApp.Models.Documentation;

public class ChapterSection
{
    public string Anchor { get; init; } = string.Empty;

    public string Title { get; init; } = string.Empty;

    public IReadOnlyList<string> Paragraphs { get; init; }
        = Array.Empty<string>();

    public IReadOnlyList<string>? KeyPoints { get; init; }
        = Array.Empty<string>();

    public string? CalloutTitle { get; init; }
        = null;

    public string? CalloutBody { get; init; }
        = null;

    public string CalloutTone { get; init; }
        = "info";
}
