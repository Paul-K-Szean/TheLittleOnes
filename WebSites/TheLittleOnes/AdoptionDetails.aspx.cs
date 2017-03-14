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
    private static Label LBLAdoptInfoStatus;

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
        LBLAdoptInfoStatus = e.Item.FindControl("LBLAdoptInfoStatus") as Label;
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

    protected void BTNAdoptMe_Click(object sender, EventArgs e)
    {
        Label17.Text = "Login First Pending (1)";


    }

    protected void DLMorePet_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        HyperLink HYPLKMorePet = e.Item.FindControl("HYPLKMorePet") as HyperLink;
        HiddenField HDFMoreAdoptInfoID = e.Item.FindControl("HDFMoreAdoptInfoID") as HiddenField;
        HiddenField HDFMorePetID = e.Item.FindControl("HDFMorePetID") as HiddenField;
        Image IMGPhoto = e.Item.FindControl("IMGPhoto") as Image;

        photoEntities = photoCtrler.getPhotoEntities(HDFMorePetID.Value.Trim(), "Pet");
        if (photoEntities != null)
        {
            IMGPhoto.ImageUrl = photoEntities[0].PhotoPath.Replace("~/", "");
            HYPLKMorePet.NavigateUrl = "AdoptionDetails.aspx?adoptinfoid=" + HDFMoreAdoptInfoID.Value;
        }
        else
            IMGPhoto.ImageUrl = "assetsG5/images/default.png";
    }
}