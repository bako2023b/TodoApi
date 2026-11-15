using System;
using System.Collections.Generic;
using System.Text;

namespace TodoWpf.App.Models
{
    public class TodoSavePload
    {
        public string Name { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
        public IdRef? Member { get; set; }
        public IdRef? Place {  get; set; }
    }
}
