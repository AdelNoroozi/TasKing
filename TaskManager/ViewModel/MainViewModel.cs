using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskManager.Models;
using TaskManager.Services;
using TaskManager.Views;

namespace TaskManager.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public ObservableCollection<TaskModel> Tasks { get; set; }=new ObservableCollection<TaskModel>();

        private readonly ITaskService _taskService;
        public MainViewModel(ITaskService taskService)
        {
            _taskService=taskService;
        }
        [ObservableProperty]
        private bool isLoading;
  
        
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
        async void Delete(TaskModel task)
        {
            if (Tasks.Contains(task))
            {
                // var taskList = await _taskService.DeleteTask(task);
                await _taskService.MakeTaskVisibleOrInvisible(task.TaskId, false);
                Tasks.Remove(task);
            }
           
        }      
        [ICommand]
        async void GoToRecycleBin()
        {
            await Shell.Current.GoToAsync($"{nameof(RecycleBinPage)}");

        }


        [ICommand]
        async Task Tap(TaskModel task)
        {
            // Assuming you have a reference to the current Page or Navigation instance
            // If not, you may need to pass it to the ViewModel or find another way to obtain it.
            var currentPage = App.Current.MainPage; // Change this to get the current Page instance

            var navParam = new Dictionary<string, object>();
            navParam.Add("TaskDetail", task);

            // Create an instance of DetailViewModel
            var detailViewModel = new DetailViewModel();

            // Set the property directly in the DetailViewModel
            detailViewModel.TaskDetail = task;

            // Use PushModalAsync instead of GoToAsync
            await Application.Current.MainPage.Navigation.PushModalAsync(new DetailPage(detailViewModel));
        }
    }
}
