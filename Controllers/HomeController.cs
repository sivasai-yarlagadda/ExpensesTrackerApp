using ExpensesTrackerApp.Models;
using ExpenseTrackerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ExpensesTrackerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExpenseTrackerContext _context;

        public HomeController(ILogger<HomeController> logger, ExpenseTrackerContext context)//Constructor
        {
            _logger = logger;
            _context = context;//Added 
        }

        public IActionResult Index(DateTime? fromDate, DateTime? toDate)
        {
            var transactions = _context.Transactions.AsQueryable();

            // ✅ Apply date filters if provided
            if (fromDate.HasValue)
                transactions = transactions.Where(t => t.Date >= fromDate.Value);

            if (toDate.HasValue)
                transactions = transactions.Where(t => t.Date <= toDate.Value);

            // ✅ Total Income
            var totalIncome = transactions
                .Where(t => t.Type == "Income")
                .Sum(t => (decimal?)t.Amount) ?? 0;

            // ✅ Total Expense
            var totalExpense = transactions
                .Where(t => t.Type == "Expense")
                .Sum(t => (decimal?)t.Amount) ?? 0;

            var balance = totalIncome - totalExpense;

            // ✅ Category-wise grouping
            var categoryExpense = transactions
                .Where(t => t.Type == "Expense")
                .Include(t => t.Category)
                .GroupBy(t => t.Category.Name)
                .Select(g => new
                {
                    CategoryName = g.Key,
                    Total = g.Sum(x => x.Amount)
                })
                .ToList();

            ViewBag.CatLabels = string.Join(",", categoryExpense.Select(c => $"'{c.CategoryName}'"));
            ViewBag.CatValues = string.Join(",", categoryExpense.Select(c => c.Total));

            ViewBag.TotalIncome = totalIncome;
            ViewBag.TotalExpense = totalExpense;
            ViewBag.Balance = balance;

            // ✅ Pass filter values back to view (for date inputs)
            ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
            ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
