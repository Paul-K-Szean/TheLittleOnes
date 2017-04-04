using System;
using System.Data;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.EnumFolder;
using TheLittleOnesLibrary.Handler;
public partial class EventEdit : BasePageTLO
{
    private Label UICtrlLabel;
    private TextBox UICtrlTextbox;
    private CheckBox UICtrlCheckbox;
    private DropDownList UICtrlDropdownlist;
    private static DataTable dTableEvent;
    private static DateTime dateTimeSelected;
    private static EventInfoEntity TLOEventInfoEntityEdit;
    private static string daySelected;
    private static string dateSelected;
    private static string timeSelected;
    private static int GVRowID;
    private static int gvPageSize = 5; // default
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
        }
        else
        {
            if (TLOAccountEntity != null)
            {
                HDFAccountID.Value = TLOAccountEntity.AccountID;
            }
            if (DDLFilterEventType.Items.Count <= 1)
            {
                DDLFilterEventType.Items.Add(new ListItem(Enums.GetDescription(EventType.AdoptionDrive), Enums.GetDescription(EventType.AdoptionDrive)));
                DDLFilterEventType.Items.Add(new ListItem(Enums.GetDescription(EventType.BirthdayParty), Enums.GetDescription(EventType.BirthdayParty)));
                DDLFilterEventType.Items.Add(new ListItem(Enums.GetDescription(EventType.PetGathering), Enums.GetDescription(EventType.PetGathering)));
                DDLFilterEventType.Items.Add(new ListItem(Enums.GetDescription(EventType.PetLearning), Enums.GetDescription(EventType.PetLearning)));
            }
        }
    }
    #region Button Control
    protected void BTNUpdate_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        if (checkRequiredFields())
        {
            // create entity
            TLOEventInfoEntityEdit.EventID = HDFEventInfoID.Value.Trim();
            TLOEventInfoEntityEdit.EventTitle = TBEventInfoTitle.Text.Trim();
            TLOEventInfoEntityEdit.EventDesc = TBEventInfoDesc.Text.Trim();
            TLOEventInfoEntityEdit.EventLocation = TBEventInfoLocation.Text.Trim();
            TLOEventInfoEntityEdit.EventType = DDLEventType.SelectedValue.Trim();
            TLOEventInfoEntityEdit.EventDateTime = DateTime.Parse(string.Concat(INPUTEventDate.Value, " ", DDLEventInfoTime.SelectedValue));
            TLOEventInfoEntityEdit.EventStatus = CHKBXCancelEvent.Checked ? Enums.GetDescription(SystemStatus.Cancelled) : LBLEventInfoStatus.Text;
            // update into database
            TLOEventInfoEntityEdit = eventInfoCrtler.updateEvent(TLOEventInfoEntityEdit);
            // update photo
            if (TLOPhotoEntities != null)
            {
                // change photo path to database instead of using temp
                photoCtrler.changePhotoPathToDatabaseFolder(TLOPhotoEntities, TLOAccountEntity.AccountID);
                // remove old photos from database
                photoCtrler.deletePhoto(TLOAccountEntity.AccountID, PhotoPurpose.EventInfo.ToString());
                // create new photos into database
                photoCtrler.createPhoto(TLOPhotoEntities, TLOAccountEntity.AccountID);
            }
            if (TLOEventInfoEntityEdit != null)
            {
                MessageHandler.SuccessMessage(LBLErrorMsg, "Event information was updated successfully");
            }
            else
            {
                MessageHandler.ErrorMessage(LBLErrorMsg, "Event information was not updated successfully");
            }
            GVEvent.DataBind();
        }
    }
    protected void BTNClose_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        PNLEventInfoEdit.Visible = false;
    }
    protected void BTNEventDate_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
    }
    #endregion
    #region Checkbox Control
    protected void CHKBXCancelEvent_CheckedChanged(object sender, EventArgs e)
    {
        if (TLOEventInfoEntityEdit != null)
            TLOEventInfoEntityEdit.EventStatus = Enums.GetDescription(SystemStatus.Cancelled);
    }
    #endregion
    #region Dropdownlist Controls
    protected void DDLDisplayRecordCountEvent_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvPageSize = int.Parse(DDLDisplayRecordCountEvent.SelectedValue);
        GVEvent.PageSize = gvPageSize;
        filterEventInfo();
    }
    protected void DDLFilterEventType_SelectedIndexChanged(object sender, EventArgs e)
    {  
        filterEventInfo();
    }
    #endregion
    #region Gridview Command
    protected void GVEvent_DataBound(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        if (dTableEvent == null)
            dTableEvent = ((DataView)SDSEvent.Select(DataSourceSelectArguments.Empty)).Table;
        updateEntryCount(dTableEvent, GVEvent, LBLEntriesCount);
    }
    protected void GVEvent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // TODO EVENTS
        }
    }
    protected void GVEvent_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        // clearStaticData();
        MessageHandler.ClearMessage(LBLErrorMsg);
        GridViewRow row = GVEvent.Rows[e.NewSelectedIndex];
        GVRowID = Convert.ToInt32(GVEvent.DataKeys[row.RowIndex].Values[0]);
        loadEvent(GVRowID.ToString());
    }
    protected void GVEvent_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        highlightSelectedRow(GVEvent);
        MessageHandler.ClearMessage(LBLErrorMsg);
    }
    protected void GVEvent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVEvent.PageIndex = e.NewPageIndex;
        // filterEventInfo();
    }
    #endregion
    #region Logical Methods  
    // Check required fields
    private bool checkRequiredFields()
    {
        if (DDLEventType.SelectedIndex == 0)
        {
            MessageHandler.ErrorMessage(LBLEventType, "Please select an event type");
        }
        else
        {
            MessageHandler.DefaultMessage(LBLEventType, "Event Type");
        }
        if (string.IsNullOrEmpty(INPUTEventDate.Value))
        {
            MessageHandler.ErrorMessage(LBLEventDate, "Please select an event date");
        }
        else
        {
            MessageHandler.DefaultMessage(LBLEventDate, "Event Date");
        }
        if (DDLEventInfoTime.SelectedIndex == 0)
        {
            MessageHandler.ErrorMessage(LBLEventTime, "Please select an event time");
        }
        else
        {
            MessageHandler.DefaultMessage(LBLEventTime, "Event Time");
        }
        if (string.IsNullOrEmpty(TBEventInfoLocation.Text))
        {
            MessageHandler.ErrorMessage(LBLEventInfoLocation, "Please enter an event location");
        }
        else
        {
            MessageHandler.DefaultMessage(LBLEventInfoLocation, "Event Location");
        }
        if (string.IsNullOrEmpty(TBEventInfoTitle.Text))
        {
            MessageHandler.ErrorMessage(LBLEventInfoTitle, "Please enter an event title");
        }
        else
        {
            MessageHandler.DefaultMessage(LBLEventInfoTitle, "Event Title");
        }
        if (string.IsNullOrEmpty(TBEventInfoDesc.Text))
        {
            MessageHandler.ErrorMessage(LBLEventInfoDesc, "Please enter an event description");
        }
        else
        {
            MessageHandler.DefaultMessage(LBLEventInfoDesc, "Event Description");
        }
        if (DDLEventType.SelectedIndex > 0 && !string.IsNullOrEmpty(INPUTEventDate.Value) && DDLEventInfoTime.SelectedIndex > 0 &&
                !string.IsNullOrEmpty(TBEventInfoTitle.Text) && !string.IsNullOrEmpty(TBEventInfoDesc.Text) && !string.IsNullOrEmpty(TBEventInfoLocation.Text))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void loadEvent(string eventID)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        TLOEventInfoEntityEdit = eventInfoCrtler.getEventInfoEntity(GVRowID.ToString());
        if (TLOEventInfoEntityEdit != null)
        {
            PNLEventInfoEdit.Visible = true;
            // load time slot
            loadEventType();
            loadEventTimeSlots();
            HDFEventInfoID.Value = TLOEventInfoEntityEdit.EventID;
            DDLEventType.SelectedValue = TLOEventInfoEntityEdit.EventType;
            TBEventInfoTitle.Text = TLOEventInfoEntityEdit.EventTitle;
            TBEventInfoDesc.Text = TLOEventInfoEntityEdit.EventDesc;
            TBEventInfoLocation.Text = TLOEventInfoEntityEdit.EventLocation;
            LBLEventInfoStatus.Text = TLOEventInfoEntityEdit.EventStatus;
            INPUTEventDate.Value = TLOEventInfoEntityEdit.EventDateTime.ToString("dd-MMM-yyyy");
            DDLEventInfoTime.SelectedValue = TLOEventInfoEntityEdit.EventDateTime.ToString("HH:mm tt");
            LBLEventInfoDateCreated.Text = TLOEventInfoEntityEdit.EventDateCreated.ToString("dd-MMM-yyyy @ HH:mm tt");
            CHKBXCancelEvent.Checked = (TLOEventInfoEntityEdit.EventStatus.Equals(Enums.GetDescription(SystemStatus.Cancelled))) ? true : false;
        }
        else
        {
            PNLEventInfoEdit.Visible = false;
            DDLEventType.SelectedIndex = 0;
            TBEventInfoTitle.Text = string.Empty;
            TBEventInfoDesc.Text = string.Empty;
            TBEventInfoLocation.Text = string.Empty;
            LBLEventInfoStatus.Text = string.Empty;
            INPUTEventDate.Value = string.Empty;
            DDLEventInfoTime.SelectedIndex = 0;
            LBLEventInfoDateCreated.Text = string.Empty;
        }
    }
    private void loadEventTimeSlots()
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        DDLEventInfoTime.Enabled = true;
        DDLEventInfoTime.DataSource = loadDDLTimeSlots();
        DDLEventInfoTime.DataBind();
    }
    private void loadEventType()
    {
        ListItem firstItem = DDLEventType.Items[0];
        DDLEventType.Items.Clear();
        DDLEventType.Items.Add(firstItem);
        DDLEventType.Items.Add(new ListItem(Enums.GetDescription(EventType.AdoptionDrive), Enums.GetDescription(EventType.AdoptionDrive)));
        DDLEventType.Items.Add(new ListItem(Enums.GetDescription(EventType.BirthdayParty), Enums.GetDescription(EventType.BirthdayParty)));
        DDLEventType.Items.Add(new ListItem(Enums.GetDescription(EventType.PetGathering), Enums.GetDescription(EventType.PetGathering)));
        DDLEventType.Items.Add(new ListItem(Enums.GetDescription(EventType.PetLearning), Enums.GetDescription(EventType.PetLearning)));
    }
    // Filter data
    public void filterEventInfo()
    {
        string filterEventType = DDLFilterEventType.SelectedValue.Trim();
        string tbSearchValue = TBSearchEvent.Text.Trim();
        dTableEvent = eventInfoCrtler.filterEventInfoData(filterEventType, tbSearchValue, LBLSearchResultEvent);
        GVEvent.DataSourceID = null;
        GVEvent.DataSource = null;
        GVEvent.DataSource = dTableEvent;
        GVEvent.DataBind();
    }
    #endregion
    #region Textbox Control
    protected void TBSearchEvent_TextChanged(object sender, EventArgs e)
    {
        //filter data
        filterEventInfo();
    }
    #endregion
    
}