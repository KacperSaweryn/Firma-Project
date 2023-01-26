using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.Models.Validators;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels.NewViewModels
{
    public class NewCommodityViewModel : OneAllViewModel<Towar, Vat>, IDataErrorInfo
    {
        #region Konstruktor

        public NewCommodityViewModel() : base("Towar", "VAT")
        {
            Item = new Towar()
            {
                IsActive = true,
                Ilosc = 0
            };

            SetLists();
            SetDefaultValues();
        }


        public NewCommodityViewModel(int id) : base("Towar", "VAT")
        {
            Item = Db.Towar.First(item => item.TowarID == id);

            SetLists();
            SetDefaultValues();
        }

        #endregion

        #region Properties

        public List<ComboBoxKeyAndValue> Jednostki { get; set; }
        public List<ComboBoxKeyAndValue> Rodzaje { get; set; }
        public List<ComboBoxKeyAndValueNumbers> VatList { get; set; }

        public string Kod
        {
            get => Item.Kod;
            set
            {
                if (value != Item.Kod)
                {
                    Item.Kod = value;
                    OnPropertyChanged(() => Kod);
                }
            }
        }

        public string Nazwa
        {
            get => Item.Nazwa;
            set
            {
                if (value != Item.Nazwa)
                {
                    Item.Nazwa = value;
                    OnPropertyChanged(() => Nazwa);
                }
            }
        }

        public int StawkaVatSprzedazy
        {
            get => Item.StawkaVatSprzedazy;
            set
            {
                if (value != Item.StawkaVatSprzedazy)
                {
                    Item.StawkaVatSprzedazy = value;

                    OnPropertyChanged(() => StawkaVatSprzedazy);
                }
            }
        }

        public int RodzajID
        {
            get => Item.RodzajID;
            set
            {
                if (value != Item.RodzajID)
                {
                    Item.RodzajID = value;
                    OnPropertyChanged(() => RodzajID);
                }
            }
        }

        public int StawkaVatZakupu
        {
            get => Item.StawkaVatZakupu;
            set
            {
                if (value != Item.StawkaVatZakupu)
                {
                    Item.StawkaVatZakupu = value;
                    OnPropertyChanged(() => StawkaVatZakupu);
                }
            }
        }

        public decimal Cena
        {
            get => Item.Cena;
            set
            {
                if (value != Item.Cena)
                {
                    Item.Cena = value;
                    OnPropertyChanged(() => Cena);
                }
            }
        }

        public decimal Marza
        {
            get => Item.Marza;
            set
            {
                if (value != Item.Marza)
                {
                    Item.Marza = value;
                    OnPropertyChanged(() => Marza);
                }
            }
        }

        public int Jednostka
        {
            get => Item.JednostkaID;
            set
            {
                if (value != Item.JednostkaID)
                {
                    Item.JednostkaID = value;
                    OnPropertyChanged(() => Jednostka);
                }
            }
        }

        public decimal? Ilosc
        {
            get => Item.Ilosc;
            set
            {
                if (value != Item.Ilosc)
                {
                    Item.Ilosc = value;
                    OnPropertyChanged(() => Ilosc);
                }
            }
        }

        #endregion


        #region Metody

        protected override void SetLists()
        {
            Rodzaje = Db.TowarRodzaj.Where(item => item.IsActive == true)
                .Select(item => new ComboBoxKeyAndValue()
                    { Key = item.TowarRodzajID, Value = item.Nazwa })
                .ToList();

            VatList = Db.Vat
                .Select(item => new ComboBoxKeyAndValueNumbers()
                    { Key = item.VatID, Value = item.StawkaVat })
                .ToList();

            Jednostki = Db.Jednostka.Where(item => item.IsActive == true)
                .Select(item => new ComboBoxKeyAndValue()
                    { Key = item.JednostkaID, Value = item.JednostkaNazwa })
                .ToList();
        }

        protected override void SetDefaultValues()
        {
            if (Jednostki.Count > 0) Jednostka = Jednostki.First().Key;

            if (VatList.Count > 0)
            {
                StawkaVatSprzedazy = VatList.First().Key;
                StawkaVatZakupu = VatList.First().Key;
            }

            if (Rodzaje.Count > 0) RodzajID = VatList.First().Key;
        }

        protected override void ShowAddView()
        {
        }

        protected override bool IsValid()
        {
            return this[nameof(Kod)] == string.Empty && this[nameof(Nazwa)] == string.Empty &&
                   this[nameof(StawkaVatZakupu)] == string.Empty && this[nameof(Cena)] == string.Empty &&
                   this[nameof(Marza)] == string.Empty;
        }

        public override void Save()
        {
            try
            {
                Item.IsActive = true;
                Db.Towar.AddObject(Item);
                Db.SaveChanges();
            }
            catch (Exception e)
            {
                Db.SaveChanges();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(StawkaVatSprzedazy):
                        return DecimalValidator.IsCorrectPercent(StawkaVatSprzedazy);
                    case nameof(StawkaVatZakupu):
                        return DecimalValidator.IsCorrectPercent(StawkaVatZakupu);
                    case nameof(Cena):
                        return DecimalValidator.IsNotMinus(Cena);
                    case nameof(Marza):
                        return DecimalValidator.IsNotMinus(Marza);
                    case nameof(Kod):
                        return StringValidator.IsNotNull(Kod);
                    case nameof(Nazwa):
                        return StringValidator.IsNotNull(Nazwa);

                    default:
                        return string.Empty;
                }
            }
        }

        public string Error => string.Empty;

        #endregion
    }
}