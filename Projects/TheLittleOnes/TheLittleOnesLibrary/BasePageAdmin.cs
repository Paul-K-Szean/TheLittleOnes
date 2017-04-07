using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.DataAccessObject;
using TheLittleOnesLibrary.Entities;
namespace TheLittleOnesLibrary
{
    public class BasePageAdmin : BasePage
    {
        // Entities for current logged in user
        protected static AccountEntity accountEntity; // profile in an object of account entity
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
        // Default Contsructor
        public BasePageAdmin()
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
            // redirect to login page, except for login page
            if (currentPage.Contains("adminlogin"))
            {
                accountCtrler.signOut();
            }
            else
            {
                if (accountEntity == null)
                {
                    LogController.LogLine("No account logged in");
                    if (!currentPage.Contains("adminlogin"))
                        HttpContext.Current.Response.Redirect("AdminLogin.aspx");
                }
                else
                {
                    accountAccessControl(accountEntity, currentPage);
                }
            }
        }
        // Validate access control for logged in user
        protected void accountAccessControl(AccountEntity accountEntity, string currentPage)
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
                        HttpContext.Current.Response.Redirect("AdminForbidden.aspx");
                    }
                    break;
                case "websponsorgroup":
                    if (
                        currentPage.Contains("adminadoptioninfoadd") ||
                        currentPage.Contains("adminadoptioninfoedit") ||
                        currentPage.Contains("adminsystemaccountadd") ||
                        currentPage.Contains("adminsystemaccountedit"))
                    {
                        HttpContext.Current.Response.Redirect("AdminForbidden.aspx");
                    }
                    break;
            }
        }
        #region Account& Profile Logic
        // Sign in account & profile entity
        public static void signInAccountProfileEntity(AccountEntity accountEntity)
        {
            BasePageAdmin.accountEntity = accountEntity;
        }
        // Sign out Account & profile entity
        public static void signOutAccountProfileEntity()
        {
            accountEntity = null;
        }
        public static AccountEntity getLoggedInAccounProfiletEntity()
        {
            if (accountEntity != null)
                return accountEntity;
            else return null;
        }
        #endregion
    }
}