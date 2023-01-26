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
    public class NumOfInvoicesInTImeViewModel : WorkspaceViewModel
    {
        #region Fields

        private ICommand _obliczCommand;

        public ICommand ObliczCommand
        {
            get
            {
                if (_obliczCommand == null)
                {
                    _obliczCommand = new BaseCommand(() => ObliczIlosc());
                }

                return _obliczCommand;
            }
        }

        private decimal _ilosc;

        public decimal Ilosc
        {
            get { return _ilosc; }
            set
            {
                if (_ilosc != value)
                {
                    _ilosc = value;
                    OnPropertyChanged((() => Ilosc));
                }
            }
        }

        public FakturaB FakturaB { get; set; }

        public DateTime DataOd
        {
            get { return FakturaB.DataOd; }
            set
            {
                if (FakturaB.DataOd != value)
                {
                    FakturaB.DataOd = value;
                    OnPropertyChanged((() => DataOd));
                }
            }
        }

        public DateTime DataDo
        {
            get { return FakturaB.DataDo; }
            set
            {
                if (FakturaB.DataDo != value)
                {
                    FakturaB.DataDo = value;
                    OnPropertyChanged((() => DataDo));
                }
            }
        }

        public List<ComboBoxKeyAndValue> Towary { get; set; }

        public int WybraneTowarId
        {
            get { return FakturaB.FakturaID; }
            set
            {
                if (FakturaB.FakturaID != value)
                {
                    FakturaB.FakturaID = value;
                    OnPropertyChanged((() => WybraneTowarId));
                }
            }
        }

        #endregion

        #region Konstruktor

        public NumOfInvoicesInTImeViewModel()
        {
            base.DisplayName = "Raport Faktur";

            FirmaEntities db = new FirmaEntities();
            FakturaB = new FakturaB(db);

            Towary = db.Towar.Where(item => item.IsActive == true)
                .Select(item => new ComboBoxKeyAndValue()
                {
                    Key = item.TowarID,
                    Value = item.Nazwa
                }).ToList();
        }

        #endregion


        #region Metody

        private void ObliczIlosc()
        {
            Ilosc = FakturaB.ObliczIlosc();
        }

        #endregion
    }
}