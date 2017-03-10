using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;

public partial class BreedDetails : BasePageTLO
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) { }
        else
        {
            if (viewPetInfoEntity != null)
            {
                loadPetInfo();
            }
            else
            {
                Response.Redirect("Breed.aspx");
            }
        }
    }

    private void loadPetInfo()
    {
        HDFPetInfoID.Value = viewPetInfoEntity.PetInfoID;
    }

    protected void DDLPetInfo_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        viewPetCharEntity = petInfoCtrler.getPetChar(viewPetInfoEntity.PetInfoID);
        Response.Write(viewPetCharEntity.CharOverallAdaptability);

        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HtmlGenericControl charBarOverallAdaptability = e.Item.FindControl("charBarOverallAdaptability") as HtmlGenericControl;
            if (charBarOverallAdaptability != null)
            {
                charBarOverallAdaptability.Attributes["style"] = string.Concat("width: ", convertToPercentage(viewPetCharEntity.CharOverallAdaptability), "%");
            }
        }
    }

    private double convertToPercentage(string input)
    {
        double result = 0;
        Response.Write(input + ", ");
        Response.Write(double.Parse(input) / 5);
        return result;
    }
}