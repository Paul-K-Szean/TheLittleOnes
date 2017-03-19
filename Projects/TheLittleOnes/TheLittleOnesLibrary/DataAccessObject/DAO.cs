using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Handler;
namespace TheLittleOnesLibrary.DataAccessObject
{
    public class DAO
    {
        private static DAO dao;
        public static DAO getInstance()
        {
            if (dao == null)
                dao = new DAO();
            return dao;
        }
        private string filePath_Database;//=  @"C:\Users\PaulKSzean\Documents\Visual Studio 2015\WebSites\TheLittleOnes\App_Data\TheLittleOnes.accdb";
        private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
        private int insertID;
        private OleDbConnection oleDbConn;
        private OleDbDataReader oleDbReader;
        private OleDbDataAdapter oleDbAdapter;
        private DataSet dataSet;
        public DAO()
        {
            filePath_Database = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/TheLittleOnes.accdb");
            connectionString += filePath_Database;
        }
        // CREATE
        public int createRecord(OleDbCommand oleDbCommand)
        {
            try
            {
                using (oleDbConn = new OleDbConnection(connectionString))
                {
                    oleDbCommand.Connection = oleDbConn;
                    oleDbConn.Open();
                    oleDbCommand.ExecuteNonQuery();
                    oleDbCommand.CommandText = "SELECT @@IDENTITY";
                    insertID = (int)oleDbCommand.ExecuteScalar();
                    if (insertID > 0)
                    {
                        LogController.LogLine("Record created!");
                        return insertID;
                    }
                    else
                    {
                        LogController.LogLine("Record not created!");
                        return 0;
                    }
                };
            }
            catch (Exception ex)
            {
                // Error occured
                LogController.LogLine(string.Concat("createRecord() : ", ex.Message));
                return 0;
            }
            finally
            {
                oleDbCommand.Parameters.Clear();
            }
        }
        // RETRIEVE SINGLE VALUE
        public string getValue(OleDbCommand oleDbCommand)
        {
            string value;
            try
            {
                using (oleDbConn = new OleDbConnection(connectionString))
                {
                    oleDbCommand.Connection = oleDbConn;
                    oleDbConn.Open();
                    using (oleDbReader = oleDbCommand.ExecuteReader())
                    {
                        while (oleDbReader.Read())
                        {
                            value = oleDbReader.GetValue(1).ToString();
                            LogController.LogLine("getValue() is " + value);
                            return value;
                        }
                    }
                };
                // return empty string if cannot find anything
                LogController.LogLine("No data found in getValue()");
                return string.Empty;
            }
            catch (Exception ex)
            {
                // Error occured
                LogController.LogLine(string.Concat("getValue() : ", ex.Message));
                return "Error";
            }
            finally
            {
                oleDbCommand.Parameters.Clear();
            }
        }
        // RETRIEVE MULTIPLE VALUE
        public DataSet getRecord(OleDbCommand oleDbCommand)
        {
            try
            {
                using (oleDbConn = new OleDbConnection(connectionString))
                {
                    oleDbCommand.Connection = oleDbConn;
                    oleDbConn.Open();
                    using (oleDbAdapter = new OleDbDataAdapter(oleDbCommand))
                    {
                        dataSet = new DataSet();
                        oleDbAdapter.Fill(dataSet);
                        return dataSet;
                    }
                }
            }
            catch (Exception ex)
            {
                // Error occured
                LogController.LogLine(string.Concat("getRecord() : ", ex.Message));
                return null;
            }
            finally
            {
                oleDbCommand.Parameters.Clear();
            }
        }
        // UPDATE
        public int updateRecord(OleDbCommand oleDbCommand)
        {
            try
            {
                using (oleDbConn = new OleDbConnection(connectionString))
                {
                    oleDbCommand.Connection = oleDbConn;
                    oleDbConn.Open();
                    insertID = oleDbCommand.ExecuteNonQuery();
                    oleDbCommand.Parameters.Clear();
                    if (insertID > 0)
                    {
                        LogController.LogLine("Record updated!");
                        return insertID;
                    }
                    else
                    {
                        LogController.LogLine("Record not updated! Please check through given parameter values");
                        return 0;
                    }
                };
            }
            catch (Exception ex)
            {
                // Error occured
                LogController.LogLine(string.Concat("updateRecord() : ", ex.Message));
                return 0;
            }
            finally
            {
                oleDbCommand.Parameters.Clear();
            }
        }
        // DELETE
        public void deleteRecord(OleDbCommand oleDbCommand)
        {
            try
            {
                using (oleDbConn = new OleDbConnection(connectionString))
                {
                    oleDbCommand.Connection = oleDbConn;
                    oleDbConn.Open();
                    insertID = oleDbCommand.ExecuteNonQuery();
                    oleDbCommand.Parameters.Clear();
                    if (insertID > 0)
                    {
                        LogController.LogLine("Record deleted!");
                    }
                    else
                    {
                        LogController.LogLine("Record not deleted! Please check through given parameter values");
                    }
                };
            }
            catch (Exception ex)
            {
                // Error occured
                LogController.LogLine(string.Concat("deleteRecord() : ", ex.Message));
            }
            finally
            {
                oleDbCommand.Parameters.Clear();
            }
        }
    }
}
