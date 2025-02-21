using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public bool IsComplete { get; set; }
        public DateTime? Created { get; set; } = DateTime.UtcNow;
    }
}