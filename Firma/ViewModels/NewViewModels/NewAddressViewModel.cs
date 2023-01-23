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
    public class NewAddressViewModel : OneAllViewModel<Adres, Wojewodztwo>, IDataErrorInfo
    {
        #region Konstruktor

        public NewAddressViewModel() : base("Nowy Adres", "Wojewodztwo")
        {
            Item = new Adres()
            {
                IsActive = true,
                Kraj = "Polska"
            };
            SetLists();
            SetDefaultValues();
        }

        public NewAddressViewModel(int id) : base("Nowy Adres", "Wojewodztwo")
        {
            Item = Db.Adres.First(item => item.AdresID == id);
            SetLists();
            SetDefaultValues();
        }

        #endregion


        #region Properties

        public List<ComboBoxKeyAndValue> Wojewodztwa { get; set; }

        public int Wojewodztwo
        {
            get { return Item.WojewodztwoID; }
            set
            {
                if (value != Item.WojewodztwoID)
                {
                    Item.WojewodztwoID = value;
                    base.OnPropertyChanged(() => Wojewodztwo);
                }
            }
        }

        public string Miasto
        {
            get { return Item.Miasto; }
            set
            {
                if (value != Item.Miasto)
                {
                    Item.Miasto = value;
                    base.OnPropertyChanged(() => Miasto);
                }
            }
        }

        public string Ulica
        {
            get { return Item.Ulica; }
            set
            {
                if (value != Item.Ulica)
                {
                    Item.Ulica = value;
                    base.OnPropertyChanged(() => Ulica);
                }
            }
        }

        public string NrDomu
        {
            get { return Item.NrDomu; }
            set
            {
                if (value != Item.NrDomu)
                {
                    Item.NrDomu = value;
                    base.OnPropertyChanged(() => NrDomu);
                }
            }
        }

        public string NrLokalu
        {
            get { return Item.NrLokalu; }
            set
            {
                if (value != Item.NrLokalu)
                {
                    Item.NrLokalu = value;
                    base.OnPropertyChanged(() => NrLokalu);
                }
            }
        }


        public string Poczta
        {
            get { return Item.Poczta; }
            set
            {
                if (value != Item.Poczta)
                {
                    Item.Poczta = value;
                    base.OnPropertyChanged(() => Poczta);
                }
            }
        }

        public string Kraj
        {
            get { return Item.Kraj; }
            set
            {
                if (value != Item.Kraj)
                {
                    Item.Kraj = value;
                    base.OnPropertyChanged(() => Kraj);
                }
            }
        }

        #endregion


        #region Metody

        protected override void ShowAddView()
        {
        }

        protected override void SetLists()
        {
            Wojewodztwa = Db.Wojewodztwo
                .Select(item => new ComboBoxKeyAndValue()
                    { Key = item.WojewodztwoID, Value = item.Nazwa })
                .ToList();
        }

        protected override void SetDefaultValues()
        {
            if (Wojewodztwa.Count > 0)
            {
                Wojewodztwo = Wojewodztwa.First().Key;
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Miasto):
                        return StringValidator.IsNotNull(Miasto);
                    case nameof(Ulica):
                        return StringValidator.IsNotNull(Ulica);
                    case nameof(NrLokalu):
                        return StringValidator.IsNotNull(NrLokalu);
                    case nameof(NrDomu):
                        return StringValidator.IsNotNull(NrDomu);
                    case nameof(Poczta):
                        return StringValidator.IsNotNull(Poczta);

                    default:
                        return string.Empty;
                }
            }
        }

        protected override bool IsValid()
        {
            return this[nameof(Miasto)] == string.Empty && this[nameof(Ulica)] == string.Empty &&
                   this[nameof(NrDomu)] == string.Empty &&
                   this[nameof(Poczta)] == string.Empty && this[nameof(Kraj)] == string.Empty;
        }

        public string Error => string.Empty;

        public override void Save()
        {
            try
            {
                Item.IsActive = true;
                Db.Adres.AddObject(Item);
                Db.SaveChanges();
            }
            catch (Exception e)
            {
                Db.SaveChanges();
            }
        }

        #endregion
    }
}