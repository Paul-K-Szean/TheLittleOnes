using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Entities;
public partial class MasterAdmin : MasterPage
{
    // Entities
    private static AccountEntity accountEntity;
    // Controllers
    private AccountController accountCtrler;
    private ProfileController profileCtrler;
    private PetInfoController petInfoCtrler;
    private PhotoController photoCtrler;
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
        loadAccountInfo();
        
    }
    private void loadAccountInfo()
    {
        accountEntity = BasePage.AccountEntity;
        if (accountEntity != null)
        {
            LBLDisplayName.Text = accountEntity.ProfileEntity.ProfileName;
        }
        else
        {
        }
    }
    // Initialize controllers
    private void initializeControllers()
    {
        if (accountCtrler == null)
        {
            accountCtrler = AccountController.getInstance();
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
    }
    // Button Clicks
    protected void LKBTNLogout_Click(object sender, EventArgs e)
    {
        accountCtrler.signOut();
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
