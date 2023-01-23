using Firma.Models.Entities;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels.NewViewModels
{
    public class NewCommodityTypeViewModel : OneViewModel<TowarRodzaj>
    {
        #region Konstruktor

        public NewCommodityTypeViewModel() : base("Rodzaj Towaru")
        {
            Item = new TowarRodzaj();
        }

        #endregion

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

        public override void Save()
        {
            Item.IsActive = true;
            //dodajemy towar do lokalnej kolekcji 
            Db.TowarRodzaj.AddObject(Item);
            //zapis do db
            Db.SaveChanges();
        }
    }
}