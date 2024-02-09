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
    public async void GoBack()
    {
        TaskService taskService = new TaskService();
        var mainViewModel = new MainViewModel(taskService);
        await Shell.Current.Navigation.PushAsync(new MainPage(mainViewModel));
    }

}
