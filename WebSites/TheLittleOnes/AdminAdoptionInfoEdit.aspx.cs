﻿using System;
using System.Data;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.EnumFolder;
using TheLittleOnesLibrary.Handler;
public partial class AdminAdoptionInfoEdit : BasePageAdmin
{
    private Label UICtrlLabel;
    private TextBox UICtrlTextbox;
    private CheckBox UICtrlCheckbox;
    private DropDownList UICtrlDropdownlist;
    private static int GVRowID;
    private static int gvPageSize = 5; // default
    private static DataTable dTableAdoptInfo;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        gvPageSize = int.Parse(DDLDisplayRecordCountAdoptInfo.SelectedValue);
        GVAdoptInfoOverview.PageSize = gvPageSize;
        if (IsPostBack)
        {
        }
        else
        {
            // clear static data
            clearStaticData();
            if (accountEntity.ShopInfoEntity != null)
            {
                HDFShopID.Value = accountEntity.ShopInfoEntity.ShopInfoID;
            }
            else
            {
                SDSAdoptInfo.SelectCommand = "SELECT AdoptInfo.shopInfoID, AdoptInfo.petID, AdoptInfo.adoptInfoID, AdoptInfo.adoptInfoStatus, Pet.petID AS Expr1, Pet.petBreed, Pet.petName, Pet.petGender, Pet.petWeight, Pet.petSize, Pet.petDesc, Pet.petEnergy, Pet.petFriendlyWithPet, Pet.petFriendlyWithPeople, Pet.petToiletTrained, Pet.petHealthInfo, ShopInfo.shopInfoID AS Expr2, ShopInfo.shopInfoName, ShopInfo.shopInfoContact, ShopInfo.shopInfoAddress, ShopInfo.shopInfoGrooming, ShopInfo.shopInfoType, ShopInfo.shopInfoDesc, ShopInfo.shopInfoCloseOnPublicHoliday FROM ((AdoptInfo INNER JOIN Pet ON AdoptInfo.petID = Pet.petID) INNER JOIN ShopInfo ON AdoptInfo.shopInfoID = ShopInfo.shopInfoID)  ORDER BY AdoptInfo.adoptInfoID DESC";
                SDSAdoptInfo.DataBind();
            }
          
        }
    }
    #region Initialize UI Control Values
    // Initial UI control values
    private void initializeUIControlValues()
    {
        if (DDLShopInfo.Items.Count <= 1)
        {
            ListItem firstItem = DDLShopInfo.Items[0];
            DDLShopInfo.Items.Clear();
            DDLShopInfo.Items.Add(firstItem);
            DDLShopInfo.DataBind();
        }
    }
    #endregion
    #region Button Control
    // Preview image uploaded
    protected void BTNPreview_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        MessageHandler.ClearMessage(LBLErrorMsg);
        // create temp files in temp foler
        photoEntities = photoCtrler.saveToTempFolder(Enums.GetDescription(PhotoPurpose.Pet), FileUpload1);
        // preview photo
        photoCtrler.previewPhotos(photoPreview);
    }
    protected void BTNUpdate_Click(object sender, EventArgs e)
    {
        string shopInfoID = DDLShopInfo.SelectedValue;
        string adoptInfoID = TBAdoptInfoID.Text.Trim();
        string adoptInfoStatus = DDLAdoptInfoStatus.SelectedValue.Trim();
        string petID = TBPetID.Text.Trim();
        string petBreed = DDLPetBreed.SelectedValue.Trim();
        string petName = TBPetName.Text.Trim();
        string petGender = DDLPetGender.SelectedValue.Trim();
        string petWeight = TBPetWeight.Text.Trim();
        string petSize = DDLPetSize.SelectedValue.Trim();
        string petDesc = TBPetDesc.Text.Trim();
        string petEnergy = DDLPetEnergy.SelectedValue.Trim();
        string petFriendlyWithPet = DDLPetFriendlyWithPet.SelectedValue.Trim();
        string petFriendlyWithPeople = DDLPetFriendlyWithPeople.SelectedValue.Trim();
        string petToiletTrained = DDLPetToiletTrain.SelectedValue.Trim();
        string petHealthInfo = TBPetHealthInfo.Text.Trim();
        if (checkRequiredFields())
        {
            // create entities with new changes
            petEntity = new PetEntity(petID, petBreed, petName, petGender, petWeight, petSize, petDesc, petEnergy, petFriendlyWithPet,
                petFriendlyWithPeople, petToiletTrained, petHealthInfo, photoEntities);
            shopInfoEntity = shopInfoCtrler.getShopInfo(shopInfoID);
            adoptInfoEntity = new AdoptInfoEntity(shopInfoEntity, petEntity, adoptInfoID, adoptInfoStatus);
            // update into database
            adoptInfoEntity = adoptInfoCtrler.updateAdoptInfo(adoptInfoEntity);
            petEntity = petCtrler.updatePet(petEntity);
            // update photo
            if (photoEntities != null)
            {
                // change photo path to database instead of using temp
                petEntity.PhotoEntities = photoCtrler.changePhotoPathToDatabaseFolder(photoEntities, petEntity.PetID);
                // remove old photos from database
                photoCtrler.deletePhoto(petEntity.PetID, Enums.GetDescription(PhotoPurpose.Pet));
                // create new photos into database
                photoCtrler.createPhoto(photoEntities, petEntity.PetID);
            }
            if (petEntity != null)
            {
                MessageHandler.SuccessMessage(LBLErrorMsg, "Adoption info successfully updated");
            }
            else
            {
                MessageHandler.ErrorMessageAdmin(LBLErrorMsg, "Adoption info was not successfully updated");
            }
            GVAdoptInfoOverview.DataBind();
            DLPhotoUploaded.DataBind();
            filterAdoptionInfo();
            clearStaticData();
        }
    }
    protected void BTNCancel_Click(object sender, EventArgs e)
    {
        PNLAdoptInfoEdit.Visible = false;
        PNLShopInfoDetails.Visible = false;
        clearUIControlValues(PNLAdoptInfoEdit.Controls);
        LBLErrorMsg.Text = string.Empty;
    }
    #endregion
    #region Dropdownlist Control
    protected void DDLDisplayRecordCountAdoptInfo_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvPageSize = int.Parse(DDLDisplayRecordCountAdoptInfo.SelectedValue);
        GVAdoptInfoOverview.PageSize = gvPageSize;
        filterAdoptionInfo();
    }
    // Dropdownlist shop info
    protected void DDLOrangisation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        shopInfoEntity = shopInfoCtrler.getShopInfo(DDLShopInfo.SelectedValue);
        loadShopInfo(shopInfoEntity);
    }
    protected void DDLFilterSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Filter data
        filterAdoptionInfo();
    }
    protected void DDLFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Filter data
        filterAdoptionInfo();
    }
    protected void DDLFilterGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Filter data
        filterAdoptionInfo();
    }
    #endregion
    #region Gridview Control
    protected void GVAdoptInfoOverview_DataBound(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        if (dTableAdoptInfo == null)
            dTableAdoptInfo = ((DataView)SDSAdoptInfo.Select(DataSourceSelectArguments.Empty)).Table;
        updateEntryCount(dTableAdoptInfo, GVAdoptInfoOverview, LBLEntriesCount);
    }
    protected void GVAdoptInfoOverview_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        clearStaticData();
        MessageHandler.ClearMessage(LBLErrorMsg);
        GridViewRow row = GVAdoptInfoOverview.Rows[e.NewSelectedIndex];
        GVRowID = Convert.ToInt32(GVAdoptInfoOverview.DataKeys[row.RowIndex].Values[0]);
        adoptInfoEntity = adoptInfoCtrler.getAdoptInfoEntity(GVRowID.ToString());
        LogController.LogLine("GVRowID: " + GVRowID);
        initializeUIControlValues();
        loadAdoptInfo(adoptInfoEntity);
    }
    protected void GVAdoptInfoOverview_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        highlightSelectedRow(GVAdoptInfoOverview);
        MessageHandler.ClearMessage(LBLErrorMsg);
    }
    protected void GVAdoptInfoOverview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVAdoptInfoOverview.PageIndex = e.NewPageIndex;
        filterAdoptionInfo();
    }
    #endregion Gridview Control
    #region Logical Methods
    // Check Required Fields
    private bool checkRequiredFields()
    {
        bool isUICtrlDropdownlistValid = true;
        bool isUICtrlTextboxValid = true;
        foreach (Control ctrl in PNLAdoptInfoEdit.Controls)
        {
            // check all drop down lists
            if (ctrl is DropDownList)
            {
                UICtrlDropdownlist = (DropDownList)ctrl;
                if (UICtrlDropdownlist.SelectedIndex == 0 && UICtrlDropdownlist.Enabled == true)
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
    // Adopt Info
    private void loadAdoptInfo(AdoptInfoEntity adoptInfoEntity)
    {
        if (adoptInfoEntity != null)
        {
            PNLShopInfoDetails.Visible = true;
            PNLAdoptInfoEdit.Visible = true;
            TBAdoptInfoID.Text = adoptInfoEntity.AdoptInfoID;
            DDLAdoptInfoStatus.SelectedValue = adoptInfoEntity.AdoptInfoStatus;
            loadPet(adoptInfoEntity.PetEntity);
            loadShopInfo(adoptInfoEntity.ShopInfoEntity);
        }
        else
        {
            PNLShopInfoDetails.Visible = false;
            PNLAdoptInfoEdit.Visible = false;
        }
    }
    // Shop Info
    private void loadShopInfo(ShopInfoEntity shopInfoEntity)
    {
        // shop info
        DDLShopInfo.SelectedValue = shopInfoEntity.ShopInfoID;
        LBLShopName.Text = shopInfoEntity.ShopInfoName;
        LBLShopInfoContact.Text = shopInfoEntity.ShopInfoContact;
        LBLShopInfoAddress.Text = shopInfoEntity.ShopInfoAddress;
        LBLShopInfoDesc.Text = shopInfoEntity.ShopInfoDesc;
    }
    // Pet
    private void loadPet(PetEntity petEntity)
    {
        // pet
        HDFPetID.Value = TBPetID.Text = petEntity.PetID;
        if (DDLPetBreed.Items.Count <= 1) DDLPetBreed.DataBind();
        DDLPetBreed.SelectedValue = petEntity.PetBreed;
        TBPetName.Text = petEntity.PetName;
        DDLPetGender.SelectedValue = petEntity.PetGender;
        TBPetWeight.Text = petEntity.PetWeight;
        DDLPetSize.SelectedValue = petEntity.PetSize;
        TBPetDesc.Text = petEntity.PetDesc;
        DDLPetEnergy.SelectedValue = petEntity.PetEnergy;
        DDLPetFriendlyWithPet.SelectedValue = petEntity.PetFriendlyWithPet;
        DDLPetFriendlyWithPeople.SelectedValue = petEntity.PetFriendlyWithPeople;
        DDLPetToiletTrain.SelectedValue = petEntity.PetToiletTrained;
        TBPetHealthInfo.Text = petEntity.PetHealthInfo;
    }
    // Filter data for adoption info
    private void filterAdoptionInfo()
    {
        string filterGender = DDLFilterGender.SelectedValue;
        string filterSize = DDLFilterSize.SelectedValue;
        string filterStatus = DDLFilterStatus.SelectedValue;
        string tbSearchValue = TBSearchAdoptInfo.Text;
        dTableAdoptInfo = adoptInfoCtrler.filterAdoptionInfoData(HDFShopID.Value, filterGender, filterSize, filterStatus, tbSearchValue, LBLSearchResultAdoptInfo);
        GVAdoptInfoOverview.DataSourceID = null;
        GVAdoptInfoOverview.DataSource = null;
        GVAdoptInfoOverview.DataSource = dTableAdoptInfo;
        GVAdoptInfoOverview.DataBind();
        DLPhotoUploaded.DataBind();
        adoptInfoEntity = null;
    }
    // Clear temp data
    private void clearStaticData()
    {
        GVRowID = 0;
        dTableAdoptInfo = null;
        photoEntities = null;
        photoPreview.InnerHtml = string.Empty;
    }
    #endregion
    #region Textbox control
    protected void TBSearchAdoptInfo_TextChanged(object sender, EventArgs e)
    {
        // Filter data
        filterAdoptionInfo();
    }
    #endregion
}
