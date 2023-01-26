using System;
using System.ComponentModel;
using System.Linq;
using Firma.Models.Entities;
using Firma.Models.Validators;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels.NewViewModels
{
    public class NewPaymentMethodViewModel : OneViewModel<SposobPlatnosci>, IDataErrorInfo
    {
        #region Konstruktor

        public NewPaymentMethodViewModel() : base("Płatność")
        {
            Item = new SposobPlatnosci()
            {
                IsActive = true
            };
        }

        public NewPaymentMethodViewModel(int id) : base("Płatność")
        {
            Item = Db.SposobPlatnosci.First(item => item.SposobPlatnosciID == id);
        }

        #endregion

        #region Properties

        public string Nazwa
        {
            get { return Item.Nazwa; }
            set
            {
                if (value != Item.Nazwa)
                {
                    Item.Nazwa = value;
                    base.OnPropertyChanged(() => Nazwa);
                }
            }
        }

        public string Opis
        {
            get { return Item.Opis; }
            set
            {
                if (value != Item.Opis)
                {
                    Item.Opis = value;
                    base.OnPropertyChanged(() => Opis);
                }
            }
        }

        #endregion

        #region Metody

        protected override bool IsValid()
        {
            return this[nameof(Nazwa)] == string.Empty;
        }

        public override void Save()
        {
            try
            {
                Item.IsActive = true;
                Db.SposobPlatnosci.AddObject(Item);
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
                    case nameof(Nazwa):
                        return StringValidator.IsNotNull(Nazwa);
                    case nameof(Opis):
                        return StringValidator.IsNotNull(Opis);
                    default:
                        return string.Empty;
                }
            }
        }

        public string Error => string.Empty;

        #endregion
    }
}