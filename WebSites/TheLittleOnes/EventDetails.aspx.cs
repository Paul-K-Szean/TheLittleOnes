using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.EnumFolder;
using TheLittleOnesLibrary.Handler;

public partial class EventDetails : BasePageTLO
{
    private Label UICtrlLabel;
    private TextBox UICtrlTextbox;
    private CheckBox UICtrlCheckbox;
    private DropDownList UICtrlDropdownlist;
    private static DataTable dTableEvent;
    private static DateTime dateTimeSelected;
    private static EventEntity eventEntityEdit;
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
    }
    #region Button Control
    protected void BTNUpdate_Click(object sender, EventArgs e)
    {

    }
    protected void BTNClose_Click(object sender, EventArgs e)
    {
    }
    protected void BTNEventDate_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        // save temp appointment data if user selected any date/time
        saveTempEvent();
    }
    #endregion
    #region Checkbox Control
    protected void CHKBXCancelEvent_CheckedChanged(object sender, EventArgs e)
    {
        if (eventEntityEdit != null)
            eventEntityEdit.EventStatus = Enums.GetDescription(SystemStatus.Cancelled);
    }
    #endregion
    #region Dropdownlist Controls
    protected void DDLDisplayRecordCountEvent_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvPageSize = int.Parse(DDLDisplayRecordCountEvent.SelectedValue);
        GVEvent.PageSize = gvPageSize;
        // filterShopInfo();
    }
    protected void DDLEventTime_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLEventTime.SelectedIndex == 0) BTNUpdate.Enabled = false;
        else
        {
            BTNUpdate.Enabled = true;
            saveTempEvent();
        }
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
        // filterShopInfo();
    }
    #endregion
    #region Logical Methods
    private void loadEvent(string appmtID)
    {

    }
    protected void saveTempEvent()
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);

    }
    protected Boolean isCancelled(string eventStatus)
    {
        if (!string.IsNullOrEmpty(eventStatus))
        {
            if (eventStatus.Equals(Enums.GetDescription(SystemStatus.Cancelled)))
            {
                return true;
            }
        }
        return false;
    }
    #endregion
    #region Textbox Control
    protected void TBSearchEvent_TextChanged(object sender, EventArgs e)
    {
        //filter data
        // filterShopInfo();
    }
    #endregion


}