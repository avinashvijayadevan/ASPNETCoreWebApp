using ASPNETCoreWebApp.Controllers;
using ASPNETCoreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ASPNETCoreWebApp.Tests;

public class CalculatorControllerTests
{
    private readonly CalculatorController _controller = new();

    [Fact]
    public void Index_Get_ReturnsViewWithEmptyModel()
    {
        var result = _controller.Index();

        var view = Assert.IsType<ViewResult>(result);
        Assert.IsType<CalculatorViewModel>(view.Model);
    }

    [Theory]
    [InlineData(10, 5, "+", 15)]
    [InlineData(10, 5, "-", 5)]
    [InlineData(10, 5, "*", 50)]
    [InlineData(10, 5, "/", 2)]
    [InlineData(10, 3, "%", 1)]
    public void Index_Post_ReturnsCorrectResult(double a, double b, string op, double expected)
    {
        var model = new CalculatorViewModel { Operand1 = a, Operand2 = b, Operator = op };

        var result = _controller.Index(model);

        var view = Assert.IsType<ViewResult>(result);
        var vm = Assert.IsType<CalculatorViewModel>(view.Model);
        Assert.Equal(expected, vm.Result);
        Assert.Null(vm.ErrorMessage);
    }

    [Theory]
    [InlineData("/")]
    [InlineData("%")]
    public void Index_Post_DivideByZero_SetsErrorMessage(string op)
    {
        var model = new CalculatorViewModel { Operand1 = 10, Operand2 = 0, Operator = op };

        var result = _controller.Index(model);

        var view = Assert.IsType<ViewResult>(result);
        var vm = Assert.IsType<CalculatorViewModel>(view.Model);
        Assert.NotNull(vm.ErrorMessage);
        Assert.False(vm.HasResult);
    }
}
