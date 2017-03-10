using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
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


        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HyperLink HYPLKPetInfo = e.Item.FindControl("HYPLKPetInfo") as HyperLink;
            Image image = e.Item.FindControl("imgBreedPhoto") as Image;
            DataRowView dataRowView = e.Item.DataItem as DataRowView;

            if (dataRowView != null)
            {
                petinfoID = Convert.ToString(dataRowView.Row["petinfoid"].ToString());
                photoEntities = photoCtrler.getPhotoEntities(petinfoID, PhotoPurpose.PetInfo.ToString());
                if (photoEntities != null && photoEntities.Count > 0)
                {
                    HYPLKPetInfo.NavigateUrl = image.ImageUrl = photoEntities[0].PhotoPath;
                }
                else
                {
                    HYPLKPetInfo.NavigateUrl = image.ImageUrl = "assetsG5/images/default.png";
                }
            }
        }
    }

    protected void LKBTNPetInfo_Command(object sender, CommandEventArgs e)
    {
        LinkButton LKBTNViewPetInfo = (LinkButton)(sender);
        viewPetinfoID = LKBTNViewPetInfo.CommandArgument;
        viewPetInfoEntity = petInfoCtrler.getPetInfo(viewPetinfoID);
        if (viewPetInfoEntity != null)
        {
            string redirect = "<script>window.open('BreedDetails.aspx');</script>";
            Response.Write(redirect);
        }
        else
        {
            Response.Redirect(getCurrentWebPage());
        }

    }
}