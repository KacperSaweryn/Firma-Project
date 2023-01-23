using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.EntitiesForView
{
    public class ContractorsForAllView

    {
        #region Properties

        public int KontrahentID { get; set; }
        public string Kod { get; set; }
        public string Nazwa { get; set; }
        public string NIP { get; set; }
        public string NrKonta { get; set; }
        public string Adres { get; set; }
        public string AdresLokal { get; set; }
        public string Status { get; set; }
        public string Rodzaj { get; set; }
        public bool isPodatnikVat { get; set; }
        public string Przedstawiciel { get; set; }
      

        #endregion
    }
}