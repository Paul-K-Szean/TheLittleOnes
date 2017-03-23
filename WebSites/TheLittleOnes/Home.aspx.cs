using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
public partial class Home : BasePageTLO
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TLOAccountEntity = accountCtrler.getLoggedInAccount();
    }
}