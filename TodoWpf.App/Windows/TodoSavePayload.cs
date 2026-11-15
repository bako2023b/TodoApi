using TodoWpf.App.Models;

namespace TodoWpfApp.Windows
{
    internal class TodoSavePayload : TodoSavePload
    {
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public IdRef Member { get; set; }
        public IdRef Place { get; set; }
    }
}