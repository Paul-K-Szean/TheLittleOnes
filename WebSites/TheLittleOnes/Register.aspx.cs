using System;
using System.Linq;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.EnumFolder;
using TheLittleOnesLibrary.Handler;

public partial class Register : BasePageTLO
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    #region Button Controls
    protected void BTNCreate_Click(object sender, EventArgs e)
    {
        if (checkRequiredFields())
        {
            string accountEmail = TBAccountEmail.Text.Trim();
            string accountPassword = TBAccountPassword02.Text.Trim();
            string profileName = TBProfileName.Text.Trim();
            string profileContact = TBProfileContact.Text.Trim();
            string profileAddress = TBProfileAddress.Text.Trim();
            if (accountCtrler.checkEmailAddressExist(accountEmail))
            {
                MessageHandler.ErrorMessage(LBLAccountEmail, "Email already in used.");
            }
            else
            {
                // create entity
                ProfileEntity registerProfileEntity = new ProfileEntity(profileName, profileContact, profileAddress, null);
                AccountEntity registerAccountEntity = new AccountEntity(accountEmail, accountPassword, 
                    Enums.GetDescription(AccountType.WebUser), registerProfileEntity,null);
                // add into database
                registerAccountEntity = accountCtrler.createAccount(registerAccountEntity);
                if (string.IsNullOrEmpty(registerAccountEntity.AccountID))
                {
                    MessageHandler.SuccessMessage(LBLErrorMsg, "Account was not created successfully");
                }
                else {
                    MessageHandler.SuccessMessage(LBLErrorMsg, "Account created successfully");
                }
            }
        }
    }

    protected void BTNGenerate_Click(object sender, EventArgs e)
    {
        int randomNum = Utility.getRandomNumber();
        string username = Utility.getName();
        TBAccountEmail.Text = Utility.getEmail();
        TBAccountPassword02.Text = TBAccountPassword01.Text = Utility.getPassword();

        TBProfileName.Text = username;
        TBProfileContact.Text = Utility.getRandomNumber(80000000, 90000000).ToString();
        TBProfileAddress.Text = string.Concat("LOCATION", Utility.getRandomNumber().ToString("00"));

    }
    #endregion
    #region Dropdownlist Controls
    protected void DDLOrangisation_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #endregion
    #region Logical Methods
    // Check Required Fields
    private bool checkRequiredFields()
    {
        bool isAccountEmailValid = true;
        bool isAccountPasswordValid = true;
        bool isProfileNameValid = true;

        string accountEmail = TBAccountEmail.Text.Trim();
        string accountPassword01 = TBAccountPassword01.Text.Trim();
        string accountPassword02 = TBAccountPassword02.Text.Trim();
        string profileName = TBProfileName.Text.Trim();


        if (string.IsNullOrEmpty(accountEmail))
        {
            isProfileNameValid = false;
            MessageHandler.ErrorMessage(LBLAccountEmail, "Email - Cannot be empty!");
        }
        else
            MessageHandler.DefaultMessage(LBLAccountEmail, "Email (LoginID)");
        if (string.IsNullOrEmpty(accountPassword01))
        {
            isAccountPasswordValid = false;
            MessageHandler.ErrorMessage(LBLPassword01, "Password - Cannot be empty!");
        }
        else
        {
            // check at least a digit
            if (accountPassword01.Any(char.IsDigit))
            {
                // check if accountPassword01 == accountPassword02 ?
                if (!accountPassword01.Equals(accountPassword02))
                {
                    isAccountPasswordValid = false; // not same
                    MessageHandler.ErrorMessage(LBLPassword02, "Password and Confirm Password does not match");
                }
                else
                {
                    MessageHandler.DefaultMessage(LBLPassword02, "Confirm Password");
                }
                MessageHandler.DefaultMessage(LBLPassword01, "Password");
            }
            else
            {
                MessageHandler.ErrorMessage(LBLPassword01, "Password must have at least a digit");
            }
        }
        if (string.IsNullOrEmpty(profileName))
        {
            isProfileNameValid = false;
            MessageHandler.ErrorMessage(LBLProfileName, "Name - Cannot be empty!");
        }
        else
            MessageHandler.DefaultMessage(LBLProfileName, "Name");

        // return condition
        if (isAccountEmailValid && isAccountPasswordValid && isProfileNameValid)
        {
            MessageHandler.ClearMessage(LBLErrorMsg);
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