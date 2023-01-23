using System.Drawing;
using System.Windows.Controls;
using Firma.Views.BaseViews;

namespace Firma.Views.NewViews
{
    /// <summary>
    /// Interaction logic for NewInvoiceView.xaml
    /// </summary>
    public partial class NewInvoiceView : OneAllViewBase
    {
        public NewInvoiceView()
        {
            InitializeComponent();
        }

        private void DataGrid_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();

            if (headername == "PozycjaFakturyID")
            {
                e.Cancel = true;
            }

            if (headername == "FakturaID")
            {
                e.Cancel = true;
            }

            if (headername == "FakturaNumer")
            {
                e.Cancel = true;
            }

            if (headername == "TowarKod")
            {
                e.Column.Header = "Kod";
            }
            else if (headername == "TowarNazwa")
            {
                e.Column.Header = "Nazwa";
            }
            else if (headername == "Ilosc")
            {
                e.Column.Header = "Ilość";
            }
            else if (headername == "CenaNetto")
            {
                e.Column.Header = "Cena netto";
                ContentStringFormat = "N2";
            }
            else if (headername == "StawkaVat")
            {
                e.Column.Header = "Vat";
            }
        }
    }
}
