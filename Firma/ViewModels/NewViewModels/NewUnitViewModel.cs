using System;
using System.ComponentModel;
using System.Linq;
using Firma.Models.Entities;
using Firma.Models.Validators;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels.NewViewModels
{
    public class NewUnitViewModel : OneViewModel<Jednostka>, IDataErrorInfo
    {
        #region Konstruktor

        public NewUnitViewModel() : base("Jednostka")
        {
            Item = new Jednostka()
            {
                IsActive = true
            };
        }

        public NewUnitViewModel(int id) : base("Jednostka")
        {
            Item = Db.Jednostka.First(item => item.JednostkaID == id);
        }

        #endregion

        #region Properties

        public string JednostkaNazwa
        {
            get { return Item.JednostkaNazwa; }
            set
            {
                if (value != Item.JednostkaNazwa)
                {
                    Item.JednostkaNazwa = value;
                    base.OnPropertyChanged(() => JednostkaNazwa);
                }
            }
        }

        #endregion

        #region Metody

        protected override bool IsValid()
        {
            return this[nameof(JednostkaNazwa)] == string.Empty;
        }

        public override void Save()
        {
            try
            {
                Item.IsActive = true;
                Db.Jednostka.AddObject(Item);
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
                    case nameof(JednostkaNazwa):
                        return StringValidator.IsNotNull(JednostkaNazwa);
                    default:
                        return string.Empty;
                }
            }
        }

        public string Error => string.Empty;

        #endregion
    }

    
}