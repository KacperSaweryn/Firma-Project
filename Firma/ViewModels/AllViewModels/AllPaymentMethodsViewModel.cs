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
    public class AllPaymentMethodsViewModel : AllViewModel<PaymentForAllView>
    {
        #region Konstruktor

        public AllPaymentMethodsViewModel() : base("Płatności")
        {
        }

        #endregion

        #region Metody

        protected override void Delete()
        {
            if (SelectedItem != null)
            {
                var status =
                    FakturyEntities.SposobPlatnosci.FirstOrDefault(item => item.SposobPlatnosciID == SelectedItem.SposobPlatnosciID);
                if (status != null)
                {
                    var messageBoxResult =
                        MessageBox.Show("Czy na pewno chcesz usunąć?", "Usuń", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        status.IsActive = false;
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
                    List = new ObservableCollection<PaymentForAllView>(SortDescending
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
                        List = new ObservableCollection<PaymentForAllView>(AllList.Where(item =>
                            item.Nazwa.ToLower().Trim().Contains(SearchText)));
                        break;
                }
            else
                List = new ObservableCollection<PaymentForAllView>(AllList);

            Sort();
        }

        protected override List<string> GetSearchComboBoxItems()
        {
            return new List<string>() { "Nazwa" };
        }

        public override void Load()
        {
            AllList = (
                from unit in firmaEntities.SposobPlatnosci
                where unit.IsActive == true
                select new PaymentForAllView() 
                {
                    SposobPlatnosciID = unit.SposobPlatnosciID,
                    Nazwa = unit.Nazwa,
                    Opis = unit.Opis,
                }
            ).Take(20).ToList();

            List = new ObservableCollection<PaymentForAllView>(AllList);
        }

        protected override int GetSelectedItemId()
        {
            return SelectedItem?.SposobPlatnosciID ?? -1;
        }

        #endregion
    }
}