using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoWpfApp.Models
{
    public class TodoSavePayload
    {
        public string Name { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public IdRef? Member { get; set; }
        public IdRef? Place { get; set; }
    }
}
