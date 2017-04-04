using System;
using System.Reflection;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.EnumFolder;
using TheLittleOnesLibrary.Handler;
public partial class EventAdd : BasePageTLO
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) { }
        else
        {
            initializeUIControlValues();
        }
    }
    #region Initialize UI Control Values
    // Initial UI control values
    private void initializeUIControlValues()
    {
        ListItem firstItem = DDLEventType.Items[0];
        DDLEventType.Items.Clear();
        DDLEventType.Items.Add(firstItem);
        DDLEventType.Items.Add(new ListItem(Enums.GetDescription(EventType.AdoptionDrive), Enums.GetDescription(EventType.AdoptionDrive)));
        DDLEventType.Items.Add(new ListItem(Enums.GetDescription(EventType.BirthdayParty), Enums.GetDescription(EventType.BirthdayParty)));
        DDLEventType.Items.Add(new ListItem(Enums.GetDescription(EventType.PetGathering), Enums.GetDescription(EventType.PetGathering)));
        DDLEventType.Items.Add(new ListItem(Enums.GetDescription(EventType.PetLearning), Enums.GetDescription(EventType.PetLearning)));
    }
    #endregion
    #region Button Click
    protected void BTNAdd_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        LBLErrorMsg.Text = checkRequiredFields().ToString();
        if (checkRequiredFields())
        {
            string eventType = DDLEventType.SelectedValue.Trim();
            string eventDate = INPUTEventDate.Value.Trim();
            string eventTime = DDLEventTime.SelectedValue.Trim();
            string eventLocation = TBEventLocation.Text.Trim();
            string eventTitle = TBEventTitle.Text.Trim();
            string eventDesc = TBEventDesc.Text.Trim();
            // create entity
            EventInfoEntity eventEntity = new EventInfoEntity(
                TLOAccountEntity,
                eventTitle,
                eventDesc,
                eventLocation,
                eventType,
                DateTime.Parse(string.Concat(eventDate, " ", eventTime)),
                Enums.GetDescription(SystemStatus.Confirmed)
                );
            eventEntity = eventInfoCrtler.createEvent(eventEntity);
            // change photo path to database instead of using temp
            if (TLOPhotoEntities != null)
            {
                photoCtrler.changePhotoPathToDatabaseFolder(TLOPhotoEntities, TLOAccountEntity.AccountID);
                PhotoController.getInstance().createPhoto(TLOPhotoEntities,TLOAccountEntity.AccountID);
            }
            if (string.IsNullOrEmpty(eventEntity.EventID))
            {
                MessageHandler.ErrorMessage(LBLErrorMsg, "Event was not created successfully!");
            }
            else {
                MessageHandler.SuccessMessage(LBLErrorMsg, "Event created successfully!");
            }
        }
    }
    protected void BTNGenerate_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        DDLEventType.SelectedIndex = new Random().Next(1, DDLEventType.Items.Count);
        TBEventLocation.Text = string.Concat("LOCATION", new Random().Next(0, 100).ToString("00"));
        TBEventTitle.Text = string.Concat("TITLE", new Random().Next(0, 100).ToString("00"));
        TBEventDesc.Text = string.Concat("DESC", new Random().Next(0, 100).ToString("00"));
    }
    // Preview image uploaded
    protected void BTNPreview_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        MessageHandler.ClearMessage(LBLErrorMsg);
        // create temp files in temp foler
        TLOPhotoEntities = photoCtrler.saveToTempFolder(PhotoPurpose.EventInfo.ToString(), FileUpload1);
        // preview photo
        photoCtrler.previewPhotos(photoPreview);
    }
    protected void BTNAppmtDate_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        if (!string.IsNullOrEmpty(INPUTEventDate.Value))
        {
            DDLEventTime.Enabled = true;
            DDLEventTime.DataSource = loadDDLTimeSlots();
            DDLEventTime.DataBind();
        }
        else
        {
            DDLEventTime.Enabled = false;
        }
    }
    #endregion
    #region Dropdownlist Control
    // Dropdownlist show/hide shop info
    protected void DDLOrangisation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
    }
    // Dropdownlist to show/hide organisation option
    protected void DDLEventType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
    }
    protected void DDLAppmtTime_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
    }
    #endregion
    #region Logical Methods
    // Check required fields
    private bool checkRequiredFields()
    {
        if (DDLEventType.SelectedIndex == 0)
        {
            MessageHandler.ErrorMessage(LBLEventType, "Please select a event type");
        }
        else
        {
            MessageHandler.DefaultMessage(LBLEventType, "Event Type");
        }
        if (string.IsNullOrEmpty(INPUTEventDate.Value))
        {
            MessageHandler.ErrorMessage(LBLEventDate, "Please select a event date");
        }
        else
        {
            MessageHandler.DefaultMessage(LBLEventDate, "Event Date");
        }
        if (DDLEventTime.SelectedIndex == 0)
        {
            MessageHandler.ErrorMessage(LBLEventTime, "Please select a event time");
        }
        else
        {
            MessageHandler.DefaultMessage(LBLEventTime, "Event Time");
        }
        if (string.IsNullOrEmpty(TBEventLocation.Text))
        {
            MessageHandler.ErrorMessage(LBLEventLocation, "Please enter a event location");
        }
        else
        {
            MessageHandler.DefaultMessage(LBLEventLocation, "Event Location");
        }
        if (string.IsNullOrEmpty(TBEventTitle.Text))
        {
            MessageHandler.ErrorMessage(LBLEventTitle, "Please enter a event title");
        }
        else
        {
            MessageHandler.DefaultMessage(LBLEventTitle, "Event Title");
        }
        if (string.IsNullOrEmpty(TBEventDesc.Text))
        {
            MessageHandler.ErrorMessage(LBLEventDesc, "Please enter a event description");
        }
        else
        {
            MessageHandler.DefaultMessage(LBLEventDesc, "Event Description");
        }
        if (DDLEventType.SelectedIndex > 0 && !string.IsNullOrEmpty(INPUTEventDate.Value) && DDLEventTime.SelectedIndex > 0 &&
                !string.IsNullOrEmpty(TBEventLocation.Text) && !string.IsNullOrEmpty(LBLEventTitle.Text) && !string.IsNullOrEmpty(LBLEventDesc.Text))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}