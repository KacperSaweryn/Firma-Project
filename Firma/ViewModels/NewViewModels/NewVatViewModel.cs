using System;
using System.ComponentModel;
using System.Linq;
using Firma.Models.Entities;
using Firma.Models.Validators;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels.NewViewModels
{
    public class NewVatViewModel : OneViewModel<Vat>, IDataErrorInfo
    {
        #region Konstruktor

        public NewVatViewModel() : base("VAT")
        {
            Item = new Vat()
            {
            };
        }

        public NewVatViewModel(int id) : base("VAT")
        {
            Item = Db.Vat.First(item => item.VatID == id);
        }

        #endregion

        #region Properties

        public int StawkaVat
        {
            get { return Item.StawkaVat; }
            set
            {
                if (value != Item.StawkaVat)
                {
                    Item.StawkaVat = value;
                    base.OnPropertyChanged(() => StawkaVat);
                }
            }
        }

        #endregion

        #region Metody

        protected override bool IsValid()
        {
            return this[nameof(StawkaVat)] == string.Empty;
        }

        public override void Save()
        {
            try
            {
                Db.Vat.AddObject(Item);
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
                    case nameof(StawkaVat):
                        return DecimalValidator.IsNotMinus(StawkaVat);
                    default:
                        return string.Empty;
                }
            }
        }

        public string Error => string.Empty;

        #endregion
    }
}