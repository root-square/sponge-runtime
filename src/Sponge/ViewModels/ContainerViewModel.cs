using CommunityToolkit.Mvvm.Messaging;
using Sponge.Messages;
using Sponge.ViewModels.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.ViewModels
{
    public class ContainerViewModel : ViewModelBase
    {
        #region ::FIELDS::

        private string _containerSource = string.Empty;

        public string ContainerSource
        {
            get => _containerSource;
            set => SetProperty(ref _containerSource, value);
        }

        private Tabs _containerTab = Tabs.Task;

        public Tabs ContainerTab
        {
            get => _containerTab;
            set
            {
                SetProperty(ref _containerTab, value);
                SyncronizeTabAndSource();
            }
        }

        #endregion

        #region ::CONSTRUCTOR::

        public ContainerViewModel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Sets the intial page.
            ContainerSource = "./Views/Pages/TaskPage.xaml";

            // Subscribes the messenger to get navigation messages.
            WeakReferenceMessenger.Default.Register<NavigationMessage>(this, (recipient, message) =>
            {
                ContainerSource = message.Value;
            });
        }

        #endregion

        #region ::METHODS::

        private void SyncronizeTabAndSource()
        {
            string source = string.Empty;

            switch (_containerTab)
            {
                case Tabs.Task:
                    source = "./Views/Pages/TaskPage.xaml";
                    break;
                case Tabs.Log:
                    source = "./Views/Pages/LogPage.xaml";
                    break;
                case Tabs.Tools:
                    source = "./Views/Pages/ToolsPage.xaml";
                    break;
                case Tabs.Settings:
                    source = "./Views/Pages/SettingsPage.xaml";
                    break;
            }

            ContainerSource = source;
        }

        #endregion
    }
}
