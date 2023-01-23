using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Firma.Helpers;
using Firma.Models.Entities;
using GalaSoft.MvvmLight.Messaging;

namespace Firma.ViewModels.Abstract
{
    public abstract class AllViewModel<T> : WorkspaceViewModel where T:class 
    {
        #region Fields

       
        public readonly FirmaEntities firmaEntities;

        private T _SelectedItem;
        public T SelectedItem {
            get

            {
                return _SelectedItem;
            }
             set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    AfterSelect();
                    OnPropertyChanged((() => SelectedItem));
                }
            }
            
            
             }

        protected virtual void AfterSelect()
        {
           
        }

        public FirmaEntities FakturyEntities
        {
            get
            {
                return firmaEntities;
            }
        }

        private ICommand _RefreshCommand;

        public ICommand RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null)
                {
                    _RefreshCommand = new BaseCommand(() => LoadItems());
                }

                return _RefreshCommand;
            }
        }

        private ICommand _DeleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new BaseCommand(() => Delete());
                }

                return _DeleteCommand;
            }
        }


        //komenda do zaladowania towarow
        private BaseCommand _LoadCommand;

        private ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand == null)
                {
                    _LoadCommand = new BaseCommand(() => LoadItems()); 
                }

                return _LoadCommand;
            }
        }

        private BaseCommand _EditCommand;

        public ICommand EditCommand
        {
            get
            {
                if (_EditCommand == null)
                {
                    _EditCommand = new BaseCommand(() => Edit()); 
                }

                return _EditCommand;
            }
        }


        private BaseCommand _AddCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new BaseCommand(() => Add()); 
                }

                return _AddCommand;
            }
        }

       
        private ObservableCollection<T> _List;

        public ObservableCollection<T> List
        {
            get
            {
                if (_List == null) 
                {
                    Load();
                }

                return _List;
            }
            set
            {
                _List = value;
                OnPropertyChanged(() => List);
            }
        }

        public List<T> AllList { get; set; }

        public List<string> SortComboBoxItems { get; set; }
        private string _SortField;

        public string SortField
        {
            get { return _SortField; }
            set
            {
                if (_SortField != value)
                {
                    _SortField = value;
                    OnPropertyChanged(() => SortField);
                }
            }
        }

        private bool _SortDescending;

        public bool SortDescending
        {
            get { return _SortDescending; }
            set
            {
                if (_SortDescending != value)
                {
                    _SortDescending = value;
                    OnPropertyChanged(() => SortDescending);
                }
            }
        }

        private ICommand _SortCommand;

        public ICommand SortCommand
        {
            get
            {
                if (_SortCommand == null)
                {
                    _SortCommand = new BaseCommand(() => Sort());
                }

                return _SortCommand;
            }
        }


        public List<string> SearchComboBoxItems { get; set; }
        private string _SearchField;

        public string SearchField
        {
            get { return _SearchField; }
            set
            {
                if (_SearchField != value)
                {
                    _SearchField = value;
                    OnPropertyChanged(() => SearchField);
                }
            }
        }

        private string _SearchText;

        public string SearchText
        {
            get { return _SearchText; }
            set
            {
                if (_SearchText != value)
                {
                    _SearchText = value?.ToLower().Trim();
                    OnPropertyChanged(() => SearchText);
                }
            }
        }

        private ICommand _SearchCommand;

        public ICommand SearchCommand
        {
            get
            {
                if (_SearchCommand == null)
                {
                    _SearchCommand = new BaseCommand(() => Search());
                }

                return _SearchCommand;
            }
        }


        #endregion

        #region Konstruktor

        public AllViewModel(string displayName)
        {
            base.DisplayName = displayName; //tu ustawiamy nazwę zakładki
            this.firmaEntities = new FirmaEntities();
            SortComboBoxItems = GetSortComboBoxItems();
            SearchComboBoxItems = GetSearchComboBoxItems();
        }

        #endregion


        #region Helpers

        protected abstract void Delete();
        protected abstract void Sort();
        protected abstract List<string> GetSortComboBoxItems();
        protected abstract void Search();
        protected abstract List<string> GetSearchComboBoxItems();

        /// <summary>
        /// Pobiera modele z DB, filtruje i sortuje. Używa <see cref="Load"/> oraz <see cref="Search"/>.
        /// </summary>
        public void LoadItems()
        {
            Load();
            Search();
        }

        /// <summary>
        /// Pobiera modele z DB.
        /// </summary>
        public abstract void Load();

        public void Add()
        {
            Messenger.Default.Send(DisplayName + " Add");
        }

        private void Edit()
        {
            MessengerMessage<AllViewModel<object>, Type, int> message =
                new MessengerMessage<AllViewModel<object>, Type, int>()
                {
                    Response = typeof(T),
                    Argument = GetSelectedItemId()
                };
            Messenger.Default.Send(message);
        }


        protected abstract int GetSelectedItemId();


        #endregion
    }
}

