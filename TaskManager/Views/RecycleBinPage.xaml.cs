using TaskManager.ViewModel;

namespace TaskManager;

public partial class RecycleBinPage : ContentPage
{
    private RecycleBinViewModel viewModel;
    public RecycleBinPage(RecycleBinViewModel vm)
    {
        InitializeComponent();
        viewModel = vm;
        this.BindingContext = vm;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.ShowInvisibleTasksCommand.Execute(null);
    }
}