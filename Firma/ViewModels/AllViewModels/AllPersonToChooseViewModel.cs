namespace Firma.ViewModels.AllViewModels
{
    public class AllPersonToChooseViewModel : AllPersonViewModel
    {
        
        #region Konstruktor

        public AllPersonToChooseViewModel() : base()
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