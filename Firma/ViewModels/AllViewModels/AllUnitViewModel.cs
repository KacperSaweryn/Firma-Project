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
    public class AllUnitViewModel : AllViewModel<UnitForAllView>
    {
        #region Konstruktor

        public AllUnitViewModel() : base("Jednostki")
        {
        }

        #endregion

        #region Metody

        protected override void Delete()
        {
            if (SelectedItem != null)
            {
                var status =
                    FakturyEntities.Jednostka.FirstOrDefault(item => item.JednostkaID == SelectedItem.JednostkaID);
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
                    List = new ObservableCollection<UnitForAllView>(SortDescending
                        ? List.OrderByDescending(item => item.JednostkaNazwa)
                        : List.OrderBy(item => item.JednostkaNazwa));
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
                        List = new ObservableCollection<UnitForAllView>(AllList.Where(item =>
                            item.JednostkaNazwa.ToLower().Trim().Contains(SearchText)));
                        break;
                }
            else
                List = new ObservableCollection<UnitForAllView>(AllList);

            Sort();
        }

        protected override List<string> GetSearchComboBoxItems()
        {
            return new List<string>() { "Nazwa" };
        }

        public override void Load()
        {
            AllList = (
                from unit in firmaEntities.Jednostka //dla kazdej faktury z db
                where unit.IsActive == true
                select new UnitForAllView() //tworzymy nowa fakture for all view
                {
                    JednostkaID = unit.JednostkaID,
                    JednostkaNazwa = unit.JednostkaNazwa
                }
            ).Take(20).ToList();

            List = new ObservableCollection<UnitForAllView>(AllList);
        }

        protected override int GetSelectedItemId()
        {
            return SelectedItem?.JednostkaID ?? -1;
        }

        #endregion
    }
}