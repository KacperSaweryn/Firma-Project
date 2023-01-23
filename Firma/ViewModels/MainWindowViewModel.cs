using Firma.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using Firma.ViewModels.AllViewModels;
using Firma.ViewModels.NewViewModels;
using Firma.ViewModels.Reports;
using Firma.Views.Reports;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Firma.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Komendy menu i paska narzedzi

        public ICommand NowyTowarCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<NewCommodityViewModel>()); }
        }

        public ICommand TowaryCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<AllCommodityViewModel>()); }
        }

        public ICommand NowaFakturaCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<NewInvoiceViewModel>()); }
        }

        public ICommand FakturyCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<AllInvoiceViewModel>()); }
        }

        public ICommand KontrahenciCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<AllContractorViewModel>()); }
        }

        public ICommand NowyKontrahentCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<NewContractorViewModel>()); }
        }

        public ICommand AdresyCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<AllAddressViewModel>()); }
        }

        public ICommand AdresCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<NewAddressViewModel>()); }
        }

        public ICommand TowarRodzajCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<AllCommodityTypeViewModel>()); }
        }

        public ICommand NowyTowarRodzajCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<NewCommodityTypeViewModel>()); }
        }

        public ICommand StatusCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<NewStatusViewModel>()); }
        }

        public ICommand StatusyCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<AllStatusViewModel>()); }
        }

        public ICommand OsobaCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<NewPersonViewModel>()); }
        }

        public ICommand OsobyCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<AllPersonViewModel>()); }
        }

        public ICommand PracownikCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<NewEmployeeViewModel>()); }
        }

        public ICommand PracownicyCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<AllEmployeeViewModel>()); }
        }

        public ICommand SaleReportCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<SaleReportViewModel>()); }
        }

        public ICommand InvoiceReportCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<NumOfInvoicesInTImeViewModel>()); }
        }

        public ICommand AddTowarToWarehouseCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<AddCommodityViewModel>()); }
        }

        public ICommand SposobyPlatnosciCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<AllPaymentMethodsViewModel>()); }
        }

        public ICommand SposobPlatnosciCommand
        {
            get { return new BaseCommand(() => CreateOrSwitchView<NewPaymentMethodViewModel>()); }
        }


        public ICommand OpenSupportWebsiteCommand
        {
            get
            {
                if (mOpenSupportWebsiteCommand == null)
                {
                    mOpenSupportWebsiteCommand = new RelayCommand<object>(OpenSupportWebsite);
                }

                return mOpenSupportWebsiteCommand;
            }
        }

        private RelayCommand<object> mOpenSupportWebsiteCommand;

        private void OpenSupportWebsite(object url)
        {
            System.Diagnostics.Process.Start(url as string);
        }

        #endregion

        #region Przyciski w menu z lewej strony

        private ReadOnlyCollection<CommandViewModel> _Commands;

        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_Commands == null)
                {
                    List<CommandViewModel>
                        cmds = this.CreateCommands();
                    _Commands =
                        new ReadOnlyCollection<CommandViewModel>(
                            cmds);
                }

                return _Commands;
            }
        }

        private List<CommandViewModel> CreateCommands()
        {
            Messenger.Default.Register<string>(this, Open);
            Messenger.Default.Register<MessengerMessage<NewInvoiceViewModel, PozycjaFaktury, object>>(this,
                OpenPozycjaFaktury);
            Messenger.Default.Register<MessengerMessage<AllViewModel<object>, Type, int>>(this, Edit);

            return new List<CommandViewModel>
            {
                new CommandViewModel("Nowa Faktura", new BaseCommand(CreateOrSwitchView<NewInvoiceViewModel>)),
                new CommandViewModel("Faktury", new BaseCommand(CreateOrSwitchView<AllInvoiceViewModel>)),
                new CommandViewModel("Nowy Towar", new BaseCommand(CreateOrSwitchView<NewCommodityViewModel>)),
                new CommandViewModel("Towary", new BaseCommand(CreateOrSwitchView<AllCommodityViewModel>)),
                new CommandViewModel("PZ", new BaseCommand(CreateOrSwitchView<AddCommodityViewModel>)),
                new CommandViewModel("Nowy Kontrahent", new BaseCommand(CreateOrSwitchView<NewContractorViewModel>)),
                new CommandViewModel("Kontrahenci", new BaseCommand(CreateOrSwitchView<AllContractorViewModel>)),
                new CommandViewModel("Nowy Rodzaj Towaru",
                    new BaseCommand(CreateOrSwitchView<NewCommodityTypeViewModel>)),
                new CommandViewModel("Rodzaje Towaru", new BaseCommand(CreateOrSwitchView<AllCommodityTypeViewModel>)),
                new CommandViewModel("Raport Faktur",
                    new BaseCommand(CreateOrSwitchView<NumOfInvoicesInTImeViewModel>)),
                new CommandViewModel("Raport Sprzedazy", new BaseCommand(CreateOrSwitchView<SaleReportViewModel>)),
                new CommandViewModel("Nowy Adres", new BaseCommand(CreateOrSwitchView<NewAddressViewModel>)),
                new CommandViewModel("Adresy", new BaseCommand(CreateOrSwitchView<AllAddressViewModel>)),
                new CommandViewModel("Nowa Jednostka", new BaseCommand(CreateOrSwitchView<NewUnitViewModel>)),
                new CommandViewModel("Jednostki", new BaseCommand(CreateOrSwitchView<AllUnitViewModel>)),
                new CommandViewModel("Nowa Płatność", new BaseCommand(CreateOrSwitchView<NewPaymentMethodViewModel>)),
                new CommandViewModel("Płatności", new BaseCommand(CreateOrSwitchView<AllPaymentMethodsViewModel>)),
                new CommandViewModel("Nowa Osoba", new BaseCommand(CreateOrSwitchView<NewPersonViewModel>)),
                new CommandViewModel("Osoby", new BaseCommand(CreateOrSwitchView<AllPersonViewModel>)),
                new CommandViewModel("Nowy Pracownik", new BaseCommand(CreateOrSwitchView<NewEmployeeViewModel>)),
                new CommandViewModel("Pracownicy", new BaseCommand(CreateOrSwitchView<AllEmployeeViewModel>)),
                new CommandViewModel("Nowy Status", new BaseCommand(CreateOrSwitchView<NewStatusViewModel>)),
                new CommandViewModel("Statusy", new BaseCommand(CreateOrSwitchView<AllStatusViewModel>)),
            };
        }

        private void OpenPozycjaFaktury(MessengerMessage<NewInvoiceViewModel, PozycjaFaktury, object> obj)
        {
            if (obj.Response == null)
            {
                NewInvoicePositionViewModel nowaPozycjaFakturyViewModel = new NewInvoicePositionViewModel();
                nowaPozycjaFakturyViewModel.Message = obj;
                CreateView(nowaPozycjaFakturyViewModel);
            }
        }

        private void Edit(MessengerMessage<AllViewModel<object>, Type, int> message)
        {
            if (message.Argument != -1)
            {
                if (message.Response == typeof(InvoiceForAllView))
                {
                    CreateView(new NewInvoiceViewModel(message.Argument));
                }
                else if (message.Response == typeof(ContractorsForAllView))
                {
                    CreateView(new NewContractorViewModel(message.Argument));
                }
                else if (message.Response == typeof(CommodityForAllView))
                {
                    CreateView(new NewCommodityViewModel(message.Argument));
                }
                else if (message.Response == typeof(CommodityTypeForAllView))
                {
                    CreateView(new NewCommodityTypeViewModel(message.Argument));
                }
                else if (message.Response == typeof(AddressForAllView))
                {
                    CreateView(new NewAddressViewModel(message.Argument));
                }
                else if (message.Response == typeof(PersonForAllView))
                {
                    CreateView(new NewPersonViewModel(message.Argument));
                }
                else if (message.Response == typeof(StatusForAllView))
                {
                    CreateView(new NewStatusViewModel(message.Argument));
                }
                else if (message.Response == typeof(UnitForAllView))
                {
                    CreateView(new NewUnitViewModel(message.Argument));
                }
                else if (message.Response == typeof(EmployeeForAllView))
                {
                    CreateView(new NewEmployeeViewModel(message.Argument));
                }
                else if (message.Response == typeof(PaymentForAllView))
                {
                    CreateView(new NewPaymentMethodViewModel(message.Argument));
                }
            }
        }

        #endregion

        #region Zakładki

        private ObservableCollection<WorkspaceViewModel> _Workspaces;

        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.onWorkspacesChanged;
                }

                return _Workspaces;
            }
        }

        private void onWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.onWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.onWorkspaceRequestClose;
        }

        private void onWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            this.Workspaces.Remove(workspace);
        }

        #endregion

        #region Funkcje pomocnicze

        /// <summary>
        /// Ta metoda wywolywana jest przez messenger po otrzymaniu wiadomości
        /// </summary>
        /// <param name="name"></param>
        private void Open(string name)
        {
            if (name == "Towary Add")
            {
                CreateOrSwitchView<NewCommodityViewModel>();
            }
            else if (name == "Faktury Add")
            {
                CreateOrSwitchView<NewInvoiceViewModel>();
            }
            else if (name == "Faktury Show")
            {
                CreateOrSwitchView<AllInvoiceViewModel>();
            }
            else if (name == "Towary Show")
            {
                CreateOrSwitchView<AllCommodityViewModel>();
            }
            else if (name == "Kontrahenci Show")
            {
                CreateOrSwitchView<AllContractorViewModel>();
            }
            else if (name == "Kontrahenci Add")
            {
                CreateOrSwitchView<NewContractorViewModel>();
            }
            else if (name == "Adresy Add")
            {
                CreateOrSwitchView<NewAddressViewModel>();
            }
            else if (name == "Adresy Show")
            {
                CreateOrSwitchView<AllAddressViewModel>();
            }
            else if (name == "Statusy Add")
            {
                CreateOrSwitchView<NewStatusViewModel>();
            }
            else if (name == "Statusy Show")
            {
                CreateOrSwitchView<AllStatusViewModel>();
            }
            else if (name == "Osoby Add")
            {
                CreateOrSwitchView<NewPersonViewModel>();
            }
            else if (name == "Osoby Show")
            {
                CreateOrSwitchView<AllPersonViewModel>();
            }
            else if (name == "Pracownicy Add")
            {
                CreateOrSwitchView<NewEmployeeViewModel>();
            }
            else if (name == "Pracownicy Show")
            {
                CreateOrSwitchView<AllEmployeeViewModel>();
            }
            else if (name == "Jednostki Add")
            {
                CreateOrSwitchView<NewUnitViewModel>();
            }
            else if (name == "Jednostki Show")
            {
                CreateOrSwitchView<AllUnitViewModel>();
            }
            else if (name == "Płatności Add")
            {
                CreateOrSwitchView<NewPaymentMethodViewModel>();
            }
            else if (name == "Płatności Show")
            {
                CreateOrSwitchView<AllPaymentMethodsViewModel>();
            }
            else if (name == "Rodzaje Towaru Add")
            {
                CreateOrSwitchView<NewCommodityTypeViewModel>();
            }
            else if (name == "Rodzaje Towaru Show")
            {
                CreateOrSwitchView<AllCommodityTypeViewModel>();
            }
        }

        private void CreateView(WorkspaceViewModel workspace)
        {
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }


        /// <summary>
        /// Tworzy nowa zakladke lub przelacza na taką, jeśli jest juz otwarta
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private void CreateOrSwitchView<T>() where T : WorkspaceViewModel, new()
        {
            T workspace = Workspaces.FirstOrDefault(vm => vm is T) as T;
            if (workspace == null)
            {
                workspace = new T();
                Workspaces.Add(workspace);
            }

            SetActiveWorkspace(workspace);
        }

        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        #endregion
    }
}