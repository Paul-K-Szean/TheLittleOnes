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

public partial class AdminAdoptionInfoAdd : BasePage
{
    private Label UICtrlLabel;
    private TextBox UICtrlTextbox;
    private CheckBox UICtrlCheckbox;
    private DropDownList UICtrlDropdownlist;
    private static List<PhotoEntity> photoEntities;
    private static string filePath_UploadFolderTemp;

    protected void Page_Load(object sender, EventArgs e)
    {


    }
    #region Button Control
    // Preview image uploaded
    protected void BTNPreview_Click(object sender, EventArgs e)
    {
        string adoptPetBreed = DDLPetBreed.SelectedValue;
        string adoptPetName = TBPetName.Text.Trim();

        // some variable to create folder
        if (!string.IsNullOrEmpty(adoptPetBreed) && !string.IsNullOrEmpty(adoptPetName))
        {
            MessageHandler.ClearMessage(LBLErrorMsg);
            filePath_UploadFolderTemp = string.Concat("~/uploadedFiles/temp/adoptinfo/", adoptPetBreed.ToLower().Replace(" ", "") + "/" + adoptPetName.ToLower().Replace(" ", "").ToString());
            LogController.LogLine("filePath_UploadFolderTemp: " + filePath_UploadFolderTemp);

            // create temp files in temp foler
            photoEntities = photoCtrler.saveToTempFolder(PhotoPurpose.Pet.ToString(),FileUpload1, filePath_UploadFolderTemp);

            // preview photo
            photoCtrler.previewPhotos(photoPreview, filePath_UploadFolderTemp);
        }
        else
        {
            MessageHandler.ErrorMessage(LBLErrorMsg, "Breed and name cannot be empty");
        }
    }

    protected void BTNAdd_Click(object sender, EventArgs e)
    {
        string adoptPetBreed = DDLPetBreed.SelectedValue;
        string adoptPetName = TBPetName.Text.Trim();


        if (checkRequiredFields())
        {
            if (adoptInfoCtrler.checkAdoptInfoExist(adoptPetBreed, adoptPetName))
            {
                MessageHandler.ErrorMessage(LBLErrorMsg, "Adoption info already exist");
            }
            else
            {
                string shopInfoID = DDLOrangisation.SelectedValue;
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

                string adoptInfoStatus = AdoptionStatus.Available.ToString();
                // ccreate entities
                shopInfoEntity = shopInfoCtrler.getShopInfo(shopInfoID);
                petEntity = new PetEntity(petBreed, petName, petGender, petWeight, petSize, petDesc, petEnergy, petFriendlyWithPet, petFriendlyWithPeople, petToiletTrained, petHealthInfo, photoEntities);
                adoptInfoEntity = new AdoptInfoEntity(shopInfoEntity, petEntity, adoptInfoStatus);

                // change photo path to database instead of using temp
                if (photoEntities != null)
                {
                    petEntity.PhotoEntities = photoCtrler.changePhotoPathToDatabaseFolder(photoEntities, filePath_UploadFolderTemp);
                }

                // add into database
                petEntity = petCtrler.createPet(petEntity);
                adoptInfoEntity = adoptInfoCtrler.createAdoptInfo(adoptInfoEntity);
                if (petEntity.PhotoEntities != null)
                    petEntity = petCtrler.createPhoto(petEntity);

                if (adoptInfoEntity != null)
                {
                    MessageHandler.SuccessMessage(LBLErrorMsg, "Adoption info successfully added");
                }
                else
                {
                    MessageHandler.ErrorMessageAdmin(LBLErrorMsg, "Adoption info was not successfully added");
                }
            }
        }
    }

    protected void BTNGenerate_Click(object sender, EventArgs e)
    {
        MessageHandler.ClearMessage(LBLErrorMsg);
        Random rnd = new Random();
        // random ddl values
        foreach (Control ctrl in UpdatePanel1.ContentTemplateContainer.Controls)
        {
            if (ctrl is DropDownList)
            {
                UICtrlDropdownlist = (DropDownList)ctrl;
                if (UICtrlDropdownlist.Enabled == true)
                    UICtrlDropdownlist.SelectedIndex = rnd.Next(1, UICtrlDropdownlist.Items.Count);
            }
        }
        // random text box values
        TBPetName.Text = DDLPetBreed.SelectedItem.Text + rnd.Next(40);
        TBPetWeight.Text = rnd.Next(12, 30).ToString();
        TBPetDesc.Text = "I am " + TBPetName.Text + ". I belong to " + DDLPetBreed.SelectedItem.Text + " and weight only " + TBPetWeight.Text + "KG";
        DDLPetSize.SelectedIndex = rnd.Next(1, DDLPetSize.Items.Count);
        loadShopInfo(shopInfoCtrler.getShopInfo(DDLOrangisation.SelectedValue));
        PNLShopInfoDetails.Visible = true;
        DDLPetGender.SelectedIndex = rnd.Next(1, DDLPetGender.Items.Count);
        DDLPetEnergy.SelectedIndex = rnd.Next(1, DDLPetEnergy.Items.Count);
        DDLPetFriendlyWithPet.SelectedIndex = rnd.Next(1, DDLPetFriendlyWithPet.Items.Count);
        DDLPetFriendlyWithPeople.SelectedIndex = rnd.Next(1, DDLPetFriendlyWithPeople.Items.Count);
        DDLPetToiletTrain.SelectedIndex = rnd.Next(1, DDLPetToiletTrain.Items.Count);
        TBPetHealthInfo.Text = "I am " + TBPetName.Text + "! Hi, am i really healthy? ";
        photoPreview.InnerHtml = string.Empty;
    }
    #endregion

    #region Dropdownlist Control
    protected void DDLOrangisation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        shopInfoEntity = shopInfoCtrler.getShopInfo(DDLOrangisation.SelectedValue);
        loadShopInfo(shopInfoEntity);
        PNLShopInfoDetails.Visible = true;
    }
    #endregion

    #region Logical Methods
    // Shop Info
    private void loadShopInfo(ShopInfoEntity shopInfoEntity)
    {
        // shop info
        LBLShopName.Text = shopInfoEntity.ShopInfoName;
        LBLShopInfoContact.Text = shopInfoEntity.ShopInfoContact;
        LBLShopInfoAddress.Text = shopInfoEntity.ShopInfoAddress;
        LBLShopInfoDesc.Text = shopInfoEntity.ShopInfoDesc;

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
    #endregion


}