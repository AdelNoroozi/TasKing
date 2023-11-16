using TaskManager.ViewModel;

namespace TaskManager.Views;

public partial class AddPage : ContentPage
{
	public AddPage(AddViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}