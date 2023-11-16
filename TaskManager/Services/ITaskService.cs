using TaskManager.Models;

namespace TaskManager.Services
{
    public interface ITaskService
    {
        Task<List<TaskModel>> GetTaskList();
        Task<int> AddTask(TaskModel task);
        Task<int> DeleteTask(TaskModel task);

    }
}
