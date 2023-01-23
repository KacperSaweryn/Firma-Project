using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Firma.Helpers;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.Models.Validators;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;

namespace Firma.ViewModels.NewViewModels
{
    public class
        NewInvoicePositionViewModel : OneViewModel<PozycjaFaktury>
    {
        #region Konstruktor

        public NewInvoicePositionViewModel() : base("Pozycja")
        {
   
            Item = new PozycjaFaktury()
            {
                IsActive = true,
                CenaNetto = 0,
                Rabat = 0,
                Ilosc = 0
                
            };
            Towary = Db.Towar.Where(item => item.IsActive == true)
                .Select(item => new ComboBoxKeyAndValue()
                    { Key = item.TowarID, Value = item.Nazwa })
                .ToList();

            if (Towary.Count > 0)
            {
                TowarID = Towary.First().Key;
                Item.StawkaVat = Db.Towar.Where(item => item.TowarID == TowarID).Select(item => item.StawkaVatSprzedazy).First();
            }
        }

        #endregion

        #region Properties

        public MessengerMessage<NewInvoiceViewModel, PozycjaFaktury, object> Message { get; set; }
        public List<ComboBoxKeyAndValue> Towary { get; set; }
        public List<ComboBoxKeyAndValue> Kontrahenci { get; set; }



        public int TowarID
        {
            get { return Item.TowarID; }
            set
            {
                if (value != Item.TowarID)
                {
                    Item.TowarID = value;
                    base.OnPropertyChanged(() => TowarID);
                }
            }
        }

        public decimal Ilosc
        {
            get { return Item.Ilosc; }
            set
            {
                if (value != Item.Ilosc)
                {
                    Item.Ilosc = value;
                    base.OnPropertyChanged(() => Ilosc);
                }
            }
        }

      

        public decimal Cena
        {
            get { return Item.CenaNetto; }
            set
            {
                if (value != Item.CenaNetto)
                {
                    Item.CenaNetto = value;
                    base.OnPropertyChanged(() => Cena);
                }
            }
        }


        public decimal Rabat
        {
            get { return Item.Rabat; }
            set
            {
                if (value != Item.Rabat)
                {
                    Item.Rabat = value;
                    base.OnPropertyChanged(() => Rabat);
                }
            }
        }

        public int StawkaVat
        {
            get { return Item.StawkaVat; }
            set
            {
                if (value != Item.StawkaVat)
                {
                    Item.StawkaVat = value;
                    base.OnPropertyChanged(() => StawkaVat);
                }
            }
        }

        #endregion


        #region Metody

        public override void Save()
        {
            Message.Response = Item;
            Messenger.Default.Send(Message);
        }

        #endregion
    }
}