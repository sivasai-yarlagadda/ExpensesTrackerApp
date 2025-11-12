using ExpenseTrackerApp.Models;

namespace ExpenseTrackerApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ExpenseTrackerContext context)
        {
            // If there are already categories, do nothing
            if (context.Categories.Any())
                return;

            var categories = new[]
            {
                new Category { Name = "Food" },
                new Category { Name = "Transport" },
                new Category { Name = "Rent" },
                new Category { Name = "Salary" },
                new Category { Name = "Entertainment" },
                new Category { Name = "Other" }
            };

            context.Categories.AddRange(categories); 
            context.SaveChanges();
        }
    }
}
