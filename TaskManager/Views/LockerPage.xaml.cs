using TaskManager.ViewModel;

namespace TaskManager.Views;

public partial class LockerPage : ContentPage
{
	public LockerPage(LockerViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}