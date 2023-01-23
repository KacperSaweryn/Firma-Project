using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.EntitiesForView
{
    public class InvoiceForAllView

    {
        #region Properties

        public int FakturaID { get; set; }
        public string NumerFak { get; set; }

        public DateTime? DataWystawienia { get; set; }
        public DateTime? TerminPlatnosci { get; set; }

        public string KontrahentNazwa { get; set; }
        public string KontrahentNIP { get; set; }

        public string KontrahentAdres { get; set; }
        public string KontrahentAdresLokal { get; set; }


        public string SposobPlatnosciNazwa { get; set; }

        #endregion
    }
}