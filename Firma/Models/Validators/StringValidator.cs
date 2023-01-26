using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.Validators
{
    public class StringValidator : Validator
    {
        public static string IsNotNull(string value) => value == null ? "Wartość jest wymagana" : string.Empty;
    }
}