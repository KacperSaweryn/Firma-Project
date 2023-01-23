using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Firma.Helpers;
using Firma.Models.BusinessLogic;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;

namespace Firma.ViewModels.Reports
{
    public class SaleReportViewModel :WorkspaceViewModel
    {
        #region Fields

        private ICommand _obliczCommand;

        public ICommand ObliczCommand
        {
            get
            {
                if (_obliczCommand == null)
                {
                    _obliczCommand = new BaseCommand(() => ObliczUtarg());
                }

                return _obliczCommand;
            }
        }

        private decimal _utarg;

        public decimal Utarg
        {
            get { return _utarg; }
            set
            {
                if (_utarg != value)
                {
                    _utarg = value;
                    OnPropertyChanged((() => Utarg));
                }
            }
        }

        public TowarB TowarB { get; set; }

        public DateTime DataOd
        {
            get { return TowarB.DataOd; }
            set
            {
                if (TowarB.DataOd != value)
                {
                    TowarB.DataOd = value;
                    OnPropertyChanged((() => DataOd));
                }
            }
        }

        public DateTime DataDo
        {
            get { return TowarB.DataDo; }
            set
            {
                if (TowarB.DataDo != value)
                {
                    TowarB.DataDo = value;
                    OnPropertyChanged((() => DataDo));
                }
            }
        }

        public List<ComboBoxKeyAndValue> Towary { get; set; }

        public int WybraneTowarId
        {
            get { return TowarB.TowarId; }
            set
            {
                if (TowarB.TowarId != value)
                {
                    TowarB.TowarId = value;
                    OnPropertyChanged((() => WybraneTowarId));
                }
            }
        }

        #endregion

        #region Konstruktor

        public SaleReportViewModel()
        {
            base.DisplayName = "Raport sprzedaży";

            FirmaEntities db = new FirmaEntities();
            TowarB = new TowarB(db);

            Towary = db.Towar.Where(item => item.IsActive == true)
                .Select(item => new ComboBoxKeyAndValue()
                {
                    Key = item.TowarID,
                    Value = item.Nazwa
                }).ToList();
        }

        #endregion


        #region Metody

        private void ObliczUtarg()
        {
            Utarg = TowarB.ObliczUtarg();
        }

        #endregion
    }
}
