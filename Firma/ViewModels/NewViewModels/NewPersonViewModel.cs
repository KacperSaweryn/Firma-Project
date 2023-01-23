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
    public class NewPersonViewModel : OneAllViewModel<Osoba, Adres>, IDataErrorInfo
    {
        #region Konstruktor

        public NewPersonViewModel() : base("Nowa Osoba", "Adres")
        {
            Item = new Osoba()
            {
                IsActive = true
            };
            SetLists();
            SetDefaultValues();
        
            Messenger.Default.Register<Adres>(this, GetWybranyAddress);
        }

        public NewPersonViewModel(int id) : base("Nowa Osoba", "Adres")
        {
            Item = Db.Osoba.First(item => item.OsobaID == id);
            SetLists();
            SetDefaultValues();
            
            DaneAdresu =
                $"{Item.Adres.Poczta}, {Item.Adres.Miasto} ul. {Item.Adres.Ulica} {Item.Adres.NrDomu}/{Item.Adres.NrLokalu}";
            Messenger.Default.Register<Adres>(this, GetWybranyAddress);
        }

        #endregion


        #region Properties

        private string _DaneAdresu;
        public List<ComboBoxKeyAndValue> Adresy { get; set; }

        public string DaneAdresu
        {
            get { return _DaneAdresu; }
            set
            {
                if (value != _DaneAdresu)
                {
                    _DaneAdresu = value;
                    OnPropertyChanged((() => DaneAdresu));
                }
            }
        }

        public string Imie
        {
            get { return Item.Imie; }
            set
            {
                if (value != Item.Imie)
                {
                    Item.Imie = value;
                    base.OnPropertyChanged(() => Imie);
                }
            }
        }

        public string Nazwisko
        {
            get { return Item.Nazwisko; }
            set
            {
                if (value != Item.Nazwisko)
                {
                    Item.Nazwisko = value;
                    base.OnPropertyChanged(() => Nazwisko);
                }
            }
        }

        public string Telefon
        {
            get { return Item.Telefon; }
            set
            {
                if (value != Item.Telefon)
                {
                    Item.Telefon = value;
                    base.OnPropertyChanged(() => Telefon);
                }
            }
        }

        public string Email
        {
            get { return Item.Email; }
            set
            {
                if (value != Item.Email)
                {
                    Item.Email = value;
                    base.OnPropertyChanged(() => Email);
                }
            }
        }

        public int? Adres
        {
            get { return Item.AdresID; }
            set
            {
                if (value != Item.AdresID)
                {
                    Item.AdresID = value;
                    base.OnPropertyChanged(() => Adres);
                }
            }
        }

        #endregion

        #region Komendy

        private ICommand _WybierzAdresCommand;

        public ICommand WybierzAdresCommand
        {
            get
            {
                if (_WybierzAdresCommand == null)
                {
                    _WybierzAdresCommand = new BaseCommand(() => WybierzAdres());
                }

                return _WybierzAdresCommand;
            }
        }

        #endregion

        #region Metody

        private void GetWybranyAddress(Adres adres)
        {
            string nrLokalu;
            if (adres.NrLokalu != string.Empty)
            {
                nrLokalu = "/" + adres.NrLokalu;
            }
            else
            {
                nrLokalu = "";
            }

            DaneAdresu = $"{adres.Poczta}, {adres.Miasto} ul. {adres.Ulica} {adres.NrDomu} {nrLokalu}";
            Adres = adres.AdresID;
        }

        private void WybierzAdres()
        {
            Window window = new Window();
            AllAddressSimpleView allAddressSimpleView = new AllAddressSimpleView();
            var vm = new AllAddressToChooseViewModel();
            allAddressSimpleView.DataContext = vm;
            window.Content = allAddressSimpleView;
            vm.RequestClose += delegate(object sender, EventArgs e) { window.Close(); };
            window.ShowDialog();
            var selected = vm.SelectedItem;
            if (selected == null)
            {
                Item.AdresID = 1;
                DaneAdresu =
                    $"{Item.Adres.Poczta}, {Item.Adres.Miasto} ul. {Item.Adres.Ulica} {Item.Adres.NrDomu}/{Item.Adres.NrLokalu}";
            }
            else
            {
                Item.AdresID = selected.AdresID;
                DaneAdresu =
                    $"{selected.Poczta}, {selected.Miasto} ul. {selected.Ulica} {selected.NrDomu}/{selected.NrLokalu}";
            }
        }

        protected override void ShowAddView()
        {
        }

        protected override void SetLists()
        {
            Adresy = Db.Adres
                .Select(item => new ComboBoxKeyAndValue()
                {
                    Key = item.AdresID,
                    Value = item.Poczta
                            + " " + item.Miasto
                            + " " + item.Ulica
                            + " " + item.NrDomu
                            + " " + item.NrLokalu
                })
                .ToList();
        }

        protected override void SetDefaultValues()
        {
            try
            {
                var id = Adresy.First(item => item.Key == Item.AdresID).Key;
                Adres = Db.Adres.First(item => item.AdresID == id).AdresID;
            }
            catch
            {
                Adres = Adresy.First().Key;
            }
        }

        protected override bool IsValid()
        {
            return this[nameof(Imie)] == string.Empty && this[nameof(Nazwisko)] == string.Empty &&
                   this[nameof(Telefon)] == string.Empty &&
                   this[nameof(Email)] == string.Empty;
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Imie):
                        return StringValidator.IsNotNull(Imie);
                    case nameof(Nazwisko):
                        return StringValidator.IsNotNull(Nazwisko);
                    case nameof(Telefon):
                        return StringValidator.IsNotNull(Telefon);
                    case nameof(Email):
                        return StringValidator.IsNotNull(Email);
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
                if (Adres == Adresy.First().Key)
                {
                    MessageBox.Show("Nie wprowadzono danych adresowych!\n" +
                                    "Ustawiono wartości domyślne. Sprawdź poprawność i w razie konieczności zmodyfikuj dane!",
                        "Błąd!");
                }

                Item.IsActive = true;
                Db.Osoba.AddObject(Item);
                Db.SaveChanges();
            }
            catch (Exception e)
            {
                if (Adres == Adresy.First().Key)
                {
                    MessageBox.Show("Nie wprowadzono danych adresowych!\n" +
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