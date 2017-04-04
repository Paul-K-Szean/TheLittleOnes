using System;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.EnumFolder;
using TheLittleOnesLibrary.Handler;
public partial class AdminLogin : BasePageAdmin
{
    string inputEmail;
    string inputPassword;
    // page load
    protected void Page_Load(object sender, EventArgs e)
    {
        // inputs
        if (IsPostBack)
        {
            inputEmail = TBLoginEmail.Text.Trim();
            inputPassword = TBLoginPassword.Text.Trim();
        }
        else
        {
        }
    }
    // Login method
    protected void BTNLogin_Click(object sender, EventArgs e)
    {
        // check required input fields to login    
        if (string.IsNullOrEmpty(inputEmail) || string.IsNullOrEmpty(inputPassword))
        {
            MessageHandler.ErrorMessage(LBLErrorMsg, "Invalid email or password");
            TBLoginPassword.Text = string.Empty;
        }
        else
        {
            inputEmail = TBLoginEmail.Text.Trim();
            inputPassword = TBLoginPassword.Text.Trim();
            // login
            accountEntity = accountCtrler.signIn(inputEmail, inputPassword);
            if (accountEntity != null)
            {
                BasePageAdmin.signInAccountProfileEntity(accountEntity);
                if (!accountEntity.AccountType.Equals(AccountType.WebUser.ToString()))
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
                else
                {
                    MessageHandler.ErrorMessage(LBLErrorMsg, "You are not authorised here");
                    TBLoginPassword.Text = string.Empty;
                    accountCtrler.signOut();
                }
            }
            else
            {
                MessageHandler.ErrorMessage(LBLErrorMsg, "Invalid email or password");
                TBLoginPassword.Text = string.Empty;
            }
        }
    }
}