    using SQLite;

    namespace TaskManager.Models
    {
        public class PasswordModel
        {
            [PrimaryKey, AutoIncrement]
            public string Password { get; set; }

        }
    }
