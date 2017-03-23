using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.EnumFolder;
using TheLittleOnesLibrary.Handler;
public partial class AdminShopInfoEdit : BasePage
{
    private Label UICtrlLabel;
    private TextBox UICtrlTextbox;
    private CheckBox UICtrlCheckbox;
    private DropDownList UICtrlDropdownlist;
    private static DataTable dTableShopInfo;
    private static int GVRowID;
    private static int gvPageSize = 5; // default
    private string shopID;
    private string shopName;
    private string shopContact;
    private string shopAddress;
    private bool shopGrooming;
    private string shopType;
    private string shopDesc;
    private bool shopCloseOnPublicHoliday;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        gvPageSize = int.Parse(DDLDisplayRecordCountShopInfo.SelectedValue);
        GVShopInfoOverview.PageSize = gvPageSize;
        if (IsPostBack)
        {
        }
        else
        {
            // clear static data
            clearStaticData();
        }
    }
    #region Initialize UI Control Values
    // Initial UI control values
    private void initializeUIControlValues()
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        // initialize hour range
        List<string> timeInterval = Utility.getTimeInterval();
        // loop over for controls
        foreach (Control ctrl in PNLShopInfoEdit.Controls)
        {
            // set dropdownlist values
            if (ctrl is DropDownList)
            {
                UICtrlDropdownlist = (DropDownList)ctrl;
                if (!UICtrlDropdownlist.ID.ToLower().Contains("shoptype"))
                {
                    UICtrlDropdownlist.DataSource = timeInterval;
                    UICtrlDropdownlist.DataBind();
                }
                if (UICtrlDropdownlist.ID.ToLower().Contains("open"))
                {
                    UICtrlDropdownlist.SelectedValue = "09:00 AM";
                }
                if (UICtrlDropdownlist.ID.ToLower().Contains("close"))
                {
                    UICtrlDropdownlist.SelectedValue = "17:00 PM";
                }
            }
        }
    }
    #endregion
    #region Button Clicks
    // Preview image uploaded
    protected void BTNPreview_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        MessageHandler.ClearMessage(LBLErrorMsg);
        // create temp files in temp foler
        photoEntities = photoCtrler.saveToTempFolder(PhotoPurpose.ShopInfo.ToString(), FileUpload1);
        // preview photo
        photoCtrler.previewPhotos(photoPreview);
    }
    protected void BTNUpdate_Click(object sender, EventArgs e)
    {
        shopID = TBShopInfoID.Text.Trim();
        shopName = TBShopName.Text.Trim();
        shopContact = TBShopContact.Text.Trim();
        shopAddress = TBShopAddress.Text.Trim();
        shopGrooming = CHKBXGroomingService.Checked ? true : false;
        shopType = DDLShopType.SelectedValue;
        shopDesc = TBShopDesc.Text.Trim();
        shopCloseOnPublicHoliday = CHKBXCloseOnPublicHoliday.Checked ? true : false;
        shopTimeEntities = getShopTime();
        if (checkRequiredFields())
        {
            // create shop info entity with new changes
            shopInfoEntity = new ShopInfoEntity(shopID, shopName, shopContact,
                shopAddress, shopGrooming, shopType, shopDesc, shopCloseOnPublicHoliday, shopTimeEntities, photoEntities);
            // update into database
            shopInfoEntity = shopInfoCtrler.updateShopInfo(shopInfoEntity);
            // remove old shoptime from database
            shopInfoCtrler.deleteShopTime(shopInfoEntity);
            // update shoptime
            shopInfoEntity = shopInfoCtrler.createShopTime(shopInfoEntity);
            // update shopphoto
            if (photoEntities != null)
            {
                // change photo path to database instead of using temp
                shopInfoEntity.PhotoEntities = photoCtrler.changePhotoPathToDatabaseFolder(photoEntities, shopInfoEntity.ShopInfoID);
                // remove old photos from database
                photoCtrler.deletePhoto(shopInfoEntity.ShopInfoID, PhotoPurpose.ShopInfo.ToString());
                // create new photos into database
                photoCtrler.createPhoto(photoEntities, shopInfoEntity.ShopInfoID);
            }
            if (shopInfoEntity != null)
            {
                MessageHandler.SuccessMessage(LBLErrorMsg, "Shop info successfully updated");
            }
            else
            {
                MessageHandler.ErrorMessageAdmin(LBLErrorMsg, "Shop info was not successfully updated");
            }
            GVShopInfoOverview.DataBind();
            DLPhotoUploaded.DataBind();
            filterShopInfo();
            clearStaticData();
        }
    }
    protected void BTNCancel_Click(object sender, EventArgs e)
    {
        PNLShopInfoEdit.Visible = false;
        clearUIControlValues(PNLShopInfoEdit.Controls);
        LBLErrorMsg.Text = string.Empty;
    }
    #endregion
    #region Checkbox Control
    // Shop info filter pet shop
    protected void CHKBXFilterShop_CheckedChanged(object sender, EventArgs e)
    {
        if (CHKBXFilterPetShop.Checked)
        {
            CHKBXFilterPetClinic.Checked = false;
        }
        //filter data
        filterShopInfo();
    }
    // Shop info filter pet clinic
    protected void CHKBXFilterClinic_CheckedChanged(object sender, EventArgs e)
    {
        if (CHKBXFilterPetClinic.Checked)
        {
            CHKBXFilterGrooming.Checked = false;
            CHKBXFilterPetShop.Checked = false;
        }
        //filter data
        filterShopInfo();
    }
    // Shop info filter grooming service
    protected void CHKBXFilterGrooming_CheckedChanged(object sender, EventArgs e)
    {
        if (CHKBXFilterGrooming.Checked)
        {
            CHKBXFilterPetClinic.Checked = false;
        }
        //filter data
        filterShopInfo();
    }
    #endregion
    #region Dropdownlist Controls
    protected void DDLDisplayRecordCountShopInfo_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvPageSize = int.Parse(DDLDisplayRecordCountShopInfo.SelectedValue);
        GVShopInfoOverview.PageSize = gvPageSize;
        filterShopInfo();
    }
    #endregion
    #region Gridview Controls
    protected void GVShopInfoOverview_DataBound(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        if (dTableShopInfo == null)
            dTableShopInfo = ((DataView)SDSShopInfo.Select(DataSourceSelectArguments.Empty)).Table;
        updateEntryCount(dTableShopInfo, GVShopInfoOverview, LBLEntriesCount);
    }
    protected void GVShopInfoOverview_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        clearStaticData();
        MessageHandler.ClearMessage(LBLErrorMsg);
        GridViewRow row = GVShopInfoOverview.Rows[e.NewSelectedIndex];
        GVRowID = Convert.ToInt32(GVShopInfoOverview.DataKeys[row.RowIndex].Values[0]);
        initializeUIControlValues();
        loadShopInfo(GVRowID.ToString());
    }
    protected void GVShopInfoOverview_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        highlightSelectedRow(GVShopInfoOverview);
        MessageHandler.ClearMessage(LBLErrorMsg);
    }
    protected void GVShopInfoOverview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVShopInfoOverview.PageIndex = e.NewPageIndex;
        filterShopInfo();
    }
    #endregion
    #region Logical Methods
    // Check Required Fields
    private bool checkRequiredFields()
    {
        bool isUICtrlDropdownlistValid = true;
        bool isUICtrlTextboxValid = true;
        foreach (Control ctrl in PNLShopInfoEdit.Controls)
        {
            // check all text boxes
            if (ctrl is TextBox)
            {
                UICtrlTextbox = (TextBox)ctrl;
                if (string.IsNullOrEmpty(UICtrlTextbox.Text.Trim()))
                {
                    isUICtrlTextboxValid = false;
                    LogController.LogLine("No value in required field: " + UICtrlTextbox.ID);
                }
            }
        }
        // return condition
        if (isUICtrlDropdownlistValid && isUICtrlTextboxValid)
        {
            return true;
        }
        else
        {
            // display error message
            MessageHandler.ErrorMessage(LBLErrorMsg, "Please ensure that all the fields are not empty");
            return false;
        }
    }
    // Get ShopTime
    private List<ShopTimeEntity> getShopTime()
    {
        shopTimeEntities = new List<ShopTimeEntity>();
        if (!CHKBXCloseMonday.Checked)
        {
            shopTimeEntity = new ShopTimeEntity("Monday", DDLOpenTimeMonday.SelectedValue, DDLCloseTimeMonday.SelectedValue);
            shopTimeEntities.Add(shopTimeEntity);
        }
        if (!CHKBXCloseTuesday.Checked)
        {
            shopTimeEntity = new ShopTimeEntity("Tuesday", DDLOpenTimeTuesday.SelectedValue, DDLCloseTimeTuesday.SelectedValue);
            shopTimeEntities.Add(shopTimeEntity);
        }
        if (!CHKBXCloseWednesday.Checked)
        {
            shopTimeEntity = new ShopTimeEntity("Wednesday", DDLOpenTimeWednesday.SelectedValue, DDLCloseTimeWednesday.SelectedValue);
            shopTimeEntities.Add(shopTimeEntity);
        }
        if (!CHKBXCloseThursday.Checked)
        {
            shopTimeEntity = new ShopTimeEntity("Thursday", DDLOpenTimeThursday.SelectedValue, DDLCloseTimeThursday.SelectedValue);
            shopTimeEntities.Add(shopTimeEntity);
        }
        if (!CHKBXCloseFriday.Checked)
        {
            shopTimeEntity = new ShopTimeEntity("Friday", DDLOpenTimeFriday.SelectedValue, DDLCloseTimeFriday.SelectedValue);
            shopTimeEntities.Add(shopTimeEntity);
        }
        if (!CHKBXCloseSaturday.Checked)
        {
            shopTimeEntity = new ShopTimeEntity("Saturday", DDLOpenTimeSaturday.SelectedValue, DDLCloseTimeSaturday.SelectedValue);
            shopTimeEntities.Add(shopTimeEntity);
        }
        if (!CHKBXCloseSunday.Checked)
        {
            shopTimeEntity = new ShopTimeEntity("Sunday", DDLOpenTimeSunday.SelectedValue, DDLCloseTimeSunday.SelectedValue);
            shopTimeEntities.Add(shopTimeEntity);
        }
        return shopTimeEntities;
    }
    // Load shop info
    private void loadShopInfo(string rowID)
    {
        shopInfoEntity = shopInfoCtrler.getShopInfo(GVRowID.ToString());
        if (shopInfoEntity != null)
        {
            PNLShopInfoEdit.Visible = true;
            TBShopInfoID.Text = shopInfoEntity.ShopInfoID;
            TBShopName.Text = shopInfoEntity.ShopInfoName;
            TBShopContact.Text = shopInfoEntity.ShopInfoContact;
            TBShopAddress.Text = shopInfoEntity.ShopInfoAddress;
            CHKBXGroomingService.Checked = shopInfoEntity.ShopInfoGrooming;
            DDLShopType.SelectedValue = shopInfoEntity.ShopInfoType;
            TBShopDesc.Text = shopInfoEntity.ShopInfoDesc;
            CHKBXCloseOnPublicHoliday.Checked = shopInfoEntity.ShopCloseOnPublicHoliday;
            CHKBXGroomingService.Enabled = DDLShopType.SelectedValue.Contains("Shop") ? true : false;
            // load operating hours
            loadShoptime();
        }
        else
        {
            PNLShopInfoEdit.Visible = false;
            TBShopInfoID.Text = string.Empty;
            TBShopName.Text = string.Empty;
            TBShopContact.Text = string.Empty;
            TBShopAddress.Text = string.Empty;
            CHKBXGroomingService.Checked = false;
            DDLShopType.SelectedValue = string.Empty;
            TBShopDesc.Text = string.Empty;
            CHKBXCloseOnPublicHoliday.Checked = false;
            CHKBXGroomingService.Enabled = false;
        }
    }
    // Load shop time
    private void loadShoptime()
    {
        List<string> workDay = new List<string>();
        List<string> noWorkDay = new List<string>();
        List<string> dayOfWeek = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        // get the work days
        foreach (ShopTimeEntity shopTimeEntity in shopInfoEntity.ShopTimeEntities)
        {
            workDay.Add(shopTimeEntity.DayOfWeek);
            if (shopTimeEntity.DayOfWeek.Equals("Monday"))
            {
                DDLOpenTimeMonday.SelectedValue = (DateTime.Parse(shopTimeEntity.OpenTime)).ToString("HH:mm tt");
                DDLCloseTimeMonday.SelectedValue = (DateTime.Parse(shopTimeEntity.CloseTime)).ToString("HH:mm tt");
            }
            if (shopTimeEntity.DayOfWeek.Equals("Tuesday"))
            {
                DDLOpenTimeTuesday.SelectedValue = (DateTime.Parse(shopTimeEntity.OpenTime)).ToString("HH:mm tt");
                DDLCloseTimeTuesday.SelectedValue = (DateTime.Parse(shopTimeEntity.CloseTime)).ToString("HH:mm tt");
            }
            if (shopTimeEntity.DayOfWeek.Equals("Wednesday"))
            {
                DDLOpenTimeWednesday.SelectedValue = (DateTime.Parse(shopTimeEntity.OpenTime)).ToString("HH:mm tt");
                DDLCloseTimeWednesday.SelectedValue = (DateTime.Parse(shopTimeEntity.CloseTime)).ToString("HH:mm tt");
            }
            if (shopTimeEntity.DayOfWeek.Equals("Thursday"))
            {
                DDLOpenTimeThursday.SelectedValue = (DateTime.Parse(shopTimeEntity.OpenTime)).ToString("HH:mm tt");
                DDLCloseTimeThursday.SelectedValue = (DateTime.Parse(shopTimeEntity.CloseTime)).ToString("HH:mm tt");
            }
            if (shopTimeEntity.DayOfWeek.Equals("Friday"))
            {
                DDLOpenTimeFriday.SelectedValue = (DateTime.Parse(shopTimeEntity.OpenTime)).ToString("HH:mm tt");
                DDLCloseTimeFriday.SelectedValue = (DateTime.Parse(shopTimeEntity.CloseTime)).ToString("HH:mm tt");
            }
            if (shopTimeEntity.DayOfWeek.Equals("Saturday"))
            {
                DDLOpenTimeSaturday.SelectedValue = (DateTime.Parse(shopTimeEntity.OpenTime)).ToString("HH:mm tt");
                DDLCloseTimeSaturday.SelectedValue = (DateTime.Parse(shopTimeEntity.CloseTime)).ToString("HH:mm tt");
            }
            if (shopTimeEntity.DayOfWeek.Equals("Sunday"))
            {
                DDLOpenTimeSunday.SelectedValue = (DateTime.Parse(shopTimeEntity.OpenTime)).ToString("HH:mm tt");
                DDLCloseTimeSunday.SelectedValue = (DateTime.Parse(shopTimeEntity.CloseTime)).ToString("HH:mm tt");
            }
        }
        // get the non working days
        foreach (string day in dayOfWeek)
        {
            if (Array.IndexOf(workDay.ToArray(), day) == -1)
            {
                noWorkDay.Add(day);
            }
        }
        // unset the checkboxes for working day
        foreach (string day in workDay)
        {
            switch (day.ToUpper().Trim())
            {
                case "MONDAY":
                    CHKBXCloseMonday.Checked = false;
                    break;
                case "TUESDAY":
                    CHKBXCloseTuesday.Checked = false;
                    break;
                case "WEDNESDAY":
                    CHKBXCloseWednesday.Checked = false;
                    break;
                case "THURSDAY":
                    CHKBXCloseThursday.Checked = false;
                    break;
                case "FRIDAY":
                    CHKBXCloseFriday.Checked = false;
                    break;
                case "SATURDAY":
                    CHKBXCloseSaturday.Checked = false;
                    break;
                case "SUNDAY":
                    CHKBXCloseSunday.Checked = false;
                    break;
            }
        }
        // set the checkboxes for non working day
        foreach (string day in noWorkDay)
        {
            switch (day.ToUpper().Trim())
            {
                case "MONDAY":
                    CHKBXCloseMonday.Checked = true;
                    break;
                case "TUESDAY":
                    CHKBXCloseTuesday.Checked = true;
                    break;
                case "WEDNESDAY":
                    CHKBXCloseWednesday.Checked = true;
                    break;
                case "THURSDAY":
                    CHKBXCloseThursday.Checked = true;
                    break;
                case "FRIDAY":
                    CHKBXCloseFriday.Checked = true;
                    break;
                case "SATURDAY":
                    CHKBXCloseSaturday.Checked = true;
                    break;
                case "SUNDAY":
                    CHKBXCloseSunday.Checked = true;
                    break;
            }
        }
    }
    // Filter data
    private void filterShopInfo()
    {
        bool chkbxPetShop = CHKBXFilterPetShop.Checked;
        bool chkbxPetClinic = CHKBXFilterPetClinic.Checked;
        bool chkbxGrooming = CHKBXFilterGrooming.Checked;
        string tbSearchValue = TBSearchShopInfo.Text;
        dTableShopInfo = shopInfoCtrler.filterShopInfoData(chkbxPetShop, chkbxPetClinic, chkbxGrooming, tbSearchValue, LBLSearchResultShopInfo);
        GVShopInfoOverview.DataSourceID = null;
        GVShopInfoOverview.DataSource = null;
        GVShopInfoOverview.DataSource = dTableShopInfo;
        GVShopInfoOverview.DataBind();
        DLPhotoUploaded.DataBind();
        shopInfoEntity = null;
    }
    // Clear temp data
    private void clearStaticData()
    {
        GVRowID = 0;
        dTableShopInfo = null;
        photoEntities = null;
        photoPreview.InnerHtml = string.Empty;
    }
    #endregion
    #region Textbox Control
    protected void TBSearchShopInfo_TextChanged(object sender, EventArgs e)
    {
        //filter data
        filterShopInfo();
    }
    #endregion
}