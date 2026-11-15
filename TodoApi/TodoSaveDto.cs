namespace TodoApi
{
    public class TodoSaveDto
    {
        public string Name { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
        public IdRefDto? Member { get; set; }
        public IdRefDto? Place { get; set; }
    }
}
