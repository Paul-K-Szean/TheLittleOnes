using System;
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
public partial class AdminProfile : BasePage
{
    private string profileID;
    private static string name;
    private static string contact;
    private static string address;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (IsPostBack)
        {
        }
        else
        {
            loadProfileInfo();
        }
    }
    void loadProfileInfo()
    {
        TBProfileID.Text = accountEntity.ProfileEntity.ProfileID;
        TBName.Text = accountEntity.ProfileEntity.ProfileName;
        TBContact.Text = accountEntity.ProfileEntity.ProfileContact;
        TBAddress.Text = accountEntity.ProfileEntity.ProfileAddress;
    }
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
    // Save changes
    protected void BTNSave_Click(object sender, EventArgs e)
    {
        name = TBName.Text.Trim();
        contact = TBContact.Text.Trim();
        address = TBAddress.Text.Trim();
        if (checkRequiredField(name, contact, address))
        {
            // update profile
            ProfileEntity profileEntityTemp = accountEntity.ProfileEntity;
            profileEntityTemp.ProfileName = name;
            profileEntityTemp.ProfileContact = contact;
            profileEntityTemp.ProfileAddress = address;
            profileEntity = profileCtrler.updateProfile(profileEntityTemp);
            if (photoEntities != null)
            {
                // change photo path to database instead of using temp
                profileEntity.PhotoEntities = photoCtrler.changePhotoPathToDatabaseFolder(photoEntities, profileEntity.ProfileID);
                // remove old photos from database
                photoCtrler.deletePhoto(profileEntity.ProfileID, PhotoPurpose.ProfileInfo.ToString());
                // create new photos into database
                photoCtrler.createPhoto(photoEntities, profileEntity.ProfileID);
            }
            if (profileEntity != null)
            {
                MessageHandler.SuccessMessage(LBLErrorMsg, "Profile successfully updated");
            }
            else
            {
                // unknown error
                MessageHandler.ErrorMessageAdmin(LBLErrorMsg, "Profile was not successfully updated");
            }
        }
        DLPhotoUploaded.DataBind();
        photoPreview.InnerHtml = string.Empty;
    }
    #endregion
    #region Logical Methods
    private bool checkRequiredField(string name, string contact, string address)
    {
        LBLErrorMsg.Text = string.Empty;
        bool isRequiredFieldsValid = true;
        if (string.IsNullOrEmpty(name))
        {
            isRequiredFieldsValid = false;
            MessageHandler.ErrorMessage(LBLName, "Name - Cannot be empty");
        }
        else
        {
            MessageHandler.DefaultMessage(LBLName, "Name");
        }
        if (!string.IsNullOrEmpty(contact))
        {
            if (contact.Any(char.IsLetter))
            {
                isRequiredFieldsValid = false;
                MessageHandler.ErrorMessage(LBLContact, "Contact - Only digit allowed" + Environment.NewLine);
            }
            if (contact.Length < 8)
            {
                isRequiredFieldsValid = false;
                MessageHandler.ErrorMessage(LBLContact, "Contact - Only digit allowed");
            }
        }
        return isRequiredFieldsValid;
    }
    #endregion
}