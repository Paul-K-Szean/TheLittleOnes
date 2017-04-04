using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary.DataAccessObject;
using TheLittleOnesLibrary.Entities;
namespace TheLittleOnesLibrary.Controllers
{
    public class EventInfoController
    {
        private static EventInfoController eventCtrl;
        public static EventInfoController getInstance()
        {
            if (eventCtrl == null)
                eventCtrl = new EventInfoController();
            return eventCtrl;
        }
        // Data Access Object
        private DAO dao;
        private OleDbCommand oleDbCommand;
        private DataSet dataSet;
        // Default Constructor
        public EventInfoController()
        {
            dao = DAO.getInstance();
        }
        // Create EVENTT entity
        public EventInfoEntity createEvent(EventInfoEntity eventEntity)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                if (eventEntity != null)
                {
                    oleDbCommand.CommandText = string.Concat("INSERT INTO EVENTT ( ACCOUNTID, EVENTTITLE, EVENTDESC, EVENTLOCATION, EVENTTYPE, EVENTDATETIME, EVENTDATECREATED, EVENTSTATUS) ",
                                                         "VALUES (@ACCOUNTID, @EVENTTITLE, @EVENTDESC, @EVENTLOCATION, @EVENTTYPE, @EVENTDATETIME, NOW(), @EVENTSTATUS);");
                    oleDbCommand.Parameters.AddWithValue("@ACCOUNTID", eventEntity.AccountEntity.AccountID);
                    oleDbCommand.Parameters.AddWithValue("@EVENTTITLE", eventEntity.EventTitle);
                    oleDbCommand.Parameters.AddWithValue("@EVENTDESC", eventEntity.EventDesc);
                    oleDbCommand.Parameters.AddWithValue("@EVENTLOCATION", eventEntity.EventLocation);
                    oleDbCommand.Parameters.AddWithValue("@EVENTTYPE", eventEntity.EventType);
                    oleDbCommand.Parameters.AddWithValue("@EVENTDATETIME", eventEntity.EventDateTime);
                    oleDbCommand.Parameters.AddWithValue("@EVENTSTATUS", eventEntity.EventStatus);
                    int insertID = dao.createRecord(oleDbCommand);
                    if (insertID > 0)
                    {
                        eventEntity.EventID = insertID.ToString();
                        return eventEntity;
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
        // Update EVENTT entity
        public EventInfoEntity updateEvent(EventInfoEntity eventEntity)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                if (eventEntity != null)
                {
                    oleDbCommand.CommandText = string.Concat("UPDATE EVENTT SET ",
                    "EVENTTITLE = @EVENTTITLE, EVENTDESC = @EVENTDESC, EVENTLOCATION = @EVENTLOCATION, EVENTTYPE = @EVENTTYPE, EVENTDATETIME = @EVENTDATETIME, EVENTSTATUS = @EVENTSTATUS  WHERE EVENTID = @EVENTID");
                    oleDbCommand.Parameters.AddWithValue("@EVENTTITLE", eventEntity.EventTitle);
                    oleDbCommand.Parameters.AddWithValue("@EVENTDESC", eventEntity.EventDesc);
                    oleDbCommand.Parameters.AddWithValue("@EVENTLOCATION", eventEntity.EventLocation);
                    oleDbCommand.Parameters.AddWithValue("@EVENTTYPE", eventEntity.EventType);
                    oleDbCommand.Parameters.AddWithValue("@EVENTDATETIME", eventEntity.EventDateTime);
                    oleDbCommand.Parameters.AddWithValue("@EVENTSTATUS", eventEntity.EventStatus);
                    oleDbCommand.Parameters.AddWithValue("@EVENTID", eventEntity.EventID);
                    int insertID = dao.updateRecord(oleDbCommand);
                    if (insertID > 0)
                    {
                        // return edited accountEntity
                        return eventEntity;
                    }
                    else
                        // return unedited accountEntity
                        return null;
                }
                else
                    return null;
            }
        }
        // Retrieve EVENTT entity
        public EventInfoEntity getEventInfoEntity(string eventID)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM EVENTT WHERE EVENTID = @EVENTID");
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTID", eventID);
                dataSet = dao.getRecord(oleDbCommand);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    EventInfoEntity accountEntity = new EventInfoEntity(
                        dataSet.Tables[0].Rows[0]["eventID"].ToString(),
                      AccountController.getInstance().getAccount(dataSet.Tables[0].Rows[0]["accountID"].ToString()),
                        dataSet.Tables[0].Rows[0]["eventTitle"].ToString(),
                        dataSet.Tables[0].Rows[0]["eventDesc"].ToString(),
                        dataSet.Tables[0].Rows[0]["eventLocation"].ToString(),
                            dataSet.Tables[0].Rows[0]["eventType"].ToString(),
                        DateTime.Parse(dataSet.Tables[0].Rows[0]["eventDateTime"].ToString()),
                        DateTime.Parse(dataSet.Tables[0].Rows[0]["eventDateCreated"].ToString()),
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
        // Retrieve EVENTT entities
        public List<EventInfoEntity> getEventEntities()
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM EVENT");
                dataSet = dao.getRecord(oleDbCommand);
                List<EventInfoEntity> eventEntities = new List<EventInfoEntity>();
                if (dataSet != null)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        eventEntities.Add(new EventInfoEntity(
                        row["eventID"].ToString(),
                        AccountController.getInstance().getAccount(row["accountID"].ToString()),
                        row["eventTitle"].ToString(),
                        row["eventDesc"].ToString(),
                        row["eventLocation"].ToString(),
                        row["eventType"].ToString(),
                        DateTime.Parse(row["eventDateTime"].ToString()),
                        DateTime.Parse(row["eventDateCreated"].ToString()),
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
        // Filter Pet Info Data
        public DataTable filterEventInfoData(string ddltype, string tbSearchValue, Label searchResult)
        {
            searchResult.ForeColor = Utility.getColorWhite();
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                string sqlQuery = string.Concat("SELECT * FROM EVENTT WHERE (EVENTTITLE LIKE @SEARCHVALUE OR ",
                                            "EVENTLOCATION LIKE @SEARCHVALUE OR ",
                                            "EVENTSTATUS LIKE @SEARCHVALUE OR ",
                                            "EVENTTYPE LIKE @SEARCHVALUE OR ",
                                            "EVENTDESC LIKE @SEARCHVALUE) ");
                searchResult.Text = "Records for Event Info ";
                if (!string.IsNullOrEmpty(tbSearchValue))
                    searchResult.Text += string.Concat("\"", tbSearchValue, "\" ");
                if (!string.IsNullOrEmpty(ddltype))
                {
                    searchResult.Text += string.Concat("\"", ddltype, "\" ");
                    sqlQuery += string.Concat(" AND (EVENTTYPE LIKE '%", ddltype, "%') ");
                }
                oleDbCommand.CommandText = sqlQuery;
                oleDbCommand.Parameters.AddWithValue("@SEARCHVALUE", string.Concat("%", tbSearchValue, "%"));
                dataSet = dao.getRecord(oleDbCommand);
                return dataSet.Tables[0];
            }
        }
    }
}
