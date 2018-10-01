using System;
using System.Data;
using System.Data.OleDb;

public class AccessHelper
{
    protected static OleDbCommand comm = new OleDbCommand();
    protected static OleDbConnection conn = new OleDbConnection();
    protected static string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=data/data.mdb;Persist Security Info=False;Jet OLEDB:Database Password=sa;";

    private static void closeConnection()
    {
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
            conn.Dispose();
            comm.Dispose();
        }
    }

    public static OleDbDataReader DataReader(string sqlstr)
    {
        OleDbDataReader reader = null;
        try
        {
            openConnection();
            comm.CommandText = sqlstr;
            comm.CommandType = CommandType.Text;
            reader = comm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch
        {
            try
            {
                reader.Close();
            }
            catch
            {
            }
        }
        return reader;
    }

    public static void DataReader(string sqlstr, ref OleDbDataReader dr)
    {
        try
        {
            openConnection();
            comm.CommandText = sqlstr;
            comm.CommandType = CommandType.Text;
            dr = comm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch
        {
            try
            {
                try
                {
                    if (!((dr == null) || dr.IsClosed))
                    {
                        dr.Close();
                    }
                }
                catch
                {
                }
            }
            finally
            {
            }
        }
    }

    public static System.Data.DataSet DataSet(string sqlstr)
    {
        System.Data.DataSet dataSet = new System.Data.DataSet();
        OleDbDataAdapter adapter = new OleDbDataAdapter();
        try
        {
            try
            {
                openConnection();
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                adapter.SelectCommand = comm;
                adapter.Fill(dataSet);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        finally
        {
        }
        return dataSet;
    }

    public static void DataSet(string sqlstr, ref System.Data.DataSet ds)
    {
        OleDbDataAdapter adapter = new OleDbDataAdapter();
        try
        {
            try
            {
                openConnection();
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                adapter.SelectCommand = comm;
                adapter.Fill(ds);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        finally
        {
        }
    }

    public static System.Data.DataTable DataTable(string sqlstr)
    {
        System.Data.DataTable dataTable = new System.Data.DataTable();
        OleDbDataAdapter adapter = new OleDbDataAdapter();
        try
        {
            try
            {
                openConnection();
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                adapter.SelectCommand = comm;
                adapter.Fill(dataTable);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        finally
        {
        }
        return dataTable;
    }

    public static System.Data.DataView DataView(string sqlstr)
    {
        OleDbDataAdapter adapter = new OleDbDataAdapter();
        System.Data.DataView defaultView = new System.Data.DataView();
        System.Data.DataSet dataSet = new System.Data.DataSet();
        try
        {
            try
            {
                openConnection();
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                adapter.SelectCommand = comm;
                adapter.Fill(dataSet);
                defaultView = dataSet.Tables[0].DefaultView;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        finally
        {
        }
        return defaultView;
    }

    public static int ExecuteSql(string sqlstr)
    {
        int num = 0;
        try
        {
            try
            {
                openConnection();
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                num = comm.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        finally
        {
        }
        return num;
    }

    private static void openConnection()
    {
        if (conn.State == ConnectionState.Closed)
        {
            conn.ConnectionString = connectionString;
            comm.Connection = conn;
            try
            {
                conn.Open();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}

