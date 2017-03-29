using System;
using System.Data;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.EnumFolder;
public partial class Breed : BasePageTLO
{
    private static string petinfoID;
    private string viewPetinfoID;
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void DLPetInfo_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        HyperLink HYPLKPetInfo = e.Item.FindControl("HYPLKPetInfo") as HyperLink;
        Image image = e.Item.FindControl("imgBreedPhoto") as Image;
        DataRowView dataRowView = e.Item.DataItem as DataRowView;
        if (dataRowView != null)
        {
            petinfoID = dataRowView.Row["petinfoid"].ToString();
            TLOPhotoEntities = photoCtrler.getPhotoEntities(petinfoID, Enums.GetDescription(PhotoPurpose.PetInfo));
            if (TLOPhotoEntities != null && TLOPhotoEntities.Count > 0)
            {
                HYPLKPetInfo.NavigateUrl = image.ImageUrl = TLOPhotoEntities[0].PhotoPath;
            }
            else
            {
                HYPLKPetInfo.NavigateUrl = image.ImageUrl = "assetsG5/images/default.png";
            }
        }
    }
}