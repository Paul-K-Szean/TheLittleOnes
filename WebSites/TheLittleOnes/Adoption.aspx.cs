using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.EnumFolder;
public partial class Adoption : BasePageTLO
{
    protected void Page_Load(object sender, EventArgs e)
    {
        loadAdoption();
    }
    private void loadAdoption()
    {
        DLAdoption.DataSource = adoptInfoCtrler.getAdoptInfoEntities();
        DLAdoption.DataBind();
    }
    protected void DLAdoption_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        HiddenField HDFadoptInfoID = e.Item.FindControl("HDFadoptInfoID") as HiddenField;
        HiddenField HDFshopInfoID = e.Item.FindControl("HDFshopInfoID") as HiddenField;
        HiddenField HDFpetID = e.Item.FindControl("HDFpetID") as HiddenField;
        Image IMGBreedPhoto = e.Item.FindControl("IMGBreedPhoto") as Image;
        HyperLink HYPLKPetInfo = e.Item.FindControl("HYPLKPetInfo") as HyperLink;
        HYPLKPetInfo.NavigateUrl = string.Concat(@"~/AdoptionDetails.aspx?adoptinfoid=", HDFadoptInfoID.Value);
        TLOPhotoEntity = photoCtrler.getPhotoEntity(HDFpetID.Value, Enums.GetDescription(PhotoPurpose.Pet));
        if (TLOPhotoEntity != null)
        {
            IMGBreedPhoto.ImageUrl = (TLOPhotoEntity.PhotoPath).Replace("~/", "");
        }
        else
        {
            IMGBreedPhoto.ImageUrl = "assetsG5/images/default.png".Replace("~/", "");
        }
    }
}