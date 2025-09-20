namespace ASPNETCoreWebApp.Models.Documentation;

public class Chapter
{
    public string Title { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public string Summary { get; init; } = string.Empty;

    public int Order { get; init; }
        = 0;

    public int EstimatedReadMinutes { get; init; }
        = 5;

    public IReadOnlyList<ChapterSection> Sections { get; init; }
        = Array.Empty<ChapterSection>();
}
