using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.EnumFolder;
using TheLittleOnesLibrary.Handler;
public partial class PetShelter : BasePageTLO
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void DLShopInfo_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label LBLShopTimeStatus = e.Item.FindControl("LBLShopTimeStatus") as Label;
            HiddenField LBLShopInfoID = e.Item.FindControl("HDFShopInfoID") as HiddenField;
            ShopInfoEntity shopInfoEntity = shopInfoCtrler.getShopInfo(LBLShopInfoID.Value);
            if (shopInfoEntity != null)
            {
                DataList DLShopTime = e.Item.FindControl("DLShopTime") as DataList;
                DLShopTime.DataSource = shopInfoEntity.ShopTimeEntities;
                DLShopTime.DataBind();
                updateOperationHourStatus(shopInfoEntity.ShopTimeEntities, LBLShopTimeStatus);
                DataList DLPhoto = e.Item.FindControl("DLPhoto") as DataList;
                if (shopInfoEntity.PhotoEntities.Count <= 0)
                {
                    List<PhotoEntity> photoDefault = new List<PhotoEntity>();
                    photoDefault.Add(new PhotoEntity("default", "assetsG5/images/default.png", Enums.GetDescription(PhotoPurpose.ShopInfo)));
                    DLPhoto.DataSource = photoDefault;
                }
                else
                {
                    DLPhoto.DataSource = shopInfoEntity.PhotoEntities;
                }
                DLPhoto.DataBind();
            }
        }
    }
    private void updateOperationHourStatus(List<ShopTimeEntity> shopTimeEntities, Label LBLShopTimeStatus)
    {
        foreach (ShopTimeEntity shopTimeEntity in shopTimeEntities)
        {
            if (DateTime.Now.DayOfWeek.ToString().Equals(shopTimeEntity.ShopDayOfWeek.ToString()))
            {
                if (DateTime.Now.TimeOfDay > (DateTime.Parse(shopTimeEntity.ShopOpenTime)).TimeOfDay &&
                    DateTime.Now.TimeOfDay < (DateTime.Parse(shopTimeEntity.ShopCloseTime)).TimeOfDay)
                {
                    MessageHandler.SuccessMessage(LBLShopTimeStatus, "Open now");
                }
                else
                {
                    MessageHandler.ErrorMessage(LBLShopTimeStatus, "Close now");
                }
                break;
            }
            else
            {
                MessageHandler.ErrorMessage(LBLShopTimeStatus, "(Close on " + DateTime.Now.DayOfWeek + ")");
            }
        }
    }
    protected void DLPhoto_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Image image = e.Item.FindControl("Image1") as Image;
        if (string.IsNullOrEmpty(image.ImageUrl))
        {
            image.ImageUrl = "assetsG5/images/default.png";
        }
    }
}