namespace Firma.ViewModels.AllViewModels
{
    public class AllContractorToChooseViewModel : AllContractorViewModel
    {
        

        #region Konstruktor

        public AllContractorToChooseViewModel() : base()
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