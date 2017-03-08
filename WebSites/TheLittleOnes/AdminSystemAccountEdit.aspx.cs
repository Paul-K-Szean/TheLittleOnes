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

public partial class AdminSystemAccountEdit : BasePage
{

    private Label UICtrlLabel;
    private TextBox UICtrlTextbox;
    private CheckBox UICtrlCheckbox;
    private DropDownList UICtrlDropdownlist;

    private static int GVRowID;
    private static int gvPageSize = 5; // default
    private static string filePath_UploadFolderTemp;

    private static DataTable dTableAccountInfo;
    protected void Page_Load(object sender, EventArgs e)
    {
        gvPageSize = int.Parse(DDLDisplayRecordCountSystemAccount.SelectedValue);
        GVSystemAccountOverview.PageSize = gvPageSize;
        if (IsPostBack) { }
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
        ListItem firstItem = DDLAccountType.Items[0];
        DDLAccountType.Items.Clear();
        DDLAccountType.Items.Add(firstItem);
        DDLAccountType.Items.Add(new ListItem(AccountType.WebAdmin.ToString(), AccountType.WebAdmin.ToString()));
        DDLAccountType.Items.Add(new ListItem(AccountType.WebShelterGroup.ToString(), AccountType.WebShelterGroup.ToString()));
        DDLAccountType.Items.Add(new ListItem(AccountType.WebSponsorGroup.ToString(), AccountType.WebSponsorGroup.ToString()));
        DDLAccountType.Items.Add(new ListItem(AccountType.WebUser.ToString(), AccountType.WebUser.ToString()));
    }

    #endregion

    #region Button Clicks
    // Preview image uploaded
    protected void BTNPreview_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        MessageHandler.ClearMessage(LBLErrorMsg);
        // create temp files in temp foler
        photoEntities = photoCtrler.saveToTempFolder(PhotoPurpose.ProfileInfo.ToString(), FileUpload1);
        // preview photo
        photoCtrler.previewPhotos(photoPreview);
    }

    protected void BTNUpdate_Click(object sender, EventArgs e)
    {

    }
    protected void BTNCancel_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region Dropdownlist controls
    protected void DDLDisplayRecordCountSystemAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        filterAccountInfo();
    }
    protected void DDLFilterAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        filterAccountInfo();
    }
    protected void DDLShopInfo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DDLAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    // Dropdownlist shop info
    protected void DDLOrangisation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        shopInfoEntity = shopInfoCtrler.getShopInfo(DDLShopInfo.SelectedValue);
        loadShopInfo(shopInfoEntity);
    }
    #endregion

    #region Gridview
    protected void GVSystemAccountOverview_DataBound(object sender, EventArgs e)
    {

    }
    protected void GVSystemAccountOverview_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        GridViewRow row = GVSystemAccountOverview.Rows[e.NewSelectedIndex];
        GVRowID = Convert.ToInt32(GVSystemAccountOverview.DataKeys[row.RowIndex].Values[0]);
        accountEntity = accCtrler.getAccount(GVRowID.ToString());
        clearStaticData();
        initializeUIControlValues();
        loadAccountInfo(accountEntity);
    }
    protected void GVSystemAccountOverview_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GVSystemAccountOverview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVSystemAccountOverview.PageIndex = e.NewPageIndex;
        filterAccountInfo();
    }
    #endregion

    #region Logical Methods
    // Load Account info
    private void loadAccountInfo(AccountEntity accountEntity)
    {
        PNLSystemAccountEdit.Visible = true;
        // shop info
        if (accountEntity.ShopInfoEntity != null)
        {
            LblOrganisation.Text = "Organisation";
            loadShopInfo(accountEntity.ShopInfoEntity);
        }
        else
        {
            LblOrganisation.Text = "Organisation - No Data";
            DDLShopInfo.SelectedIndex = 0;
            LBLShopName.Text = string.Empty;
            LBLShopInfoContact.Text = string.Empty;
            LBLShopInfoAddress.Text = string.Empty;
            LBLShopInfoDesc.Text = string.Empty;
        }
        // account info
        TBAccountID.Text = accountEntity.AccountID;
        TBAccountEmail.Text = accountEntity.AccountEmail;
        DDLAccountType.SelectedValue = accountEntity.AccountType;
        // profile info
        TBProfileID.Text = accountEntity.ProfileEntity.ProfileID;
        TBProfileName.Text = accountEntity.ProfileEntity.ProfileName;
        TBProfileContact.Text = accountEntity.ProfileEntity.ProfileContact;
        TBProfileAddress.Text = accountEntity.ProfileEntity.ProfileAddress;

    }
    // Shop Info
    private void loadShopInfo(ShopInfoEntity shopInfoEntity)
    {
        PNLShopInfoDetails.Visible = true;
        // shop info
        DDLShopInfo.SelectedValue = shopInfoEntity.ShopInfoID;
        LBLShopName.Text = shopInfoEntity.ShopInfoName;
        LBLShopInfoContact.Text = shopInfoEntity.ShopInfoContact;
        LBLShopInfoAddress.Text = shopInfoEntity.ShopInfoAddress;
        LBLShopInfoDesc.Text = shopInfoEntity.ShopInfoDesc;
    }
    // Filter data
    public void filterAccountInfo()
    {
        string filterAccountType = DDLFilterAccountType.SelectedValue.Trim();
        string tbSearchValue = TBSearchSystemAccount.Text;
        dTableAccountInfo = accCtrler.filterAccountInfoData(filterAccountType, tbSearchValue, LBLSearchResultSystemAccount);
        GVSystemAccountOverview.DataSourceID = null;
        GVSystemAccountOverview.DataSource = null;
        GVSystemAccountOverview.DataSource = dTableAccountInfo;
        GVSystemAccountOverview.DataBind();
    }
    // Clear temp data
    private void clearStaticData()
    {
        GVRowID = 0;
        dTableAccountInfo = null;
        photoEntities = null;
        photoPreview.InnerHtml = string.Empty;
    }
    #endregion


    #region Textbox Control
    protected void TBSearchSystemAccount_TextChanged(object sender, EventArgs e)
    {

    }
    #endregion
}