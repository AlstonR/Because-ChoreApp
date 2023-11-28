using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Google.Cloud.Firestore;
using Firebase.Database;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace because;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{

        var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			})
			.UseMauiCommunityToolkit();

		builder.Services.AddMauiBlazorWebView();
		builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
		{
			ApiKey = "AIzaSyCcEhwQU4BhoLl_mYVHDkwUPT2JmShLMag",
			AuthDomain = "because-f3c54.firebaseapp.com",
			Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
			{
				new EmailProvider()
			}
		}));


#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		AddServices(builder.Services);

		return builder.Build();
	}

	private static void AddServices(IServiceCollection services)
	{
		services.AddSingleton<AppViewModel>()
				.AddSingleton<MauiInterop>()
				.AddSingleton<AppState>();


		services.AddTransient<AuthService>()
                .AddSingleton<CurrentUser>()
				.AddSingleton<ChildService>()
				.AddSingleton<FirestoreService>()
				.AddSingleton<HouseHoldService>()
				.AddSingleton<ChoreService>()
				.AddSingleton<RewardService>()
				.AddSingleton<ParentService>();

	}
}
