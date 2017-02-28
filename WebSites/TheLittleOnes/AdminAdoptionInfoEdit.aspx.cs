using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

public partial class AdminAdoptionInfoEdit : BasePage
{

    private Label UICtrlLabel;
    private TextBox UICtrlTextbox;
    private CheckBox UICtrlCheckbox;
    private DropDownList UICtrlDropdownlist;
    private static List<PhotoEntity> photoEntities;

    private static int GVRowID;
    private static int gvPageSize = 5; // default
    private static string filePath_UploadFolderTemp;

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
            // hide edit panel
            PNLAdoptInfoEdit.Visible = false;
            // clear static data
            clearStaticData();
        }
    }

    #region Button Control
    // Preview image uploaded
    protected void BTNPreview_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        string adoptPetBreed = DDLPetBreed.SelectedValue;
        string adoptPetName = TBPetName.Text.Trim();

        // some variable to create folder
        if (!string.IsNullOrEmpty(adoptPetBreed) && !string.IsNullOrEmpty(adoptPetName))
        {
            MessageHandler.ClearMessage(LBLErrorMsg);
            filePath_UploadFolderTemp = string.Concat("~/uploadedFiles/temp/adoptinfo/", adoptPetBreed.ToLower().Replace(" ", "") + "/" + adoptPetName.ToLower().Replace(" ", "").ToString());
            LogController.LogLine("filePath_UploadFolderTemp: " + filePath_UploadFolderTemp);

            // create temp files in temp foler
            photoEntities = photoCtrler.saveToTempFolder(PhotoPurpose.Pet.ToString(), FileUpload1, filePath_UploadFolderTemp);

            // preview photo
            photoCtrler.previewPhotos(photoPreview, filePath_UploadFolderTemp);
        }
        else
        {
            MessageHandler.ErrorMessage(LBLErrorMsg, "Breed and name cannot be empty");
        }
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
            adoptInfoEntity = new AdoptInfoEntity(adoptInfoID, shopInfoEntity, petEntity, adoptInfoStatus);

            // update entites
            adoptInfoEntity = adoptInfoCtrler.updateAdoptInfo(adoptInfoEntity);
            petEntity = petCtrler.updatePet(petEntity);
            // update photo
            if (photoEntities != null)
            {
                // change photo path to database instead of using temp
                petEntity.PhotoEntities = photoCtrler.changePhotoPathToDatabaseFolder(photoEntities, filePath_UploadFolderTemp);
                // remove old photos from database
                photoCtrler.deletePhoto(petEntity.PetID, PhotoPurpose.Pet.ToString());
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
            clearStaticData();


        }
    }

    protected void BTNCancel_Click(object sender, EventArgs e)
    {
        PNLAdoptInfoEdit.Visible = false;
        PNLShopInfoDetails.Visible = false;
        clearUIControlValues(PNLAdoptInfoEdit.Controls);
    }
    #endregion

    #region Dropdownlist Control
    protected void DDLDisplayRecordCountAdoptInfo_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvPageSize = int.Parse(DDLDisplayRecordCountAdoptInfo.SelectedValue);
        GVAdoptInfoOverview.PageSize = gvPageSize;
    }

    protected void DDLOrangisation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        shopInfoEntity = shopInfoCtrler.getShopInfo(DDLShopInfo.SelectedValue);
        loadShopInfo(shopInfoEntity);
        PNLShopInfoDetails.Visible = true;
    }
    #endregion

    #region Gridview Control
    protected void GVAdoptInfoOverview_DataBound(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        updateEntryCount(SDSAdoptInfo, GVAdoptInfoOverview, LBLEntriesCount);
    }

    protected void GVAdoptInfoOverview_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        GridViewRow row = GVAdoptInfoOverview.Rows[e.NewSelectedIndex];
        GVRowID = Convert.ToInt32(GVAdoptInfoOverview.DataKeys[row.RowIndex].Values[0]);
        adoptInfoEntity = adoptInfoCtrler.getAdoptInfo(GVRowID.ToString());
        clearStaticData();
        loadAdoptInfo(adoptInfoEntity);
    }

    protected void GVAdoptInfoOverview_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        highlightSelectedRow(GVAdoptInfoOverview);
        MessageHandler.ClearMessage(LBLErrorMsg);
    }
    #endregion Gridview Control

    #region Logical Methods
    // Adopt Info
    private void loadAdoptInfo(AdoptInfoEntity adoptInfoEntity)
    {
        TBAdoptInfoID.Text = adoptInfoEntity.AdoptInfoID;
        DDLAdoptInfoStatus.SelectedValue = adoptInfoEntity.AdoptInfoStatus;
        loadPet(adoptInfoEntity.PetEntity);
        loadShopInfo(adoptInfoEntity.ShopInfoEntity);
    }
    // Shop Info
    private void loadShopInfo(ShopInfoEntity shopInfoEntity)
    {
        PNLShopInfoDetails.Visible = true;
        // shop info
        if (DDLShopInfo.Items.Count <= 1) DDLShopInfo.DataBind();
        DDLShopInfo.SelectedValue = adoptInfoEntity.ShopInfoEntity.ShopInfoID;
        LBLShopName.Text = shopInfoEntity.ShopInfoName;
        LBLShopInfoContact.Text = shopInfoEntity.ShopInfoContact;
        LBLShopInfoAddress.Text = shopInfoEntity.ShopInfoAddress;
        LBLShopInfoDesc.Text = shopInfoEntity.ShopInfoDesc;
    }
    // Pet
    private void loadPet(PetEntity petEntity)
    {
        PNLAdoptInfoEdit.Visible = true;
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
        // pet photo
        // SDSPhoto.SelectCommand = string.Concat("SELECT * FROM PET INNER JOIN PHOTO ON PET.PETID = PHOTO.PHOTOOWNERID WHERE PET.PETID = ", petEntity.PetID, " AND PHOTO.PHOTOPURPOSE = ", PhotoPurpose.Pet.ToString());
        // DLPhotoUploaded.DataBind();

    }

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

    // Filter data
    private void filterData()
    {
        string filterGender = DDLFilterGender.SelectedValue;
        string filterSize = DDLFilterSize.SelectedValue;
        string filterStatus = DDLFilterStatus.SelectedValue;
        string tbSearchValue = TBSearchAdoptInfo.Text;

        GVAdoptInfoOverview.DataSourceID = null;
        GVAdoptInfoOverview.DataSource = null;
        GVAdoptInfoOverview.DataSource = adoptInfoCtrler.filterData(filterGender, filterSize, filterStatus, tbSearchValue, LBLSearchResultAdoptInfo);
        GVAdoptInfoOverview.DataBind();
    }

    // Clear temp data
    private void clearStaticData()
    {
        GVRowID = 0;
        photoEntities = null;
        photoPreview.InnerHtml = string.Empty;
        filePath_UploadFolderTemp = string.Empty;
    }

    #endregion

    #region Textbox control
    protected void TBSearchAdoptInfo_TextChanged(object sender, EventArgs e)
    {
        // Filter data
        filterData();
    }
    #endregion


    protected void DDLFilterSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Filter data
        filterData();
    }

    protected void DDLFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Filter data
        filterData();
    }

    protected void DDLFilterGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Filter data
        filterData();
    }

}