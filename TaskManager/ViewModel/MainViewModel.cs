using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskManager.Models;
using TaskManager.Services;
using TaskManager.Views;

namespace TaskManager.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public ObservableCollection<TaskModel> Tasks { get; set; } = new ObservableCollection<TaskModel>();

        private readonly ITaskService _taskService;
        public MainViewModel(ITaskService taskService)
        {
            _taskService=taskService;
            if (!_Unlock){ // first lock
                //load password TODO
                _Unlock = true;
                _isRegistering = false;
                _hasPassword = true;
                var lockerViewModel = new LockerViewModel();
                lockerViewModel.IsRegistering = false;

                _Password = lockerViewModel.firstRunPass;
                Shell.Current.Navigation.PushAsync(new LockerPage(lockerViewModel));
                
                _UnlockEnded = true;
            } 
        }

        [ObservableProperty]
        private bool isLoading;
        public static string _Password;
        public static bool _Unlock;
        public static bool _UnlockEnded;
        public static bool _isRegistering;
        public static bool _hasPassword;
        

        private TaskStatus _selectedStatus;
        public TaskStatus SelectedStatus
        {
            get { return _selectedStatus; }
            set
            {
            }
        }


        [ICommand]
        public async void GetTaskList()
        {
            IsLoading = true;

            Tasks.Clear();
            await Task.Delay(1);
            var taskList = await _taskService.GetTaskList();
            if (taskList?.Count > 0)
            {
                foreach (var task in taskList)
                {
                    Tasks.Add(task);
                }
            }
            IsLoading = false;

        }


        [ICommand]
        public async void Add()
        {
            await Shell.Current.GoToAsync($"{nameof(AddPage)}");
        }

        [ICommand]
        async void MoveToRecycleBin(TaskModel task)
        {
            if (Tasks.Contains(task))
            {
                // var taskList = await _taskService.DeleteTask(task);
                await _taskService.MakeTaskVisibleOrInvisible(task.TaskId, false);
                Tasks.Remove(task);
            }

        }

        [ICommand]
        async void ToDoFilter()
        {
            var currentPage = App.Current.MainPage;

            var navParam = new Dictionary<string, string>();
            navParam.Add("Status", "To Do");
            TaskService taskService = new TaskService();
            var filterViewModel = new FilterViewModel(taskService);

            filterViewModel.Status = "To Do";

            await Application.Current.MainPage.Navigation.PushModalAsync(new FilterPage(filterViewModel));
        }

        [ICommand]
        async void InProgressFilter()
        {
            var currentPage = App.Current.MainPage;

            var navParam = new Dictionary<string, string>();
            navParam.Add("Status", "In Progress");
            TaskService taskService = new TaskService();
            var filterViewModel = new FilterViewModel(taskService);

            filterViewModel.Status = "In Progress";

            await Application.Current.MainPage.Navigation.PushModalAsync(new FilterPage(filterViewModel));
        }

        [ICommand]
        async void DoneFilter()
        {
            var currentPage = App.Current.MainPage;

            var navParam = new Dictionary<string, string>();
            navParam.Add("Status", "Done");
            TaskService taskService = new TaskService();
            var filterViewModel = new FilterViewModel(taskService);

            filterViewModel.Status = "Done";

            await Application.Current.MainPage.Navigation.PushModalAsync(new FilterPage(filterViewModel));
        }

        [ICommand]
        async void ChangeStatus(TaskModel task)
        {
            if (Tasks.Contains(task))
            {
                string currentStatus = task.Status;

                switch (currentStatus)
                {
                    case "To Do":
                        await _taskService.UpdateTaskStatus(task.TaskId, "In Progress");
                        break;

                    case "In Progress":
                        await _taskService.UpdateTaskStatus(task.TaskId, "Done");
                        break;

                    case "Done":
                        await _taskService.UpdateTaskStatus(task.TaskId, "To Do");
                        break;
                }
                TaskService taskService = new TaskService();
                var mainViewModel = new MainViewModel(taskService);
                await Shell.Current.Navigation.PushAsync(new MainPage(mainViewModel));
            }
        }


        [ICommand]
        async void GoToRecycleBin()
        {
            await Shell.Current.GoToAsync($"{nameof(RecycleBinPage)}");

        }
        [ICommand]
        async void GoToLocker()
        {
            await Shell.Current.GoToAsync($"{nameof(LockerPage)}");

        }

        [ICommand]
        async Task Tap(TaskModel task)
        {
            var currentPage = App.Current.MainPage; 

            var navParam = new Dictionary<string, object>();
            navParam.Add("TaskDetail", task);

            var detailViewModel = new DetailViewModel();

            detailViewModel.TaskDetail = task;

            await Application.Current.MainPage.Navigation.PushModalAsync(new DetailPage(detailViewModel));
        }
    }
}