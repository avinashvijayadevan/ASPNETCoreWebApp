using ASPNETCoreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using MethodTimer;

namespace ASPNETCoreWebApp.Controllers
{
    [Time]
    public class CalculatorController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new CalculatorViewModel());
        }

        [HttpPost]
        public IActionResult Index(CalculatorViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Operand1 is null || model.Operand2 is null)
            {
                model.ErrorMessage = "Both operands are required.";
                return View(model);
            }

            double a = model.Operand1.Value;
            double b = model.Operand2.Value;

            model.Result = model.Operator switch
            {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" when b == 0 => null,
                "/" => a / b,
                "%" when b == 0 => null,
                "%" => a % b,
                _ => null
            };

            if (model.Result is null && (model.Operator == "/" || model.Operator == "%") && b == 0)
                model.ErrorMessage = "Cannot divide by zero.";

            return View(model);
        }
    }
}
