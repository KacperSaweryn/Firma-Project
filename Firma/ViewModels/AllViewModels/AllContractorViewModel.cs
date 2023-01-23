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
    public class AllContractorViewModel : AllViewModel<ContractorsForAllView>
    {
        private Kontrahent _wybranyKontrahent;
        
        public Kontrahent WybranyKontrahent
        {
            get { return _wybranyKontrahent; }
            set
            {
                if (_wybranyKontrahent != value)
                {
                    _wybranyKontrahent = value;
                    OnPropertyChanged((() => WybranyKontrahent));
                    if (_wybranyKontrahent != null)
                    {
                        MessageBox.Show($"Wybrano klienta {_wybranyKontrahent.Nazwa} - {_wybranyKontrahent.NIP} " +
                                        $"({_wybranyKontrahent.Kod})", "Info");
                        Messenger.Default.Send(_wybranyKontrahent);
                        OnRequestClose();
                    }
                }
            }
        }


        #region Konstruktor

        public AllContractorViewModel() : base("Kontrahenci")
        {
        }

        #endregion

        #region Helpers

        public override void Load()
        {
            AllList = (
                from kontrahent in firmaEntities.Kontrahent
                where kontrahent.IsActive == true
                select new ContractorsForAllView()
                {
                    KontrahentID = kontrahent.KontrahentID,
                    Kod = kontrahent.Kod,
                    Nazwa = kontrahent.Nazwa,
                    NIP = kontrahent.NIP,
                    NrKonta = kontrahent.NrKonta,
                    Adres = kontrahent.Adres.Poczta + " " +
                            kontrahent.Adres.Miasto + ", ul " +
                            kontrahent.Adres.Ulica + " " +
                            kontrahent.Adres.NrDomu,
                    AdresLokal = "/" + kontrahent.Adres.NrLokalu,
                    Status = kontrahent.Status.Nazwa,
                   // Rodzaj = kontrahent.KontrahentRodzaj.Nazwa,
                    isPodatnikVat = kontrahent.IsPodatnikVat,
                    Przedstawiciel = kontrahent.Osoba.Nazwisko
                }
            ).Take(20).ToList();

            List = new ObservableCollection<ContractorsForAllView>(AllList);
        }

        protected override int GetSelectedItemId()
        {
            return SelectedItem?.KontrahentID ?? -1;
        }
      

        protected override void Sort()
        {
            switch (SortField)
            {
                case "Nazwa":
                    List = new ObservableCollection<ContractorsForAllView>(SortDescending
                        ? List.OrderByDescending(item => item.Nazwa)
                        : List.OrderBy(item => item.Nazwa));
                    break;
                case "NIP":
                    List = new ObservableCollection<ContractorsForAllView>(SortDescending
                        ? List.OrderByDescending(item => item.NIP)
                        : List.OrderBy(item => item.NIP));
                    break;
                case "Kod":
                    List = new ObservableCollection<ContractorsForAllView>(SortDescending
                        ? List.OrderByDescending(item => item.Kod)
                        : List.OrderBy(item => item.Kod));
                    break;
            }
        }


        protected override void Search()
        {
            if (!string.IsNullOrEmpty(SearchText))
                switch (SearchField)
                {
                    case "Nazwa":
                        List = new ObservableCollection<ContractorsForAllView>(AllList.Where(item =>
                            item.Nazwa.ToLower().Trim().Contains(SearchText)));
                        break;
                    case "NIP":
                        List = new ObservableCollection<ContractorsForAllView>(AllList.Where(item =>
                            item.NIP.ToLower().Trim().Contains(SearchText)));
                        break;
                    case "Kod":
                        List = new ObservableCollection<ContractorsForAllView>(AllList.Where(item =>
                            item.Kod.ToLower().Trim().Contains(SearchText)));
                        break;
                }
            else
                List = new ObservableCollection<ContractorsForAllView>(AllList);

            Sort();
        }

        protected override List<string> GetSearchComboBoxItems()
        {
            return new List<string>() { "Nazwa", "NIP", "Kod" };
        }

        protected override List<string> GetSortComboBoxItems()
        {
            return new List<string>() { "Nazwa","NIP","Kod" };
        }

        protected override void Delete()
        {
            if (SelectedItem != null)
            {
                var kontrahent =
                    FakturyEntities.Kontrahent.FirstOrDefault(item => item.KontrahentID == SelectedItem.KontrahentID);
                if (kontrahent != null)
                {
                    var messageBoxResult =
                        MessageBox.Show("Czy na pewno chcesz usunąć?", "Usuń", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        kontrahent.IsActive = false;
                        FakturyEntities.SaveChanges();
                        AllList.Remove(SelectedItem);
                        List.Remove(SelectedItem);
                    }
                }
            }
        }

       
        #endregion
    }
}