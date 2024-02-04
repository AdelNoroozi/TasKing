using TaskManager.Views;

namespace TaskManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
            Routing.RegisterRoute(nameof(AddPage), typeof(AddPage));        
            Routing.RegisterRoute(nameof(RecycleBinPage), typeof(RecycleBinPage));        }
    }
}