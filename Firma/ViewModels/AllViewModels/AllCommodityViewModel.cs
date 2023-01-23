using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels.AllViewModels
{
    public class AllCommodityViewModel : AllViewModel<CommodityForAllView>
    {
        #region Konstruktor

        public AllCommodityViewModel() : base("Towary")
        {
        }

        #endregion

        #region Helpers

        protected override void Delete()
        {
            if (SelectedItem != null)
            {
                Towar towar =
                    FakturyEntities.Towar.FirstOrDefault(item => item.TowarID == SelectedItem.TowarID);
                if (towar != null)
                {
                    MessageBoxResult messageBoxResult =
                        MessageBox.Show("Czy na pewno chcesz usunąć?", "Usuń", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        towar.IsActive = false;
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
                case "Nazwa":
                    List = new ObservableCollection<CommodityForAllView>(SortDescending
                        ? List.OrderByDescending(item => item.Nazwa)
                        : List.OrderBy(item => item.Nazwa));
                    break;
            }
        }

        protected override List<string> GetSortComboBoxItems()
        {
            return new List<string>() { "Nazwa", "Rodzaj", "Kod" };
        }

        protected override void Search()
        {
            if (!string.IsNullOrEmpty(SearchText))
                switch (SearchField)
                {
                    case "Nazwa":
                        List = new ObservableCollection<CommodityForAllView>(AllList.Where(item =>
                            item.Nazwa.ToLower().Trim().Contains(SearchText)));
                        break;
                    case "Rodzaj":
                        List = new ObservableCollection<CommodityForAllView>(AllList.Where(item =>
                            item.RodzajTowaru.ToLower().Trim().Contains(SearchText)));
                        break;
                    case "Kod":
                        List = new ObservableCollection<CommodityForAllView>(AllList.Where(item =>
                            item.Kod.ToLower().Trim().Contains(SearchText)));
                        break;

                }
            else
                List = new ObservableCollection<CommodityForAllView>(AllList);

            Sort();
        }

        protected override List<string> GetSearchComboBoxItems()
        {
            return new List<string>() { "Nazwa", "Rodzaj", "Kod" };
        }

        public override void Load()
        {
            AllList = (
                from towar in firmaEntities.Towar
                where towar.IsActive == true
                select new CommodityForAllView()
                {
                    TowarID = towar.TowarID,
                    Kod = towar.Kod,
                    Nazwa = towar.Nazwa,
                    RodzajTowaru = towar.TowarRodzaj.Nazwa,
                    JednostkaTowaru = towar.Jednostka.JednostkaNazwa,
                    StawkaVatSprzedazy = towar.Vat.StawkaVat,
                    StawkaVatZakupu = towar.Vat.StawkaVat,
                    Cena = towar.Cena,
                    Marza = towar.Marza,
                    Ilosc = towar.Ilosc

                }
            ).Take(20).ToList();

        }

        protected override int GetSelectedItemId()
        {
            return SelectedItem?.TowarID ?? -1;
        }

        #endregion
    }
}