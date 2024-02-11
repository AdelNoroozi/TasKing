using TaskManager.ViewModel;

namespace TaskManager;

public partial class FilterPage : ContentPage
{
    private FilterViewModel viewModel;
    public FilterPage(FilterViewModel vm)
    {

        InitializeComponent();
        BindingContext = vm;
        viewModel = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.ShowFilteredTasksCommand.Execute(null);
    }
}