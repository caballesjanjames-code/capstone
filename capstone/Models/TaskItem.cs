namespace Capstone.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Priority { get; set; } = "Low";
        public DateTime DueDate { get; set; } = DateTime.Now;
    }
}