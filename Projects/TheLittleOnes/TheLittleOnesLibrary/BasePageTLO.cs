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
    public class BasePageTLO : Page
    {
        protected static BasePageTLO BasePageInstance;
        public static BasePageTLO getInstance()
        {
            if (BasePageInstance == null)
                BasePageInstance = new BasePageTLO();
            return BasePageInstance;
        }
        // Entities for current logged in user
        protected static AccountEntity TLOAccountEntity;
        protected static AdoptRequestEntity TLOAdoptRequestEntity;
        //protected static ProfileEntity TLOProfileEntity;
        //protected static PetInfoEntity TLOPetInfoEntity;
        //protected static PetCharEntity TLOPetCharEntity;
        //protected static PetEntity TLOPetEntity;
        //protected static ShopInfoEntity TLOShopInfoEntity;
        //protected static ShopTimeEntity TLOShopTimeEntity;
        //protected static List<ShopTimeEntity> TLOShopTimeEntities;
        //protected static AdoptInfoEntity TLOAdoptInfoEntity;
        //protected static List<AdoptInfoEntity> TLOAdoptInfoEntites;
        //protected static AdoptRequestEntity TLOAdoptRequestEntity;
        //protected static List<AdoptRequestEntity> TLOAdoptReqEntites;
        protected static PhotoEntity TLOPhotoEntity;
        protected static List<PhotoEntity> TLOPhotoEntities;
        // Entities for account editing
        protected static AccountEntity TLOEditAccountEntity;
        protected static ProfileEntity TLOEditProfileEntity;
        protected static List<PhotoEntity> TLOEditPhotoEntities;
        protected static ShopInfoEntity TLOEditShopInfoEntity;
        protected static List<ShopTimeEntity> TLOEditShopTimeEntities;
        // Controllers
        protected static AccountController accountCtrler;
        protected static ProfileController profileCtrler;
        protected static PetInfoController petInfoCtrler;
        protected static ShopInfoController shopInfoCtrler;
        protected static PhotoController photoCtrler;
        protected static AdoptInfoController adoptInfoCtrler;
        protected static PetController petCtrler;
        // Data Access Object
        protected DAO dao;
        // Default Contsructor
        public BasePageTLO()
        {
            // initialize controllers
            initializeControllers();
            // initialize folders
            initializeFolders();
            // capture page control
            postBackControl();
        }
        // Manage page control
        protected void postBackControl()
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
        }
        // Validate access control for logged in user
        protected void checkForAccessControl(AccountEntity TLOAccountEntity, string currentPage)
        {
            // pages that are not allowed for different account
            switch (TLOAccountEntity.AccountType.ToLower().Trim())
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
        protected void initializeFolders()
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
        protected void initializeControllers()
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
        protected void clearUIControlValues(ControlCollection pageControls)
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
        // Return current web page
        protected string getCurrentWebPage()
        {
            return Path.GetFileName(Request.Url.AbsolutePath);
        }
        // Split  Camel Case
        protected static string splitCamelCase(string inputString)
        {
            List<char> chars = new List<char>();
            if (!isAlreadyCamelCase(inputString))
            {
                // Author Reed Copsey
                // Source : http://stackoverflow.com/questions/17093423/how-do-i-programmatically-change-camelcase-names-to-displayable-names
                chars.Add(inputString[0]);
                foreach (char c in inputString.Skip(1))
                {
                    if (char.IsUpper(c))
                    {
                        chars.Add(' ');
                        chars.Add(Char.ToUpper(c));
                    }
                    else
                        chars.Add(c);
                }
            }
            return new string(chars.ToArray());
        }
        private static bool isAlreadyCamelCase(string inputString)
        {
            string[] splitString = inputString.Split(' ');
            foreach (string word in splitString)
            {
                Char.ToUpper(word[0]);
            }
            return true;
        }
    }
}