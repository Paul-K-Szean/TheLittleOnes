using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.Handler;

public partial class AdminProfile : BasePage
{
    string name;
    string contact;
    string address;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) { }
        else
        {
            if (profileEntity != null)
            {
                loadProfileInfo();
            }
        }

    }

    void loadProfileInfo()
    {
        TBProfileID.Text = profileEntity.ProfileID;
        TBName.Text = profileEntity.ProfileName;
        TBContact.Text = profileEntity.ProfileContact;
        TBAddress.Text = profileEntity.ProfileAddress;
    }




    #region Button Clicks
    protected void BTNPreview_Click(object sender, EventArgs e)
    {

    }

    protected void BTNSave_Click(object sender, EventArgs e)
    {
        name = TBName.Text.Trim();
        contact = TBContact.Text.Trim();
        address = TBAddress.Text.Trim();

        if (checkRequiredField(name, contact, address))
        {
            // update profile
            ProfileEntity profileEntityTemp = profileEntity;
            profileEntityTemp.ProfileName = name;
            profileEntityTemp.ProfileContact = contact;
            profileEntityTemp.ProfileAddress = address;
            // update 
            profileEntity = profileCtrler.updateProfile(profileEntityTemp);
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