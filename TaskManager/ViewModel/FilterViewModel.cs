using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.ViewModel;

[QueryProperty(nameof(Status), "Status")]
public partial class FilterViewModel : ObservableObject
{

    [ObservableProperty]
    private string status;

    public ObservableCollection<TaskModel> FilteredTasks { get; set; } = new ObservableCollection<TaskModel>();

    private readonly ITaskService _taskService;
    public FilterViewModel(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [ObservableProperty]
    private bool isLoading;

    [ICommand]
    public async void ShowFilteredTasks()
    {
        IsLoading = true;

        FilteredTasks.Clear();
        var filteredTaskList = await _taskService.GetTasksByStatus(Status);
        if (filteredTaskList?.Count > 0)
        {
            foreach (var task in filteredTaskList)
            {
                FilteredTasks.Add(task);
            }
        }
        IsLoading = false;
    }

    [ICommand]
    async void MoveToRecycleBin(TaskModel task)
    {
        if (FilteredTasks.Contains(task))
        {
            await _taskService.MakeTaskVisibleOrInvisible(task.TaskId, false);
            FilteredTasks.Remove(task);
        }

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

    [ICommand]
    public async void GoBack()
    {
        TaskService taskService = new TaskService();
        var mainViewModel = new MainViewModel(taskService);
        await Shell.Current.Navigation.PushAsync(new MainPage(mainViewModel));
    }

}
