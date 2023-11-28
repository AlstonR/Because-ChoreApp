using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace because.States
{
    public class AppState : INotifyPropertyChanged
    {
        //public event EventHandler<string> SelectdMenuItemChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public string SelectedMenuItem { get; private set; } = AppConstants.MenuItems.Home;

        public string PageTitle => SelectedMenuItem switch
        {
            AppConstants.MenuItems.Home => AppConstants.AppName,
            _ => SelectedMenuItem
        };

        private string _innerPageTitle = string.Empty;
        public string InnerPageTitle {
            get => _innerPageTitle;
            private set
            {
                _innerPageTitle = value;
                Notify();
            }
        }

        public TabbarItem[] TabbarItems { get; set; } = Array.Empty<TabbarItem>();

        public void AddTabbarItems(params TabbarItem[] tabbarItems)
        {
            TabbarItems = tabbarItems;
            Notify(nameof(TabbarItems));
        }
        public void NoTabbarItems() => AddTabbarItems(Array.Empty<TabbarItem>());

        public void SetSelectedMenuItem(string pageName)
        {
            SelectedMenuItem = pageName;
            //SelectdMenuItemChanged?.Invoke(this, pageName);
            Notify(nameof(SelectedMenuItem));
        }

        public void SetInnerPageTitle(string pageTitle)
        {
            InnerPageTitle = pageTitle;
        }

        private void Notify([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

