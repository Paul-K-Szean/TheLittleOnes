using System;
using System.Linq;
using System.Reflection;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.Handler;
public partial class AdminAccount : BasePageAdmin
{
    string passwordOld;
    string passwordNew;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (accountEntity != null)
        {
            loadAccountInfo();
            loadShopInfo();
        }
    }
    private void loadShopInfo()
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        if (accountEntity.ShopInfoEntity != null)
        {
            TBShopInfoID.Text = accountEntity.ShopInfoEntity.ShopInfoID;
            TBShopInfoName.Text = accountEntity.ShopInfoEntity.ShopInfoName;
            TBShopInfoContact.Text = accountEntity.ShopInfoEntity.ShopInfoContact;
            TBShopInfoAddress.Text = accountEntity.ShopInfoEntity.ShopInfoAddress;
        }
    }
    void loadAccountInfo()
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        TBAccountID.Text = accountEntity.AccountID;
        TBEmail.Text = accountEntity.AccountEmail;
        TBAccountType.Text = accountEntity.AccountType;
    }
    #region Button Clicks
    protected void BTNSave_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        passwordOld = TBPasswordOld.Text.Trim();
        passwordNew = TBPasswordNew.Text.Trim();
        if (checkRequiredField(passwordOld, passwordNew))
        {
            // check old password
            if (accountCtrler.checkPassword(accountEntity.AccountID, passwordOld))
            {
                // change password
                AccountEntity accEntityTemp = accountEntity;
                accEntityTemp.AccountPassword = passwordNew;
                // update 
                accountEntity = accountCtrler.changePassword(accEntityTemp);
                if (accountEntity != null)
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
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
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