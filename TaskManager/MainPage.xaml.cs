using TaskManager.ViewModel;

namespace TaskManager
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel viewModel;
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            viewModel = vm;
            this.BindingContext = vm;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.GetTaskListCommand.Execute(null);
        }
    }
}