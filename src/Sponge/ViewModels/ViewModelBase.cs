using CommunityToolkit.Mvvm.ComponentModel;
using Sponge.Behaviors.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Sponge.ViewModels
{
    /// <summary>
    /// Provides a mechanism for notifying the view model that navigation events have occurred.
    /// </summary>
    public abstract class ViewModelBase : ObservableObject, INavigationAware
    {
        /// <summary>
        /// The event that occur at the start of a frame navigation.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The navigating cancel event arguments</param>
        public virtual void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            // TO-DO: This method is not implemented.
        }

        /// <summary>
        /// The event that occur at the end of a frame navigation.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The navigation event arguments</param>
        public virtual void OnNavigated(object sender, NavigationEventArgs e)
        {
            // TO-DO: This method is not implemented.
        }
    }
}
