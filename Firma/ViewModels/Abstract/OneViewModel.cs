using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Firma.Helpers;
using Firma.Models.Entities;

namespace Firma.ViewModels.Abstract
{
    //klasa z ktorej beda dziedziczyly wszystkie zakladki np dodajace recordy
    public abstract class OneViewModel<T> : WorkspaceViewModel
    {
        #region Fields

        //tu jest cala db
        public FirmaEntities Db { get; set; }

        //tu jest dodawany towar
        public T Item { get; set; }

        #endregion

        #region Konstruktor

        public OneViewModel(string displayName)
        {
            base.DisplayName = displayName; //tu ustawiamy nazwę zakładki
            Db = new FirmaEntities();
        }

        #endregion

        #region Command

        //to jest komenda ktora zostanie podpieta (zbindowana) z przyciskiem zapisz i zamknij. Komenda ta wywola funkcje SaveAndClose
        private BaseCommand _SaveAndCloseCommand;

        public ICommand SaveAndCloseCommand
        {
            get
            {
                if (_SaveAndCloseCommand == null)
                {
                    _SaveAndCloseCommand = new BaseCommand(() => saveAndClose());
                }

                return _SaveAndCloseCommand;
            }
        }

        #endregion

        #region Save
        public abstract void Save();
        private void saveAndClose()
        {
            if (IsValid())
            {
                //Messenger.Default.Send();
                //zapisuje towar
                Save();
                MessageBox.Show("Zapis poprawny", "Sukces");
                //zamyka zakladke
                base.OnRequestClose();
            }
            else
            {
                MessageBox.Show("Popraw błędy", "Błąd");
            }
        }

        protected virtual bool IsValid()
        {
            return true;
        }

        #endregion
    }
}