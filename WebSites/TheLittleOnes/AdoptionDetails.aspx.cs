using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            if (TLOAdoptionAppointmentEntity != null && TLOAccountEntity != null)
            {
                dateSelected = INPUTAppmtDate.Value = TLOAdoptionAppointmentEntity.AppmtDateTime.ToString("dd-MMMM-yyyy");
                loadOperatingHours();
                timeSelected = DDLAppmtTime.SelectedValue = TLOAdoptionAppointmentEntity.AppmtDateTime.ToString("HH:mm tt");
            }
            else
            {
                TLOAdoptionAppointmentEntity = null;
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
            MessageHandler.SuccessMessage(LBLErrorMsg, " check for availability");
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
            // UI display
            DDLAppmtTime.Enabled = isOperating;
            BTNAdoptMe.Enabled = isOperating;
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
            }
            else
            {
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
            TLOAdoptionAppointmentEntity = new AdoptionAppointmentEntity(TLOAccountEntity, viewAdoptinfoEntity, dateTimeSelected);
        }
        else if (!string.IsNullOrEmpty(dateSelected))
        {
            dateTimeSelected = DateTime.Parse(dateSelected + " 00:00 AM");
            TLOAdoptionAppointmentEntity = new AdoptionAppointmentEntity(TLOAccountEntity, viewAdoptinfoEntity, dateTimeSelected);
        }
    }
    #endregion
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
}