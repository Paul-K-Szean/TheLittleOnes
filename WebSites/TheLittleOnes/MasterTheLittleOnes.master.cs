﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.Handler;

public partial class MasterTheLittleOnes : System.Web.UI.MasterPage
{
    AccountEntity accountEntity;
    AccountController accountCtrler;
    string loginEmail;
    string loginPassword;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {

        }
        else
        {

        }
        // load controllers
        if (accountCtrler == null)
            accountCtrler = AccountController.getInstance();
        anyUserLoggedIn();
    }
    #region Button Clicks
    protected void BTNLogin_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        if (checkRequiredFields())
        {
            loginEmail = TBLoginEmail.Text.Trim();
            loginPassword = TBLoginPassword.Text.Trim();
            accountEntity = accountCtrler.signIn(loginEmail, loginPassword);
            if (accountEntity != null)
            {
                string adoptInfoID = HttpContext.Current.Request.QueryString["adoptinfoid"];
                string currentPage = Path.GetFileName(Request.Url.AbsolutePath).ToLower().Trim();

                // TODO where should the system response after logging in 
                if (currentPage.Contains("adoptiondetails"))
                    Response.Redirect("AdoptionDetails.aspx?adoptinfoid=" + adoptInfoID);
                else
                    Response.Redirect(currentPage);
            }
        }
    }
    protected void LKBTNSignOut_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        string currentPage = Path.GetFileName(Request.Url.AbsolutePath).ToLower().Trim();
        accountCtrler.signOut();
        // must check for pages that requires login
        if (currentPage.Contains("adoptionappointment"))
        {
            Response.Redirect("Home.aspx");
        }
        else
        {
            Response.Redirect(currentPage);
        }
    }
    #endregion
    #region Logical Methods
    private bool checkRequiredFields()
    {
        loginEmail = TBLoginEmail.Text.Trim();
        loginPassword = TBLoginPassword.Text.Trim();
        MessageHandler.ErrorMessage(LBLErrorMsg, "Either email or password is invalid");
        if (string.IsNullOrEmpty(loginEmail) || string.IsNullOrEmpty(loginPassword))
        {
            MessageHandler.ErrorMessage(LBLErrorMsg, "Either email or password is invalid");
            return false;
        }
        else
        {
            return true;
        }
    }
    private void anyUserLoggedIn()
    {
        accountEntity = accountCtrler.getLoggedInAccount();
        if (accountEntity != null)
        {
            HYPLKAccountInfo.Text = accountEntity.ProfileEntity.ProfileName + "<span class=\"caret\"></span>";
            HYPLKAccountInfo.Attributes["data-target"] = "";
            HYPLKAccountInfo.Attributes["data-toggle"] = "dropdown";
            loadAccountInfo();
        }
        else
        {
            HYPLKAccountInfo.Text = "Sign In";
            HYPLKAccountInfo.Attributes["data-target"] = "#login";
            HYPLKAccountInfo.Attributes["data-toggle"] = "modal";
        }
    }
    private void loadAccountInfo()
    {

    }
    #endregion

}
