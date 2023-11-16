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
            var taskList = await _dbConnection.Table<TaskModel>().ToListAsync();
            return taskList;
        }
    }
}
