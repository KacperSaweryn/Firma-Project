using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Firma.Helpers;
using Firma.Models.BusinessLogic;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.Models.Validators;
using Firma.ViewModels.Abstract;
using Firma.ViewModels.AllViewModels;
using Firma.Views.AllViews;
using GalaSoft.MvvmLight.Messaging;

namespace Firma.ViewModels.NewViewModels
{
    public class AddCommodityViewModel : OneViewModel<Towar>, IDataErrorInfo
    {
        #region Fields

        private ICommand _DodajIloscCommand;

        public ICommand DodajIloscCommand
        {
            get
            {
                if (_DodajIloscCommand == null)
                {
                    _DodajIloscCommand = new BaseCommand(() => AddToWarehouse());
                }

                return _DodajIloscCommand;
            }
        }

        private decimal _Ilosc;

        public decimal Ilosc
        {
            get { return _Ilosc; }
            set
            {
                if (_Ilosc != value)
                {
                    _Ilosc = value;
                    OnPropertyChanged((() => Ilosc));
                }
            }
        }

        public TowarToWarehouse TowarToWarehouse { get; set; }


        public List<ComboBoxKeyAndValue> Towary { get; set; }
        public List<Towar> TowaryDb { get; set; }

        public int WybraneTowarId
        {
            get { return TowarToWarehouse.TowarId; }
            set
            {
                if (TowarToWarehouse.TowarId != value)
                {
                    TowarToWarehouse.TowarId = value;
                    OnPropertyChanged((() => WybraneTowarId));
                }
            }
        }

        #endregion

        #region Konstruktor

        public AddCommodityViewModel() : base("PZ")
        {
            FirmaEntities db = new FirmaEntities();
            TowarToWarehouse = new TowarToWarehouse(db);

            Towary = db.Towar.Where(item => item.IsActive == true)
                .Select(item => new ComboBoxKeyAndValue()
                {
                    Key = item.TowarID,
                    Value = item.Nazwa
                }).ToList();
        }

        #endregion


        #region Metody

        private void AddToWarehouse()
        {
            decimal ilosc = 0;
            Towar towar = new Towar();
            TowaryDb = Db.Towar.Where(item => item.IsActive).ToList();


            if (Ilosc <= 0 || Ilosc == null || Ilosc.ToString() == string.Empty || WybraneTowarId <= 0)
            {
                towar.Ilosc += ilosc;
                MessageBox.Show("Zapis niepoprawny, sprawdź wartości", "Bląd");
            }
            else
            {
                towar = TowaryDb.First(item => item.TowarID == WybraneTowarId);
                towar.Ilosc += Ilosc;
                MessageBox.Show("Zapis poprawny", "Sukces");
            }

            Save();
        }


        public override void Save()
        {
            Db.SaveChanges();
        }


        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Ilosc):
                        return DecimalValidator.IsNotMinus(Ilosc);
                    default:
                        return string.Empty;
                }
            }
        }

        public string Error => string.Empty;

        #endregion
    }
}