namespace TodoApi
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsCompleted { get; set; }
        public virtual Member? Member { get; set; }
        public virtual Place? Place { get; set; }
    }
}
