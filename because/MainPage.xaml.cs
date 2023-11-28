using Firebase.Database;

namespace because;

public partial class MainPage : ContentPage
{
	readonly FirebaseClient firebaseClient = new FirebaseClient(baseUrl: "https://mauifirebasedemo-2540e-default-rtdb.asia-southeast1.firebasedatabase.app/");
	public MainPage(AppViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
