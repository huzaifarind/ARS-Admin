using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using ArsAdmin.Models;

namespace ArsAdmin.Controllers
{
    public class FlightScheduleController : Controller
    {
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        private SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());
            connection.Open();
            return connection;
        }

        private void CloseConnection(SqlConnection connection)
        {
            connection.Close();
        }

        private List<FlightSchedule> GetFlightSchedules()
        {
            List<FlightSchedule> schedules = new List<FlightSchedule>();

            string query = "SELECT fs.FlightScheduleId AS RefNo, p.PlaneName, p.PlaneId, fs.ArrivalAirportId, fs.DepartureAirportId, fs.DateTiming, " +
                           "a.AirportName AS ArrivalAirportName, ad.AirportName AS DepartureAirportName FROM FlightSchedule fs " +
                           "INNER JOIN Planes p ON fs.PlaneId = p.PlaneId INNER JOIN Airport a ON fs.ArrivalAirportId = a.AirportId " +
                           "INNER JOIN Airport ad ON fs.DepartureAirportId = ad.AirportId";
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FlightSchedule schedule = new FlightSchedule
                            {
                                FlightScheduleId = Convert.ToInt32(reader["RefNo"]),
                                PlaneName = reader["PlaneName"].ToString(),
                                PlaneId = Convert.ToInt32(reader["PlaneId"]),
                                ArrivalAirportId = Convert.ToInt32(reader["ArrivalAirportId"]),
                                ArrivalAirportName = reader["ArrivalAirportName"].ToString(),
                                DepartureAirportId = Convert.ToInt32(reader["DepartureAirportId"]),
                                DepartureAirportName = reader["DepartureAirportName"].ToString(),
                                DateTiming = Convert.ToDateTime(reader["DateTiming"])
                            };

                            schedules.Add(schedule);
                        }
                    }
                }
            }

            return schedules;
        }

        public ActionResult Index()
        {
            List<FlightSchedule> flightSchedules = GetFlightSchedules();

            return View(flightSchedules);
        }

        public ActionResult Create()
        {
            return View(new FlightSchedule());
        }

        // POST: RegisterPassenger/Create
        [HttpPost]
        public ActionResult Create(FlightSchedule schedule)
        {
            try
            {
                // Insert data into the database
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                string query = "INSERT INTO FlightSchedule(PlaneId, ArrivalAirportId, DepartureAirportId, DateTiming)" +
"VALUES((SELECT PlaneId FROM Planes WHERE PlaneName = @PlaneName AND PlaneRegistrationNo = @PlaneRegistrationNo), @ArrivalAirportId, @DepartureAirportId, @DateTiming)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", schedule.PlaneId);
                        command.Parameters.AddWithValue("@FirstName", schedule.PlaneName);
                        command.Parameters.AddWithValue("@LastName", schedule.ArrivalAirportId);
                        command.Parameters.AddWithValue("@LastName", schedule.ArrivalAirportName);
                        command.Parameters.AddWithValue("@DateOfBirth", schedule.DepartureAirportId);
                        command.Parameters.AddWithValue("@DateOfBirth", schedule.DepartureAirportName);
                        command.Parameters.AddWithValue("@MobileNo", schedule.DateTiming);

                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the database operation
                // You can add appropriate error handling or logging here
                return View(schedule);
            }
        }






    }
}
