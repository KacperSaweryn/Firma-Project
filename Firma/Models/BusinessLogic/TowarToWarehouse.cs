using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firma.Models.Entities;

namespace Firma.Models.BusinessLogic
{
    public class TowarToWarehouse : DatabaseClass
    {
        #region PolaIWlasciwosci

        public int TowarId { get; set; }

        public decimal Ilosc { get; set; }
        

        #endregion


        #region Konstruktory

        public TowarToWarehouse(FirmaEntities db) : base(db)
        {
            Ilosc = 0;
            
        }

        #endregion

    }
}