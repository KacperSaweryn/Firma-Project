using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.EntitiesForView
{
    public class CommodityForAllView

    {
        #region Properties

        public int TowarID { get; set; }
        public string Kod { get; set; }
        public string Nazwa { get; set; }
        public string RodzajTowaru { get; set; }
        public string JednostkaTowaru { get; set; }
        public int StawkaVatSprzedazy { get; set; }
        public int StawkaVatZakupu { get; set; }
        public decimal Cena { get; set; }
        public decimal Marza { get; set; }
        public decimal? Ilosc { get; set; }

        #endregion
    }
}