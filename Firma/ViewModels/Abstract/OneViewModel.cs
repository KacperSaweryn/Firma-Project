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
    /// <summary>
    /// Klasa po której dziedziczą zakladki
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class OneViewModel<T> : WorkspaceViewModel
    {
        #region Fields

        /// <summary>
        /// Cała baza
        /// </summary>
        public FirmaEntities Db { get; set; }

       /// <summary>
       /// Dodawanie Itemu
       /// </summary>
        public T Item { get; set; }

        #endregion

        #region Konstruktor

        public OneViewModel(string displayName)
        {
            base.DisplayName = displayName; 
            Db = new FirmaEntities();
        }

        #endregion

        #region Command

       
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
        /// <summary>
        /// Zapisz i zamknij. Używa metody Save();
        /// </summary>
        private void saveAndClose()
        {
            if (IsValid())
            {
                Save();
                MessageBox.Show("Zapis poprawny", "Sukces");
                
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