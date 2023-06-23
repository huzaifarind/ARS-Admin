using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArsAdmin.Models
{
    public class ADONetMethods:SP_Const
    {
        public DataTable CRUD_RegisterPassenger(int RegisterPassengerId, string FirstName, string LastName, string DateOfBirth, string MobileNo, string EmailAddress, string Password, string Gender, string Address)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("CRUD_RegisterPassenger"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RegisterPassengerId", SqlDbType.Int).Value = RegisterPassengerId;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName;
                    cmd.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = DateOfBirth;
                    cmd.Parameters.Add("@MobileNo", SqlDbType.Int).Value = MobileNo;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = EmailAddress;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = Password;
                    cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = Gender;
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = Address;
                    OpenConnection(true);
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw new Exception(string.Format("Error occured during CRUD_RegisterPassenger : {0}", ex.Message), ex);
            }
            return dt;
        }

        public DataTable CRUD_Planes(string OperationType= "Select",int? PlaneId=null,string PlaneName=null,int ? AirlineId=null,int ? PlaneTypeId=null,string PlaneRegistrationNo=null,string CurrentStatus=null,int? LoginUserId=null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("CRUD_Planes"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.VarChar).Value = OperationType;
                    cmd.Parameters.Add("@PlaneId", SqlDbType.Int).Value = PlaneId;
                    cmd.Parameters.Add("@PlaneName", SqlDbType.VarChar).Value = PlaneName;
                    cmd.Parameters.Add("@AirlineId", SqlDbType.Int).Value = AirlineId;
                    cmd.Parameters.Add("@PlaneTypeId", SqlDbType.Int).Value = PlaneTypeId;
                    cmd.Parameters.Add("@PlaneRegistrationNo", SqlDbType.VarChar).Value = PlaneRegistrationNo;
                    cmd.Parameters.Add("@CurrentStatus", SqlDbType.VarChar).Value = CurrentStatus;
                    cmd.Parameters.Add("@LoginUserId", SqlDbType.Int).Value = LoginUserId;
                    OpenConnection(true);
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw new Exception(string.Format("Error occured during CRUD_Planes : {0}", ex.Message), ex);
            }
            return dt;
        }



        public DataTable CRUD_FlightSchedule(string OperationType = "Select", int? FlightScheduleId = null, int? PlaneId = null, 
            int? ArrivalAirportId = null, int? DepartureAirportId = null, DateTime?
            DateTiming = null, String IsActive = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("CRUD_Planes"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.VarChar).Value = OperationType;
                    cmd.Parameters.Add("@PlaneId", SqlDbType.Int).Value = PlaneId;
                    cmd.Parameters.Add("@PlaneName", SqlDbType.VarChar).Value = PlaneId;
                    cmd.Parameters.Add("@ArrivalAirportId", SqlDbType.Int).Value = ArrivalAirportId;
                    cmd.Parameters.Add("@DepartureAirportId", SqlDbType.Int).Value = DepartureAirportId;
                    cmd.Parameters.Add("@DateTime", SqlDbType.VarChar).Value = DateTiming;
                    cmd.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = IsActive;

                    OpenConnection(true);
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw new Exception(string.Format("Error occured during CRUD_Planes : {0}", ex.Message), ex);
            }
            return dt;
        }

    }
}