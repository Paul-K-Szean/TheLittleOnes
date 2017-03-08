﻿using System;
using System.Collections.Generic;
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

public partial class AdminSystemAccountAdd : BasePage
{

    private Label UICtrlLabel;
    private TextBox UICtrlTextbox;
    private DropDownList UICtrlDropdownlist;

    private string profileID;

    private static string filePath_UploadFolderTemp;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) { }
        else
        {
            initializeUIControlValues();
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

    #region Button Click
    protected void BTNAdd_Click(object sender, EventArgs e)
    {
        if (checkRequiredFields())
        {
            MessageHandler.ClearMessage(LBLErrorMsg);
            string accountEmail = TBAccountEmail.Text.Trim().ToLower();
            string accountType = DDLAccountType.SelectedValue.Trim();
            string profileName = TBProfileName.Text.Trim();
            string profileContact = TBProfileContact.Text.Trim();
            string profileAddress = TBProfileAddress.Text.Trim();
            // create entity
            profileEntity = new ProfileEntity(profileName, profileContact, profileAddress, photoEntities);
            accountEntity = new AccountEntity(accountEmail, profileName.ToLower(), accountType, profileEntity, shopInfoEntity);

            if (accCtrler.checkEmailAddressExist(accountEntity.AccountEmail))
            {
                MessageHandler.ErrorMessage(LBLErrorMsg, "Email already exists");
            }
            else
            {
                // add into database
                accountEntity = accCtrler.createAccount(accountEntity);
                // change photo path to database instead of using temp
                if (photoEntities != null)
                {
                    profileEntity.PhotoEntities =
                        photoCtrler.changePhotoPathToDatabaseFolder(photoEntities, filePath_UploadFolderTemp, accountEntity.ProfileEntity.ProfileID);
                }

                if (profileEntity.PhotoEntities != null)
                    profileEntity = profileCtrler.createPhoto(profileEntity);

                if (accountEntity != null)
                {
                    MessageHandler.SuccessMessage(LBLErrorMsg, "Account info successfully added");
                }
                else
                {
                    MessageHandler.ErrorMessageAdmin(LBLErrorMsg, "Account info was not successfully added");
                }
            }
        }

    }

    protected void BTNGenerate_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        MessageHandler.ClearMessage(LBLErrorMsg);
        Random rnd = new Random();
        photoPreview.InnerHtml = string.Empty;
        DDLAccountType.SelectedIndex = rnd.Next(1, DDLAccountType.Items.Count);
        string accountType = DDLAccountType.SelectedValue.Trim();
        string name = string.Concat(DDLAccountType.SelectedValue.Trim(), rnd.Next(60).ToString("00"));
        TBProfileName.Text = name;
        TBProfileContact.Text = Utility.getRandomNumber(90000000, 99999999).ToString();
        TBAccountEmail.Text = string.Concat(name, "@hotmail.com");

        if (accountType.ToLower().Contains("webuser") || accountType.ToLower().Contains("webadmin"))
        {
            LBLOrganisation.Text = "Not applicable";
            DDLOrangisation.SelectedIndex = 0;
            shopInfoEntity = null;
            DDLOrangisation.Visible = false;
            PNLShopInfoDetails.Visible = false;
        }
        else
        {
            LBLOrganisation.Text = "Organisation";
            DDLOrangisation.Visible = true;
            DDLOrangisation.SelectedIndex = rnd.Next(1, DDLOrangisation.Items.Count);
            shopInfoEntity = shopInfoCtrler.getShopInfo(DDLOrangisation.SelectedValue);
            loadShopInfo(shopInfoEntity);
            PNLShopInfoDetails.Visible = true;
        }
    }

    // Preview image uploaded
    protected void BTNPreview_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        profileID = "000";
        string name = TBProfileName.Text.Trim();

        // some variable to create folder
        if (!string.IsNullOrEmpty(profileID) && !string.IsNullOrEmpty(name))
        {
            MessageHandler.ClearMessage(LBLErrorMsg);
            filePath_UploadFolderTemp = string.Concat("~/uploadedFiles/temp/profileinfo/", profileID.ToLower().Replace(" ", "") + "_" + name.ToLower().Replace(" ", "").ToString());
            LogController.LogLine("filePath_UploadFolderTemp: " + filePath_UploadFolderTemp);

            // create temp files in temp foler
            photoEntities = photoCtrler.saveToTempFolder(PhotoPurpose.ProfileInfo.ToString(), FileUpload1, filePath_UploadFolderTemp);
            // preview photo
            photoCtrler.previewPhotos(photoPreview, filePath_UploadFolderTemp);
        }
        else
        {
            MessageHandler.ErrorMessage(LBLErrorMsg, "User name cannot be empty");
        }

    }

    #endregion


    #region Dropdownlist Control
    // Dropdownlist show/hide shop info
    protected void DDLOrangisation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        if (!string.IsNullOrEmpty(DDLOrangisation.SelectedValue))
        {
            shopInfoEntity = shopInfoCtrler.getShopInfo(DDLOrangisation.SelectedValue);
            loadShopInfo(shopInfoEntity);
            PNLShopInfoDetails.Visible = true;
        }
        else
        {
            PNLShopInfoDetails.Visible = false;
        }
    }
    // Dropdownlist to show/hide organisation option
    protected void DDLAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLAccountType.SelectedValue.Contains("WebUser") || DDLAccountType.SelectedValue.Contains("WebAdmin"))
        {
            LBLOrganisation.Text = "Not applicable";
            DDLOrangisation.SelectedIndex = 0;
            DDLOrangisation.Visible = false;
            PNLShopInfoDetails.Visible = false;
        }
        else
        {
            LBLOrganisation.Text = "Organisation";
            DDLOrangisation.Visible = true;

        }
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

        if (DDLOrangisation.SelectedIndex == 0 && DDLOrangisation.Visible)
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
    #endregion
}