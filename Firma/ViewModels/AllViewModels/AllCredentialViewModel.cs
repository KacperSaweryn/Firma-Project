using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Firma.Models.Entities;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels.AllViewModels
{
    public class AllCredentialViewModel : AllViewModel<Credential>
    {
        #region Konstruktor

        public AllCredentialViewModel() : base("Hasla")
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
            List = new ObservableCollection<Credential>(
                from credential
                in firmaEntities.Credential
                where credential.IsActive == true
                select credential
            );
        }

        protected override int GetSelectedItemId()
        {
            throw new NotImplementedException();
        }
    }
}