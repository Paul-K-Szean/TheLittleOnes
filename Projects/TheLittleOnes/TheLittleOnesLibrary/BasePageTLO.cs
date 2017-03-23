﻿using System;
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
        protected static AppointmentEntity TLOAppointmentEntity;
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
        protected static AppointmentController appointmentCrtler;
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
            TLOAccountEntity = accountCtrler.getLoggedInAccount();
            if (TLOAccountEntity == null)
            {
                LogController.LogLine("No account logged in");
                if (!currentPage.Contains("home"))
                {
                    if (currentPage.ToLower().Contains("appointmentdetails"))
                    {
                        HttpContext.Current.Response.Redirect("Home.aspx");
                    }
                }
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
            if (appointmentCrtler == null)
            {
                appointmentCrtler = AppointmentController.getInstance();
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
        // Load operation hours
        protected void loadOperatingHours(string dateSelected, string daySelected, HtmlInputText INPUTAppmtDate,
            List<ShopTimeEntity> shopTimeEntities, DropDownList DDLAppmtTime, Label LBLAppmtDate, Label LBLAppmtTime, string AppmtFromID, string AppmtToID)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            dateSelected = INPUTAppmtDate.Value; // get dateSelected
            if (!string.IsNullOrEmpty(dateSelected))
            {
                daySelected = (DateTime.Parse(dateSelected)).DayOfWeek.ToString();
                // to get which day is operating
                bool isOperating = false;
                ShopTimeEntity shopTimeEntitySelected = null;
                foreach (ShopTimeEntity shopTimeEntity in shopTimeEntities)
                {
                    if (shopTimeEntity.DayOfWeek.ToLower().Contains(daySelected.ToLower()))
                    {
                        isOperating = true;
                        shopTimeEntitySelected = shopTimeEntity;
                        break;
                    }
                }
                // Enale drop down list to select time
                DDLAppmtTime.Enabled = isOperating;
                if (isOperating)
                {
                    MessageHandler.DefaultMessage(LBLAppmtTime, "Appointment Time");
                    MessageHandler.DefaultMessage(LBLAppmtDate, string.Concat("Appointment Date (", shopTimeEntitySelected.DayOfWeek, ")"));
                    // display operation hours of a particular day
                    var firstItem = DDLAppmtTime.Items[0];
                    DDLAppmtTime.Items.Clear();
                    DDLAppmtTime.Items.Add(firstItem);
                    DDLAppmtTime.DataSource = Utility.getTimeInterval(shopTimeEntitySelected.OpenTime, shopTimeEntitySelected.CloseTime);
                    DDLAppmtTime.DataBind();
                    List<AppointmentEntity> adoptRequestEntities = appointmentCrtler.getAllAppointmentEntities(AppmtToID);
                    foreach (AppointmentEntity appointmentEntity in adoptRequestEntities)
                    {
                        ListItem item;
                        if (daySelected.ToLower().Equals(appointmentEntity.AppmtDateTime.DayOfWeek.ToString().ToLower()))
                        {
                            // remove time slot that aleady been booked
                            item = DDLAppmtTime.Items.FindByValue(appointmentEntity.AppmtDateTime.ToString("HH:mm tt"));
                            if (item != null)
                            {
                                // except the user booked time
                                if (string.IsNullOrEmpty(AppmtFromID))
                                    DDLAppmtTime.Items.Remove(item);
                                if (!appointmentEntity.AppmtID.Equals(AppmtFromID))
                                    DDLAppmtTime.Items.Remove(item);
                            }
                        }
                    }
                    // remove time selection after operating hours on current day
                    if (dateSelected.Equals(DateTime.Now.ToString("dd-MMMM-yyyy")))
                    {
                        if ((DateTime.Parse(shopTimeEntitySelected.CloseTime).TimeOfDay < DateTime.Now.TimeOfDay))
                        {
                            MessageHandler.ErrorMessage(LBLAppmtDate, string.Concat("Appointment Date (Close Now)"));
                            DDLAppmtTime.Enabled = false;
                        }
                        else
                        {
                            DDLAppmtTime.Enabled = true;
                            // still in operation, but need to remove time slot that is past current time
                            List<string> operationTimes = new List<string>();
                            foreach (ListItem item in DDLAppmtTime.Items)
                            {
                                operationTimes.Add(item.Value);
                            }
                            ListItem itemTime = new ListItem();
                            foreach (string time in operationTimes)
                            {
                                if (!string.IsNullOrEmpty(time))
                                {
                                    itemTime = DDLAppmtTime.Items.FindByValue(time);
                                    if (DateTime.Parse(time).TimeOfDay < DateTime.Now.TimeOfDay)
                                    {
                                        DDLAppmtTime.Items.Remove(itemTime);
                                    }
                                }
                            }
                            // additional information of operation status
                            if (DDLAppmtTime.Items.Count <= 2)
                            {
                                MessageHandler.WarningMessage(LBLAppmtDate, string.Concat("Appointment Date (", shopTimeEntitySelected.DayOfWeek, ") Closing soon"));
                            }
                            else
                            {
                                MessageHandler.DefaultMessage(LBLAppmtDate, string.Concat("Appointment Date (", shopTimeEntitySelected.DayOfWeek, ")"));
                            }
                        }
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                    MessageHandler.ErrorMessage(LBLAppmtTime, "Appointment Time - Not open on selected date");
                    MessageHandler.DefaultMessage(LBLAppmtDate, string.Concat("Appointment Date"));
                }
            }
        }
    }
}