namespace because;

public partial class App : Application
{

    public App(AppViewModel viewModel)
	{
		InitializeComponent();

		MainPage = new MainPage(viewModel);
    }

    protected override async void OnStart()
    {
        base.OnStart();
    }
}
