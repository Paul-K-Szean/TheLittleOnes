﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.Handler;

public partial class AdminAccount : BasePage
{
    string passwordOld;
    string passwordNew;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (accEntity != null)
        {
            loadAccountInfo();
        }
    }

    void loadAccountInfo()
    {
        TBAccountID.Text = accEntity.AccountID;
        TBEmail.Text = accEntity.AccountEmail;
        TBAccountType.Text = accEntity.AccountType;
    }

    #region Button Clicks
    protected void BTNSave_Click(object sender, EventArgs e)
    {
        passwordOld = TBPasswordOld.Text.Trim();
        passwordNew = TBPasswordNew.Text.Trim();
        if (checkRequiredField(passwordOld, passwordNew))
        {
            // check old password
            if (accCtrler.checkPassword(accEntity.AccountID, passwordOld))
            {
                // change password
                AccountEntity accEntityTemp = accEntity;
                accEntityTemp.AccountPassword = passwordNew;
                // update 
                accEntity = accCtrler.changePassword(accEntityTemp);
                if (accEntity != null)
                {
                    MessageHandler.SuccessMessage(LBLErrorMsg, "Password successfully updated");
                }
                else
                {
                    // unknown error
                    MessageHandler.ErrorMessageAdmin(LBLErrorMsg, "Password was not successfully updated");
                }
            }
            else
            {
                MessageHandler.ErrorMessage(LBLErrorMsg, "Invalid old password");
            }

        }


    }
    #endregion

    #region Logical Methods
    private bool checkRequiredField(string passwordOld, string passwordNew)
    {
        bool isRequiredFieldsValid = true;
        if (string.IsNullOrEmpty(passwordOld) && string.IsNullOrEmpty(passwordNew))
        {
            isRequiredFieldsValid = false;
            MessageHandler.WarningMessage(LBLErrorMsg, "Nothing to update");
        }
        else
        {
            // check if old and new password is empty
            if (string.IsNullOrEmpty(passwordOld) || string.IsNullOrEmpty(passwordNew))
            {
                isRequiredFieldsValid = false;
                MessageHandler.ErrorMessage(LBLErrorMsg, "Old or new password cannot be empty");
            }
            else
            {
                // check if old and new password is the same
                if (passwordOld.Equals(passwordNew))
                {
                    isRequiredFieldsValid = false;
                    MessageHandler.ErrorMessage(LBLErrorMsg, "New password cannot be the same as old password");
                }

                // check at least a digit
                if (!passwordNew.Any(char.IsDigit))
                {
                    isRequiredFieldsValid = false;
                    MessageHandler.ErrorMessage(LBLErrorMsg, "New password must have at least a digit");
                }
            }
        }


        return isRequiredFieldsValid;
    }
    #endregion

}