using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskManager.Services;

namespace TaskManager.ViewModel
{
    public partial class AddViewModel : ObservableObject
    {
        private readonly ITaskService _taskService;
        public AddViewModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [ObservableProperty]
        public string title;

        [ObservableProperty]
        public string description;

        [ICommand]
        public async void Add()
        {
            if (Title != null || Description != null)
            {
                var response = await _taskService.AddTask(new Models.TaskModel
                {
                    Title = Title,
                    Description = Description
                });

                if (response > 0)
                {
                    await Shell.Current.DisplayAlert("Record Added", "Record Added to Task Table", "ok");

                }
                else
                {
                    await Shell.Current.DisplayAlert("Heads Up!", "Something went wrong while adding record", "ok");
                }
                GoBack();
            }

            else
            {
                await Shell.Current.DisplayAlert("Hold Up!", "Title or Description is Empty!", "ok");

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
