using System;
using System.Collections.Generic;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels.AllViewModels
{
    public class AllInvoicePositionViewModel : AllViewModel<InvoicePositionForAllView>
    {
        #region Konstruktor

        public AllInvoicePositionViewModel() : base("Pozycje")
        {
        }

        #endregion


        protected override void Delete()
        {
            throw new NotImplementedException();
        }

        protected override void Sort()
        {
            throw new NotImplementedException();
        }

        protected override List<string> GetSortComboBoxItems()
        {
            throw new NotImplementedException();
        }

        protected override void Search()
        {
            throw new NotImplementedException();
        }

        protected override List<string> GetSearchComboBoxItems()
        {
            throw new NotImplementedException();
        }

        public override void Load()
        {
            // List = new ObservableCollection<InvoicePositionForAllView>(
            //     from pozycjaFaktury in firmaEntities.PozycjaFaktury
            //     where pozycjaFaktury.IsActive == true
            //     select new InvoicePositionForAllView()
            //     {
            //         PozycjaFakturyID = pozycjaFaktury.PozycjaFakturyID,
            //         FakturaNumer = pozycjaFaktury.Faktura.NumerFak,
            //         TowarKod = pozycjaFaktury.Towar.Kod,
            //         TowarNazwa = pozycjaFaktury.Towar.Nazwa,
            //         Ilosc = pozycjaFaktury.Ilosc,
            //         StawkaVat = pozycjaFaktury.Vat.StawkaVat,
            //         CenaNetto = pozycjaFaktury.CenaNetto,
            //         Rabat = pozycjaFaktury.Rabat,
            //     }
            // );
        }

        protected override int GetSelectedItemId()
        {
            throw new NotImplementedException();
        }
    }
}