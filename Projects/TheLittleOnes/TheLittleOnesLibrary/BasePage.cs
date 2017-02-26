using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.DataAccessObject;
using TheLittleOnesLibrary.Handler;
using System.IO;

namespace TheLittleOnesLibrary
{

    public class BasePage : Page
    {
        // Entities
        protected static AccountEntity accEntity;
        protected static ProfileEntity profileEntity;
        protected static PetInfoEntity petInfoEntity;
        protected static PetCharEntity petCharEntity;
        protected static PhotoEntity photoEntity;
        protected static ShopInfoEntity shopInfoEntity;
        protected static ShopTimeEntity shopTimeEntity;

        // Controllers
        protected AccountController accCtrler;
        protected ProfileController profileCtrler;
        protected PetInfoController petInfoCtrler;
        protected ShopInfoController shopInfoCtrler;
        protected PhotoController photoCtrler;

        // Data Access Object
        protected DAO dao;

        // Default Contsructor
        public BasePage()
        {
            // initialize controllers
            initializeControllers();
            // initialize folders
            initializeFolders();
            // capture page control
            postBackControl();
        }
        // Manage page control
        private void postBackControl()
        {
            string currentPage = HttpContext.Current.Request.Url.AbsoluteUri;
            if (IsPostBack)
            {
                LogController.LogLine("Page posted back: " + currentPage);
            }
            else
            {
                LogController.LogLine("Page loaded: " + currentPage);
            }

            // redirect to login page, except for login page
            if (accEntity == null && !currentPage.Contains("AdminLogin"))
            {
                LogController.LogLine("No account logged in");
                HttpContext.Current.Response.Redirect("AdminLogin.aspx");
            }
            else
            {
                accEntity = accCtrler.getLoggedInAccount();
                profileEntity = profileCtrler.getLoggedInProfile();
            }

        }

        // Initialize folders
        private void initializeFolders()
        {
            // for photos
            string filePath_UploadFolderTemp = "~/uploadedFiles/temp";
            string filePath_UploadFolderDatabase = "~/uploadedFiles/database";
            bool isfilePath_UploadFolderTempExists = Directory.Exists(filePath_UploadFolderTemp);
            bool isfilePath_UploadFolderDatabaseExists = Directory.Exists(filePath_UploadFolderDatabase);
            // check for temp folders path
            if (!isfilePath_UploadFolderTempExists)
            {
                // dont exists - create path
                Directory.CreateDirectory(Server.MapPath(filePath_UploadFolderTemp));
            }

            // check for database folders path
            if (!isfilePath_UploadFolderTempExists)
            {
                // dont exists - create path
                Directory.CreateDirectory(Server.MapPath(filePath_UploadFolderDatabase));
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
            if (shopInfoCtrler == null)
            {
                shopInfoCtrler = ShopInfoController.getInstance();
            }
            if (photoCtrler == null)
            {
                photoCtrler = PhotoController.getInstance();
            }


            accEntity = accCtrler.getLoggedInAccount();
            profileEntity = profileCtrler.getLoggedInProfile();

        }

        // Highlight select row for gridview
        protected void highlightSelectedRow(GridView gridview)
        {
            int selectedIndex = gridview.SelectedIndex;
            foreach (GridViewRow row in gridview.Rows)
            {
                if (row.RowIndex == gridview.SelectedIndex)
                {
                    row.BackColor = Utility.getColorLightBlue();
                    row.ForeColor = Utility.getColorWhite();
                }
                else
                {
                    row.ForeColor = Utility.getDefaultColor();
                    if (row.RowIndex % 2 == 0)
                    {
                        // even rows
                        row.BackColor = Utility.getColorWhite();
                    }
                    else
                    {
                        // odd rows
                        row.BackColor = Utility.getColorLightGray();
                    }

                }
            }
        }



    }
}
