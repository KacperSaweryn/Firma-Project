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
    public class NewContractorViewModel : OneAllViewModel<Kontrahent, Adres>, IDataErrorInfo
    {
        public NewContractorViewModel() : base("Kontrahent", "Adres")
        {
            Item = new Kontrahent()
            {
                IsActive = true,
                PrzedstawicielKontrahentaID = 1
            };
            SetLists();
            SetDefaultValues();

            Messenger.Default.Register<Adres>(this, GetWybranyAddress);
        }

        public NewContractorViewModel(int id) : base("Kontrahent", "Adres")
        {
            Item = Db.Kontrahent.First(item => item.KontrahentID == id);

            SetLists();
            SetDefaultValues();

            DaneAdresu =
                $"{Item.Adres.Poczta}, {Item.Adres.Miasto} ul. {Item.Adres.Ulica} {Item.Adres.NrDomu}/{Item.Adres.NrLokalu}";
            Messenger.Default.Register<Adres>(this, GetWybranyAddress);
        }


        #region Properties

        public List<ComboBoxKeyAndValue> Adresy { get; set; }

        public List<ComboBoxKeyAndValue> Statusy { get; set; }

        public List<KontrahentRodzaj> Rodzaje { get; set; }


        private string _DaneAdresu;

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

        public string Kod
        {
            get { return Item.Kod; }
            set
            {
                if (value != Item.Kod)
                {
                    Item.Kod = value;
                    base.OnPropertyChanged(() => Kod);
                }
            }
        }

        public string Nazwa
        {
            get { return Item.Nazwa; }
            set
            {
                if (value != Item.Nazwa)
                {
                    Item.Nazwa = value;
                    base.OnPropertyChanged(() => Nazwa);
                }
            }
        }

        public string NIP
        {
            get { return Item.NIP; }
            set
            {
                if (value != Item.NIP)
                {
                    Item.NIP = value;
                    base.OnPropertyChanged(() => NIP);
                }
            }
        }

        public string NrKonta
        {
            get { return Item.NrKonta; }
            set
            {
                if (value != Item.NrKonta)
                {
                    Item.NrKonta = value;
                    base.OnPropertyChanged(() => NrKonta);
                }
            }
        }

        public bool IsPodatnikVat
        {
            get { return Item.IsPodatnikVat; }
            set
            {
                if (value != Item.IsPodatnikVat)
                {
                    Item.IsPodatnikVat = value;
                    base.OnPropertyChanged(() => IsPodatnikVat);
                }
            }
        }


        public int KontrahentAdres
        {
            get { return Item.AdresID; }
            set
            {
                if (value != Item.AdresID)
                {
                    Item.AdresID = value;
                    base.OnPropertyChanged(() => KontrahentAdres);
                }
            }
        }

        public int KontrahentStatus
        {
            get { return Item.StatusID; }
            set
            {
                if (value != Item.StatusID)
                {
                    Item.StatusID = value;
                    base.OnPropertyChanged(() => KontrahentStatus);
                }
            }
        }

        public int? PrzedstawicielKontrahentaID
        {
            get { return Item.PrzedstawicielKontrahentaID; }
            set
            {
                if (value != Item.PrzedstawicielKontrahentaID)
                {
                    Item.PrzedstawicielKontrahentaID = value;
                    base.OnPropertyChanged(() => PrzedstawicielKontrahentaID);
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
            KontrahentAdres = adres.AdresID;
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
                DaneAdresu = "   Błąd!";
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

            Rodzaje = Db.KontrahentRodzaj.Where(item => item.IsActive).ToList();
            Statusy = Db.Status.Where(item => item.IsActive == true)
                .Select(item => new ComboBoxKeyAndValue()
                {
                    Key = item.StatusID,
                    Value = item.Nazwa
                })
                .ToList();
        }

        protected override void SetDefaultValues()
        {
            if (Statusy.Count > 0)
            {
                KontrahentStatus = Statusy.First().Key;
            }

            try
            {
                var id = Adresy.First(item => item.Key == Item.AdresID).Key;
                KontrahentAdres = Db.Adres.First(item => item.AdresID == id).AdresID;
            }
            catch
            {
                KontrahentAdres = Adresy.First().Key;
            }
        }

        public override void Save()
        {
            try
            {
                if (KontrahentAdres == Adresy.First().Key)
                {
                    MessageBox.Show("Nie wprowadzono danych adresowych!\n" +
                                    "Ustawiono wartości domyślne. Sprawdź poprawność i w razie konieczności zmodyfikuj dane!",
                        "Błąd!");
                }

                Item.IsActive = true;
                Db.Kontrahent.AddObject(Item);
                Db.SaveChanges();
            }
            catch (Exception e)
            {
                if (KontrahentAdres == Adresy.First().Key)
                {
                    MessageBox.Show("Nie wprowadzono danych adresowych!\n" +
                                    "Ustawiono wartości domyślne. Sprawdź poprawność i w razie konieczności zmodyfikuj dane!",
                        "Błąd!");
                    Db.SaveChanges();
                }

                Db.SaveChanges();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(NIP):
                        return StringValidator.IsNotNull(NIP);
                    case nameof(Kod):
                        return StringValidator.IsNotNull(Kod);
                    case nameof(Nazwa):
                        return StringValidator.IsNotNull(Nazwa);
                    case nameof(NrKonta):
                        return StringValidator.IsNotNull(NrKonta);
                    default:
                        return string.Empty;
                }
            }
        }

        protected override bool IsValid()
        {
            return this[nameof(Nazwa)] == string.Empty && this[nameof(Kod)] == string.Empty &&
                   this[nameof(NrKonta)] == string.Empty &&
                   this[nameof(NrKonta)] == string.Empty &&
                   this[nameof(NIP)] == string.Empty;
        }

        public string Error => string.Empty;

        #endregion
    }
}