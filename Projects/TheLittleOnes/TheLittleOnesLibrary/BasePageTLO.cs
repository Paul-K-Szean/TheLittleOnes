using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.DataAccessObject;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.Handler;
namespace TheLittleOnesLibrary
{
    public class BasePageTLO : BasePage
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
        protected static ProfileEntity TLOProfileEntity;
        protected static AppointmentEntity TLOAppointmentEntity;
        protected static PhotoEntity TLOPhotoEntity;
        protected static List<PhotoEntity> TLOPhotoEntities;
        // Entities for account editing
        protected static AccountEntity TLOEditAccountEntity;
        protected static ProfileEntity TLOEditProfileEntity;
        protected static List<PhotoEntity> TLOEditPhotoEntities;
        protected static ShopInfoEntity TLOEditShopInfoEntity;
        protected static List<ShopTimeEntity> TLOEditShopTimeEntities;

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
            checkForAccessControl(TLOAccountEntity, currentPage);

        }
        // Validate access control for logged in user
        protected void checkForAccessControl(AccountEntity TLOAccountEntity, string currentPage)
        {
            // pages that are not allowed if user did not sign in
            if (TLOAccountEntity == null)
            {
                LogController.LogLine("No account logged in");
                if (!currentPage.Contains("home"))
                {
                    if (currentPage.ToLower().Contains("appointmentdetails"))
                    {
                        HttpContext.Current.Response.Redirect("Forbidden.aspx");
                    }
                }
            }
        }
        #region Account& Profile Logic
        // Sign in account & profile entity
        public static void signInAccountProfileEntity(AccountEntity accountEntity)
        {
            TLOAccountEntity = accountEntity;
        }
        // Sign out Account & profile entity
        public static void signOutAccountProfileEntity()
        {
            TLOAccountEntity = null;
        }
        public static AccountEntity getLoggedInAccounProfiletEntity()
        {
            if (TLOAccountEntity != null)
                return TLOAccountEntity;
            else return null;
        }
        #endregion

    }
}