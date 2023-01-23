using System;
using System.ComponentModel;
using System.Linq;
using Firma.Models.Entities;
using Firma.Models.Validators;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels.NewViewModels
{
    public class NewStatusViewModel : OneViewModel<Status>, IDataErrorInfo
    {
        #region Konstruktor

        public NewStatusViewModel() : base("Status")
        {
            Item = new Status()
            {
                IsActive = true
            };
        }

        public NewStatusViewModel(int id) : base("Status")
        {
            Item = Db.Status.First(item => item.StatusID == id);
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
                Db.Status.AddObject(Item);
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