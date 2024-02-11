using TaskManager.Models;

namespace TaskManager.Services
{
    public interface ITaskService
    {
        Task<List<TaskModel>> GetTaskList();
        Task<List<TaskModel>> GetInvisibleTaskList();
        Task<int> AddTask(TaskModel task);
        Task<int> DeleteTask(TaskModel task);
        Task<int> MakeTaskVisibleOrInvisible(int taskId, bool isVisible);
        Task<int> UpdateTaskStatus(int taskId, string newStatus);
        Task<List<TaskModel>> GetTasksByStatus(string status);
    }
}
