using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Firma.Helpers;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.Models.Validators;
using Firma.ViewModels.Abstract;
using Firma.ViewModels.AllViewModels;
using Firma.Views.AllViews;
using GalaSoft.MvvmLight.Messaging;

namespace Firma.ViewModels.NewViewModels
{
    public class NewEmployeeViewModel : OneAllViewModel<Pracownik, Osoba>, IDataErrorInfo
    {
        #region Konstruktor

        public NewEmployeeViewModel() : base("Nowy Pracownik", "Osoba")
        {
            Item = new Pracownik()
            {
                IsActive = true,
                CredentialID = 1
            };
            SetLists();
            SetDefaultValues();
            Messenger.Default.Register<Osoba>(this, GetWybranaOsoba);
        }

        public NewEmployeeViewModel(int id) : base("Nowy Pracownik", "Osoba")
        {
            Item = Db.Pracownik.First(item => item.PracownikID == id);
            SetLists();
            SetDefaultValues();
            var osobaId = Osoby.First(item => item.Key == Item.OsobaID).Key;

            //Item.OsobaID = Db.Osoba.First(item => item.OsobaID == id).OsobaID;
           // Osoba = Db.Osoba.First(item => item.OsobaID == osobaId).OsobaID;
            DaneOsobowe = $"{Item.Osoba.Nazwisko} {Item.Osoba.Imie}";

            Messenger.Default.Register<Osoba>(this, GetWybranaOsoba);
        }

        #endregion


        #region Properties

        public List<ComboBoxKeyAndValue> Osoby { get; set; }

        public List<ComboBoxKeyAndValue> Credentiale { get; set; }

        private string _DaneOsobowe;

        public string DaneOsobowe
        {
            get { return _DaneOsobowe; }
            set
            {
                if (value != _DaneOsobowe)
                {
                    _DaneOsobowe = value;
                    OnPropertyChanged((() => DaneOsobowe));
                }
            }
        }

        public int Osoba
        {
            get { return Item.OsobaID; }
            set
            {
                if (value != Item.OsobaID)
                {
                    Item.OsobaID = value;
                    base.OnPropertyChanged(() => Osoba);
                }
            }
        }

        public string Stanowisko
        {
            get { return Item.Stanowisko; }
            set
            {
                if (value != Item.Stanowisko)
                {
                    Item.Stanowisko = value;
                    base.OnPropertyChanged(() => Stanowisko);
                }
            }
        }

        public int CredentialID
        {
            get { return Item.CredentialID; }
            set
            {
                if (value != Item.CredentialID)
                {
                    Item.CredentialID = value;
                    base.OnPropertyChanged(() => CredentialID);
                }
            }
        }

        #endregion

        #region Komendy

        private ICommand _WybierzOsobeCommand;

        public ICommand WybierzOsobeCommand
        {
            get
            {
                if (_WybierzOsobeCommand == null)
                {
                    _WybierzOsobeCommand = new BaseCommand(() => WybierzOsobe());
                }

                return _WybierzOsobeCommand;
            }
        }

        #endregion

        #region Metody

        private void GetWybranaOsoba(Osoba osoba)
        {
            DaneOsobowe = $"{osoba.Nazwisko} {osoba.Imie}";
            Osoba = osoba.OsobaID;
        }

        private void WybierzOsobe()
        {
            Window window = new Window();
            AllPersonView allPersonSimpleView = new AllPersonView();
            var vm = new AllPersonToChooseViewModel();
            allPersonSimpleView.DataContext = vm;
            window.Content = allPersonSimpleView;
            vm.RequestClose += delegate(object sender, EventArgs e) { window.Close(); };
            window.ShowDialog();
            var selected = vm.SelectedItem;
            if (selected == null)
            {
                Item.OsobaID = 1;
                DaneOsobowe = "   Błąd!";
                //$"{Item.Osoba.Nazwisko} {Item.Osoba.Imie}";
            }
            else
            {
                Item.OsobaID = selected.OsobaID;
                DaneOsobowe = $"{selected.Nazwisko} {selected.Imie}";
            }
        }

        protected override void ShowAddView()
        {
        }

        protected override void SetLists()
        {
            Osoby = Db.Osoba.Where(item => item.IsActive == true).Select(item => new ComboBoxKeyAndValue()
            {
                Key = item.OsobaID,
                Value = item.Nazwisko + " " + item.Imie
            }).ToList();
        }

        protected override void SetDefaultValues()
        {
            if (Osoby.Count > 0)
            {
                try
                {
                    var id = Osoby.First(item => item.Key == Item.OsobaID).Key;
                    Osoba = Db.Osoba.First(item => item.OsobaID == id).OsobaID;
                }
                catch
                {
                    Osoba = Osoby.First().Key;
                }
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Stanowisko):
                        return StringValidator.IsNotNull(Stanowisko);
                    default:
                        return string.Empty;
                }
            }
        }

        public string Error => string.Empty;

        public override void Save()
        {
            try
            {
                if (Osoba == Osoby.First().Key)
                {
                    MessageBox.Show("Nie wprowadzono danych osobowych!\n" +
                                    "Ustawiono wartości domyślne. Sprawdź poprawność i w razie konieczności zmodyfikuj dane!",
                        "Błąd!");
                }

                Item.IsActive = true;
                Db.Pracownik.AddObject(Item);
                Db.SaveChanges();
            }
            catch (Exception e)
            {
                if (Osoba == Osoby.First().Key)
                {
                    MessageBox.Show("Nie wprowadzono danych osobowych!\n" +
                                    "Ustawiono wartości domyślne. Sprawdź poprawność i w razie konieczności zmodyfikuj dane!",
                        "Błąd!");
                    Db.SaveChanges();
                }

                Db.SaveChanges();
            }
        }

        #endregion
    }
}