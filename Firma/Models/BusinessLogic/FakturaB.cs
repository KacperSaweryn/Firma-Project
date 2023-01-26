using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firma.Models.Entities;

namespace Firma.Models.BusinessLogic
{
    public class FakturaB : DatabaseClass
    {
        #region PolaIWlasciwosci

        public int FakturaID { get; set; }

        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }

        #endregion


        #region Konstruktory

        public FakturaB(FirmaEntities db) : base(db)
        {
            DataOd = DateTime.Now.AddDays(-1);
            DataDo = DateTime.Now;
        }

        #endregion


        #region Metody

        public int ObliczIlosc()

        {
            return Db.Faktura.Where(item => item.DataWystawienia >= DataOd && item.DataWystawienia <= DataDo).ToList()
                .Count;
        }

        #endregion
    }
}