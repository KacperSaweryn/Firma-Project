using System.Windows;
using System.Windows.Controls;

namespace Firma.Views.BaseViews
{
    public class OneAllViewBase : UserControl
    {
        static OneAllViewBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OneAllViewBase), new FrameworkPropertyMetadata(typeof(OneAllViewBase)));
        }
    }
}

