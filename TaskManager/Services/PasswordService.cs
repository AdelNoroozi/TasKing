using TaskManager.Models;
using SQLite;

namespace TaskManager.Services
{
    public class PasswordService
    {

        private SQLiteAsyncConnection _dbConnection;

        public string Password { get; private set; }
        public async Task SetPassword(string password)
        {
            string targetFile = Path.Combine(FileSystem.AppDataDirectory, "password.txt");
            using (FileStream outputStream = File.OpenWrite(targetFile))
            using (StreamWriter streamWriter = new StreamWriter(outputStream))
            {
                await streamWriter.WriteAsync(password);
            }
        }

        public async Task<string> GetPassword()
        {
            string targetFile = Path.Combine(FileSystem.AppDataDirectory, "password.txt");
            if (File.Exists(targetFile))
            {
                using (FileStream inputStream = File.OpenRead(targetFile))
                using (StreamReader reader = new StreamReader(inputStream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
            else
            {
                return "";
            }
        }
    }
}
