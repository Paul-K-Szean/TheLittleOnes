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
public partial class AppointmentDetails : BasePageTLO
{
    private static string adoptInfoID;
    private static AdoptInfoEntity viewAdoptinfoEntity;
    private static ShopInfoEntity viewPetShopinfoEntity;
    private static PetEntity viewPetEntity;
    protected void Page_Load(object sender, EventArgs e)
    {
        // get account info
        TLOAccountEntity = accountCtrler.getLoggedInAccount();
        if (IsPostBack) { }
        else
        {
           
        }
    }
    #region Button Control
   
    #endregion
    #region Datalist Command
    
    #endregion
    #region Dropdownlist Control
   
    #endregion
    #region Logical Methods
   
    #endregion
}