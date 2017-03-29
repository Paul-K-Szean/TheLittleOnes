using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.EnumFolder;
using TheLittleOnesLibrary.Handler;
public partial class AdminShopInfoAdd : BasePageAdmin
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
    // page load
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (IsPostBack)
        {
        }
        else
        {
            // Initial UI control values
            initializeUIControlValues();
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
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        MessageHandler.ClearMessage(LBLErrorMsg);
        // create temp files in temp foler
        photoEntities = photoCtrler.saveToTempFolder(PhotoPurpose.ShopInfo.ToString(), FileUpload1);
        // preview photo
        photoCtrler.previewPhotos(photoPreview);
    }
    protected void BTNAdd_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        // get inputs
        shopName = TBShopName.Text.Trim();
        shopContact = TBShopContact.Text.Trim().Replace(" ", "");
        shopAddress = TBShopAddress.Text.Trim();
        shopGrooming = CHKBXGroomingService.Checked ? true : false;
        shopType = DDLShopType.SelectedValue;
        shopDesc = TBShopDesc.Text.Trim();
        shopCloseOnPublicHoliday = CHKBXCloseOnPublicHoliday.Checked ? true : false;
        shopTimeEntities = getShopTime();
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
                // add into database
                shopInfoEntity = shopInfoCtrler.createShopInfo(shopInfoEntity);
                shopInfoEntity = shopInfoCtrler.createShopTime(shopInfoEntity);
                // change photo path to database instead of using temp
                if (photoEntities != null)
                {
                    shopInfoEntity = shopInfoCtrler.createPhoto(shopInfoEntity);
                    shopInfoEntity.PhotoEntities = photoCtrler.changePhotoPathToDatabaseFolder(photoEntities, shopInfoEntity.ShopInfoID);
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
    }
    protected void BTNGenerate_Click(object sender, EventArgs e)
    {
        ShopInfoEntity shopInfoEntityTemp = Utility.getShopInfoEntity();
        shopName = TBShopName.Text = shopInfoEntityTemp.ShopInfoName;
        shopContact = TBShopContact.Text = shopInfoEntityTemp.ShopInfoContact.Replace(" ", "");
        shopAddress = TBShopAddress.Text = shopInfoEntityTemp.ShopInfoAddress;
        shopDesc = TBShopDesc.Text = shopInfoEntityTemp.ShopInfoDesc;
        CHKBXGroomingService.Checked = shopInfoEntityTemp.ShopInfoGrooming.Equals("yes") ? true : false;
        DDLShopType.SelectedIndex = shopInfoEntityTemp.ShopInfoType.Equals(ShopType.PetShop.ToString()) ? 1 : 2;
        CHKBXGroomingService.Enabled = (DDLShopType.SelectedIndex == 1) ? true : false;
    }
    #endregion
    #region Dropdownlist Controls
    protected void DDLShopType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLShopType.SelectedValue.Contains("Clinic") || DDLShopType.SelectedValue.Contains("Shelter"))
        {
            CHKBXGroomingService.Enabled = false;
            CHKBXGroomingService.Checked = false;
        }
    }
    #endregion
    #region Logical Methods
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
    #endregion
}