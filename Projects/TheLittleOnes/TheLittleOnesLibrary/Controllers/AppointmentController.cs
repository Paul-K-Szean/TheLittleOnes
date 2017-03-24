using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using TheLittleOnesLibrary.DataAccessObject;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.EnumFolder;
namespace TheLittleOnesLibrary.Controllers
{
    public class AppointmentController
    {
        private static AppointmentController AppointmentCtrl;
        public static AppointmentController getInstance()
        {
            if (AppointmentCtrl == null)
                AppointmentCtrl = new AppointmentController();
            return AppointmentCtrl;
        }
        private static List<AppointmentEntity> appointmentEntities;
        // Data Access Object
        private DAO dao;
        private OleDbCommand oleDbCommand;
        private DataSet dataSet;
        // Default Constructor
        public AppointmentController()
        {
            dao = DAO.getInstance();
        }
        // Automatically checks if adopt request date is over
        public void checkAppointmentStatus()
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            List<AppointmentEntity> adoptRequestEntities = new List<AppointmentEntity>();
            DateTime currentDateTime = DateTime.Now;
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM APPOINTMENT");
                dataSet = dao.getRecord(oleDbCommand);
                foreach (AppointmentEntity appointmentEntity in adoptRequestEntities)
                {
                    if (appointmentEntity.AppmtDateTime < currentDateTime)
                    {
                        LogController.LogLine(Enums.GetDescription(SystemStatus.Completed));
                    }
                }
            }
        }
        // Check if Appointment exists
        public bool checkAppointmentExist(string appmtFromID, string appmtToID)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT APPMTID FROM APPOINTMENT WHERE APPMTFROMID = @APPMTFROMID AND APPMTTOID = @APPMTTOID AND APPMTSTATUS='Confirmed'");
                oleDbCommand.Parameters.AddWithValue("@APPMTFROMID", string.Concat(appmtFromID));
                oleDbCommand.Parameters.AddWithValue("@APPMTTOID", string.Concat(appmtToID));
                if (string.IsNullOrEmpty(dao.getValue(oleDbCommand)))
                {
                    // empty = no record exists
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        // Create Appointment
        public AppointmentEntity createAppointment(AppointmentEntity appointmentEntity)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("INSERT INTO APPOINTMENT(APPMTFROMID,APPMTTOID,APPMTDATETIME,APPMTDATECREATED, APPMTSTATUS, APPMTTYPE) ",
                                                         "              VALUES (@APPMTFROMID,@APPMTTOID,@APPMTDATETIME,NOW(),@APPMTSTATUS,@APPMTTYPE);");
                oleDbCommand.Parameters.AddWithValue("@APPMTID", appointmentEntity.AccountEntity.AccountID);
                oleDbCommand.Parameters.AddWithValue("@APPMTTOID", appointmentEntity.AppmtToID);
                oleDbCommand.Parameters.AddWithValue("@APPMTDATETIME", appointmentEntity.AppmtDateTime);
                oleDbCommand.Parameters.AddWithValue("@APPMTSTATUS", Enums.GetDescription(SystemStatus.Confirmed));
                oleDbCommand.Parameters.AddWithValue("@APPMTTYPE", appointmentEntity.AppmtType);
                int insertID = dao.createRecord(oleDbCommand);
                if (insertID > 0)
                {
                    appointmentEntity.AppmtID = insertID.ToString();
                    return appointmentEntity;
                }
                else
                {
                    return null;
                }
            }
        }
        // Retrieve Total Appointment 
        public List<AppointmentEntity> getAllAppointmentEntities(string appmtToID)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            DataTable dTableAllUserAppointments;
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM APPOINTMENT WHERE APPMTTOID = @APPMTTOID");
                oleDbCommand.Parameters.AddWithValue("@APPMTTOID", string.Concat(appmtToID));
                dataSet = dao.getRecord(oleDbCommand);
                appointmentEntities = new List<AppointmentEntity>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    AppointmentEntity adoptReqEntity = new AppointmentEntity(
                        row[0].ToString(),
                        row[1] as AccountEntity,
                        row[2].ToString(),
                        DateTime.Parse(row[3].ToString()),
                        DateTime.Parse(row[4].ToString()),
                        row[5].ToString(),
                        row[6].ToString());
                    appointmentEntities.Add(adoptReqEntity);
                }
                return appointmentEntities;
            }
        }
        // Retrieve user Appointment 
        public AppointmentEntity getUserAppointmentEntity(string appmtFromID, string appmtToID)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM APPOINTMENT WHERE APPMTFROMID = @APPMTFROMID AND APPMTTOID = @APPMTTOID");
                oleDbCommand.Parameters.AddWithValue("@APPMTFROMID", string.Concat(appmtFromID));
                oleDbCommand.Parameters.AddWithValue("@APPMTTOID", string.Concat(appmtToID));
                dataSet = dao.getRecord(oleDbCommand);
                if (dataSet != null)
                {
                    return new AppointmentEntity(
                       dataSet.Tables[0].Rows[0]["APPMTID"].ToString(),
                       AccountController.getInstance().getAccount(dataSet.Tables[0].Rows[0]["APPMTFROMID"].ToString()),
                       dataSet.Tables[0].Rows[0]["APPMTTOID"].ToString(),
                       DateTime.Parse(dataSet.Tables[0].Rows[0]["APPMTDATETIME"].ToString()),
                       DateTime.Parse(dataSet.Tables[0].Rows[0]["APPMTDATECREATED"].ToString()),
                       dataSet.Tables[0].Rows[0]["APPMTSTATUS"].ToString(),
                       dataSet.Tables[0].Rows[0]["APPMTTYPE"].ToString()
                       );
                }
                else return null;
            }
        }
        // Retrieve user Appointment 
        public AppointmentEntity getUserAppointmentEntity(string appmtID)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM APPOINTMENT WHERE APPMTID = @APPMTID");
                oleDbCommand.Parameters.AddWithValue("@APPMTID", string.Concat(appmtID));
                dataSet = dao.getRecord(oleDbCommand);
                if (dataSet != null)
                {
                    return new AppointmentEntity(
                       dataSet.Tables[0].Rows[0]["APPMTID"].ToString(),
                       AccountController.getInstance().getAccount(dataSet.Tables[0].Rows[0]["APPMTFROMID"].ToString()),
                       dataSet.Tables[0].Rows[0]["APPMTTOID"].ToString(),
                       DateTime.Parse(dataSet.Tables[0].Rows[0]["APPMTDATETIME"].ToString()),
                       DateTime.Parse(dataSet.Tables[0].Rows[0]["APPMTDATECREATED"].ToString()),
                       dataSet.Tables[0].Rows[0]["APPMTSTATUS"].ToString(),
                       dataSet.Tables[0].Rows[0]["APPMTTYPE"].ToString()
                       );
                }
                else return null;
            }
        }
        // Retrieve all user Appointment 
        public List<AppointmentEntity> getUserAppointmentEntities(string appmtFromID)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM APPOINTMENT WHERE APPMTFROMID = @APPMTFROMID");
                oleDbCommand.Parameters.AddWithValue("@APPMTFROMID", string.Concat(appmtFromID));
                dataSet = dao.getRecord(oleDbCommand);
                appointmentEntities = new List<AppointmentEntity>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    AppointmentEntity adoptReqEntity = new AppointmentEntity(
                       dataSet.Tables[0].Rows[0]["APPMTID"].ToString(),
                       AccountController.getInstance().getAccount(dataSet.Tables[0].Rows[0]["APPMTFROMID"].ToString()),
                       dataSet.Tables[0].Rows[0]["APPMTTOID"].ToString(),
                       DateTime.Parse(dataSet.Tables[0].Rows[0]["APPMTDATETIME"].ToString()),
                       DateTime.Parse(dataSet.Tables[0].Rows[0]["APPMTDATECREATED"].ToString()),
                       dataSet.Tables[0].Rows[0]["APPMTSTATUS"].ToString(),
                       dataSet.Tables[0].Rows[0]["APPMTTYPE"].ToString()
                        );
                    appointmentEntities.Add(adoptReqEntity);
                }
                return appointmentEntities;
            }
        }
        // Update Appointment
        public AppointmentEntity updateAppointment(AppointmentEntity appointmentEntity)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("UPDATE APPOINTMENT SET ",
                                                        "APPMTDATETIME = @APPMTDATETIME, APPMTSTATUS = @APPMTSTATUS",
                                                        " WHERE(APPMTID = @APPMTID)");
                oleDbCommand.Parameters.AddWithValue("@APPMTDATETIME", appointmentEntity.AppmtDateTime);
                oleDbCommand.Parameters.AddWithValue("@APPMTSTATUS", appointmentEntity.AppmtStatus);
                oleDbCommand.Parameters.AddWithValue("@APPMTID", appointmentEntity.AppmtID);
                int insertID = dao.updateRecord(oleDbCommand);
                if (insertID > 0)
                {
                    return appointmentEntity;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}