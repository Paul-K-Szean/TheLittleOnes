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
public partial class AppointmentDetails : BasePageTLO
{
    private Label UICtrlLabel;
    private TextBox UICtrlTextbox;
    private CheckBox UICtrlCheckbox;
    private DropDownList UICtrlDropdownlist;
    private static DataTable dTableAppointment;
    private static DateTime dateTimeSelected;
    private static AppointmentEntity appointmentEntityEdit;
    private static ShopInfoEntity TLOShopInfoEntity;
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
        }
        if (TLOShopInfoEntity != null)
            // for some reason, the value of HDFShopInfoID is lost. Reassign the HDFShopInfoID
            HDFShopInfoID.Value = TLOShopInfoEntity.ShopInfoID;
    }
    #region Initialize UI Controls
    #endregion
    #region Button Control
    protected void BTNUpdate_Click(object sender, EventArgs e)
    {
        if (appointmentEntityEdit != null)
        {
            LBLErrorMsg.Text = appointmentEntityEdit.AppmtID.ToString() + " "
                             + appointmentEntityEdit.AccountEntity.AccountID.ToString() + " "
                             + appointmentEntityEdit.AppmtDateTime.ToString();
            bool isCancelAppointmentChecked = CHKBXCancelAppointment.Checked;
            appointmentEntityEdit = appointmentCrtler.updateAppointment(appointmentEntityEdit);
            if (appointmentEntityEdit != null)
            {
                MessageHandler.SuccessMessage(LBLErrorMsg, "Appointment was succussfully updated");
            }
            else
            {
                MessageHandler.ErrorMessage(LBLErrorMsg, "Appointment was not succussfully updated");
            }
            GVAppointment.DataBind();
        }
    }
    protected void BTNClose_Click(object sender, EventArgs e)
    {
    }
    protected void BTNAppmtDate_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        // to get date selected from bootstrap datepicker 
        loadOperatingHours(dateSelected, daySelected, INPUTAppmtDate,
                          shopInfoCtrler.getShopTime(HDFShopInfoID.Value), DDLAppmtTime, LBLAppmtDate, LBLAppmtTime, appointmentEntityEdit.AppmtID, appointmentEntityEdit.AppmtToID);
        // save temp appointment data if user selected any date/time
        saveTempAppointment();
    }
    #endregion
    #region Checkbox Control
    protected void CHKBXCancelAppointment_CheckedChanged(object sender, EventArgs e)
    {
        if (appointmentEntityEdit != null)
            appointmentEntityEdit.AppmtStatus = Enums.GetDescription(SystemStatus.Cancelled);
    }
    #endregion
    #region Datalist Command
    #endregion
    #region Dropdownlist Controls
    protected void DDLDisplayRecordCountAppointment_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvPageSize = int.Parse(DDLDisplayRecordCountAppointment.SelectedValue);
        GVAppointment.PageSize = gvPageSize;
        // filterShopInfo();
    }
    protected void DDLAppmtTime_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLAppmtTime.SelectedIndex == 0) BTNUpdate.Enabled = false;
        else
        {
            BTNUpdate.Enabled = true;
            saveTempAppointment();
        }
    }
    #endregion
    #region Gridview Command
    protected void GVAppointment_DataBound(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        if (dTableAppointment == null)
            dTableAppointment = ((DataView)SDSAppointment.Select(DataSourceSelectArguments.Empty)).Table;
        updateEntryCount(dTableAppointment, GVAppointment, LBLEntriesCount);
    }
    protected void GVAppointment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            AppointmentEntity appointmentEntity = new AppointmentEntity(
                 DataBinder.Eval(e.Row.DataItem, "APPMTID").ToString(),
                 TLOAccountEntity,
                 DataBinder.Eval(e.Row.DataItem, "APPMTTOID").ToString(),
                 DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "APPMTDATETIME").ToString()),
                 DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "APPMTDATECREATED").ToString()),
                 DataBinder.Eval(e.Row.DataItem, "APPMTSTATUS").ToString(),
                 DataBinder.Eval(e.Row.DataItem, "APPMTTYPE").ToString()
                 );
            // display shop info name instead of id
            Label LBLAppmtToID = (Label)e.Row.FindControl("LBLAppmtToID");
            // display pet name of adoption instead of id
            Label LBLPetName = (Label)e.Row.FindControl("LBLPetName");
            if (appointmentEntity.AppmtType.Equals(Enums.GetDescription(AppointmentType.Adoption)))
            {
                string petName = adoptInfoCtrler.getAdoptInfoEntity(appointmentEntity.AppmtToID).PetEntity.PetName;
                LBLPetName.Text = petName;
                string shopInfoName = adoptInfoCtrler.getAdoptInfoEntity(appointmentEntity.AppmtToID).ShopInfoEntity.ShopInfoName;
                LBLAppmtToID.Text = shopInfoName;
            }
            if (appointmentEntity.AppmtType.Equals(Enums.GetDescription(AppointmentType.Clinic)))
            {
                string shopInfoName = shopInfoCtrler.getShopInfo(appointmentEntity.AppmtToID).ShopInfoName;
                LBLAppmtToID.Text = shopInfoName;
            }
        }
    }
    protected void GVAppointment_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        // clearStaticData();
        MessageHandler.ClearMessage(LBLErrorMsg);
        GridViewRow row = GVAppointment.Rows[e.NewSelectedIndex];
        GVRowID = Convert.ToInt32(GVAppointment.DataKeys[row.RowIndex].Values[0]);
        loadAppointment(GVRowID.ToString());
    }
    protected void GVAppointment_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        highlightSelectedRow(GVAppointment);
        MessageHandler.ClearMessage(LBLErrorMsg);
    }
    protected void GVAppointment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVAppointment.PageIndex = e.NewPageIndex;
        // filterShopInfo();
    }
    #endregion
    #region Logical Methods
    private void loadAppointment(string appmtID)
    {
        appointmentEntityEdit = appointmentCrtler.getUserAppointmentEntity(appmtID);
        PNLAppointmentEdit.Visible = true;
        INPUTAppmtDate.Value = dateSelected = appointmentEntityEdit.AppmtDateTime.ToString("dd-MMMM-yyyy");
        if (appointmentEntityEdit.AppmtType.Equals(Enums.GetDescription(AppointmentType.Adoption)))
        {
            TLOShopInfoEntity = adoptInfoCtrler.getAdoptInfoEntity(appointmentEntityEdit.AppmtToID).ShopInfoEntity;
        }
        if (appointmentEntityEdit.AppmtType.Equals(Enums.GetDescription(AppointmentType.Clinic)))
        {
            TLOShopInfoEntity = shopInfoCtrler.getShopInfo(appointmentEntityEdit.AppmtToID);
        }
        HDFShopInfoID.Value = TLOShopInfoEntity.ShopInfoID;
        LBLShopInfoName.Text = TLOShopInfoEntity.ShopInfoName;
        LBLShopInfoContact.Text = TLOShopInfoEntity.ShopInfoContact;
        LBLShopInfoAddress.Text = TLOShopInfoEntity.ShopInfoAddress;
        loadOperatingHours(dateSelected, daySelected, INPUTAppmtDate,
                          shopInfoCtrler.getShopTime(HDFShopInfoID.Value), DDLAppmtTime, LBLAppmtDate, LBLAppmtTime, appointmentEntityEdit.AppmtID, appointmentEntityEdit.AppmtToID);
        DDLAppmtTime.SelectedValue = appointmentEntityEdit.AppmtDateTime.ToString("HH:mm tt");
        LBLAppmtStatus.Text = appointmentEntityEdit.AppmtStatus;
    }
    protected void saveTempAppointment()
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        dateSelected = INPUTAppmtDate.Value;
        timeSelected = DDLAppmtTime.SelectedValue.Trim();
        if (!string.IsNullOrEmpty(dateSelected) && !string.IsNullOrEmpty(timeSelected))
        {
            dateTimeSelected = DateTime.Parse(dateSelected + " " + timeSelected);
            appointmentEntityEdit = new AppointmentEntity(appointmentEntityEdit.AppmtID, TLOAccountEntity, appointmentEntityEdit.AppmtToID, dateTimeSelected, DateTime.Now, Enums.GetDescription(SystemStatus.Confirmed), Enums.GetDescription(AppointmentType.Adoption));
        }
        else if (!string.IsNullOrEmpty(dateSelected))
        {
            dateTimeSelected = DateTime.Parse(dateSelected + " 00:00 AM");
            appointmentEntityEdit = new AppointmentEntity(appointmentEntityEdit.AppmtID, TLOAccountEntity, appointmentEntityEdit.AppmtToID, dateTimeSelected, DateTime.Now, Enums.GetDescription(SystemStatus.Confirmed), Enums.GetDescription(AppointmentType.Adoption));
        }
    }
    // For GUI display word instead of bool
    protected Boolean isCancelled(string appmtStatus)
    {
        if (!string.IsNullOrEmpty(appmtStatus))
        {
            if (appmtStatus.Equals(Enums.GetDescription(SystemStatus.Cancelled)))
            {
                return true;
            }
        }
        return false;
    }
    #endregion
    #region Textbox Control
    protected void TBSearchAppointment_TextChanged(object sender, EventArgs e)
    {
        //filter data
        // filterShopInfo();
    }
    #endregion
    
}