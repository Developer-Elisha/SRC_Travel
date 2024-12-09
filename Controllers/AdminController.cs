using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using SRC_Travel_Agencies.Context;
using SRC_Travel_Agencies.Models;
using System.Linq;

namespace SRC_Travel_Agencies.Controllers
{
    public class AdminController : Controller
    {
        private readonly SqlContext sc;

        public AdminController(SqlContext context)
        {
            sc = context;
        }

        public IActionResult Index()
        {
            SetActiveNavigation("Index");
            var em = HttpContext.Session.GetString("Employee");
            if (em != null)
            {
                // Retrieve session data for the employee
                ViewBag.login = em;

                // Count records for employees, buses, drivers, and tickets
                var totalEmployees = sc.Employees.Count();
                var totalBuses = sc.uploads.Count();
                var totalBusCategory = sc.Category.Count();
                var totalTickets = sc.Ticket.Count();

                // Pass counts to the view using ViewData or ViewBag
                ViewBag.TotalEmployees = totalEmployees;
                ViewBag.TotalBuses = totalBuses;
                ViewBag.TotalBusCategory = totalBusCategory;
                ViewBag.TotalTickets = totalTickets;

                return View();
            }
            else
            {
                return RedirectToAction("login");
            }
    }



        public IActionResult Add_Buses()
        {
            SetActiveNavigation("Add_Buses");
            var categories = sc.Category.ToList();
            ViewBag.Category = categories;
            var em = HttpContext.Session.GetString("Employee");
            if (em != null)
            {
                ViewBag.login = em;
                return View();
            }
            else
            {
                return RedirectToAction("login");
            }
        }

        [HttpPost]
        public IActionResult Add_Buses(Upload_Bus bus)
        {
            if (bus == null || string.IsNullOrEmpty(bus.Bus_name) || bus.Bus_price == 0 || string.IsNullOrEmpty(bus.Air_Condition) || bus.category_Id == 0)
            {
                ModelState.AddModelError(string.Empty, "");
                return View(bus);
            }
            sc.uploads.Add(bus);
            sc.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("Add_Buses");
        }

        public IActionResult Bus_Category()
        {
            SetActiveNavigation("Bus_Category");
            var em = HttpContext.Session.GetString("Employee");
            if (em != null)
            {
                ViewBag.login = em;
                return View();
            }
            else
            {
                return RedirectToAction("login");
            }
        }

        [HttpPost]
        public IActionResult Bus_Category(Bus_Category category)
        {
            if (category == null || string.IsNullOrEmpty(category.Category_Name))
            {
                ModelState.AddModelError(string.Empty, "");
                return View(category);
            }
            sc.Add(category);
            sc.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("Bus_Category");
        }

        public IActionResult showBuses()
        {
            SetActiveNavigation("showBuses");
            var buses = sc.uploads.Include(x => x.Category).ToList();
            var em = HttpContext.Session.GetString("Employee");
            if (em != null)
            {
                ViewBag.login = em;
                return View(buses);
            }
            else
            {
                return RedirectToAction("login");
            }
        }

        public IActionResult Update_Bus(int id)
        {
            SetActiveNavigation("showBuses");
            var bus = sc.uploads.Include(x => x.Category).FirstOrDefault(x => x.Bus_id == id);

            if (bus == null)
            {
                return RedirectToAction("error404");
            }

            var categories = sc.Category.ToList();
            ViewBag.Category = categories;

            var em = HttpContext.Session.GetString("Employee");
            if (em != null)
            {
                ViewBag.login = em;
                return View(bus);
            }
            else
            {
                return RedirectToAction("login");
            }
        }

        [HttpPost]
        public IActionResult Update_Bus(Upload_Bus bus)
        {
            sc.Entry(bus).State = EntityState.Modified;
            sc.SaveChanges();
            return RedirectToAction("showBuses");
        }

        public IActionResult Delete_Bus(int id)
        {
            var bus = sc.uploads.Find(id);
            if (bus == null)
            {
                return RedirectToAction("error404");
            }

            sc.uploads.Remove(bus);
            sc.SaveChanges();
            return RedirectToAction("showBuses");
        }

        public IActionResult Add_Employee()
        {
            SetActiveNavigation("Add_Employee");
            var em = HttpContext.Session.GetString("Employee");
            if (em != null)
            {
                ViewBag.login = em;
                return View();
            }
            else
            {
                return RedirectToAction("login");
            }
        }

