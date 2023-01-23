using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.EntitiesForView
{
    public class InvoicePositionForAllView

    {
        #region Properties

        public int PozycjaFakturyID { get; set; }
        public string FakturaNumer { get; set; }
        public string TowarKod { get; set; }
        public string TowarNazwa { get; set; }

        public decimal Ilosc { get; set; }
        public int StawkaVat { get; set; }
        public decimal CenaNetto { get; set; }
        public decimal Rabat { get; set; }

        public InvoicePositionForAllView(string towarKod, string towarNazwa, decimal cenaNetto, decimal ilosc, decimal rabat, int stawkaVat)
        {
            TowarKod = towarKod;
            TowarNazwa = towarNazwa;
            CenaNetto = cenaNetto;
            Ilosc = ilosc;
            Rabat = rabat;
            StawkaVat = stawkaVat;
            
        }

        #endregion
    }
}