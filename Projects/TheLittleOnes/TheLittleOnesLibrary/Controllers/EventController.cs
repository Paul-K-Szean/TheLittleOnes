using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TheLittleOnesLibrary.DataAccessObject;
using TheLittleOnesLibrary.Entities;

namespace TheLittleOnesLibrary.Controllers
{
    public class EventController
    {
        private static EventController eventCtrl;
        private static List<EventEntity> eventEntities;
        public static EventController getInstance()
        {
            if (eventCtrl == null)
                eventCtrl = new EventController();
            return eventCtrl;
        }
        // Data Access Object
        private DAO dao;
        private OleDbCommand oleDbCommand;
        private DataSet dataSet;
        // Default Constructor
        public EventController()
        {
            dao = DAO.getInstance();
        }
        // Create event entity
        public EventEntity createEvent(EventEntity accountEntity)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                if (accountEntity != null)
                {
                    oleDbCommand.CommandText = string.Concat("INSERT INTO EENT ( ACCOUNTID, EVENTTITLE, EVENTDESC, EVENTTYPE, EVENTDATETIME, EVENTCREATEDDATE, EVENTSTATUS)",
                                                         "VALUES (@ACCOUNTID, @EVENTTITLE, @EVENTDESC, @EVENTTYPE, @EVENTDATETIME, NOW(), @EVENTSTATUS);");
                    oleDbCommand.Parameters.AddWithValue("@ACCOUNTID", accountEntity.AccountEntity);
                    oleDbCommand.Parameters.AddWithValue("@EVENTTITLE", accountEntity.EventTitle);
                    oleDbCommand.Parameters.AddWithValue("@EVENTDESC", accountEntity.EventDesc);
                    oleDbCommand.Parameters.AddWithValue("@EVENTTYPE", accountEntity.EventType);
                    oleDbCommand.Parameters.AddWithValue("@EVENTDATETIME", accountEntity.EventDateTime);
                    oleDbCommand.Parameters.AddWithValue("@EVENTSTATUS", accountEntity.EventStatus);
                    int insertID = dao.createRecord(oleDbCommand);
                    if (insertID > 0)
                    {
                        accountEntity.EventID = insertID.ToString();
                        return accountEntity;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                    return null;
            }
        }
        // Update event entity
        public EventEntity updateEvent(EventEntity accountEntity)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                if (accountEntity != null)
                {
                    oleDbCommand.CommandText = string.Concat("UPDATE EVENT SET ",
                    "EVENTTITLE = @EVENTTITLE, EVENTDESC = @EVENTDESC,  EVENTTYPE = @EVENTTYPE, EVENTDATETIME = @EVENTDATETIME, EVENTSTATUS = @EVENTSTATUS  WHERE EVENTID = @EVENTID");
                    oleDbCommand.Parameters.AddWithValue("@EVENTTITLE", accountEntity.EventTitle);
                    oleDbCommand.Parameters.AddWithValue("@EVENTDESC", accountEntity.EventDesc);
                    oleDbCommand.Parameters.AddWithValue("@EVENTTYPE", accountEntity.EventType);
                    oleDbCommand.Parameters.AddWithValue("@EVENTDATETIME", accountEntity.EventDateTime);
                    oleDbCommand.Parameters.AddWithValue("@EVENTSTATUS", accountEntity.EventStatus);
                    oleDbCommand.Parameters.AddWithValue("@EVENTID", accountEntity.EventID);
                    int insertID = dao.updateRecord(oleDbCommand);
                    if (insertID > 0)
                    {
                        // return edited accountEntity
                        return accountEntity;
                    }
                    else
                        // return unedited accountEntity
                        return null;
                }
                else
                    return null;
            }
        }
        // Retrieve event entity
        public EventEntity getEventEntity(string eventID)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM EVENT WHERE EVENTID = @EVENTID");
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTID", eventID);
                dataSet = dao.getRecord(oleDbCommand);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    EventEntity accountEntity = new EventEntity(
                        dataSet.Tables[0].Rows[0]["eventID"].ToString(),
                      AccountController.getInstance().getAccount(dataSet.Tables[0].Rows[0]["accountID"].ToString()),
                        dataSet.Tables[0].Rows[0]["eventTitle"].ToString(),
                        dataSet.Tables[0].Rows[0]["eventDesc"].ToString(),
                        dataSet.Tables[0].Rows[0]["eventLocation"].ToString(),
                            dataSet.Tables[0].Rows[0]["eventType"].ToString(),
                        DateTime.Parse(dataSet.Tables[0].Rows[0]["eventDateTime"].ToString()),
                        DateTime.Parse(dataSet.Tables[0].Rows[0]["eventCreatedDate"].ToString()),
                      dataSet.Tables[0].Rows[0]["eventStatus"].ToString());
                    return accountEntity;
                }
                else
                {
                    // cannot login, return null object
                    return null;
                }
            }
        }
        // Retrieve event entities
        public List<EventEntity> getEventEntities()
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM EVENT");
                dataSet = dao.getRecord(oleDbCommand);
                List<EventEntity> eventEntities = new List<EventEntity>();
                if (dataSet != null)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        eventEntities.Add(new EventEntity(
                        row["eventID"].ToString(),
                        AccountController.getInstance().getAccount(row["accountID"].ToString()),
                        row["eventTitle"].ToString(),
                        row["eventDesc"].ToString(),
                        row["eventLocation"].ToString(),
                        row["eventType"].ToString(),
                        DateTime.Parse(row["eventDateTime"].ToString()),
                        DateTime.Parse(row["eventCreatedDate"].ToString()),
                        row["eventStatus"].ToString()));
                    }
                    return eventEntities;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
