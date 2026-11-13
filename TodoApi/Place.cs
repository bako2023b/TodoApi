namespace TodoApi
{
    public class Place
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}
