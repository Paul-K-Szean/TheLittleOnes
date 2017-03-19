using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.EnumFolder;
using TheLittleOnesLibrary.Handler;
public partial class uploadedFiles_AdoptionDetails : BasePageTLO
{
    private static string adoptInfoID;
    private static AdoptInfoEntity viewAdoptinfoEntity;
    private static ShopInfoEntity viewPetShopinfoEntity;
    private static PetEntity viewPetEntity;
    private static DateTime dateTimeSelected;
    private static string daySelected;
    private static string dateSelected;
    private static string timeSelected;

    protected void Page_Load(object sender, EventArgs e)
    {
        TLOAccountEntity = accountCtrler.getLoggedInAccount();
        if (IsPostBack)
        {
            if (string.IsNullOrEmpty(INPUTAppmtDate.Value) || string.IsNullOrEmpty(DDLAppmtTime.SelectedValue))
            {
                // no input selected
                BTNAdoptMe.Attributes["data-target "] = "";
                BTNAdoptMe.Attributes["data-toggle "] = "";
            }
            else
            {
                // input selected, check if logged in
                if (TLOAccountEntity == null)
                {
                    BTNAdoptMe.Attributes["data-target "] = "#login";
                    BTNAdoptMe.Attributes["data-toggle "] = "modal";
                }
                else
                {
                    BTNAdoptMe.Attributes["data-target "] = "";
                    BTNAdoptMe.Attributes["data-toggle "] = "";
                    BTNAdoptMe.Enabled = true;
                }
            }
        }
        else
        {
            adoptInfoID = HttpContext.Current.Request.QueryString["adoptinfoid"];
            if (string.IsNullOrEmpty(adoptInfoID))
            {
                Response.Redirect("Adoption.aspx");
            }
            else
            {
                loadAdoptionInfo();
            }
        }
    }
    #region Button Clicks
    protected void BTNAdoptMe_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        if (!string.IsNullOrEmpty(INPUTAppmtDate.Value) && !string.IsNullOrEmpty(DDLAppmtTime.SelectedValue))
        {
            UpdatePanel updatepanel = Master.FindControl("UpdatePanel1") as UpdatePanel;
            TextBox TBLoginEmail = updatepanel.ContentTemplateContainer.FindControl("TBLoginEmail") as TextBox;
            TextBox TBLoginPassword = updatepanel.ContentTemplateContainer.FindControl("TBLoginPassword") as TextBox;
            MessageHandler.ClearMessage(TBLoginEmail);
            MessageHandler.ClearMessage(TBLoginPassword);
            MessageHandler.ClearMessage(LBLErrorMsg);
            if (TLOAdoptRequestEntity != null && TLOAccountEntity != null)
            {
                TLOAdoptRequestEntity.AccountEntity = TLOAccountEntity;
                TLOAdoptRequestEntity = adoptInfoCtrler.createAdoptRequest(TLOAdoptRequestEntity);
                if (!string.IsNullOrEmpty(TLOAdoptRequestEntity.AdoptReqID))
                {
                    MessageHandler.SuccessMessage(LBLErrorMsg, "Request created");
                    Response.Redirect(string.Concat(getCurrentWebPage(), "?adoptinfoid=", adoptInfoID));
                }
                else
                {
                    MessageHandler.ErrorMessageAdmin(LBLErrorMsg, "Unable to make request.");
                }
            }// else just started, TODO: what to do 
        }
        else
        {
            MessageHandler.ErrorMessage(LBLErrorMsg, "Appointment date or time cannot be empty");
        }
    }
    protected void BTNAppmtDate_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        // to get date selected from bootstrap datepicker 
        loadOperatingHours();
        // save temp appointment data if user selected any date/time
        saveTempAppointment();
    }
    #endregion
    #region Datalist Command
    protected void DLAdoptInfo_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        Label LBLTotalRequest = e.Item.FindControl("LBLTotalRequest") as Label;
        LBLTotalRequest.Text = string.Concat("Total Request: ", adoptInfoCtrler.getAllAdoptRequestEntities(adoptInfoID).Count.ToString());
        checkAdoptionDetails();
    }
    protected void DLMorePet_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        HyperLink HYPLKMorePet = e.Item.FindControl("HYPLKMorePet") as HyperLink;
        HiddenField HDFMoreAdoptInfoID = e.Item.FindControl("HDFMoreAdoptInfoID") as HiddenField;
        HiddenField HDFMorePetID = e.Item.FindControl("HDFMorePetID") as HiddenField;
        // photos
        Image IMGPhoto = e.Item.FindControl("IMGPhoto") as Image;
        TLOPhotoEntities = photoCtrler.getPhotoEntities(HDFMorePetID.Value.Trim(), "Pet");
        if (TLOPhotoEntities != null && TLOPhotoEntities.Count > 0)
        {
            IMGPhoto.ImageUrl = TLOPhotoEntities[0].PhotoPath.Replace("~/", "");
            HYPLKMorePet.NavigateUrl = "AdoptionDetails.aspx?adoptinfoid=" + HDFMoreAdoptInfoID.Value;
        }
        else
            IMGPhoto.ImageUrl = "assetsG5/images/default.png";
    }
    #endregion
    #region Logical Methods
    // Load adoption info
    protected void loadAdoptionInfo()
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        viewAdoptinfoEntity = adoptInfoCtrler.getAdoptInfo(adoptInfoID);
        HDFAdoptInfoID.Value = viewAdoptinfoEntity.AdoptInfoID;
        HDFPetID.Value = viewAdoptinfoEntity.PetEntity.PetID;
        HDFShopInfoID.Value = viewAdoptinfoEntity.ShopInfoEntity.ShopInfoID;
    }
    // Load operating hours based on different days
    protected void loadOperatingHours()
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        dateSelected = INPUTAppmtDate.Value; // get dateSelected
        if (!string.IsNullOrEmpty(dateSelected))
        {
            daySelected = (DateTime.Parse(dateSelected)).DayOfWeek.ToString();
            // to get which day is operating
            bool isOperating = false;
            ShopTimeEntity shopTimeEntitySelected = null;
            foreach (ShopTimeEntity shopTimeEntity in viewAdoptinfoEntity.ShopInfoEntity.ShopTimeEntities)
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
                List<AdoptRequestEntity> adoptRequestEntities = adoptInfoCtrler.getAllAdoptRequestEntities(adoptInfoID);
                foreach (AdoptRequestEntity adopRequestEntity in adoptRequestEntities)
                {
                    ListItem item;
                    if (daySelected.ToLower().Equals(adopRequestEntity.AdoptReqDateTime.DayOfWeek.ToString().ToLower()))
                    {
                        item = DDLAppmtTime.Items.FindByValue(adopRequestEntity.AdoptReqDateTime.ToString("HH:mm tt"));
                        if (item != null) { DDLAppmtTime.Items.Remove(item); }
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
    // Save current selected appointment date/time
    protected void saveTempAppointment()
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        dateSelected = INPUTAppmtDate.Value;
        timeSelected = DDLAppmtTime.SelectedValue.Trim();
        if (!string.IsNullOrEmpty(dateSelected) && !string.IsNullOrEmpty(timeSelected))
        {
            dateTimeSelected = DateTime.Parse(dateSelected + " " + timeSelected);
            TLOAdoptRequestEntity = new AdoptRequestEntity(TLOAccountEntity, viewAdoptinfoEntity, dateTimeSelected, DateTime.Now, SystemStatus.Pending.ToString());
        }
        else if (!string.IsNullOrEmpty(dateSelected))
        {
            dateTimeSelected = DateTime.Parse(dateSelected + " 00:00 AM");
            TLOAdoptRequestEntity = new AdoptRequestEntity(TLOAccountEntity, viewAdoptinfoEntity, dateTimeSelected, DateTime.Now, SystemStatus.Pending.ToString());
        }
    }
    // Check if adoption already being requested
    private void checkAdoptionDetails()
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        if (TLOAccountEntity != null)
        {
            // user logged in, 
            if (adoptInfoCtrler.checkAdoptRequestExist(TLOAccountEntity.AccountID, adoptInfoID))
            {
                // already requested
                PNLAdoptReqExist.Visible = true;
                TLOAdoptRequestEntity = adoptInfoCtrler.getUserAdoptRequestEntity(TLOAccountEntity.AccountID, adoptInfoID);
                LBLAppmtDateTimeExistDetails.Text = TLOAdoptRequestEntity.AdoptReqDateTime.ToString("dd-MMMM-yyy @ HH:mm tt");
                // display request status
                if (TLOAdoptRequestEntity.AdoptReqStatus.Equals(Enums.GetDescription(SystemStatus.Confirmed)))
                    MessageHandler.SuccessMessage(LBLAppmtDateTimeStatusDetails, TLOAdoptRequestEntity.AdoptReqStatus);
                if (TLOAdoptRequestEntity.AdoptReqStatus.Equals(Enums.GetDescription(SystemStatus.Cancelled)))
                    MessageHandler.WarningMessage(LBLAppmtDateTimeStatusDetails, TLOAdoptRequestEntity.AdoptReqStatus);
            }
            else
            {
                // new requested
                PNLAdoptReq.Visible = true;
                if (TLOAdoptRequestEntity != null)
                {
                    if (TLOAdoptRequestEntity.AdoptInfoEntity.AdoptInfoID.Equals(adoptInfoID))
                    {
                        dateSelected = INPUTAppmtDate.Value = TLOAdoptRequestEntity.AdoptReqDateTime.ToString("dd-MMMM-yyyy");
                        loadOperatingHours();
                        timeSelected = DDLAppmtTime.SelectedValue = TLOAdoptRequestEntity.AdoptReqDateTime.ToString("HH:mm tt");
                    }
                }
                else
                {
                    TLOAdoptRequestEntity = null;
                }
            }
        }
        else
        {
            PNLAdoptReq.Visible = true;
            INPUTAppmtDate.Attributes["disabled"] = "disabled";
            MessageHandler.ErrorMessage(LBLErrorMsg, "Please log in first ");
        }
    }
    #endregion
    #region Dropdownlist Controls
    protected void DDLAppmtTime_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        if (DDLAppmtTime.SelectedIndex == 0) BTNAdoptMe.Enabled = false;
        else
        {
            BTNAdoptMe.Enabled = true;
            saveTempAppointment();
        }
    }
    #endregion
}