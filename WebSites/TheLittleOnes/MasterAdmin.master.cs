using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Entities;

public partial class MasterAdmin : MasterPage
{
    // Entities
    protected static AccountEntity accEntity;
    protected static ProfileEntity profileEntity;
    protected static PetInfoEntity petInfoEntity;
    protected static PetCharEntity petCharEntity;
    protected static PhotoEntity photoEntity;

    // Controllers
    protected AccountController accCtrler;
    protected ProfileController profileCtrler;
    protected PetInfoController petInfoCtrler;
    protected PhotoController photoCtrler;


    protected void Page_Load(object sender, EventArgs e)
    {
        // initialize controllers
        initializeControllers();
        // capture page control
        postBackControl();

    }
    // Manage page control
    private void postBackControl()
    {
        string currentPage = HttpContext.Current.Request.Url.AbsoluteUri;
        if (IsPostBack)
        {

        }
        else
        {

        }
        setUserInfo();
    }

    private void setUserInfo()
    {
        if (accEntity != null)
        {
            accEntity = accCtrler.getLoggedInAccount();
            profileEntity = profileCtrler.getLoggedInProfile();

            LBLDisplayName.Text = profileEntity.ProfileName;
        }
    }
    // Initialize controllers
    private void initializeControllers()
    {
        if (accCtrler == null)
        {
            accCtrler = AccountController.getInstance();
        }
        if (profileCtrler == null)
        {
            profileCtrler = ProfileController.getInstance();
        }
        if (petInfoCtrler == null)
        {
            petInfoCtrler = PetInfoController.getInstance();
        }
        if (photoCtrler == null)
        {
            photoCtrler = PhotoController.getInstance();
        }
        accEntity = accCtrler.getLoggedInAccount();
        profileEntity = profileCtrler.getLoggedInProfile();
    }
    // Button Clicks
    protected void LKBTNLogout_Click(object sender, EventArgs e)
    {
        accCtrler.SignOut();
        Response.Redirect("AdminLogin.aspx");
    }
    // Validate access control for logged in user
    private void accountAccessControl(AccountEntity accountEntity, string currentPage)
    {
        // pages that are not allowed for different account
        switch (accountEntity.AccountType.ToLower().Trim())
        {
            case "websheltergroup":
                if (currentPage.Contains("adminpetinfoadd") ||
                    currentPage.Contains("adminpetinfoedit") ||
                    currentPage.Contains("adminshopinfoadd") ||
                    currentPage.Contains("adminshopinfoedit") ||
                    currentPage.Contains("adminsystemaccountadd") ||
                    currentPage.Contains("adminsystemaccountedit"))
                {
                    HttpContext.Current.Response.Redirect("AdminDashboard.aspx");
                }
                break;
            case "websponsorgroup":
                if (
                    currentPage.Contains("adminadoptioninfoadd") ||
                    currentPage.Contains("adminadoptioninfoedit") ||
                    currentPage.Contains("adminsystemaccountadd") ||
                    currentPage.Contains("adminsystemaccountedit"))
                {
                    HttpContext.Current.Response.Redirect("AdminDashboard.aspx");
                }
                break;
        }

    }


}
