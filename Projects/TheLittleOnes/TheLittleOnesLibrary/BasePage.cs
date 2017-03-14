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
using System.Data;
using TheLittleOnesLibrary.EnumFolder;

namespace TheLittleOnesLibrary
{

    public class BasePage : Page
    {
        // Entities for current logged in user
        protected static AccountEntity accountEntity;
        protected static ProfileEntity profileEntity;
        protected static PetInfoEntity petInfoEntity;
        protected static PetCharEntity petCharEntity;
        protected static PetEntity petEntity;
        protected static ShopInfoEntity shopInfoEntity;
        protected static ShopTimeEntity shopTimeEntity;

        protected static List<ShopTimeEntity> shopTimeEntities;
        protected static AdoptInfoEntity adoptInfoEntity;
        protected static List<AdoptInfoEntity> adoptInfoEntites;
        protected static PhotoEntity photoEntity;
        protected static List<PhotoEntity> photoEntities;

        // Entities for system account editing
        protected static AccountEntity editAccountEntity;
        protected static ProfileEntity editProfileEntity;
        protected static List<PhotoEntity> editPhotoEntities;
        protected static ShopInfoEntity editShopInfoEntity;
        protected static List<ShopTimeEntity> editShopTimeEntities;

        // Controllers
        protected AccountController accCtrler;
        protected ProfileController profileCtrler;
        protected PetInfoController petInfoCtrler;
        protected ShopInfoController shopInfoCtrler;
        protected PhotoController photoCtrler;
        protected AdoptInfoController adoptInfoCtrler;
        protected PetController petCtrler;
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
            string currentPage = HttpContext.Current.Request.Url.AbsoluteUri.ToLower();
            if (IsPostBack)
            {
                LogController.LogLine("Page posted back: " + currentPage);
            }
            else
            {
                LogController.LogLine("Page loaded: " + currentPage);
            }

            accountEntity = accCtrler.getLoggedInAccount();
            profileEntity = profileCtrler.getLoggedInProfile();
            // redirect to login page, except for login page
            if (currentPage.Contains("adminlogin"))
            {
                accCtrler.SignOut();
            }
            else
            {
                if (accountEntity == null)
                {
                    LogController.LogLine("No account logged in");
                    HttpContext.Current.Response.Redirect("AdminLogin.aspx");
                }
                else
                {
                    accountAccessControl(accountEntity, currentPage);
                }
            }


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
            if (adoptInfoCtrler == null)
            {
                adoptInfoCtrler = AdoptInfoController.getInstance();
            }
            if (petCtrler == null)
            {
                petCtrler = PetController.getInstance();
            }
            accountEntity = accCtrler.getLoggedInAccount();
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
        // Calculate gridview entry size
        protected void updateEntryCount(DataTable dTable, GridView gridview, Label LBLEntriesCount)
        {
            int totalSize = dTable.Rows.Count;
            int currentPageIndex = gridview.PageIndex * gridview.PageSize + 1;
            int pageSize = gridview.PageSize * (gridview.PageIndex + 1);
            int rowSize = gridview.Rows.Count;

            if (pageSize > totalSize)
                pageSize = totalSize;

            if (rowSize == 0)
            {
                currentPageIndex = rowSize;
                LBLEntriesCount.Text = string.Concat("No Record(s) found. Showing ", currentPageIndex, " to ", pageSize, " of ", totalSize, " entries");
            }
            else
            {
                LBLEntriesCount.Text = string.Concat("Showing ", currentPageIndex, " to ", pageSize, " of ", totalSize, " entries");
            }

        }
        // Clear control value
        public void clearUIControlValues(ControlCollection pageControls)
        {
            TextBox textbox;
            DropDownList dropdownlist;
            foreach (Control ctrl in pageControls)
            {
                if (ctrl is TextBox)
                {
                    {
                        textbox = (TextBox)ctrl;
                        textbox.Text = string.Empty;
                    }

                }
                if (ctrl is DropDownList)
                {
                    {
                        dropdownlist = (DropDownList)ctrl;
                        dropdownlist.SelectedIndex = 0;
                    }

                }
            }
        }
    }
}
