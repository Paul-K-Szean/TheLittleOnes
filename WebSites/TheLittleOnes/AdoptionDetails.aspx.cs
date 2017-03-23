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
            if (TLOAppointmentEntity != null && TLOAccountEntity != null)
            {
                TLOAppointmentEntity.AccountEntity = TLOAccountEntity;
                TLOAppointmentEntity = appointmentCrtler.createAppointment(TLOAppointmentEntity);
                if (!string.IsNullOrEmpty(TLOAppointmentEntity.AppmtID))
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
        // loadOperatingHours();
        loadOperatingHours(dateSelected, daySelected, INPUTAppmtDate,
                           shopInfoCtrler.getShopTime(HDFShopInfoID.Value), DDLAppmtTime, LBLAppmtDate, LBLAppmtTime, null, adoptInfoID);
        // save temp appointment data if user selected any date/time
        saveTempAppointment();
    }
    #endregion
    #region Datalist Command
    protected void DLAdoptInfo_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        Label LBLTotalRequest = e.Item.FindControl("LBLTotalRequest") as Label;
        LBLTotalRequest.Text = string.Concat("Total Request: ", appointmentCrtler.getAllAppointmentEntities(adoptInfoID).Count.ToString());
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
    //protected void loadOperatingHours()
    //{
    //    LogController.LogLine(MethodBase.GetCurrentMethod().Name);
    //    dateSelected = INPUTAppmtDate.Value; // get dateSelected
    //    if (!string.IsNullOrEmpty(dateSelected))
    //    {
    //        daySelected = (DateTime.Parse(dateSelected)).DayOfWeek.ToString();
    //        // to get which day is operating
    //        bool isOperating = false;
    //        ShopTimeEntity shopTimeEntitySelected = null;
    //        foreach (ShopTimeEntity shopTimeEntity in viewAdoptinfoEntity.ShopInfoEntity.ShopTimeEntities)
    //        {
    //            if (shopTimeEntity.DayOfWeek.ToLower().Contains(daySelected.ToLower()))
    //            {
    //                isOperating = true;
    //                shopTimeEntitySelected = shopTimeEntity;
    //                break;
    //            }
    //        }
    //        // Enale drop down list to select time
    //        DDLAppmtTime.Enabled = isOperating;
    //        if (isOperating)
    //        {
    //            MessageHandler.DefaultMessage(LBLAppmtTime, "Appointment Time");
    //            MessageHandler.DefaultMessage(LBLAppmtDate, string.Concat("Appointment Date (", shopTimeEntitySelected.DayOfWeek, ")"));
    //            // display operation hours of a particular day
    //            var firstItem = DDLAppmtTime.Items[0];
    //            DDLAppmtTime.Items.Clear();
    //            DDLAppmtTime.Items.Add(firstItem);
    //            DDLAppmtTime.DataSource = Utility.getTimeInterval(shopTimeEntitySelected.OpenTime, shopTimeEntitySelected.CloseTime);
    //            DDLAppmtTime.DataBind();
    //            List<AppointmentEntity> adoptRequestEntities = appointmentCrtler.getAllAppointmentEntities(adoptInfoID);
    //            foreach (AppointmentEntity appointmentEntity in adoptRequestEntities)
    //            {
    //                ListItem item;
    //                if (daySelected.ToLower().Equals(appointmentEntity.AppmtDateTime.DayOfWeek.ToString().ToLower()))
    //                {
    //                    // remove time slot that aleady been booked
    //                    item = DDLAppmtTime.Items.FindByValue(appointmentEntity.AppmtDateTime.ToString("HH:mm tt"));
    //                    if (item != null) { DDLAppmtTime.Items.Remove(item); }
    //                }
    //            } 
    //            // remove time selection after operating hours on current day
    //            if (dateSelected.Equals(DateTime.Now.ToString("dd-MMMM-yyyy")))
    //            {
    //                if ((DateTime.Parse(shopTimeEntitySelected.CloseTime).TimeOfDay < DateTime.Now.TimeOfDay))
    //                {
    //                    MessageHandler.ErrorMessage(LBLAppmtDate, string.Concat("Appointment Date (Close Now)"));
    //                    DDLAppmtTime.Enabled = false;
    //                }
    //                else
    //                {
    //                    MessageHandler.DefaultMessage(LBLAppmtDate, string.Concat("Appointment Date (", shopTimeEntitySelected.DayOfWeek, ")"));
    //                    DDLAppmtTime.Enabled = true;
    //                    // still in operation, but need to remove time slot that is past current time
    //                    List<string> operationTimes = new List<string>();
    //                    foreach (ListItem item in DDLAppmtTime.Items)
    //                    {
    //                        operationTimes.Add(item.Value);
    //                    }
    //                    ListItem itemTime = new ListItem();
    //                    foreach (string time in operationTimes)
    //                    {
    //                        if (!string.IsNullOrEmpty(time))
    //                        {
    //                            itemTime = DDLAppmtTime.Items.FindByValue(time);
    //                            if (DateTime.Parse(time).TimeOfDay < DateTime.Now.TimeOfDay)
    //                            {
    //                                DDLAppmtTime.Items.Remove(itemTime);
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            Thread.Sleep(1000);
    //            MessageHandler.ErrorMessage(LBLAppmtTime, "Appointment Time - Not open on selected date");
    //            MessageHandler.DefaultMessage(LBLAppmtDate, string.Concat("Appointment Date"));
    //        }
    //    }
    //}
    // Save current selected appointment date/time
    protected void saveTempAppointment()
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        dateSelected = INPUTAppmtDate.Value;
        timeSelected = DDLAppmtTime.SelectedValue.Trim();
        if (!string.IsNullOrEmpty(dateSelected) && !string.IsNullOrEmpty(timeSelected))
        {
            dateTimeSelected = DateTime.Parse(dateSelected + " " + timeSelected);
            TLOAppointmentEntity = new AppointmentEntity(TLOAccountEntity, viewAdoptinfoEntity.AdoptInfoID, dateTimeSelected, DateTime.Now, SystemStatus.Pending.ToString(), Enums.GetDescription(AppointmentType.Adoption));
        }
        else if (!string.IsNullOrEmpty(dateSelected))
        {
            dateTimeSelected = DateTime.Parse(dateSelected + " 00:00 AM");
            TLOAppointmentEntity = new AppointmentEntity(TLOAccountEntity, viewAdoptinfoEntity.AdoptInfoID, dateTimeSelected, DateTime.Now, SystemStatus.Pending.ToString(), Enums.GetDescription(AppointmentType.Adoption));
        }
    }
    // Check if adoption already being requested
    private void checkAdoptionDetails()
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        if (TLOAccountEntity != null)
        {
            // user logged in, 
            if (appointmentCrtler.checkAppointmentExist(TLOAccountEntity.AccountID, adoptInfoID))
            {
                // already requested
                PNLAdoptReqExist.Visible = true;
                TLOAppointmentEntity = appointmentCrtler.getUserAppointmentEntity(TLOAccountEntity.AccountID, adoptInfoID);
                LBLAppmtDateTimeExistDetails.Text = TLOAppointmentEntity.AppmtDateTime.ToString("dd-MMMM-yyy @ HH:mm tt");
                // display request status
                if (TLOAppointmentEntity.AppmtStatus.Equals(Enums.GetDescription(SystemStatus.Confirmed)))
                    MessageHandler.SuccessMessage(LBLAppmtDateTimeStatusDetails, TLOAppointmentEntity.AppmtStatus);
                if (TLOAppointmentEntity.AppmtStatus.Equals(Enums.GetDescription(SystemStatus.Cancelled)))
                    MessageHandler.WarningMessage(LBLAppmtDateTimeStatusDetails, TLOAppointmentEntity.AppmtStatus);
            }
            else
            {
                // new requested
                PNLAdoptReq.Visible = true;
                if (TLOAppointmentEntity != null)
                {
                    if (TLOAppointmentEntity.AppmtToID.Equals(adoptInfoID))
                    {
                        dateSelected = INPUTAppmtDate.Value = TLOAppointmentEntity.AppmtDateTime.ToString("dd-MMMM-yyyy");
                        loadOperatingHours(dateSelected, daySelected, INPUTAppmtDate,
                           shopInfoCtrler.getShopTime(HDFShopInfoID.Value), DDLAppmtTime, LBLAppmtDate, LBLAppmtTime, null, adoptInfoID);
                        timeSelected = DDLAppmtTime.SelectedValue = TLOAppointmentEntity.AppmtDateTime.ToString("HH:mm tt");
                    }
                }
                else
                {
                    TLOAppointmentEntity = null;
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