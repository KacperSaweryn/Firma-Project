using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;

namespace Firma.ViewModels.AllViewModels
{
    public class AllAddressViewModel : AllViewModel<AddressForAllView>
    {
        #region Properties

        private Adres _wybranyAddress;

        public Adres WybranyAddress
        {
            get { return _wybranyAddress; }
            set
            {
                if (_wybranyAddress != value)
                {
                    _wybranyAddress = value;
                    OnPropertyChanged((() => WybranyAddress));
                    if (_wybranyAddress != null)
                    {
                        MessageBox.Show($"Wybrano adres {_wybranyAddress.Poczta}, {_wybranyAddress.Miasto} " +
                                        $"({_wybranyAddress.Ulica} {_wybranyAddress.NrDomu} {_wybranyAddress.NrLokalu})",
                            "Info");
                        Messenger.Default.Send(_wybranyAddress);
                        OnRequestClose();
                    }
                }
            }
        }

        #endregion

        #region Konstruktor

        public AllAddressViewModel() : base("Adresy")
        {
        }

        #endregion

        #region Helpers

        protected override void Delete()
        {
            if (SelectedItem != null)
            {
                var adres =
                    FakturyEntities.Adres.FirstOrDefault(item => item.AdresID == SelectedItem.AdresID);
                if (adres != null)
                {
                    var messageBoxResult =
                        MessageBox.Show("Czy na pewno chcesz usunąć?", "Usuń", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        adres.IsActive = false;
                        FakturyEntities.SaveChanges();
                        AllList.Remove(SelectedItem);
                        List.Remove(SelectedItem);
                    }
                }
            }
        }

        protected override void Sort()
        {
            switch (SortField)
            {
                case "Miasto":
                    List = new ObservableCollection<AddressForAllView>(SortDescending
                        ? List.OrderByDescending(item => item.Miasto)
                        : List.OrderBy(item => item.Miasto));
                    break;
                case "Poczta":
                    List = new ObservableCollection<AddressForAllView>(SortDescending
                        ? List.OrderByDescending(item => item.Poczta)
                        : List.OrderBy(item => item.Poczta));
                    break;
                case "Ulica":
                    List = new ObservableCollection<AddressForAllView>(SortDescending
                        ? List.OrderByDescending(item => item.Ulica)
                        : List.OrderBy(item => item.Ulica));
                    break;
            }
        }

        protected override List<string> GetSortComboBoxItems()
        {
            return new List<string>() { "Miasto", "Poczta", "Ulica" };
        }

        protected override void Search()
        {
            if (!string.IsNullOrEmpty(SearchText))
                switch (SearchField)
                {
                    case "Miasto":
                        List = new ObservableCollection<AddressForAllView>(AllList.Where(item =>
                            item.Miasto.ToLower().Trim().Contains(SearchText)));
                        break;
                    case "Ulica":
                        List = new ObservableCollection<AddressForAllView>(AllList.Where(item =>
                            item.Ulica.ToLower().Trim().Contains(SearchText)));
                        break;
                    case "Poczta":
                        List = new ObservableCollection<AddressForAllView>(AllList.Where(item =>
                            item.Poczta.ToLower().Trim().Contains(SearchText)));
                        break;
                }
            else
                List = new ObservableCollection<AddressForAllView>(AllList);

            Sort();
        }

        protected override List<string> GetSearchComboBoxItems()
        {
            return new List<string>() { "Miasto", "Poczta", "Ulica" };
        }

        protected override int GetSelectedItemId()
        {
            return SelectedItem?.AdresID ?? -1;
        }

        public override void Load()
        {
           
            AllList = (
                from adres in firmaEntities.Adres
                where adres.IsActive == true
                select new AddressForAllView()
                {
                    Wojewodztwo = adres.Wojewodztwo.Nazwa,
                    AdresID = adres.AdresID,
                    Poczta = adres.Poczta,
                    Miasto = adres.Miasto,
                    Ulica = adres.Ulica,
                    NrDomu = adres.NrDomu,
                    NrLokalu = adres.NrLokalu
                }
            ).Take(20).ToList();

            List = new ObservableCollection<AddressForAllView>(AllList);
        }

        #endregion
    }
}