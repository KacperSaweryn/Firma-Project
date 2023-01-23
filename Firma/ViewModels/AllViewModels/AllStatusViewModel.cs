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
    public class AllStatusViewModel : AllViewModel<StatusForAllView>
    {
        #region Konstruktor

        public AllStatusViewModel() : base("Statusy")
        {
        }

        #endregion

        #region Metody

        protected override void Delete()
        {
            if (SelectedItem != null)
            {
                var status =
                    FakturyEntities.Status.FirstOrDefault(item => item.StatusID == SelectedItem.StatusID);
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
                    List = new ObservableCollection<StatusForAllView>(SortDescending
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
                        List = new ObservableCollection<StatusForAllView>(AllList.Where(item =>
                            item.Nazwa.ToLower().Trim().Contains(SearchText)));
                        break;
                }
            else
                List = new ObservableCollection<StatusForAllView>(AllList);

            Sort();
        }


        protected override List<string> GetSearchComboBoxItems()
        {
            return new List<string>() { "Nazwa" };
        }

        public override void Load()
        {
            AllList = (
                from status in firmaEntities.Status //dla kazdej faktury z db
                where status.IsActive == true
                select new StatusForAllView() //tworzymy nowa fakture for all view
                {
                    StatusID = status.StatusID,
                    Nazwa = status.Nazwa
                }
            ).Take(20).ToList();

            List = new ObservableCollection<StatusForAllView>(AllList);
        }

        protected override int GetSelectedItemId()
        {
            return SelectedItem?.StatusID ?? -1;
        }

        #endregion
    }
}