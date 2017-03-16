using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.EnumFolder;

public partial class AdoptionAppointment : BasePageTLO
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

            }
            else
            {
                loadAdoptionInfo();
                initializeUIControlValues();
            }
        }
        // check any user logged in
        TLOAccountEntity = accountCtrler.getLoggedInAccount();
    }

    #region Initialize UI Control Values
    // Initial UI control values
    private void initializeUIControlValues()
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        // initialize hour range
        List<string> timeInterval = Utility.getTimeInterval();

        DDLAppmtTime.DataSource = timeInterval;
        DDLAppmtTime.DataBind();
        DDLAppmtTime.SelectedValue = "09:00 AM";

    }
    #endregion

    #region Button Control
    protected void BTNCancel_Click(object sender, EventArgs e)
    {

    }
    #endregion
    #region Datalist Command
    protected void DLAdoptInfo_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);

    }
    #endregion
    #region Dropdownlist Control
    protected void DDLAppmtTime_SelectedIndexChanged(object sender, EventArgs e)
    {

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
    #endregion




}