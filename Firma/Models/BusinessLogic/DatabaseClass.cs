using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firma.Models.Entities;

namespace Firma.Models.BusinessLogic
{
    public class DatabaseClass
    {
        protected FirmaEntities Db { get; set; }

        public DatabaseClass(FirmaEntities db)
        {
            Db = db;
        }
    }
}