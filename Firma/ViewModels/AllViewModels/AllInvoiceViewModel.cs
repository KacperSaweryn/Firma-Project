using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels.AllViewModels
{
    public class AllInvoiceViewModel : AllViewModel<InvoiceForAllView>
    {
        #region Konstruktor

        public AllInvoiceViewModel() : base("Faktury")
        {
        }

        #endregion

        #region Helpers


        public override void Load()
        {
            AllList = (
                from faktura in firmaEntities.Faktura //dla kazdej faktury z db
                where faktura.IsActive == true
                select new InvoiceForAllView() //tworzymy nowa fakture for all view
                {
                    FakturaID = faktura.FakturaID,
                    NumerFak = faktura.NumerFak,
                    DataWystawienia = faktura.DataWystawienia,
                    KontrahentNazwa = faktura.Kontrahent.Nazwa,
                    KontrahentNIP = faktura.Kontrahent.NIP,
                    KontrahentAdres = faktura.Kontrahent.Adres.Poczta + " " +
                                      faktura.Kontrahent.Adres.Miasto + ", ul " +
                                      faktura.Kontrahent.Adres.Ulica + " " +
                                      faktura.Kontrahent.Adres.NrDomu,
                    KontrahentAdresLokal = "/" + faktura.Kontrahent.Adres.NrLokalu,
                    TerminPlatnosci = faktura.TerminPlatnosci,
                    SposobPlatnosciNazwa = faktura.SposobPlatnosci.Nazwa
                }
            ).Take(20).ToList();

            List = new ObservableCollection<InvoiceForAllView>(AllList);
        }


        protected override int GetSelectedItemId()
        {
            return SelectedItem?.FakturaID ?? -1;
        }


        protected override void Sort()
        {
            switch (SortField)
            {
                case "Numer":
                    List = new ObservableCollection<InvoiceForAllView>(SortDescending
                        ? List.OrderByDescending(item => item.NumerFak)
                        : List.OrderBy(item => item.NumerFak));
                    break;
            }
        }


        protected override void Search()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                switch (SearchField)
                {
                    case "Numer":
                        List = new ObservableCollection<InvoiceForAllView>(AllList.Where(item =>
                            item.NumerFak.ToLower().Trim().Contains(SearchText)));
                        break;
                    case "Kontrahent":
                        List = new ObservableCollection<InvoiceForAllView>(AllList.Where(item =>
                            item.KontrahentNazwa.ToLower().Trim().Contains(SearchText)));
                        break;
                    case "NIP":
                        List = new ObservableCollection<InvoiceForAllView>(AllList.Where(item =>
                            item.KontrahentNIP.ToLower().Trim().Contains(SearchText)));
                        break;
                }
            }
            else
            {
                List = new ObservableCollection<InvoiceForAllView>(AllList);
            }

            Sort();
        }

        protected override List<string> GetSearchComboBoxItems()
        {
            return new List<string>() { "Numer", "Kontrahent", "NIP" };
        }

        protected override List<string> GetSortComboBoxItems()
        {
            return new List<string>() { "Numer", "Kontrahent", "NIP" };
        }

        protected override void Delete()
        {
            if (SelectedItem != null)
            {
                Faktura faktura =
                    FakturyEntities.Faktura.FirstOrDefault(item => item.FakturaID == SelectedItem.FakturaID);
                if (faktura != null)
                {
                    MessageBoxResult messageBoxResult =
                        MessageBox.Show("Czy na pewno chcesz usunąć?", "Usuń", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        faktura.IsActive = false;
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