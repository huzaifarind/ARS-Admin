using ArsAdmin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArsAdmin.Controllers
{
    public class RegisterPassengerController : Controller
    {
        // GET: RegisterPassenger
        public ActionResult Index()
        {
            List<RegisterPassenger> passengers = new List<RegisterPassenger>();

            // Retrieve data from the database
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string query = "SELECT RegisterPassengerId,FirstName, LastName,DateOfBirth,MobileNo,EmailAddress,Password,Gender,Address FROM RegisterPassenger";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RegisterPassenger passenger = new RegisterPassenger();

                            passenger.RegisterPassengerId = Convert.ToInt32(reader["RegisterPassengerId"]);
                            passenger.FirstName = reader["FirstName"].ToString();
                            passenger.LastName = reader["LastName"].ToString();
                            passenger.DateOfBirth = (DateTime)reader["DateOfBirth"];
                            passenger.MobileNo = reader["MobileNo"].ToString();
                            passenger.EmailAddress = reader["EmailAddress"].ToString();
                            passenger.Password = reader["Password"].ToString();
                            passenger.Gender = reader["Gender"].ToString();
                            passenger.Address = reader["Address"].ToString();
                            passengers.Add(passenger);
                        }
                    }
                }

                connection.Close();
            }

            return View(passengers);
        }




        // GET: RegisterPassenger/Create
        // GET: RegisterPassenger/Create
        public ActionResult Create()
        {
            return View(new RegisterPassenger());
        }

        // POST: RegisterPassenger/Create
        [HttpPost]
        public ActionResult Create(RegisterPassenger passenger)
        {
            try
            {
                // Insert data into the database
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                string query = "INSERT INTO RegisterPassenger (FirstName, LastName, DateOfBirth, MobileNo, EmailAddress, Password, Gender, Address) " +
                               "VALUES (@FirstName, @LastName, @DateOfBirth, @MobileNo, @EmailAddress, @Password, @Gender, @Address)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", passenger.FirstName);
                        command.Parameters.AddWithValue("@LastName", passenger.LastName);
                        command.Parameters.AddWithValue("@DateOfBirth", passenger.DateOfBirth);
                        command.Parameters.AddWithValue("@MobileNo", passenger.MobileNo);
                        command.Parameters.AddWithValue("@EmailAddress", passenger.EmailAddress);
                        command.Parameters.AddWithValue("@Password", passenger.Password);
                        command.Parameters.AddWithValue("@Gender", passenger.Gender);
                        command.Parameters.AddWithValue("@Address", passenger.Address);

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
                return View(passenger);
            }
        }


        public ActionResult Edit()
        {

            return View(new RegisterPassenger());
        }

        [HttpPost]
        public ActionResult Edit (int id, RegisterPassenger updatedPassenger)
        {
            try
            {
                if (ModelState.IsValid)
                {            // Insert data into the database
                    string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    string query = "UPDATE RegisterPassenger SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, " +
                  "MobileNo = @MobileNo, EmailAddress = @EmailAddress, Password = @Password, Gender = @Gender, Address = @Address " +
                  "WHERE RegisterPassengerId = @RegisterPassengerId";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@RegisterPassengerId", id);
                            command.Parameters.AddWithValue("@FirstName", updatedPassenger.FirstName);
                            command.Parameters.AddWithValue("@LastName", updatedPassenger.LastName);
                            command.Parameters.AddWithValue("@DateOfBirth", updatedPassenger.DateOfBirth);
                            command.Parameters.AddWithValue("@MobileNo", updatedPassenger.MobileNo);
                            command.Parameters.AddWithValue("@EmailAddress", updatedPassenger.EmailAddress);
                            command.Parameters.AddWithValue("@Password", updatedPassenger.Password);
                            command.Parameters.AddWithValue("@Gender", updatedPassenger.Gender);
                            command.Parameters.AddWithValue("@Address", updatedPassenger.Address);

                            command.ExecuteNonQuery();
                        }

                        connection.Close();
                    }

                    return RedirectToAction("Index");       
                    } 
            }



            catch (Exception)
            {
             
            }
            return View(updatedPassenger);
        }


        // GET: RegisterPassenger/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                string query = "DELETE FROM RegisterPassenger WHERE RegisterPassengerId = @RegisterPassengerId";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RegisterPassengerId", id);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Deletion successful
                            return RedirectToAction("Index");
                        }
                        else
                        {
                          
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

            return View("Index");
        }




    }
}
