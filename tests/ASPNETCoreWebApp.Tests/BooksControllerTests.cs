using ASPNETCoreWebApp.Controllers;
using ASPNETCoreWebApp.Models.Documentation;
using ASPNETCoreWebApp.Services;
using ASPNETCoreWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ASPNETCoreWebApp.Tests;

public class BooksControllerTests
{
    [Fact]
    public void Index_ReturnsBooksFromService()
    {
        var books = new List<Book>
        {
            new()
            {
                Title = "Sample",
                Slug = "sample",
                Chapters = new List<Chapter>()
            }
        };
        var service = new Mock<IDocumentationService>();
        service.Setup(s => s.GetBooks()).Returns(books);
        var controller = new BooksController(service.Object);

        var result = controller.Index();

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Same(books, viewResult.Model);
    }

    [Fact]
    public void Chapter_InvalidBook_ReturnsNotFound()
    {
        var service = new Mock<IDocumentationService>();
        service.Setup(s => s.GetBookBySlug(It.IsAny<string>())).Returns((Book?)null);
        var controller = new BooksController(service.Object);

        var result = controller.Chapter("unknown", "chapter");

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Chapter_ValidRequest_ReturnsViewWithViewModel()
    {
        var book = new Book
        {
            Title = "Demo",
            Slug = "demo",
            Chapters = new List<Chapter>
            {
                new()
                {
                    Title = "Intro",
                    Slug = "intro",
                    Order = 1,
                    Sections = new List<ChapterSection>()
                }
            }
        };

        var service = new Mock<IDocumentationService>();
        service.Setup(s => s.GetBookBySlug("demo")).Returns(book);
        service.Setup(s => s.GetChapterWithSiblings("demo", "intro"))
            .Returns((book.Chapters[0], null, null));

        var controller = new BooksController(service.Object);

        var result = controller.Chapter("demo", "intro");

        var viewResult = Assert.IsType<ViewResult>(result);
        var viewModel = Assert.IsType<BookChapterViewModel>(viewResult.Model);
        Assert.Equal(book, viewModel.Book);
        Assert.Equal("intro", viewModel.CurrentChapter?.Slug);
    }
}
