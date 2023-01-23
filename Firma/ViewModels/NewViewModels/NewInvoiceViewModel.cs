using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
    public class
        NewInvoiceViewModel : OneAllViewModel<Faktura,
            InvoicePositionForAllView>, IDataErrorInfo
    {
        #region Konstruktor

        public NewInvoiceViewModel() : base("Faktura", "Pozycje Faktury")
        {
            Faktury = Db.Faktura.ToList();
            Item = new Faktura()
            {
                IsActive = true,
                CzyZatwierdzona = true,
                KtoZatwierdzil = 2,
                DataWystawienia = DateTime.Today,
                TerminPlatnosci = DateTime.Today.AddDays(7),
                KontrahentID = 5,
                NumerFak = DateTime.Today.Year.ToString() + "/" + Faktury.Count
            };
            Db.Faktura.AddObject(Item);
            SetLists();
            SetDefaultValues();
            
            Messenger.Default.Register<Kontrahent>(this, GetWybranyKontrahent);
            Messenger.Default.Register<PozycjaFaktury>(this, GetPozycjaFaktury);
            Messenger.Default.Register<MessengerMessage<NewInvoiceViewModel, PozycjaFaktury, object>>(this,
                GetPozycjaFaktury2);
        }

        public NewInvoiceViewModel(int id) : base("Faktura", "Pozycje Faktury")
        {
            Faktury = Db.Faktura.ToList();

            Item = Db.Faktura.First(item => item.FakturaID == id);

            SetLists();
            SetDefaultValues();
            GetOldPozycjeFaktury();
            DaneKontrahenta = $"{Item.Kontrahent.Nazwa} - {Item.Kontrahent.NIP} - {Item.Kontrahent.Kod}";
            Messenger.Default.Register<Kontrahent>(this, GetWybranyKontrahent);
            Messenger.Default.Register<PozycjaFaktury>(this, GetPozycjaFaktury);
            Messenger.Default.Register<MessengerMessage<NewInvoiceViewModel, PozycjaFaktury, object>>(this,
                GetPozycjaFaktury2);
        }

        #endregion

        #region Properties

        public List<ComboBoxKeyAndValue> SposobyPlatnosci { get; set; }
        public List<ComboBoxKeyAndValue> Kontrahenci { get; set; }
        public List<Faktura> Faktury { get; set; }
        private List<PozycjaFaktury> PozycjeFaktury { get; set; }
        public List<Towar> Towary { get; set; }


        private string _DaneKontrahenta;

        public string DaneKontrahenta
        {
            get { return _DaneKontrahenta; }
            set
            {
                if (value != _DaneKontrahenta)
                {
                    _DaneKontrahenta = value;
                    OnPropertyChanged((() => DaneKontrahenta));
                }
            }
        }

        public string NumerFak
        {
            get { return Item.NumerFak; }
            set
            {
                if (value != Item.NumerFak)
                {
                    Item.NumerFak = value;
                    base.OnPropertyChanged(() => NumerFak);
                }
            }
        }

        public DateTime DataWystawienia
        {
            get { return Item.DataWystawienia; }
            set
            {
                if (value != Item.DataWystawienia)
                {
                    Item.DataWystawienia = value;
                    base.OnPropertyChanged(() => DataWystawienia);
                }
            }
        }

        public DateTime TerminPlatnosci
        {
            get { return Item.TerminPlatnosci; }
            set
            {
                if (value != Item.TerminPlatnosci)
                {
                    Item.TerminPlatnosci = value;
                    base.OnPropertyChanged(() => TerminPlatnosci);
                }
            }
        }

        public int KontrahentID
        {
            get { return Item.KontrahentID; }
            set
            {
                if (value != Item.KontrahentID)
                {
                    Item.KontrahentID = value;
                    base.OnPropertyChanged(() => KontrahentID);
                }
            }
        }

        public int SposobPlatnosciID
        {
            get { return Item.SposobPlatnosciID; }
            set
            {
                if (value != Item.SposobPlatnosciID)
                {
                    Item.SposobPlatnosciID = value;
                    base.OnPropertyChanged(() => SposobPlatnosciID);
                }
            }
        }

        public int KtoZatwierdzil
        {
            get { return Item.KtoZatwierdzil; }
            set
            {
                if (value != Item.KtoZatwierdzil)
                {
                    Item.KtoZatwierdzil = value;
                    base.OnPropertyChanged(() => KtoZatwierdzil);
                }
            }
        }

        #endregion


        #region Komendy

        private ICommand _WybierzKontrahentaCommand;

        public ICommand WybierzKontrahentaCommand
        {
            get
            {
                if (_WybierzKontrahentaCommand == null)
                {
                    _WybierzKontrahentaCommand = new BaseCommand(() => WybierzKontrahenta());
                }

                return _WybierzKontrahentaCommand;
            }
        }

        #endregion


        #region Metody

        protected override void SetDefaultValues()
        {
            if (SposobyPlatnosci.Count > 0)
            {
                SposobPlatnosciID = SposobyPlatnosci.First().Key;
            }

            try
            {
                var id = Kontrahenci.First(item => item.Key == Item.KontrahentID).Key;
                KontrahentID = Db.Kontrahent.First(item => item.KontrahentID == id).KontrahentID;
            }
            catch
            {
                KontrahentID = Kontrahenci.First().Key;
            }
        }

        protected override void SetLists()
        {
            List = new ObservableCollection<InvoicePositionForAllView>();
            Towary = Db.Towar.Where(item => item.IsActive).ToList();

            Kontrahenci = Db.Kontrahent.Where(item => item.IsActive).Select(item => new ComboBoxKeyAndValue()
            {
                Key = item.KontrahentID,
                Value = item.Nazwa + " - " + item.NIP + " | " + item.Kod
            }).ToList();

            SposobyPlatnosci = Db.SposobPlatnosci.Where(item => item.IsActive == true)
                .Select(item => new ComboBoxKeyAndValue()
                    { Key = item.SposobPlatnosciID, Value = item.Nazwa })
                .ToList();
        }

        private void WybierzKontrahenta()
        {
            Window window = new Window();
            AllContractorSimpleView allContractorSimpleView = new AllContractorSimpleView();
            var vm = new AllContractorToChooseViewModel();
            allContractorSimpleView.DataContext = vm;
            window.Content = allContractorSimpleView;
            vm.RequestClose += delegate(object sender, EventArgs e) { window.Close(); };
            window.ShowDialog();
            var selected = vm.SelectedItem;
            if (selected == null)
            {
                Item.KontrahentID = 5;
                DaneKontrahenta = $"{Item.Kontrahent.Nazwa} - {Item.Kontrahent.NIP} - {Item.Kontrahent.Kod}";
            }
            else
            {
                Item.KontrahentID = selected.KontrahentID;
                DaneKontrahenta = $"{selected.Nazwa} - {selected.NIP} - {selected.Kod}";
            }
        }

        protected override void ShowAddView()
        {
            MessengerMessage<NewInvoiceViewModel, PozycjaFaktury, object> message =
                new MessengerMessage<NewInvoiceViewModel, PozycjaFaktury, object>()
                {
                    Sender = this
                };
            Messenger.Default.Send(message);
        }

        private void GetWybranyKontrahent(Kontrahent kontrahent)
        {
            DaneKontrahenta = $"{kontrahent.Nazwa} - {kontrahent.NIP} - {kontrahent.Kod}";
            KontrahentID = kontrahent.KontrahentID;
        }

        private void GetPozycjaFaktury(PozycjaFaktury pozycjaFaktury)
        {
            Item.PozycjaFaktury.Add(pozycjaFaktury);
            Towar towar = Db.Towar.First(item => item.TowarID == pozycjaFaktury.TowarID);

            List.Add(new InvoicePositionForAllView(
                    towar.Kod,
                    towar.Nazwa,
                    Convert.ToDecimal(pozycjaFaktury.CenaNetto.ToString("F")),
                    pozycjaFaktury.Ilosc,
                    pozycjaFaktury.Rabat,
                    pozycjaFaktury.Towar.Vat.StawkaVat
                    
                )
            
            ); towar.Ilosc -= pozycjaFaktury.Ilosc;
        }

        //TODO dodac gdzies przy modify wypelnianie listy
        private void GetPozycjaFaktury2(MessengerMessage<NewInvoiceViewModel, PozycjaFaktury, object> obj)
        {
            if (obj.Sender == this && obj.Response != null)
            {
                Item.PozycjaFaktury.Add(obj.Response);
                Towar towar = Db.Towar.First(item => item.TowarID == obj.Response.TowarID);
                
                List.Add(new InvoicePositionForAllView(
                    towar.Kod,
                    towar.Nazwa,
                     Convert.ToDecimal(obj.Response.CenaNetto.ToString("F")),
                    obj.Response.Ilosc,
                    obj.Response.Rabat,
                    obj.Response.Towar.Vat.StawkaVat)
                ); towar.Ilosc -= obj.Response.Ilosc;
            }
        }

        private void GetOldPozycjeFaktury()
        {
            PozycjeFaktury = Db.PozycjaFaktury.ToList();

            foreach (var pozycja in PozycjeFaktury)
            {
                if (pozycja.FakturaID == Item.FakturaID)
                {
                    List.Add(new InvoicePositionForAllView(
                        pozycja.Towar.Kod,
                        pozycja.Towar.Nazwa,
                        Convert.ToDecimal(pozycja.CenaNetto.ToString("F")),
                        pozycja.Ilosc,
                        pozycja.Rabat,
                        pozycja.Towar.Vat.StawkaVat));
                }

            }
            
        }

        protected override bool IsValid()
        {
            if (this[nameof(KontrahentID.ToString)] != "-1" || Item.NumerFak != null ||
                this[nameof(KontrahentID.ToString)] != "null"
                || Item.KontrahentID > 0)
            {
                return true;
            }

            return false;
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(NumerFak):
                        return StringValidator.IsNotNull(NumerFak);
                    case nameof(KontrahentID):
                        return DecimalValidator.IsNotMinus(KontrahentID);
                    default:
                        return string.Empty;
                }
            }
        }

        public string Error => string.Empty;

        public override void Save()
        {
            if (Item.KontrahentID > 1 && Item.KontrahentID != 5 && Item.NumerFak != string.Empty)
            {
                
                Db.SaveChanges();
            }
            else if (Item.KontrahentID > 1 && Item.KontrahentID != 5 && Item.NumerFak != string.Empty)
            {
                NumerFak = DateTime.Today.Year.ToString() + "/" + Faktury.Count;
            }
            else if(Item.KontrahentID == 5)
            {
                Item.KontrahentID = 5;

                MessageBox.Show("Brak kontrahenta!\n" +
                                "Ustawiono wartość domyślną. Sprawdź fakturę przed wydrukiem!", "Błąd");
                Db.SaveChanges();
            }
        }

        #endregion
    }
}