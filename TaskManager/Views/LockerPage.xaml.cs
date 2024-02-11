using TaskManager.ViewModel;
using TaskManager.Services;
namespace TaskManager.Views;

public partial class LockerPage : ContentPage
{
    private LockerViewModel loverViewModel;
    public LockerPage(LockerViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        this.BindingContext = vm;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        PasswordService _passwordService = new PasswordService();
        string readpassword = await _passwordService.GetPassword();
        if (readpassword != null)
        {
            MainViewModel._Password = readpassword;
        }
        var loverViewModel = new LockerViewModel();
        loverViewModel.LoadPasswordCommand.Execute(null);
    }
}