using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.EnumFolder;
using TheLittleOnesLibrary.Handler;

public partial class AdminShopInfoAdd : BasePage
{
    private Label UICtrlLabel;
    private TextBox UICtrlTextbox;
    private DropDownList UICtrlDropdownlist;

    private string shopName;
    private string shopContact;
    private string shopAddress;
    private bool shopGrooming;
    private string shopType;
    private string shopDesc;
    private bool shopCloseOnPublicHoliday;
    private List<ShopTimeEntity> shopTimeEntities;
    private List<PhotoEntity> photoEntities;
    private static string filePath_UploadFolderTemp;

    // page load
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (IsPostBack)
        {

        }
        else
        {
            initializeUIControlValues();
        }
    }


    #region Initialize UI Control Values
    // Initial UI control values
    private void initializeUIControlValues()
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        // initialize hour range
        List<string> timeInterval = setupHourRange();
        // loop over for controls
        foreach (Control ctrl in UpdatePanel1.ContentTemplateContainer.Controls)
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

    protected void BTNAdd_Click(object sender, EventArgs e)
    {
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
            // check if shop info exists
            if (shopInfoCtrler.checkOutletExist(shopAddress))
            {
                // exists
                MessageHandler.ErrorMessage(LBLErrorMsg, "Shop info already exists");
            }
            else
            {
                // create shop info entity
                shopInfoEntity = new ShopInfoEntity(shopName, shopContact,
                    shopAddress, shopGrooming, shopType, shopDesc, shopCloseOnPublicHoliday, shopTimeEntities, photoEntities);

                // change photo path to database instead of using temp
                if (photoEntities != null)
                {
                    shopInfoEntity.PhotoEntities = photoCtrler.changePhotoPathToDatabaseFolder(photoEntities, filePath_UploadFolderTemp);
                }

                // add into database
                shopInfoEntity = shopInfoCtrler.createShopInfo(shopInfoEntity);
                shopInfoEntity = shopInfoCtrler.createShopTime(shopInfoEntity);
                if (shopInfoEntity.PhotoEntities != null)
                    shopInfoEntity = shopInfoCtrler.createPetPhoto(shopInfoEntity);

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
    }

    protected void BTNGenerate_Click(object sender, EventArgs e)
    {
        ShopInfoEntity shopInfoEntityTemp = Utility.getShopInfoEntity();
        shopName = TBShopName.Text = shopInfoEntityTemp.ShopInfoName;
        shopContact = TBShopContact.Text = shopInfoEntityTemp.ShopInfoContact;
        shopAddress = TBShopAddress.Text = shopInfoEntityTemp.ShopInfoAddress;
        shopDesc = TBShopDesc.Text = shopInfoEntityTemp.ShopInfoDesc;

        CHKBXGroomingService.Checked = shopInfoEntityTemp.ShopInfoGrooming.Equals("yes") ? true : false;
        DDLShopType.SelectedIndex = shopInfoEntityTemp.ShopInfoType.Equals(ShopType.PetShop.ToString()) ? 1 : 2;


    }
    #endregion

    #region Logical Methods
    /// <summary>
    /// Source from https://forums.asp.net/t/2000851.aspx?24+hours+time+format
    /// User: a2h
    /// Purpose: preload time interval and bind to drop down list
    /// </summary>
    private List<string> setupHourRange()
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);

        // defualt start time value
        DateTime start = DateTime.ParseExact("00:00", "HH:mm", null);
        // default end time value
        DateTime end = DateTime.ParseExact("23:59", "HH:mm", null);

        //set the interval time 
        int interval = 30;
        //list to hold the values of intervals
        List<string> listTimeIntervals = new List<string>();
        //populate the list with the interval values
        for (DateTime i = start; i <= end; i = i.AddMinutes(interval))
            listTimeIntervals.Add(i.ToString("HH:mm tt"));

        return listTimeIntervals;
        ////Assign the list to datasource of dropdownlist
        //DropDownList1.DataSource = listTimeIntervals;
        ////Databind the dropdownlist
        //DropDownList1.DataBind();
    }

    private bool checkRequiredFields()
    {
        bool isUICtrlDropdownlistValid = true;
        bool isUICtrlTextboxValid = true;
        foreach (Control ctrl in UpdatePanel1.ContentTemplateContainer.Controls)
        {
            // check all drop down lists
            if (ctrl is DropDownList)
            {
                UICtrlDropdownlist = (DropDownList)ctrl;

                if (UICtrlDropdownlist.ID.ToLower().Contains("shoptype") && UICtrlDropdownlist.SelectedIndex == 0)
                {
                    isUICtrlDropdownlistValid = false;
                    LogController.LogLine("Error control: " + UICtrlDropdownlist.ID);
                }
            }
            // check all text boxes
            if (ctrl is TextBox)
            {
                UICtrlTextbox = (TextBox)ctrl;
                if (string.IsNullOrEmpty(UICtrlTextbox.Text.Trim()))
                {
                    isUICtrlTextboxValid = false;
                    LogController.LogLine("Error control: " + UICtrlTextbox.ID);
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

    private List<ShopTimeEntity> getShopTime()
    {
        shopTimeEntities = new List<ShopTimeEntity>();
        shopTimeEntity = new ShopTimeEntity("Monday", DDLOpenTimeMonday.SelectedValue, DDLCloseTimeMonday.SelectedValue);
        shopTimeEntities.Add(shopTimeEntity);
        shopTimeEntity = new ShopTimeEntity("Tuesday", DDLOpenTimeTuesday.SelectedValue, DDLCloseTimeTuesday.SelectedValue);
        shopTimeEntities.Add(shopTimeEntity);
        shopTimeEntity = new ShopTimeEntity("Wednesday", DDLOpenTimeWednesday.SelectedValue, DDLCloseTimeWednesday.SelectedValue);
        shopTimeEntities.Add(shopTimeEntity);
        shopTimeEntity = new ShopTimeEntity("Thursday", DDLOpenTimeThursday.SelectedValue, DDLCloseTimeThursday.SelectedValue);
        shopTimeEntities.Add(shopTimeEntity);
        shopTimeEntity = new ShopTimeEntity("Friday", DDLOpenTimeFriday.SelectedValue, DDLCloseTimeFriday.SelectedValue);
        shopTimeEntities.Add(shopTimeEntity);
        shopTimeEntity = new ShopTimeEntity("Saturday", DDLOpenTimeSaturday.SelectedValue, DDLCloseTimeSaturday.SelectedValue);
        shopTimeEntities.Add(shopTimeEntity);
        shopTimeEntity = new ShopTimeEntity("Sunday", DDLOpenTimeSunday.SelectedValue, DDLCloseTimeSunday.SelectedValue);
        shopTimeEntities.Add(shopTimeEntity);
        return shopTimeEntities;
    }


    #endregion



}