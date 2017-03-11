using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Entities;

public partial class BreedDetails : BasePageTLO
{
    private static string petinfoID;
    private static PetInfoEntity viewPetInfoEntity;
    private static PetCharEntity viewPetCharEntity;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) { }
        else
        {
            petinfoID = HttpContext.Current.Request.QueryString["petinfoid"];
            if (string.IsNullOrEmpty(petinfoID))
            {
                Response.Redirect("Breed.aspx");
            }
            else
            {
                loadPetInfo();
            }
        }
    }

    private void loadPetInfo()
    {
        viewPetInfoEntity = petInfoCtrler.getPetInfo(petinfoID);
        viewPetCharEntity = petInfoCtrler.getPetChar(petinfoID);
        HDFPetInfoID.Value = viewPetInfoEntity.PetInfoID;
    }

    protected void DDLPetInfo_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        viewPetCharEntity = petInfoCtrler.getPetChar(viewPetInfoEntity.PetInfoID);

        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label LBLOverallScore = e.Item.FindControl("LBLOverallScore") as Label;
            HtmlGenericControl InlineAdaptability = e.Item.FindControl("InlineAdaptability") as HtmlGenericControl;
            HtmlGenericControl InlineFriendliness = e.Item.FindControl("InlineFriendliness") as HtmlGenericControl;
            HtmlGenericControl InlineGrooming = e.Item.FindControl("InlineGrooming") as HtmlGenericControl;
            HtmlGenericControl InlineTrainability = e.Item.FindControl("InlineTrainability") as HtmlGenericControl;
            HtmlGenericControl InlineExercise = e.Item.FindControl("InlineExercise") as HtmlGenericControl;

            HtmlGenericControl charBarOverallAdaptability = e.Item.FindControl("charBarOverallAdaptability") as HtmlGenericControl;
            HtmlGenericControl charBarOverallFriendliness = e.Item.FindControl("charBarOverallFriendliness") as HtmlGenericControl;
            HtmlGenericControl charBarOverallGrooming = e.Item.FindControl("charBarOverallGrooming") as HtmlGenericControl;
            HtmlGenericControl charBarOverallTrainability = e.Item.FindControl("charBarOverallTrainability") as HtmlGenericControl;
            HtmlGenericControl charBarOverallExercise = e.Item.FindControl("charBarOverallExercise") as HtmlGenericControl;

            double subTotal = calculateSubTotal(viewPetCharEntity);
            LBLOverallScore.Text = "Total Score " + subTotal + " / 100";
            InlineAdaptability.Attributes["style"] = string.Concat("width: ", convertToPercentage(subTotal, viewPetCharEntity.CharOverallAdaptability), "%");
            InlineFriendliness.Attributes["style"] = string.Concat("width: ", convertToPercentage(subTotal, viewPetCharEntity.CharOverallFriendliness), "%");
            InlineGrooming.Attributes["style"] = string.Concat("width: ", convertToPercentage(subTotal, viewPetCharEntity.CharOverallGrooming), "%");
            InlineTrainability.Attributes["style"] = string.Concat("width: ", convertToPercentage(subTotal, viewPetCharEntity.CharOverallTrainability), "%");
            InlineExercise.Attributes["style"] = string.Concat("width: ", convertToPercentage(subTotal, viewPetCharEntity.CharOverallExercise), "%");

            // Individual satistic
            charBarOverallAdaptability.Attributes["style"] = string.Concat("width: ", double.Parse(viewPetCharEntity.CharOverallAdaptability) * 20, "%");
            charBarOverallFriendliness.Attributes["style"] = string.Concat("width: ", double.Parse(viewPetCharEntity.CharOverallFriendliness) * 20, "%");
            charBarOverallGrooming.Attributes["style"] = string.Concat("width: ", double.Parse(viewPetCharEntity.CharOverallGrooming) * 20, "%");
            charBarOverallTrainability.Attributes["style"] = string.Concat("width: ", double.Parse(viewPetCharEntity.CharOverallTrainability) * 20, "%");
            charBarOverallExercise.Attributes["style"] = string.Concat("width: ", double.Parse(viewPetCharEntity.CharOverallExercise) * 20, "%");


        }
    }

    private double calculateSubTotal(PetCharEntity viewPetCharEntity)
    {
        double result = double.Parse(viewPetCharEntity.CharOverallAdaptability) +
                        double.Parse(viewPetCharEntity.CharOverallFriendliness) +
                        double.Parse(viewPetCharEntity.CharOverallGrooming) +
                        double.Parse(viewPetCharEntity.CharOverallTrainability) +
                        double.Parse(viewPetCharEntity.CharOverallExercise);
        return result * 4;
    }

    private double convertToPercentage(double subTotal, string individualScore)
    {

        double result = 0;
        result = (100 / subTotal) * double.Parse(individualScore) * 4;
        return result;
    }
}