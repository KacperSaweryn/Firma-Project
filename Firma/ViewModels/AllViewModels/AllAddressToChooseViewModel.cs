namespace Firma.ViewModels.AllViewModels
{
    public class AllAddressToChooseViewModel : AllAddressViewModel
    {
        #region Konstruktor

        public AllAddressToChooseViewModel() : base()
        {
        }

        #endregion

        #region Helpers

        protected override void AfterSelect()
        {
            OnRequestClose();
        }

        #endregion
    }
}