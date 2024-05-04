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

        private Tabs _containerTab = Tabs.Encoder;

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
            ContainerSource = "./Views/Pages/EncoderPage.xaml";

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
                case Tabs.Encoder:
                    source = "./Views/Pages/EncoderPage.xaml";
                    break;
                case Tabs.Decoder:
                    source = "./Views/Pages/DecoderPage.xaml";
                    break;
                case Tabs.Utilities:
                    source = "./Views/Pages/UtilitiesPage.xaml";
                    break;
                case Tabs.Settings:
                    source = "./Views/Pages/SettingsPage.xaml";
                    break;
                default:
                    source = "./Views/Pages/AlertPage.xaml";
                    break;
            }

            ContainerSource = source;
        }

        #endregion
    }
}
