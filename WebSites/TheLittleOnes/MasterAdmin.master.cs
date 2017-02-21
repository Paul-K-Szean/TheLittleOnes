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
    // protected PhotoController photoCtrler;


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

        accEntity = accCtrler.getLoggedInAccount();
        profileEntity = profileCtrler.getLoggedInProfile();
    }



    protected void LKBTNLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminLogin.aspx");
    }
}
