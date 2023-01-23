using Firma.Models.Entities;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels.NewViewModels
{
    public class NewCredentialViewModel : OneViewModel<Credential>
    {
        #region Konstruktor

        public NewCredentialViewModel() : base("Nowe haslo")
        {
            Item = new Credential();
        }

        #endregion

        public string Login
        {
            get { return Item.Login; }
            set
            {
                if (value != Item.Login)
                {
                    Item.Login = value;
                    base.OnPropertyChanged(() => Login);
                }
            }
        }

        public string Password
        {
            get { return Item.Password; }
            set
            {
                if (value != Item.Password)
                {
                    Item.Password = value;
                    base.OnPropertyChanged(() => Password);
                }
            }
        }

        public override void Save()
        {
            Item.IsActive = true;
            Db.Credential.AddObject(Item);
            Db.SaveChanges();
        }
    }
}