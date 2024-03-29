using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskManager.Models;
using TaskManager.Services;
using TaskManager.Views;

namespace TaskManager.ViewModel
{
    public partial class RecycleBinViewModel : ObservableObject
    {
        public ObservableCollection<TaskModel> DeletedTasks { get; set; } = new ObservableCollection<TaskModel>();

        private readonly ITaskService _taskService;
        public RecycleBinViewModel(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [ObservableProperty]
        private bool isLoading;

        [ICommand]
        async void Delete(TaskModel task)
        {
            if (DeletedTasks.Contains(task))
            {
                var taskList = await _taskService.DeleteTask(task);
                DeletedTasks.Remove(task);
            }

        }

        [ICommand]
        public async void ShowInvisibleTasks()
        {
            IsLoading = true;

            DeletedTasks.Clear();
            var invisibleTaskList = await _taskService.GetInvisibleTaskList();
            if (invisibleTaskList?.Count > 0)
            {
                foreach (var task in invisibleTaskList)
                {
                    DeletedTasks.Add(task);
                }
            }
            IsLoading = false;
        }
        [ICommand]
        async void Restore(TaskModel task)
        {
            if (DeletedTasks.Contains(task))
            {
                await _taskService.MakeTaskVisibleOrInvisible(task.TaskId, true);
                DeletedTasks.Remove(task);
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

