using ASPNETCoreWebApp.Models.Documentation;

namespace ASPNETCoreWebApp.Services;

public interface IDocumentationService
{
    IReadOnlyList<Book> GetBooks();

    Book? GetBookBySlug(string slug);

    Chapter? GetChapter(string bookSlug, string chapterSlug);

    (Chapter? chapter, Chapter? previous, Chapter? next) GetChapterWithSiblings(string bookSlug, string chapterSlug);
}
