using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.Validators
{
    public class DecimalValidator : Validator
    {
        public static string IsNotMinus(decimal value) => value < 0 ? "Ta wartość nie może być ujemna" : string.Empty;

        public static string IsCorrectPercent(decimal value) =>
            value >= 0 && value <= 99 ? string.Empty : "Procent musi być z zakresu 0-99";

        public static string IsCorrectPercent(int value) =>
            value >= 0 && value <= 99 ? string.Empty : "Procent musi być z zakresu 0-99";
    }
}