using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApp.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Transaction>? Transactions { get; set; }
    }
}
