using System;
using CommunityToolkit.Maui.Alerts;

namespace because
{
    public class MauiInterop
    {
        private readonly AppViewModel _appViewModel;

        public MauiInterop(AppViewModel appViewModel)
        {
            _appViewModel = appViewModel;
        }

        public void ShowLoader() => _appViewModel.ToggleIsBusy(true);
        public void HideLoader() => _appViewModel.ToggleIsBusy(false);

        public async Task ShowErrorAlertAsync(string message, string? title = "Error") =>
            await App.Current.MainPage.DisplayAlert(title, message, "Ok");

        public async Task ShowSuccessAlertAsync(string message, string? title = "Success") =>
            await App.Current.MainPage.DisplayAlert(title, message, "Ok");

        public bool IsAndroid => DeviceInfo.Current.Platform == DevicePlatform.Android;

        public bool IsIOS => DeviceInfo.Current.Platform == DevicePlatform.iOS;

        public async Task ShowToastAsync(string message) =>
            await Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Short).Show();

        public async Task<string?> ShowPromptAsync(string title, string message, string okButtonText, string placeholder)
        {
            return await App.Current.MainPage.DisplayPromptAsync(title, message, okButtonText, placeholder: placeholder);
        }

        public async Task OpenInLauncher(string uri) =>
            await Launcher.Default.OpenAsync(uri);
    }
}

