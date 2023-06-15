using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Models
{
    public class ToDoItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public Guid ToDoListId { get; set; }
        public ToDoList ToDoList { get; set; } = null!;
        public string ToDo { get; set; } = null!;
        public DateTime? DueDate { get; set; }
        public bool Completed { get; set; }
        public string? AssignedTo { get; set; }
    }
}
