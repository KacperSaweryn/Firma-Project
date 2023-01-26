using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.Validators
{
    public class ObjectValidator : Validator
    {
        public static string IsNotNull(Object obj) => obj == null ? "Wartość jest wymagana" : string.Empty;
    }
}