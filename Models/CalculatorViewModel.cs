using System.ComponentModel.DataAnnotations;

namespace ASPNETCoreWebApp.Models
{
    public class CalculatorViewModel
    {
        [Required]
        public double? Operand1 { get; set; }

        [Required]
        public double? Operand2 { get; set; }

        [Required]
        public string Operator { get; set; } = "+";

        public double? Result { get; set; }

        public string? ErrorMessage { get; set; }

        public bool HasResult => Result.HasValue;
    }
}
