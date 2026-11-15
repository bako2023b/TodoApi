namespace TodoApi
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }

        // Idegen kulcsok (Foreign Keys) hozzáadása:
        public int? MemberId { get; set; }
        public virtual Member? Member { get; set; }

        public int? PlaceId { get; set; }
        public virtual Place? Place { get; set; }
    }
}
