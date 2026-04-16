using Microsoft.AspNetCore.Mvc;
using Capstone.Models;

namespace TaskFlow.Controllers
{
    public class AccountController : Controller
    {
        private static List<User> _users = new List<User>();
        private static int _nextId = 1;

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Please fill in all fields";
                return View();
            }

            if (_users.Any(u => u.Username == username))
            {
                ViewBag.Error = "Username already exists";
                return View();
            }

            _users.Add(new User
            {
                Id = _nextId++,
                Username = username,
                Password = password
            });

            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password";
                return View();
            }

            HttpContext.Session.SetString("User", username);
            return RedirectToAction("Index", "Task");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User");
            return RedirectToAction("Login");
        }
    }
}