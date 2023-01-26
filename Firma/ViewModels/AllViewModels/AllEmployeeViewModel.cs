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
    public class AllEmployeeViewModel : AllViewModel<EmployeeForAllView>
    {
        #region Konstruktor

        public AllEmployeeViewModel() : base("Pracownicy")
        {
        }

        #endregion

        #region Helpers

        protected override void Sort()
        {
            switch (SortField)
            {
                case "Nazwisko":
                    List = new ObservableCollection<EmployeeForAllView>(SortDescending
                        ? List.OrderByDescending(item => item.Nazwisko)
                        : List.OrderBy(item => item.Nazwisko));
                    break;
                case "Imie":
                    List = new ObservableCollection<EmployeeForAllView>(SortDescending
                        ? List.OrderByDescending(item => item.Imie)
                        : List.OrderBy(item => item.Imie));
                    break;
                case "Stanowisko":
                    List = new ObservableCollection<EmployeeForAllView>(SortDescending
                        ? List.OrderByDescending(item => item.Stanowisko)
                        : List.OrderBy(item => item.Stanowisko));
                    break;
            }
        }

        protected override void Delete()
        {
            if (SelectedItem != null)
            {
                var pracownik =
                    FakturyEntities.Pracownik.FirstOrDefault(item => item.PracownikID == SelectedItem.PracownikID);
                if (pracownik != null)
                {
                    var messageBoxResult =
                        MessageBox.Show("Czy na pewno chcesz usunąć?", "Usuń", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        pracownik.IsActive = false;
                        FakturyEntities.SaveChanges();
                        AllList.Remove(SelectedItem);
                        List.Remove(SelectedItem);
                    }
                }
            }
        }

        protected override List<string> GetSortComboBoxItems()
        {
            return new List<string>() { "Nazwisko", "Imie", "Stanowisko" };
        }

        protected override void Search()
        {
            if (!string.IsNullOrEmpty(SearchText))
                switch (SearchField)
                {
                    case "Nazwisko":
                        List = new ObservableCollection<EmployeeForAllView>(AllList.Where(item =>
                            item.Nazwisko.ToLower().Trim().Contains(SearchText)));
                        break;
                    case "Imie":
                        List = new ObservableCollection<EmployeeForAllView>(AllList.Where(item =>
                            item.Imie.ToLower().Trim().Contains(SearchText)));
                        break;
                    case "Stanowisko":
                        List = new ObservableCollection<EmployeeForAllView>(AllList.Where(item =>
                            item.Stanowisko.ToLower().Trim().Contains(SearchText)));
                        break;
                }
            else
                List = new ObservableCollection<EmployeeForAllView>(AllList);

            Sort();
        }

        protected override List<string> GetSearchComboBoxItems()
        {
            return new List<string>() { "Nazwisko", "Imie", "Stanowisko" };
        }

        protected override int GetSelectedItemId()
        {
            return SelectedItem?.PracownikID ?? -1;
        }

        public override void Load()
        {
            AllList = (
                from pracownik in firmaEntities.Pracownik
                where pracownik.IsActive == true
                select new EmployeeForAllView()
                {
                    PracownikID = pracownik.PracownikID,
                    Stanowisko = pracownik.Stanowisko,
                    Nazwisko = pracownik.Osoba.Nazwisko,
                    Imie = pracownik.Osoba.Imie,
                    Adres = pracownik.Osoba.Adres.Poczta + "," +
                            pracownik.Osoba.Adres.Miasto + " " +
                            pracownik.Osoba.Adres.Ulica + " " +
                            pracownik.Osoba.Adres.NrDomu + "/" +
                            pracownik.Osoba.Adres.NrLokalu
                }
            ).Take(20).ToList();

            List = new ObservableCollection<EmployeeForAllView>(AllList);
        }

        #endregion
    }
}