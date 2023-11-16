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
            var response = await _taskService.AddTask(new Models.TaskModel
            {
                Title = Title,
                Description = Description
            });
            if (response > 0)
            {
                await Shell.Current.DisplayAlert("Record Added", "Record Added to Task Table", "ok");
                try
                {
                    await Shell.Current.GoToAsync("..");
                }
                catch
                {

                }

            }
            else
            {
                await Shell.Current.DisplayAlert("Heads Up!", "Something went wrong while adding record", "ok");
                await Shell.Current.GoToAsync("..");
            }

        }
    }
}
