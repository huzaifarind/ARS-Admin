using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SP_Const
/// </summary>
public class SP_Fl : IDisposable
{
    SqlConnection sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    SqlTransaction sqlTransaction = null;
    SqlCommand sqlCommand = null;
    bool IsSqlTransactionUse = false;
    int Sql_CommandTimeout = 36000;
    public string SQL_Insert(string query)
    {
        sqlCommand = new SqlCommand(query, sqlConnection);

        try
        {
            OpenConnection(false);

            int value = sqlCommand.ExecuteNonQuery();

            if (value >= 1)
            {
                return "";
            }
            else
            { return "Error:Contact Administrator."; }


        }
        catch (Exception exp)
        {
            return exp.Message;
        }
        finally
        {
            CloseConnection();
        }
    }

    public void OpenConnection(bool SqlTransactionUse)
    {
        if (sqlConnection == null)
        {
            sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            sqlConnection.Open();
            IsSqlTransactionUse = SqlTransactionUse;
            if (IsSqlTransactionUse)
            {
                sqlTransaction = sqlConnection.BeginTransaction();
            }
        }
    }
    public void CloseConnection()
    {
        if (sqlConnection != null)
        {
            if (IsSqlTransactionUse)
            {
                if (sqlTransaction != null)
                {
                    try
                    {
                        sqlTransaction.Commit();
                    }
                    catch (Exception)
                    {
                    }
                    sqlTransaction = null;
                }
            }
            sqlConnection.Close();
            sqlConnection.Dispose();
            sqlConnection = null;
        }
    }
    void IDisposable.Dispose()
    {
        CloseConnection();
        GC.SuppressFinalize(this);
    }
    public DataTable GetData(SqlCommand pObjCommand, SqlParameter[] sqlp = null)
    {
        try
        {
            if (IsSqlTransactionUse) pObjCommand.Transaction = sqlTransaction;
            pObjCommand.Connection = sqlConnection;
            pObjCommand.CommandTimeout = Sql_CommandTimeout;
            if (sqlp != null)
            {
                for (int i = 0; i < sqlp.Length; i++)
                {
                    sqlCommand.Parameters.Add(sqlp[i]);
                }
            }
            DataTable dataTable = new DataTable();
            SqlDataAdapter objAdapter = new SqlDataAdapter(pObjCommand);
            objAdapter.Fill(dataTable);
            objAdapter.Dispose();
            CloseConnection();
            return dataTable;
        }
        catch (SqlException exception)
        {
            throw exception;
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }
    public DataSet GetDataSet(SqlCommand pObjCommand, SqlParameter[] sqlp = null)
    {
        try
        {
            if (IsSqlTransactionUse) pObjCommand.Transaction = sqlTransaction;
            pObjCommand.Connection = sqlConnection;
            pObjCommand.CommandTimeout = Sql_CommandTimeout;
            if (sqlp != null)
            {
                for (int i = 0; i < sqlp.Length; i++)
                {
                    sqlCommand.Parameters.Add(sqlp[i]);
                }
            }
            DataSet ds = new DataSet();
            SqlDataAdapter objAdapter = new SqlDataAdapter(pObjCommand);
            objAdapter.Fill(ds);
            objAdapter.Dispose();
            CloseConnection();
            return ds;
        }
        catch (SqlException exception)
        {
            throw exception;
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    public void Rollback()
    {
        if (sqlConnection != null)
        {
            if (IsSqlTransactionUse)
            {
                if (sqlTransaction != null)
                {
                    try
                    {
                        sqlTransaction.Rollback();
                    }
                    catch (Exception)
                    {
                    }
                    sqlTransaction = null;
                }
            }
            sqlConnection.Close();
            sqlConnection.Dispose();
            sqlConnection = null;
        }
    }
    public DataTable SQL_Select_DataTable(string storedprocedure, SqlParameter[] sqlp = null)
    {
        sqlCommand = new SqlCommand(storedprocedure, sqlConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;
        if (sqlp != null)
        {
            for (int i = 0; i < sqlp.Length; i++)
            {
                sqlCommand.Parameters.Add(sqlp[i]);
            }
        }

        try
        {
            OpenConnection(false);
            using (DataTable dataTable = new DataTable())
            {
                SqlDataAdapter objAdapter = new SqlDataAdapter(sqlCommand);
                objAdapter.Fill(dataTable);
                return dataTable;
            }
        }
        catch (Exception exp)
        {
            return null;
        }
        finally
        {
            CloseConnection();
        }
    }
    public string SQL_Select_sp(string storedprocedure, SqlParameter[] sqlp)
    {
        SqlDataAdapter sqlda = new SqlDataAdapter();

        sqlCommand = new SqlCommand(storedprocedure, sqlConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;
        for (int i = 0; i < sqlp.Length; i++)
        {
            sqlCommand.Parameters.Add(sqlp[i]);
        }

        try
        {
            OpenConnection(false);

            object obj = sqlCommand.ExecuteScalar();

            return obj.ToString();

        }
        catch (Exception exp)
        {
            return "";
        }
        finally
        {
            CloseConnection();

        }



    }
    public int SQL_Update(string storedprocedure, SqlParameter[] sqlp)
    {
        SqlDataAdapter sqlda = new SqlDataAdapter();

        sqlCommand = new SqlCommand(storedprocedure, sqlConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;
        for (int i = 0; i < sqlp.Length; i++)
        {
            sqlCommand.Parameters.Add(sqlp[i]);
        }

        try
        {
            OpenConnection(false);

            int i = sqlCommand.ExecuteNonQuery();
            return 1;


        }
        catch (Exception exp)
        {
            return 0;
        }
        finally
        {
            CloseConnection();

        }



    }
    public void SQL_Delete_sp(string storedprocedure, SqlParameter[] sqlp)
    {
        SqlDataAdapter sqlda = new SqlDataAdapter();

        sqlCommand = new SqlCommand(storedprocedure, sqlConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;
        for (int i = 0; i < sqlp.Length; i++)
        {
            sqlCommand.Parameters.Add(sqlp[i]);
        }

        try
        {
            OpenConnection(false);

            sqlCommand.ExecuteNonQuery();


        }
        catch (Exception exp)
        {
            throw exp;
        }
        finally
        {
            CloseConnection();
        }
    }
    public DataTable Crud_FlightSchedule(int FlightScheduleId,int PlaneId,string PlaneRegistrationNo ,int ArrivalAirortId, int DepartureAirportId, DateTime DateTiming)
    {
        DataTable dt = new DataTable();
        try
        {
            using (SqlCommand cmd = new SqlCommand("Crud_FlightSchedule"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FlightScheduleId", SqlDbType.Int).Value = FlightScheduleId;
                cmd.Parameters.Add("@PlaneId", SqlDbType.VarChar).Value = PlaneId;
                cmd.Parameters.Add("@PlaneRegistrationNo", SqlDbType.VarChar).Value = PlaneRegistrationNo;
                cmd.Parameters.Add("@ArrivalAirortId", SqlDbType.Int).Value = ArrivalAirortId;
                cmd.Parameters.Add("@DepartureAirportId", SqlDbType.Int).Value = DepartureAirportId;
                cmd.Parameters.Add("@DateTiming", SqlDbType.Date).Value = DateTiming;
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
}