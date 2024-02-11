using TaskManager.Models;
using SQLite;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {

        private SQLiteAsyncConnection _dbConnection;

        private async Task SetUpDb()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Task.db3");
                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<TaskModel>();
            }
        }

        public async Task<int> AddTask(TaskModel task)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(task);
        }

        public async Task<int> DeleteTask(TaskModel task)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(task);
        }

        public async Task<List<TaskModel>> GetTaskList()
        {
            await SetUpDb();
            var visibleTaskList = await _dbConnection.Table<TaskModel>().Where(t => t.IsVisible).ToListAsync();
            return visibleTaskList;
        }

        public async Task<List<TaskModel>> GetInvisibleTaskList()
        {
            await SetUpDb();
            var invisibleTaskList = await _dbConnection.Table<TaskModel>().Where(t => !t.IsVisible).ToListAsync();
            return invisibleTaskList;
        }


        public async Task<int> MakeTaskVisibleOrInvisible(int taskId, bool isVisible)
        {
            await SetUpDb();
            var taskToUpdate = await _dbConnection.Table<TaskModel>().Where(t => t.TaskId == taskId).FirstOrDefaultAsync();

            if (taskToUpdate != null)
            {
                taskToUpdate.IsVisible = isVisible;
                return await _dbConnection.UpdateAsync(taskToUpdate);
            }

            return 0;
        }

        public async Task<List<TaskModel>> GetTasksByStatus(string status)
        {
            await SetUpDb();
            var tasksByStatus = await _dbConnection.Table<TaskModel>().Where(t => t.Status == status && t.IsVisible).ToListAsync();
            return tasksByStatus;
        }


        public async Task<int> UpdateTaskStatus(int taskId, string newStatus)
        {
            await SetUpDb();
            var taskToUpdate = await _dbConnection.Table<TaskModel>().Where(t => t.TaskId == taskId).FirstOrDefaultAsync();

            if (taskToUpdate != null)
            {
                taskToUpdate.Status = newStatus;
                return await _dbConnection.UpdateAsync(taskToUpdate);
            }

            return 0; // Task not found
        }

    }
}
