using System;
using System.ComponentModel;
using System.Linq;
using Firma.Models.Entities;
using Firma.Models.Validators;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels.NewViewModels
{
    public class NewCommodityTypeViewModel : OneViewModel<TowarRodzaj>, IDataErrorInfo
    {
        #region Konstruktor

        public NewCommodityTypeViewModel() : base("Rodzaj Towaru")
        {
            Item = new TowarRodzaj()
            {
                IsActive = true
            };
        }

        public NewCommodityTypeViewModel(int id) : base("Rodzaj Towaru")
        {
            Item = Db.TowarRodzaj.First(item => item.TowarRodzajID == id);
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
                Db.TowarRodzaj.AddObject(Item);
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
                    default:
                        return string.Empty;
                }
            }
        }

        public string Error => string.Empty;

        #endregion
    }
}