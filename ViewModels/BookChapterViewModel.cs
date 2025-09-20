using ASPNETCoreWebApp.Models.Documentation;

namespace ASPNETCoreWebApp.ViewModels;

public class BookChapterViewModel
{
    public BookChapterViewModel(Book book, Chapter? current, Chapter? previous, Chapter? next)
    {
        Book = book;
        CurrentChapter = current;
        PreviousChapter = previous;
        NextChapter = next;
    }

    public Book Book { get; }

    public Chapter? CurrentChapter { get; }

    public Chapter? PreviousChapter { get; }

    public Chapter? NextChapter { get; }

    public bool IsActiveChapter(Chapter chapter)
    {
        return CurrentChapter is not null &&
               string.Equals(CurrentChapter.Slug, chapter.Slug, StringComparison.OrdinalIgnoreCase);
    }
}
