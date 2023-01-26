using System.Windows;

namespace Firma.Views.BaseViews
{
    public class OneAllNoAddViewBase : OneAllViewBase
    {
        static OneAllNoAddViewBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OneAllNoAddViewBase),
                new FrameworkPropertyMetadata(typeof(OneAllNoAddViewBase)));
        }
    }
}