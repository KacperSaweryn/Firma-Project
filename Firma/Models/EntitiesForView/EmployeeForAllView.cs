using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.EntitiesForView
{
    public class EmployeeForAllView

    {
        #region Properties

        public int PracownikID { get; set; }
        public string Stanowisko { get; set; }
        public string Osoba { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Adres { get; set; }

        #endregion
    }
}