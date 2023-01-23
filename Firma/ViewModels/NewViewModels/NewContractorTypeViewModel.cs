using Firma.Models.Entities;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels.NewViewModels
{
    public class NewContractorTypeViewModel : OneViewModel<KontrahentRodzaj>
    {
        #region Konstruktor

        public NewContractorTypeViewModel() : base("Rodzaj Kontrahenta")
        {
            Item = new KontrahentRodzaj();
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

        public override void Save()
        {
            Item.IsActive = true;
            Db.KontrahentRodzaj.AddObject(Item);
            Db.SaveChanges();
        }
    }
}