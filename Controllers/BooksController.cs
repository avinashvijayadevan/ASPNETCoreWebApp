using ASPNETCoreWebApp.Services;
using ASPNETCoreWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreWebApp.Controllers;

[Route("books")]
public class BooksController : Controller
{
    private readonly IDocumentationService _documentationService;

    public BooksController(IDocumentationService documentationService)
    {
        _documentationService = documentationService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var books = _documentationService.GetBooks();
        return View(books);
    }

    [HttpGet("{bookSlug}")]
    public IActionResult Book(string bookSlug)
    {
        var book = _documentationService.GetBookBySlug(bookSlug);
        if (book is null)
        {
            return NotFound();
        }

        var orderedChapters = book.Chapters
            .OrderBy(chapter => chapter.Order)
            .ThenBy(chapter => chapter.Title)
            .ToList();

        var firstChapter = orderedChapters.FirstOrDefault();
        var nextChapter = orderedChapters.Skip(1).FirstOrDefault();

        var viewModel = new BookChapterViewModel(book, firstChapter, null, nextChapter);

        return View("Chapter", viewModel);
    }

    [HttpGet("{bookSlug}/chapter/{chapterSlug}")]
    public IActionResult Chapter(string bookSlug, string chapterSlug)
    {
        var book = _documentationService.GetBookBySlug(bookSlug);
        if (book is null)
        {
            return NotFound();
        }

        var (chapter, previous, next) = _documentationService.GetChapterWithSiblings(bookSlug, chapterSlug);
        if (chapter is null)
        {
            return RedirectToAction(nameof(Book), new { bookSlug });
        }

        var viewModel = new BookChapterViewModel(book, chapter, previous, next);
        return View(viewModel);
    }
}
