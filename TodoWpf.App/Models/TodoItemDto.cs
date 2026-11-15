using System;
using System.Collections.Generic;
using System.Text;

namespace TodoWpf.App.Models
{
    public class TodoItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
        public string? Member {  get; set; }
        public string? Place {  get; set; }
        public bool? IsCompleted { get; internal set; }
    }
}
