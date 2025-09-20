namespace ASPNETCoreWebApp.Models.Documentation;

public class Book
{
    public string Title { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string Icon { get; init; } = string.Empty;

    public string AccentColor { get; init; } = "#4F46E5";

    public DateTimeOffset LastUpdated { get; init; }
        = DateTimeOffset.UtcNow;

    public IReadOnlyList<string> Tags { get; init; } = Array.Empty<string>();

    public IReadOnlyList<Chapter> Chapters { get; init; } = Array.Empty<Chapter>();
}
