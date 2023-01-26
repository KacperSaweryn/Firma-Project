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
    public class AllVatViewModel : AllViewModel<VatForAllView>
    {
        #region Konstruktor

        public AllVatViewModel() : base("Stawki VAT")
        {
        }

        #endregion

        #region Metody

        protected override void Delete()
        {
            if (SelectedItem != null)
            {
                var status =
                    FakturyEntities.Vat.FirstOrDefault(item => item.VatID == SelectedItem.VatID);
                if (status != null)
                {
                    var messageBoxResult =
                        MessageBox.Show("Czy na pewno chcesz usunąć?", "Usuń", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
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
                case "Stawka":
                    List = new ObservableCollection<VatForAllView>(SortDescending
                        ? List.OrderByDescending(item => item.StawkaVat)
                        : List.OrderBy(item => item.StawkaVat));
                    break;
            }
        }

        protected override List<string> GetSortComboBoxItems()
        {
            return new List<string>() { "Stawka" };
        }

        protected override void Search()
        {
            if (!string.IsNullOrEmpty(SearchText))
                switch (SearchField)
                {
                    case "Stawka":
                        List = new ObservableCollection<VatForAllView>(AllList.Where(item =>
                            item.StawkaVat.ToString().Trim().Contains(SearchText)));
                        break;
                }
            else
                List = new ObservableCollection<VatForAllView>(AllList);

            Sort();
        }

        protected override List<string> GetSearchComboBoxItems()
        {
            return new List<string>() { "Stawka" };
        }

        public override void Load()
        {
            AllList = (
                from unit in firmaEntities.Vat
                select new VatForAllView()
                {
                    VatID = unit.VatID,
                    StawkaVat = unit.StawkaVat
                }
            ).Take(20).ToList();

            List = new ObservableCollection<VatForAllView>(AllList);
        }

        protected override int GetSelectedItemId()
        {
            return SelectedItem?.VatID ?? -1;
        }

        #endregion
    }
}