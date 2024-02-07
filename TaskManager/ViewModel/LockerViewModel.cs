using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using TaskManager.Services;
namespace TaskManager.ViewModel { 
public partial class LockerViewModel : ObservableObject
    {
        private MainViewModel mainViewModel;

        private bool _isLogin;
        private bool _isRegister;

        public bool IsLogin
        {
            get => _isLogin;
            set
            {
                _isLogin = value;
                OnPropertyChanged();
            }
        }

        public bool IsRegister
        {
            get => _isRegister;
            set
            {
                _isRegister = value;
                OnPropertyChanged();
            }
        }

        [ICommand]
        public async void GoBack()
        {
            TaskService taskService = new TaskService();
            var mainViewModel = new MainViewModel(taskService);
            await Shell.Current.Navigation.PushAsync(new MainPage(mainViewModel));
        }
    }
}