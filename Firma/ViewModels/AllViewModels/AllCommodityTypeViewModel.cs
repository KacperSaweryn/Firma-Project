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
    public class AllCommodityTypeViewModel : AllViewModel<CommodityTypeForAllView>
    {
        #region Konstruktor

        public AllCommodityTypeViewModel() : base("Rodzaje Towaru")
        {
        }

        #endregion

        #region Metody

        protected override void Delete()
        {
            if (SelectedItem != null)
            {
                var tRodzaj =
                    FakturyEntities.TowarRodzaj.FirstOrDefault(item => item.TowarRodzajID == SelectedItem.TowarRodzajID);
                if (tRodzaj != null)
                {
                    var messageBoxResult =
                        MessageBox.Show("Czy na pewno chcesz usunąć?", "Usuń", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        tRodzaj.IsActive = false;
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
                    List = new ObservableCollection<CommodityTypeForAllView>(SortDescending
                        ? List.OrderByDescending(item => item.Nazwa)
                        : List.OrderBy(item => item.Nazwa));
                    break;
            }
        }

        protected override List<string> GetSortComboBoxItems()
        {
            return new List<string>() { "Nazwa" };
        }

        protected override void Search()
        {
            if (!string.IsNullOrEmpty(SearchText))
                switch (SearchField)
                {
                    case "Nazwa":
                        List = new ObservableCollection<CommodityTypeForAllView>(AllList.Where(item =>
                            item.Nazwa.ToLower().Trim().Contains(SearchText)));
                        break;
                }
            else
                List = new ObservableCollection<CommodityTypeForAllView>(AllList);

            Sort();
        }


        protected override List<string> GetSearchComboBoxItems()
        {
            return new List<string>() { "Nazwa" };
        }

        public override void Load()
        {
            AllList = (
                from tRodzaj in firmaEntities.TowarRodzaj 
                where tRodzaj.IsActive == true
                select new CommodityTypeForAllView() 
                {
                    TowarRodzajID = tRodzaj.TowarRodzajID,
                    Nazwa = tRodzaj.Nazwa
                }
            ).Take(20).ToList();

            List = new ObservableCollection<CommodityTypeForAllView>(AllList);
        }

        protected override int GetSelectedItemId()
        {
            return SelectedItem?.TowarRodzajID ?? -1;
        }

        #endregion
    }
}