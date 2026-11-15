using System;
using System.Collections.Generic;
using System.Text;

namespace TodoWpf.App.Models
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<string>? TodoItemsName {  get; set; }
    }
}
