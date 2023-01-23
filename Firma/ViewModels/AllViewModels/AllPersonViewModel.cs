using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels.AllViewModels
{
    public class AllPersonViewModel : AllViewModel<PersonForAllView>
    {
        #region Konstruktor

        public AllPersonViewModel() : base("Osoby")
        {
        }

        #endregion

        #region Helpers

        protected override void Delete()
        {
            if (SelectedItem != null)
            {
                var osoba =
                    FakturyEntities.Osoba.FirstOrDefault(item => item.OsobaID == SelectedItem.OsobaID);
                if (osoba != null)
                {
                    var messageBoxResult =
                        MessageBox.Show("Czy na pewno chcesz usunąć?", "Usuń", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        osoba.IsActive = false;
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
                case "Imie":
                    List = new ObservableCollection<PersonForAllView>(SortDescending
                        ? List.OrderByDescending(item => item.Imie)
                        : List.OrderBy(item => item.Imie));
                    break;
                case "Nazwisko":
                    List = new ObservableCollection<PersonForAllView>(SortDescending
                        ? List.OrderByDescending(item => item.Nazwisko)
                        : List.OrderBy(item => item.Nazwisko));
                    break;
                case "Telefon":
                    List = new ObservableCollection<PersonForAllView>(SortDescending
                        ? List.OrderByDescending(item => item.Telefon)
                        : List.OrderBy(item => item.Telefon));
                    break;
                case "Email":
                    List = new ObservableCollection<PersonForAllView>(SortDescending
                        ? List.OrderByDescending(item => item.Email)
                        : List.OrderBy(item => item.Email));
                    break;

            }
        }

        protected override List<string> GetSortComboBoxItems()
        {
            return new List<string>() { "Imie", "Nazwisko", "Telefon", "Email" };
        }

        protected override void Search()
        {

            if (!string.IsNullOrEmpty(SearchText))
                switch (SearchField)
                {
                    case "Imie":
                        List = new ObservableCollection<PersonForAllView>(AllList.Where(item =>
                            item.Imie.ToLower().Trim().Contains(SearchText)));
                        break;
                    case "Nazwisko":
                        List = new ObservableCollection<PersonForAllView>(AllList.Where(item =>
                            item.Nazwisko.ToLower().Trim().Contains(SearchText)));
                        break;
                    case "Telefon":
                        List = new ObservableCollection<PersonForAllView>(AllList.Where(item =>
                            item.Telefon.ToLower().Trim().Contains(SearchText)));
                        break;
                    case "Email":
                        List = new ObservableCollection<PersonForAllView>(AllList.Where(item =>
                            item.Email.ToLower().Trim().Contains(SearchText)));
                        break;


                }
            else
                List = new ObservableCollection<PersonForAllView>(AllList);

            Sort();
        }

        protected override List<string> GetSearchComboBoxItems()
        {
            return new List<string>() { "Imie", "Nazwisko", "Telefon", "Email" };
        }

        public override void Load()
        {
            AllList = (
                from osoba in firmaEntities.Osoba 
                where osoba.IsActive == true
                select new PersonForAllView() 
                {
                    OsobaID = osoba.OsobaID,
                    Imie = osoba.Imie,
                    Nazwisko = osoba.Nazwisko,
                    Telefon = osoba.Telefon,
                    Email = osoba.Email,
                    Adres = osoba.Adres.Poczta + " " +
                            osoba.Adres.Miasto + ", ul " +
                            osoba.Adres.Ulica + " " +
                            osoba.Adres.NrDomu + "/" +
                            osoba.Adres.NrLokalu
                }
            ).Take(20).ToList();

            List = new ObservableCollection<PersonForAllView>(AllList);
        }

        protected override int GetSelectedItemId()
        {
            return SelectedItem?.OsobaID ?? -1;
        }

        #endregion
    }
}