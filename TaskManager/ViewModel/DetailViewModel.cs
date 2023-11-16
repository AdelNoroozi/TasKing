using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.ViewModel;

[QueryProperty(nameof(TaskDetail),"TaskDetail")]
  public partial class DetailViewModel : ObservableObject
{
    [ObservableProperty]
    private TaskModel taskDetail;

    [ICommand]
    public async void GoBack()
    {
        TaskService taskService = new TaskService();
        var mainViewModel = new MainViewModel(taskService);
        await Shell.Current.Navigation.PushAsync(new MainPage(mainViewModel));
    }

}
