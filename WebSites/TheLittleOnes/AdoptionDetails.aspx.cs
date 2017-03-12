using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.EnumFolder;

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
    }

    private void loadAdoptionInfo()
    {
        viewAdoptinfoEntity = adoptInfoCtrler.getAdoptInfo(adoptInfoID);
        HDFAdoptInfoID.Value = viewAdoptinfoEntity.AdoptInfoID;
        HDFPetID.Value = viewAdoptinfoEntity.PetEntity.PetID;
        HDFShopInfoID.Value = viewAdoptinfoEntity.ShopInfoEntity.ShopInfoID;
    }

    protected void DLAdoptInfo_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Label LBLAdoptInfoStatus = e.Item.FindControl("LBLAdoptInfoStatus") as Label;
        if (LBLAdoptInfoStatus != null)
        {
            if (viewAdoptinfoEntity.AdoptInfoStatus.Equals(AdoptionStatus.Adopted.ToString()))
            {
                LBLAdoptInfoStatus.ForeColor = Utility.getErrorColor();
            }
            if (viewAdoptinfoEntity.AdoptInfoStatus.Equals(AdoptionStatus.Available.ToString()))
            {
                LBLAdoptInfoStatus.ForeColor = Utility.getSuccessColor();
            }
            if (viewAdoptinfoEntity.AdoptInfoStatus.Equals(AdoptionStatus.Pending.ToString()))
            {
                LBLAdoptInfoStatus.ForeColor = Utility.getWarningColor();
            }
        }
    }
}