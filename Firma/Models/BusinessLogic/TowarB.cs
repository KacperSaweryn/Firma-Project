using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firma.Models.Entities;

namespace Firma.Models.BusinessLogic
{
    public class TowarB : DatabaseClass
    {
        #region PolaIWlasciwosci

        public int TowarId { get; set; }

        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }

        #endregion


        #region Konstruktory

        public TowarB(FirmaEntities db) : base(db)
        {
            DataOd = DataOd = DateTime.Now.AddDays(-1);
            DataDo = DateTime.Now;
        }

        #endregion


        #region Metody

        public decimal ObliczUtarg()

        {
            try
            {
                return Math.Round((Db.PozycjaFaktury.Where(item => item.TowarID == TowarId &&
                                                       item.Faktura.DataWystawienia >= DataOd &&
                                                       item.Faktura.DataWystawienia <= DataDo).Sum(item =>
                    (item.Ilosc * item.CenaNetto) * (1 - item.Rabat))),2);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        #endregion
    }
}