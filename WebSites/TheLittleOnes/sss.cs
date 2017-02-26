using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.Handler;

public partial class AdminShopInfoEdit : BasePage
{
    private Label UICtrlLabel;
    private TextBox UICtrlTextbox;
    private DropDownList UICtrlDropdownlist;

    private string shopID;
    private string shopName;
    private string shopContact;
    private string shopAddress;
    private bool shopGrooming;
    private string shopType;
    private string shopDesc;
    private bool shopCloseOnPublicHoliday;
    private static string filePath_UploadFolderTemp;
    private static List<ShopTimeEntity> shopTimeEntities;
    private static List<PhotoEntity> photoEntities;

    private static DataTable dTablePhoto;
    private static DataTable dTableOld;
    private static DataTable dTableEdit;

    private static int GVRowID;
    private static int gvPageSize = 10; // default

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (IsPostBack)
        {

        }
        else
        {
            // hide edit panel
            panelShopInfoEdit.Visible = false;
        }
    }

    #region Initialize UI Control Values
    // Initial UI control values
    private void initializeUIControlValues()
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        // initialize hour range
        List<string> timeInterval = Utility.setupHourRange();
        // loop over for controls
        foreach (Control ctrl in panelShopInfoEdit.Controls)
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
        shopName = TBShopName.Text.Trim();
        shopContact = TBShopContact.Text.Trim();

        // need category and breed to create folder
        if (!string.IsNullOrEmpty(shopName) && !string.IsNullOrEmpty(shopContact))
        {
            LBLErrorMsg.Text = string.Empty;
            filePath_UploadFolderTemp = string.Concat("~/uploadedFiles/temp/shopinfo/", shopName.ToLower().Replace(" ", "") + shopContact.ToLower().Replace(" ", "").ToString());

            // create temp files in temp foler
            photoCtrler.previewPhotos(FileUpload1, filePath_UploadFolderTemp);

            // display images from temp folders
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath(filePath_UploadFolderTemp));
            photoPreview.InnerHtml = string.Empty;
            foreach (var file in dir.GetFiles("*.jpg"))
            {
                LogController.LogLine("File name: " + file.Name);
                LogController.LogLine(string.Concat(filePath_UploadFolderTemp, "/", file.Name));
                photoPreview.InnerHtml += string.Concat(
                    "<img  src =\"",
                    string.Concat(filePath_UploadFolderTemp, "/", file.Name).Replace("~/", ""),
                    "\" Height=\"100\"/>",
                    "<br>", file.Name, "<hr/>");
            }
        }
        else
        {
            MessageHandler.ErrorMessage(LBLErrorMsg, "Shop name and contact cannot be empty");
        }
        LogController.Log();
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
        photoEntities = photoCtrler.getPhotoEntities();

        if (checkRequiredFields())
        {
            // create shop info entity
            shopInfoEntity = new ShopInfoEntity(shopID, shopName, shopContact,
                shopAddress, shopGrooming, shopType, shopDesc, shopCloseOnPublicHoliday, shopTimeEntities, photoEntities);
            // update shopinfo
            shopInfoEntity = shopInfoCtrler.updateShopInfo(shopInfoEntity);
            // update shoptime
            shopInfoEntity.ShopTimeEntities = shopInfoCtrler.updateShoptime(shopInfoEntity.ShopTimeEntities);
            // update shopphoto
            if (photoEntities != null)
            {
                // change photo path to database instead of using temp
                shopInfoEntity.PhotoEntities = photoCtrler.changePhotoPathToDatabaseFolder(photoEntities, filePath_UploadFolderTemp);
                // remove old photos from database
                shopInfoCtrler.deleteShopPhoto(shopInfoEntity);
                // create new photos into database
                shopInfoCtrler.createShopPhoto(shopInfoEntity);

            }

            if (shopInfoEntity != null)
            {
                MessageHandler.SuccessMessage(LBLErrorMsg, "Shop info successfully added");
            }
            else
            {
                MessageHandler.ErrorMessageAdmin(LBLErrorMsg, "Shop info was not successfully added");
            }


        }
    }

    protected void BTNCancel_Click(object sender, EventArgs e)
    {
        foreach (Control ctrl in panelShopInfoEdit.Controls)
        {
            if (ctrl is TextBox)
            {
                UICtrlTextbox = (TextBox)ctrl;
                UICtrlTextbox.Text = string.Empty;
            }
            if (ctrl is DropDownList)
            {
                UICtrlDropdownlist = (DropDownList)ctrl;
                UICtrlDropdownlist.SelectedIndex = 0;
            }
        }
        panelShopInfoEdit.Visible = false;
    }
    #endregion

    #region Checkbox Control
    // Shop info filter clinic
    protected void CHKBXFilterClinic_CheckedChanged(object sender, EventArgs e)
    {
        if (CHKBXFilterClinic.Checked)
        {
            CHKBXFilterGrooming.Checked = false;
            SDSShopInfo.SelectCommand = "SELECT * FROM SHOPINFO WHERE SHOPINFOTYPE LIKE '%CLINIC%' ";
            SDSShopInfo.DataBind();
        }
    }
    // Shop info filter grooming service
    protected void CHKBXFilterGrooming_CheckedChanged(object sender, EventArgs e)
    {
        if (CHKBXFilterGrooming.Checked)
        {
            CHKBXFilterClinic.Checked = false;
            SDSShopInfo.SelectCommand = "SELECT * FROM SHOPINFO WHERE SHOPINFOGROOMING = TRUE ";
            SDSShopInfo.DataBind();
        }
    }
    #endregion

    #region Dropdownlist Controls
    protected void DDLDisplayRecordCountShopInfo_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvPageSize = int.Parse(DDLDisplayRecordCountShopInfo.SelectedValue);
        GVShopInfoOverview.PageSize = gvPageSize;
    }
    #endregion

    #region Gridview Controls
    protected void GVShopInfoOverview_DataBound(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        DataView dataView = (DataView)SDSShopInfo.Select(DataSourceSelectArguments.Empty);
        int totalSize = dataView.Count;
        int currentPageIndex = GVShopInfoOverview.PageIndex + 1;
        int pageSize = GVShopInfoOverview.PageSize * currentPageIndex;
        int rowSize = GVShopInfoOverview.Rows.Count;

        if (pageSize > totalSize)
            pageSize = totalSize;
        LBLEntriesCountShopInfo.Text = string.Concat("Showing ", currentPageIndex, " to ", pageSize, " of ", totalSize, " entries");

    }

    protected void GVShopInfoOverview_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        GridViewRow row = GVShopInfoOverview.Rows[e.NewSelectedIndex];
        GVRowID = Convert.ToInt32(GVShopInfoOverview.DataKeys[row.RowIndex].Values[0]);
        loadShopInfo(GVRowID.ToString());
        panelShopInfoEdit.Visible = true;
        initializeUIControlValues();
    }
    #endregion

    #region Logical Methods
    // Check Required Fields
    private bool checkRequiredFields()
    {
        bool isUICtrlDropdownlistValid = true;
        bool isUICtrlTextboxValid = true;
        foreach (Control ctrl in panelShopInfoEdit.Controls)
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
        if (!CHKXBXCloseSaturday.Checked)
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
        TBShopInfoID.Text = shopInfoEntity.ShopInfoID;
        TBShopName.Text = shopInfoEntity.ShopInfoName;
        TBShopContact.Text = shopInfoEntity.ShopInfoContact;
        TBShopAddress.Text = shopInfoEntity.ShopInfoAddress;
        CHKBXGroomingService.Checked = shopInfoEntity.ShopInfoGrooming;
        DDLShopType.SelectedValue = shopInfoEntity.ShopInfoType;
        TBShopDesc.Text = shopInfoEntity.ShopInfoDesc;
        CHKBXCloseOnPublicHoliday.Checked = shopInfoEntity.ShopCloseOnPublicHoliday;

        // load operating hours
        List<string> workDay = new List<string>();
        List<string> noWorkDay = new List<string>();
        List<string> dayOfWeek = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        // get the work days
        foreach (ShopTimeEntity shopTimeEntity in shopInfoEntity.ShopTimeEntities)
        {
            workDay.Add(shopTimeEntity.DayOfWeek);
        }
        // get the non working days
        foreach (string day in dayOfWeek)
        {
            if (Array.IndexOf(workDay.ToArray(), day) == -1)
            {
                noWorkDay.Add(day);
            }
        }
        // set the UI controls
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
                    CHKXBXCloseSaturday.Checked = true;
                    break;
                case "SUNDAY":
                    CHKBXCloseSunday.Checked = true;
                    break;

            }
        }

    }

    // Clear temp data
    private void clearTempData()
    {
        dTableOld = null;
        dTableEdit = null;
        photoEntities = null;
    }
    #endregion



}