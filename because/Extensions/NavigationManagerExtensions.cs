using System;
using Microsoft.AspNetCore.Components;

namespace because.Extensions
{
    public static class NavigationManagerExtensions
    {
        // navigationManager.Uri ->  http://0.0.0.0/trips/add
        // navigationManager.BasdeUri -> http://0.0.0.0/
        // GetCurrentPageUrl -> /trips/add
        public static string GetCurrentPageUrl(this NavigationManager navigationManager) =>
            $"/{navigationManager.Uri.Replace(navigationManager.BaseUri, "").TrimStart('/')}";

        public static void GoToInnerPage(this NavigationManager navigationManager, string innerPageUrl)
        {
            navigationManager.NavigateTo(innerPageUrl, new NavigationOptions
            {
                HistoryEntryState = navigationManager.GetCurrentPageUrl()
            });
        }

        public static void GoBack(this NavigationManager navigationManager, string? fallbackUrl = "/selectuser")
        {
            var previousPageUrl = navigationManager.HistoryEntryState ?? fallbackUrl;
            navigationManager.NavigateTo(previousPageUrl, new NavigationOptions
            {
                HistoryEntryState = null,
                ReplaceHistoryEntry = true
            });
        }
    }
}

