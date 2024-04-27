using MySql.Data.MySqlClient;
using ca3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ca3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Constructor injection for ILogger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Action method for the index page
        public IActionResult Index()
        {
            return View();
        }

        // Action method for the privacy page
        public IActionResult Privacy()
        {
            return View();
        }

        // Action method for the login page
        public IActionResult Login()
        {
            return View();
        }

        // POST action method to handle login form submission
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Connection string for your MySQL database
            string connectionString = "Server=localhost;Database=ca3;Uid=root;Pwd=1234;";

            // SQL query to check user credentials
            string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";

            // Create a MySQL connection and command objects
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                try
                {
                    // Open the connection
                    connection.Open();

                    // Execute the SQL command
                    MySqlDataReader reader = command.ExecuteReader();

                    // Check if the reader has any rows (i.e., if the login is successful)
                    if (reader.HasRows)
                    {
                        // Redirect the user to the home page or any other page upon successful login
                        return RedirectToAction("home", "Home");
                    }
                    else
                    {
                        // If login fails, return a view indicating login failure
                        TempData["ErrorMessage"] = "Invalid username or password.";
                        return View("Login");
                    }
                }
                catch (Exception ex)
                {
                    // Log any exceptions
                    _logger.LogError($"Error during login: {ex.Message}");
                    return View("Error");
                }
            }
        }

        // Action method for the password page

        public IActionResult Home()
        {
            return View();
        }
        public IActionResult Password()
        {
            return View();
        }

        // Action method for the hostel page
        public IActionResult Hostel()
        {
            return View();
        }

        // Action method for the education page
        public IActionResult Education()
        {
            return View();
        }

        // Action method for the test page
        public IActionResult Test()
        {
            return View();
        }

        // Error handling action method
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
