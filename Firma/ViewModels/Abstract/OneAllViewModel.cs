using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Firma.Helpers;

namespace Firma.ViewModels.Abstract
{
    /// <summary>
    /// ViewModel realizujacy połączenie 1 do wielu
    /// </summary>
    /// <typeparam name="J">Typ jeden np. Faktura</typeparam>
    /// <typeparam name="W">Typ wiele np. PozycjeFaktury</typeparam>
    public abstract class OneAllViewModel<J, W> : OneViewModel<J>
    {
        #region Pola i Wlasciwosci

        public string DisplayNameList { get; set; }

        private ObservableCollection<W> _List;

        public ObservableCollection<W> List
        {
            get { return _List; }
            set
            {
                if (value != _List)
                {
                    _List = value;
                    OnPropertyChanged((() => List));
                }
            }
        }

        private ICommand _ShowAddViewCommand;

        public ICommand ShowAddViewCommand
        {
            get
            {
                if (_ShowAddViewCommand == null)
                {
                    _ShowAddViewCommand = new BaseCommand(() => ShowAddView());
                }

                return _ShowAddViewCommand;
            }
        }

        #endregion

        #region Konstruktor

        public OneAllViewModel(string displayName, string displayNameList) : base(displayName)
        {
            DisplayNameList = displayNameList;
           
        }

        #endregion


        #region Metody

        

        protected abstract void ShowAddView();
        protected abstract void SetLists();
        protected abstract void SetDefaultValues();

      

        #endregion
    }
}