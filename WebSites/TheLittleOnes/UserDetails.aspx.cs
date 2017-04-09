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
using TheLittleOnesLibrary.Handler;
public partial class AccountProfile : BasePageTLO
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) { }
        else
        {
            if (TLOAccountEntity != null)
            {
                loadUserDetails();
            }
        }
    }
    protected void BTNSave_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        if (checkRequiredField())
        {
            string profileName = TBProfileName.Text.Trim();
            string profileContact = TBProfileContact.Text.Trim();
            string profileAddress = TBProfileAddress.Text.Trim();
            string accountEmail = TLOAccountEntity.AccountEmail;
            string accountPassword = string.IsNullOrEmpty(TBAccountPasswordNew.Text) ? TLOAccountEntity.AccountPassword : TBAccountPasswordNew.Text.Trim();
            string accountType = TLOAccountEntity.AccountType;
            // create entity
            TLOEditProfileEntity = new ProfileEntity(TLOAccountEntity.ProfileEntity.ProfileID,profileName, profileContact, profileAddress, null);
            TLOEditAccountEntity = new AccountEntity(TLOAccountEntity.AccountID,accountEmail,accountPassword,accountType,TLOEditProfileEntity,null,TLOAccountEntity.DateJoined);
            // update into database
            TLOAccountEntity = accountCtrler.updateAccount(TLOEditAccountEntity);
            TLOAccountEntity.ProfileEntity = profileCtrler.updateProfile(TLOEditProfileEntity);
            if (TLOAccountEntity != null && TLOAccountEntity.ProfileEntity != null)
            {
                MessageHandler.SuccessMessage(LBLErrorMsg,"User details updated successfully");
            }
            else {
                MessageHandler.DefaultMessage(LBLErrorMsg, "User details was not updated successfully");
            }
        }
    }
    #region Logical Methods
    // Check Required Fields
    private bool checkRequiredField()
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        bool isProfileNameValid = true;
        bool isPasswordValid = true;
        string accountEmail = TBAccountEmail.Text.Trim();
        string accountPasswordOld = TBAccountPasswordOld.Text.Trim();
        string accountPasswordNew = TBAccountPasswordNew.Text.Trim();
        string profileName = TBProfileName.Text.Trim();
        if (string.IsNullOrEmpty(accountPasswordOld) && string.IsNullOrEmpty(accountPasswordNew))
        {
            // dont update password
        }
        else
        {
            // check if old and new password is empty
            if (string.IsNullOrEmpty(accountPasswordOld) || string.IsNullOrEmpty(accountPasswordNew))
            {
                isPasswordValid = false;
                MessageHandler.ErrorMessage(LBLErrorMsg, "Old or new password cannot be empty");
            }
            else
            {
                // check if old and new password is the same
                if (accountPasswordOld.Equals(accountPasswordNew))
                {
                    isPasswordValid = false;
                    MessageHandler.ErrorMessage(LBLPasswordNew, "New password cannot be the same as old password");
                }
                else
                {
                    // check at least a digit
                    if (!accountPasswordNew.Any(char.IsDigit))
                    {
                        isPasswordValid = false;
                        MessageHandler.ErrorMessage(LBLPasswordNew, "New password must have at least a digit");
                    }
                    else
                    {
                        MessageHandler.DefaultMessage(LBLPasswordNew, "New Password");
                    }
                    MessageHandler.DefaultMessage(LBLPasswordOld, "Old Password");
                }
            }
        }
        if (string.IsNullOrEmpty(profileName))
        {
            isProfileNameValid = false;
            MessageHandler.ErrorMessage(LBLProfileName, "Name cannot be empty");
        }
        else
        {
            MessageHandler.DefaultMessage(LBLProfileName, "Name");
        }
        return isPasswordValid & isProfileNameValid;
    }
    // load user details
    private void loadUserDetails()
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        TBAccountID.Text = TLOAccountEntity.AccountID;
        TBAccountEmail.Text = TLOAccountEntity.AccountEmail;
        TBAccountType.Text = TLOAccountEntity.AccountType;
        TBProfileID.Text = TLOAccountEntity.ProfileEntity.ProfileID;
        TBProfileName.Text = TLOAccountEntity.ProfileEntity.ProfileName;
        TBProfileContact.Text = TLOAccountEntity.ProfileEntity.ProfileContact;
        TBProfileAddress.Text = TLOAccountEntity.ProfileEntity.ProfileAddress;
    }
    #endregion
}