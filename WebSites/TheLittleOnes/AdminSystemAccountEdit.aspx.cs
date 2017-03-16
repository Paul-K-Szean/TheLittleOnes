using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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

    private static DataTable dTableAccountInfo;

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
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
        ListItem firstItemDDLAccountType = DDLAccountType.Items[0];
        DDLAccountType.Items.Clear();
        DDLAccountType.Items.Add(firstItemDDLAccountType);
        DDLAccountType.Items.Add(new ListItem(AccountType.WebAdmin.ToString(), AccountType.WebAdmin.ToString()));
        DDLAccountType.Items.Add(new ListItem(AccountType.WebShelterGroup.ToString(), AccountType.WebShelterGroup.ToString()));
        DDLAccountType.Items.Add(new ListItem(AccountType.WebSponsorGroup.ToString(), AccountType.WebSponsorGroup.ToString()));
        DDLAccountType.Items.Add(new ListItem(AccountType.WebUser.ToString(), AccountType.WebUser.ToString()));

        if (DDLShopInfo.Items.Count <= 1)
        {
            ListItem firstItemDDLShopInfo = DDLShopInfo.Items[0];
            DDLShopInfo.Items.Clear();
            DDLShopInfo.Items.Add(firstItemDDLShopInfo);
            DDLShopInfo.DataBind();
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
        editPhotoEntities = photoCtrler.saveToTempFolder(PhotoPurpose.ProfileInfo.ToString(), FileUpload1);
        // preview photo
        photoCtrler.previewPhotos(photoPreview);
    }
    // Update account
    protected void BTNUpdate_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        if (checkRequiredFields())
        {
            MessageHandler.ClearMessage(LBLErrorMsg);
            string accountID = TBAccountID.Text.Trim();
            string accountEmail = TBAccountEmail.Text.Trim();
            string accountType = DDLAccountType.SelectedValue;
            string accountDateJoined = editAccountEntity.DateJoined.ToString();
            string profileID = TBProfileID.Text.Trim();
            string profileName = TBProfileName.Text.Trim();
            string profileContact = TBProfileContact.Text.Trim();
            string profileAddress = TBProfileAddress.Text.Trim();
            // reset old password
            string accountPassword = CHKBXReset.Checked ? profileName.ToLower().Replace(" ", "") : editAccountEntity.AccountPassword;
            editShopInfoEntity = shopInfoCtrler.getShopInfo(DDLShopInfo.SelectedValue.ToString());

            // create entities
            editProfileEntity = new ProfileEntity(profileID, profileName, profileContact, profileAddress, editPhotoEntities);
            editAccountEntity = new AccountEntity(accountID, accountEmail, accountPassword, accountType, editProfileEntity, editShopInfoEntity, DateTime.Parse(accountDateJoined));


            // update into database
            editAccountEntity = accountCtrler.updateSystemAccount(editAccountEntity);
            editProfileEntity = profileCtrler.updateSystemProfile(editProfileEntity);

            // update photo
            if (editPhotoEntities != null)
            {
                // change photo path to database instead of using temp
                editProfileEntity.PhotoEntities = photoCtrler.changePhotoPathToDatabaseFolder(editPhotoEntities, editProfileEntity.ProfileID);
                // remove old photos from database
                photoCtrler.deletePhoto(editProfileEntity.ProfileID, PhotoPurpose.ProfileInfo.ToString());
                // create new photos into database
                photoCtrler.createPhoto(editPhotoEntities, editProfileEntity.ProfileID);
            }
            if (editAccountEntity != null)
            {
                MessageHandler.SuccessMessage(LBLErrorMsg, "Account info successfully updated");
            }
            else
            {
                MessageHandler.ErrorMessageAdmin(LBLErrorMsg, "Account info was not successfully updated");
            }
            GVSystemAccountOverview.DataBind();
            DLPhotoUploaded.DataBind();
            filterAccountInfo();
            clearStaticData();
        }
    }
    protected void BTNCancel_Click(object sender, EventArgs e)
    {
        PNLSystemAccountEdit.Visible = false;
        clearUIControlValues(PNLSystemAccountEdit.Controls);
        LBLErrorMsg.Text = string.Empty;
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
        if (DDLAccountType.SelectedValue.Equals(AccountType.WebShelterGroup.ToString()) ||
            DDLAccountType.SelectedValue.Equals(AccountType.WebSponsorGroup.ToString()))
        {
            if (!DDLShopInfo.Enabled)
            { // if disabled
                DDLShopInfo.Enabled = true;
            }
        }
        else
        {
            DDLShopInfo.SelectedIndex = 0;
            DDLShopInfo.Enabled = false;
            editShopInfoEntity = null;
            PNLShopInfoDetails.Visible = false;
        }
    }
    // Dropdownlist shop info
    protected void DDLOrangisation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);

        if (DDLShopInfo.SelectedIndex == 0)
        {
            PNLShopInfoDetails.Visible = false;
            editShopInfoEntity = null;
        }
        else
        {
            PNLShopInfoDetails.Visible = true;
            editShopInfoEntity = shopInfoCtrler.getShopInfo(DDLShopInfo.SelectedValue);
            loadShopInfo(editShopInfoEntity);
        }

    }
    #endregion

    #region Gridview
    protected void GVSystemAccountOverview_DataBound(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        if (dTableAccountInfo == null)
            dTableAccountInfo = ((DataView)SDSSystemAccount.Select(DataSourceSelectArguments.Empty)).Table;
        updateEntryCount(dTableAccountInfo, GVSystemAccountOverview, LBLEntriesCount);
    }
    protected void GVSystemAccountOverview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Retrieve the underlying data item. 
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            // Retrieve the key value for the current row. 
            string selectedAccountID = rowView["accountID"].ToString();
            if (accountEntity.AccountID.Equals(selectedAccountID))
            {
                
                e.Row.Cells[GVSystemAccountOverview.Columns.Count-1].CssClass = "hide";
            }
        }

    }
    protected void GVSystemAccountOverview_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        clearStaticData();
        MessageHandler.ClearMessage(LBLErrorMsg);
        GridViewRow row = GVSystemAccountOverview.Rows[e.NewSelectedIndex];
        GVRowID = Convert.ToInt32(GVSystemAccountOverview.DataKeys[row.RowIndex].Values[0]);
        editAccountEntity = accountCtrler.getAccount(GVRowID.ToString());
        initializeUIControlValues();
        loadAccountInfo(editAccountEntity);
    }
    protected void GVSystemAccountOverview_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        highlightSelectedRow(GVSystemAccountOverview);
        MessageHandler.ClearMessage(LBLErrorMsg);
    }
    protected void GVSystemAccountOverview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVSystemAccountOverview.PageIndex = e.NewPageIndex;
        filterAccountInfo();
    }
    #endregion

    #region Logical Methods
    // Check Required Fields
    private bool checkRequiredFields()
    {
        bool isAccountTypeValid = true;
        bool isAccountEmailValid = true;
        bool isProfileNameValid = true;
        bool isProfileContactValid = true;
        bool isShopInfoValid = true;

        string accountType = DDLAccountType.SelectedValue.Trim();
        string accountEmail = TBAccountEmail.Text.Trim();
        string profileName = TBProfileName.Text.Trim();
        string profileContact = TBProfileContact.Text.Trim();

        if (DDLAccountType.SelectedIndex == 0)
        {
            isAccountTypeValid = false;
        }

        if (string.IsNullOrEmpty(accountEmail))
            isAccountEmailValid = false;

        if (string.IsNullOrEmpty(profileName))
            isProfileNameValid = false;

        if (!string.IsNullOrEmpty(profileContact))
        {
            isProfileContactValid = !(profileContact.Any(x => char.IsLetter(x)));
        }
        if (isProfileContactValid)
        {
            MessageHandler.DefaultMessage(LBLProfileContact, "Contact");
        }
        else
        {
            MessageHandler.ErrorMessage(LBLProfileContact, "Contact - Only digits allowed!");
        }

        if (DDLShopInfo.SelectedIndex == 0 &&
           (accountType.Equals(AccountType.WebShelterGroup.ToString()) || accountType.Equals(AccountType.WebSponsorGroup.ToString())))
        {
            isShopInfoValid = false;
        }

        // return condition
        if (isAccountTypeValid && isAccountEmailValid && isProfileNameValid && isProfileContactValid && isShopInfoValid)
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
    // Load Account info
    private void loadAccountInfo(AccountEntity editAccountEntity)
    {
        if (editAccountEntity != null)
        {
            PNLSystemAccountEdit.Visible = true;
            PNLShopInfoDetails.Visible = true;
            // account info
            TBAccountID.Text = editAccountEntity.AccountID;
            TBAccountEmail.Text = editAccountEntity.AccountEmail;
            DDLAccountType.SelectedValue = editAccountEntity.AccountType;
            TBDateJoined.Text = editAccountEntity.DateJoined.ToString("dd MMM yyy");
            // profile info
            TBProfileID.Text = editAccountEntity.ProfileEntity.ProfileID;
            TBProfileName.Text = editAccountEntity.ProfileEntity.ProfileName;
            TBProfileContact.Text = editAccountEntity.ProfileEntity.ProfileContact;
            TBProfileAddress.Text = editAccountEntity.ProfileEntity.ProfileAddress;
            // shop info
            if (editAccountEntity.ShopInfoEntity != null)
            {
                DDLShopInfo.Enabled = true;
                LblOrganisation.Text = "Organisation";
                loadShopInfo(editAccountEntity.ShopInfoEntity);
            }
            else
            {
                DDLShopInfo.SelectedIndex = 0;
                PNLShopInfoDetails.Visible = false;
                if (editAccountEntity.AccountType.Equals(AccountType.WebAdmin.ToString()) || editAccountEntity.AccountType.Equals(AccountType.WebUser.ToString()))
                {
                    LblOrganisation.Text = "Organisation - Not Applicable";
                    DDLShopInfo.Enabled = false;
                }
                else
                {
                    LblOrganisation.Text = "Organisation - No Data";
                    DDLShopInfo.Enabled = true;
                }
                LBLShopName.Text = string.Empty;
                LBLShopInfoContact.Text = string.Empty;
                LBLShopInfoAddress.Text = string.Empty;
                LBLShopInfoDesc.Text = string.Empty;
            }
        }
    }
    // Shop Info
    private void loadShopInfo(ShopInfoEntity editShopInfoEntity)
    {
        // shop info
        DDLShopInfo.SelectedValue = editShopInfoEntity.ShopInfoID;
        LBLShopName.Text = editShopInfoEntity.ShopInfoName;
        LBLShopInfoContact.Text = editShopInfoEntity.ShopInfoContact;
        LBLShopInfoAddress.Text = editShopInfoEntity.ShopInfoAddress;
        LBLShopInfoDesc.Text = editShopInfoEntity.ShopInfoDesc;
    }
    // Filter data
    public void filterAccountInfo()
    {
        string filterAccountType = DDLFilterAccountType.SelectedValue.Trim();
        string tbSearchValue = TBSearchSystemAccount.Text.Trim();
        dTableAccountInfo = accountCtrler.filterAccountInfoData(filterAccountType, tbSearchValue, LBLSearchResultSystemAccount);
        GVSystemAccountOverview.DataSourceID = null;
        GVSystemAccountOverview.DataSource = null;
        GVSystemAccountOverview.DataSource = dTableAccountInfo;
        GVSystemAccountOverview.DataBind();
        DLPhotoUploaded.DataBind();
    }
    // Clear temp data
    private void clearStaticData()
    {
        GVRowID = 0;
        dTableAccountInfo = null;
        editPhotoEntities = null;
        photoPreview.InnerHtml = string.Empty;
    }
    #endregion

    #region Textbox Control
    protected void TBSearchSystemAccount_TextChanged(object sender, EventArgs e)
    {
        filterAccountInfo();
    }
    #endregion


}