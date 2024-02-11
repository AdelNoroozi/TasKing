using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskManager.Services;
using TaskManager.Views;
using TaskManager.Models;

namespace TaskManager.ViewModel {
    public partial class LockerViewModel : ObservableObject
    {
        private bool _isLocked;
        private bool _isRegistering;
        private static string _Password; //local input password (could be wrong)
        private string _newPassword;
        private string _repeatPassword;
        private string _errorMessage;
        private PasswordService _passService;
        public LockerViewModel()
        {
            _isRegistering = MainViewModel._isRegistering;

            if (MainViewModel._Password == null)
            {
                _isRegistering = true;
            }
        }

        public bool HasPassword
        {
            get => MainViewModel._Password != null;
        }

        public bool NoPassword
        {
            get => MainViewModel._Password == null;
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
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsLoggingIn));
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
                if (_isRegistering)
                {
                    UpdatePassword();
                }
                if (_newPassword != null)
                {
                    MainViewModel._hasPassword = true;
                    MainViewModel._isRegistering = false;
                }
                Password = "";
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
                if (_Password != MainViewModel._Password)
                {
                    if (IsRegistering)
                    {
                        return "Incorrect old password.";
                    }
                    else
                    {
                        return "Incorrect password.";// + _Password + "!= "+ MainViewModel._Password;
                    }
                }
            }
            if (_isRegistering) { 
                if (NewPassword != RepeatPassword)
                {
                    return "Passwords do not match.";
                }
            }
            return string.Empty;
        }
        //private async void UpdatePassword()
        //{
        //    PasswordModel newPasswordModel = new PasswordModel { Password = _newPassword };
        //    await _passService.SavePassword(newPasswordModel);
        //}
        private void UpdatePassword()
        {
            MainViewModel._Password = NewPassword;
        }
        [ICommand]
        private void Switch()
        {
            MainViewModel._isRegistering = !MainViewModel._isRegistering;
            Password = "";
            Shell.Current.Navigation.PushAsync(new LockerPage(new LockerViewModel()));
        }
    }
}