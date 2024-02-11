using TaskManager.Models;
using SQLite;

namespace TaskManager.Services
{
    public class PasswordService
    {

        private SQLiteAsyncConnection _dbConnection;

        public string Password { get; private set; }

        private async Task SetUpDb()
        {
            if (_dbConnection == null)
            {   
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Password.db3");
                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<PasswordModel>();
            }
        }
        public async Task<int> SavePassword(PasswordModel password)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(password); ;
        }

        public async Task<string> GetPassword() 
        {
            await SetUpDb();
            PasswordModel passwordModel = await _dbConnection.FindAsync<PasswordModel>(1);
            if (passwordModel != null)
            {
                //return passwordModel.Password;
                return "password found";
            }
            return "password not found";
        }
    }
}
