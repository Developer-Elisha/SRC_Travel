using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SRC_Travel_Agencies.Context;
using SRC_Travel_Agencies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace SRC_Travel_Agencies.Controllers
{
    public class Ticket_ReservationController : Controller
    {
        private readonly SqlContext sc;

        public Ticket_ReservationController(SqlContext context)
        {
            sc = context;
        }
        public IActionResult Index()
        {
            var buses = sc.uploads.Include(x => x.Category).ToList();

            var em = HttpContext.Session.GetString("Employee");
            if (em != null)
            {
                ViewBag.login = em;
                ViewBag.ActiveStep = "AvailableBuses";
                ViewBag.BusId = HttpContext.Session.GetInt32("BusId");
                return View(buses);
            }
            else
            {
                return RedirectToAction("login", "Admin");
            }
        }

        [HttpPost]
        public IActionResult Index(int id)
        {
            var reservation = new reserve1
            {
                BusId = id
            };

            sc.Reserves.Add(reservation);
            sc.SaveChanges();

            ViewBag.ActiveStep = "TravelDates";

            return RedirectToAction("Travel_Dates");
        }

        public IActionResult Travel_Dates()
        {
            var em = HttpContext.Session.GetString("Employee");
            if (em != null)
            {
                ViewBag.login = em;
                ViewBag.ActiveStep = "TravelDates";
                return View();
            }
            else
            {
                return RedirectToAction("login", "Admin");
            }
        }

        [HttpPost]
        public IActionResult Travel_Dates(reserve2 rs2)
        {
            if (rs2 == null || string.IsNullOrEmpty(rs2.From) || string.IsNullOrEmpty(rs2.TO) || string.IsNullOrEmpty(rs2.Distance) || string.IsNullOrEmpty(rs2.Estimated_Hours))
            {
                ModelState.AddModelError(string.Empty, "");
                return View(rs2);
            }
            sc.Reserves2.Add(rs2);
            sc.SaveChanges();
            ModelState.Clear();

            ViewBag.ActiveStep = "BookSeat";

            return RedirectToAction("Book_Seats");
        }

        public IActionResult Book_Seats()
        {
            var buses = sc.Reserves.ToList(); // BusID
            var dates = sc.Reserves2.ToList(); // Date
            var tickets = sc.Ticket.ToList(); // tickets.BusId == bus.BusId && tickets.Date == date.Date

            // Filter tickets based on conditions
            var filteredTickets = tickets.Where(t => buses.Any(b => b.BusId == t.BusId) && dates.Any(d => d.Date == t.Date)).ToList();

            var em = HttpContext.Session.GetString("Employee");
            if (em != null)
            {
                ViewBag.login = em;
                ViewBag.SeatNumbers = filteredTickets.Select(t => t.Seat_Number).ToList();
                ViewBag.ActiveStep = "BookSeat";
                return View();
            }
            else
            {
                return RedirectToAction("login", "Admin");
            }
        }


        [HttpPost]
        public IActionResult Book_Seats(reserve3 model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
           
            string selectedSeatNumbers = model.Seat_Number;
            
            int price = model.Ticket_Price;
            var reservation = new reserve3
            {
                Seat_Number = selectedSeatNumbers,
                Ticket_Price = price
            };
            if (ModelState.IsValid)
            {
                sc.Reserves3.Add(reservation);
                sc.SaveChanges();
                ViewBag.ActiveStep = "Customer";
                return RedirectToAction("Customer");
            }
            else
            {
                return View();
            }
            
        }

        public IActionResult Customer()
        {
            var em = HttpContext.Session.GetString("Employee");
            if (em != null)
            {
                ViewBag.login = em;
                ViewBag.ActiveStep = "Customer";
                return View();
            }
            else
            {
                return RedirectToAction("login", "Admin");
            }
        }

        [HttpPost]
        public IActionResult Customer(reserve4 rs4)
        {
            if (rs4 == null)
            {
                ModelState.AddModelError("", "Invalid bus data");
                return View();
            }
            sc.Reserves4.Add(rs4);
            sc.SaveChanges();
            ModelState.Clear();

            ViewBag.ActiveStep = "Appreciation";

            return RedirectToAction("Appreciation");
        }

        public IActionResult Appreciation()
        {
            var em = HttpContext.Session.GetString("Employee");
            if (em != null)
            {
                ViewBag.login = em;
                ViewBag.ActiveStep = "Appreciation";
                return View();
            }
            else
            {
                return RedirectToAction("login", "Admin");
            }
        }

        public IActionResult GenerateTicket()
        {
            var reserve1Data = sc.Reserves.FirstOrDefault();
            var reserve2Data = sc.Reserves2.FirstOrDefault();
            var reserve3Data = sc.Reserves3.FirstOrDefault();
            var reserve4Data = sc.Reserves4.FirstOrDefault();

            if (reserve1Data != null && reserve2Data != null && reserve3Data != null && reserve4Data != null)
            {
                var ticket = new Ticket
                {
                    BookingId = reserve1Data.BookingId,
                    BusId = reserve1Data.BusId,
                    From = reserve2Data.From,
                    To = reserve2Data.TO,
                    Date = reserve2Data.Date,
                    Distance = reserve2Data.Distance,
                    Estimated_Hours = reserve2Data.Estimated_Hours,
                    Seat_Number = reserve3Data.Seat_Number,
                    Ticket_Price = reserve3Data.Ticket_Price,
                    Name = reserve4Data.Name,
                    Email = reserve4Data.Email,
                    Phone = reserve4Data.phone
                };

                sc.Ticket.Add(ticket);
                sc.SaveChanges();

                sc.Reserves.Remove(reserve1Data);
                sc.Reserves2.Remove(reserve2Data);
                sc.Reserves3.Remove(reserve3Data);
                sc.Reserves4.Remove(reserve4Data);
                sc.SaveChanges();

                return RedirectToAction("Ticket");
            }
            return RedirectToAction("Appreciation");
        }

        public IActionResult Ticket()
        {
            var lastTicket = sc.Ticket.OrderByDescending(e => e.TicketId).FirstOrDefault();
            List<Ticket> ticketList = new List<Ticket>();
            if (lastTicket != null)
            {
                ticketList.Add(lastTicket);
            }

            var em = HttpContext.Session.GetString("Employee");
            if (em != null)
            {
                ViewBag.login = em;
                ViewBag.ActiveStep = "Ticket";
                return View(ticketList);
            }
            else
            {
                return RedirectToAction("login", "Admin");
            }
        }

        public IActionResult UpdatePass()
        {
            var em = HttpContext.Session.GetString("Employee");
            if (em != null)
            {
                ViewBag.login = em;
                return View();
            }
            else
            {
                return RedirectToAction("login", "Admin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePass(string password)
        {
            var em = HttpContext.Session.GetString("Employee");
            if (em != null)
            {
                var employee = await sc.Employees.FirstOrDefaultAsync(e => e.Username == em);
                if (employee != null)
                {
                    employee.Password = password;
                    await sc.SaveChangesAsync();
                    return RedirectToAction("Index"); // Redirect to appropriate action after password update
                }
            }
            return RedirectToAction("login", "Admin"); // Redirect to login if employee not found or not logged in
        }

        public IActionResult Login()
        {
            var em = HttpContext.Session.GetString("Employee");
            if (em != null)
            {
                ViewBag.login = em;
                return View();
            }
            else
            {
                return RedirectToAction("login", "Admin");
            }
        }

        public void CleanupOldTickets()
        {
            var oneWeekAgo = DateTime.Now.AddDays(-7);
            var oldTickets = sc.Ticket.Where(t => t.Date < oneWeekAgo).ToList();

            sc.Ticket.RemoveRange(oldTickets);
            sc.SaveChanges();
        }

        public IActionResult Booking_Details()
        {
            var ticket = sc.Ticket.ToList();
            var em = HttpContext.Session.GetString("Employee");
            if (em != null)
            {
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

        public IActionResult PassUpdate()
        {
            return View();
        }
    }
}
