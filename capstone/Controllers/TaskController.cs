using Microsoft.AspNetCore.Mvc;
using Capstone.Models;

namespace TaskFlow.Controllers
{
    public class TaskController : Controller
    {
        private static List<TaskItem> _tasks = new List<TaskItem>();
        private static int _nextId = 1;

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                return RedirectToAction("Login", "Account");
            }
            return View(_tasks);
        }

        [HttpPost]
        public IActionResult Add(string title, string priority, DateTime dueDate)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                _tasks.Add(new TaskItem
                {
                    Id = _nextId++,
                    Title = title,
                    Priority = priority,
                    DueDate = dueDate
                });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _tasks.RemoveAll(t => t.Id == id);
            return RedirectToAction("Index");
        }
    }
}