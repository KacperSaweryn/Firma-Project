using System.Windows.Input;
using Firma.Views.BaseViews;

namespace Firma.Views.NewViews
{
    /// <summary>
    /// Interaction logic for NewCommodityView.xaml
    /// </summary>
    public partial class NewCommodityView : OneAllNoAddViewBase
    {
        public NewCommodityView()
        {
            InitializeComponent();
           
        }

        private void TextBox_MouseLeftButtonUp(object sender,MouseButtonEventArgs e)
        {

        }

        private void TextBoxKod_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // TextBoxKod.Clear();
            // TextBoxKod.Focus();
            // //TextBoxKod.Text = "";
            //  TextBoxKod.Text = "{Binding Path=Kod, UpdateSourceTrigger=PropertyChanged, ValidatesOnDataErrors=True}";
        }

        private void TextBoxKod_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // TextBoxKod.Clear();
            // TextBoxKod.Focus();
            // //TextBoxKod.Text = "";
            // TextBoxKod.Text = "{Binding Path=Kod, UpdateSourceTrigger=PropertyChanged, ValidatesOnDataErrors=True}";
        }
    }
}
