using SQLite;

namespace TaskManager.Models
{
    public class TaskModel
    {
        [PrimaryKey, AutoIncrement]
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; } = true;

    }
}
