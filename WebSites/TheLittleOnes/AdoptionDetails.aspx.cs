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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) { }
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
        // check any user logged in
        TLOAccountEntity = accountCtrler.getLoggedInAccount();
        if (TLOAccountEntity == null)
        {
            BTNAdoptMe.Attributes["data-target "] = "#login";
            BTNAdoptMe.Attributes["data-toggle "] = "modal";
        }
        else
        {

            BTNAdoptMe.Attributes["data-target "] = "";
            BTNAdoptMe.Attributes["data-toggle "] = "";
        }
    }

    #region Button Clicks
    protected void BTNAdoptMe_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        UpdatePanel updatepanel = Master.FindControl("UpdatePanel1") as UpdatePanel;
        TextBox TBLoginEmail = updatepanel.ContentTemplateContainer.FindControl("TBLoginEmail") as TextBox;
        TextBox TBLoginPassword = updatepanel.ContentTemplateContainer.FindControl("TBLoginPassword") as TextBox;
        Label LBLErrorMsg = updatepanel.ContentTemplateContainer.FindControl("LBLErrorMsg") as Label;

        MessageHandler.ClearMessage(TBLoginEmail);
        MessageHandler.ClearMessage(TBLoginPassword);
        MessageHandler.ClearMessage(LBLErrorMsg);

        Label9.Text = " check for availability";


    }
    protected void BTNAppmtDate_Click(object sender, EventArgs e)
    {
        // to get date selected from bootstrap datepicker 
        checkOperatingHours();
    }
    #endregion

    #region Datalist Command
    protected void DLAdoptInfo_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);

    }
    protected void DLMorePet_ItemDataBound(object sender, DataListItemEventArgs e)
    {

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
    private void loadAdoptionInfo()
    {
        viewAdoptinfoEntity = adoptInfoCtrler.getAdoptInfo(adoptInfoID);
        HDFAdoptInfoID.Value = viewAdoptinfoEntity.AdoptInfoID;
        HDFPetID.Value = viewAdoptinfoEntity.PetEntity.PetID;
        HDFShopInfoID.Value = viewAdoptinfoEntity.ShopInfoEntity.ShopInfoID;
    }
    protected void checkOperatingHours()
    {
        string dateSelected = INPUTAppmtDate.Value;
        DateTime dateTimeSelected = DateTime.Parse(dateSelected);
        string daySelected = dateTimeSelected.DayOfWeek.ToString();
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
            // allow only start time to end time of operation
            DDLAppmtTime.DataSource = Utility.getTimeInterval(shopTimeEntitySelected.OpenTime, shopTimeEntitySelected.CloseTime);
            DDLAppmtTime.DataBind();

        }
        else
        {
            MessageHandler.ErrorMessage(LBLAppmtTime, "Appointment Time - Not open on selected date");
            MessageHandler.DefaultMessage(LBLAppmtDate, string.Concat("Appointment Date"));
        }
    }
    #endregion




    protected void DDLAppmtTime_SelectedIndexChanged(object sender, EventArgs e)
    {
        // TODO Check for availablility
    }
}