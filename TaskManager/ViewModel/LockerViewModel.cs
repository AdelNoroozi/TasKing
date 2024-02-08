using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using TaskManager.Services;
using TaskManager.Views;
namespace TaskManager.ViewModel {
    public partial class LockerViewModel : ObservableObject
    {
        private MainViewModel mainViewModel;
        private LockerViewModel mainViewModel2;
        private bool _hasPassword;
        private bool _isLocked;
        private bool _isRegistering;
        private string _Password;
        private string _newPassword;
        private string _repeatPassword;
        private string _errorMessage;

        public LockerViewModel()
        {
            _isRegistering = true;
            _hasPassword = true;
            _isLocked = false;
        }

        public bool HasPassword
        {
            get => _hasPassword;
            set
            {
                _hasPassword = value;
                OnPropertyChanged();
            }
        }

        public bool IsLocked
        {
            get => _isLocked;
            set
            {
                _isLocked = value;
                OnPropertyChanged();
            }
        }

        public bool IsRegistering
        {
            get => _isRegistering;
            set
            {
                if (_isRegistering != value)
                {
                    _isRegistering = value;
                    OnPropertyChanged(); // Notify the UI of property change
                    OnPropertyChanged(nameof(IsLoggingIn)); // Update dependent property
                }
            }
        }
        public bool IsLoggingIn => !IsRegistering;

        public string Password
        {
            get => _Password;
            set
            {
                _Password = value;
                OnPropertyChanged();
            }
        }

        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged();
            }
        }

        public string RepeatPassword
        {
            get => _repeatPassword;
            set
            {
                _repeatPassword = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }


        [ICommand]
        public async void GoBack()
        {
            TaskService taskService = new TaskService();
            var mainViewModel = new MainViewModel(taskService);
            await Shell.Current.Navigation.PushAsync(new MainPage(mainViewModel));
        }
        [ICommand]
        private async void Submit()
        {
            string errorMessage = ValidateForm();

            if (string.IsNullOrEmpty(errorMessage))
            {
                UpdatePassword();
                GoBack();
            }
            else
            {
                ErrorMessage = errorMessage;
            }
        }

        private string ValidateForm()
        {
            if (HasPassword)
            {
                if (Password != "1111")
                {
                    if (IsRegistering)
                    {
                        return "Incorrect old password.";
                    }
                    else
                    {
                        return "Incorrect password.";
                    }
                }
            }

            if (NewPassword != RepeatPassword)
            {
                return "Passwords do not match.";
            }

            return string.Empty;
        }

        private void UpdatePassword()
        {

        }

        [ICommand]
        private async void Switch()
        {
            _isRegistering = !_isRegistering;
            ErrorMessage = IsLoggingIn.ToString();

            await Shell.Current.Navigation.PushAsync(new LockerPage(new LockerViewModel()));



        }


    }
}