        [HttpPost]
        public IActionResult Add_Employee(Employees em)
        {
            if (em == null || string.IsNullOrEmpty(em.Username) || em.Age == 0 || string.IsNullOrEmpty(em.Email) || string.IsNullOrEmpty(em.Password) || string.IsNullOrEmpty(em.PhoneNumber) || string.IsNullOrEmpty(em.Address))
            {
                ModelState.AddModelError(string.Empty, "");
                return View(em);
            }

            sc.Add(em);
            sc.SaveChanges();
            ModelState.Clear();
            return View();
        }

        public IActionResult Our_Employees()
        {
            SetActiveNavigation("Our_Employees");
            var emp = sc.Employees.Where(em => em.Role != 1);
            var em = HttpContext.Session.GetString("Employee");
            if (em != null)
            {
                ViewBag.login = em;
                return View(emp);
            }
            else
            {
                return RedirectToAction("login");
            }
        }

        public IActionResult Delete_Employee(int id)
        {
            var employee = sc.Employees.Find(id);
            if (employee == null)
            {
                return RedirectToAction("error404");
            }

            sc.Employees.Remove(employee);
            sc.SaveChanges();
            return RedirectToAction("Our_Employees");
        }

        public IActionResult Update_Emp(int id)
        {
            var employee = sc.Employees.Find(id);
            if (employee == null)
            {
                return RedirectToAction("error404");
            }
            else
            {
                SetActiveNavigation("Our_Employees");
                var em = HttpContext.Session.GetString("Employee");
                if (em != null)
                {
                    ViewBag.login = em;
                    return View(employee);
                }
                else
                {
                    return RedirectToAction("login");
                }
            }
        }

        [HttpPost]
        public IActionResult Update_Emp(Employees updatedEmployee)
        {
            var existingEmployee = sc.Employees.Find(updatedEmployee.EmployeeId);
            if (updatedEmployee == null || string.IsNullOrEmpty(updatedEmployee.Username) || updatedEmployee.Age == 0 || string.IsNullOrEmpty(updatedEmployee.PhoneNumber) || string.IsNullOrEmpty(updatedEmployee.Address))
            {
                ModelState.AddModelError(string.Empty, "");
                return View(updatedEmployee);
            }

            existingEmployee.Username = updatedEmployee.Username;
            existingEmployee.Email = updatedEmployee.Email;
            existingEmployee.PhoneNumber = updatedEmployee.PhoneNumber;
            existingEmployee.Address = updatedEmployee.Address;
            existingEmployee.Age = updatedEmployee.Age;
            existingEmployee.Qualification = updatedEmployee.Qualification;

            sc.SaveChanges();

            return RedirectToAction("Our_Employees");
        }

        public IActionResult login()
        {
            SetActiveNavigation(""); // No active navigation link
            return View();
        }

        [HttpPost]
        public IActionResult login(Employees em)
        {
            var employee = sc.Employees.Where(a => a.Email == em.Email && a.Password == em.Password).FirstOrDefault();
            if (employee != null)
            {
                var name = employee.Username;
                HttpContext.Session.SetString("Employee", name);
                var Employeelogin = HttpContext.Session.GetString("Employee");
                ViewBag.login = Employeelogin;
                if (employee.Role == 1)
                {
                    var totalEmployees = sc.Employees.Count();
                    var totalBuses = sc.uploads.Count();
                    var totalBusCategory = sc.Category.Count();
                    var totalTickets = sc.Ticket.Count();

                    ViewBag.TotalEmployees = totalEmployees;
                    ViewBag.TotalBuses = totalBuses;
                    ViewBag.TotalBusCategory = totalBusCategory;
                    ViewBag.TotalTickets = totalTickets;
                    return View("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Ticket_Reservation");
                }
            }
            else
            {
                ViewBag.err = "Credentials did not match"; 
                return View("login");
            }
        }

        public IActionResult logout()
        {
            HttpContext.Session.Clear(); // Clear all session data
            return RedirectToAction("login");
        }


        public IActionResult Booking_Details()
        {
            var ticket = sc.Ticket.ToList();
            var em = HttpContext.Session.GetString("Employee");
            if (em != null)
            {
                SetActiveNavigation("Booking_Details");
                ViewBag.login = em;
                return View(ticket);
            }
            else
            {
                return RedirectToAction("login", "Admin");
            }
        }

        public IActionResult Cancel_Ticket(int id)
        {
            var ticket = sc.Ticket.Find(id);
            if (ticket == null)
            {
                return RedirectToAction("error404");
            }

            sc.Ticket.Remove(ticket);
            sc.SaveChanges();
            return RedirectToAction("Booking_Details");
        }

        // Helper method to set active navigation link
        private void SetActiveNavigation(string action)
        {
            ViewBag.ActiveAction = action;
        }
    }
}
