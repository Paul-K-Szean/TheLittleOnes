using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.Handler;

public partial class AdminShopInfoAdd : System.Web.UI.Page
{
    private Label UICtrlLabel;
    private TextBox UICtrlTextbox;
    private DropDownList UICtrlDropdownlist;

    private string shopName;
    private string shopContact;
    private string shopAddress;
    private List<ShopHourEntity> shopHoursEntities;
    private List<PhotoEntity> photoEntities;

    // page load
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (IsPostBack)
        {
            shopName = TBShopName.Text.Trim();
            shopContact = TBContact.Text.Trim();
            shopAddress = TBAddress.Text.Trim();
        }
        else
        {
            initializeUIControlValues();
        }
    }


    #region Initialize UI Control Values
    // Initial UI control values
    private void initializeUIControlValues()
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        // initialize hour range
        List<string> timeInterval = setupHourRange();
        // loop over for controls
        foreach (Control ctrl in UpdatePanel1.ContentTemplateContainer.Controls)
        {
            // set dropdownlist values
            if (ctrl is DropDownList)
            {
                UICtrlDropdownlist = (DropDownList)ctrl;
                UICtrlDropdownlist.DataSource = timeInterval;
                UICtrlDropdownlist.DataBind();
                if (UICtrlDropdownlist.ID.ToLower().Contains("open"))
                {
                    UICtrlDropdownlist.SelectedValue = "09:00 AM";
                }

                if (UICtrlDropdownlist.ID.ToLower().Contains("close"))
                {
                    UICtrlDropdownlist.SelectedValue = "17:00 PM";
                }
            }

        }
    }


    #endregion

    #region Button Clicks
    // Preview image uploaded
    protected void BTNPreview_Click(object sender, EventArgs e)
    {
        // need category and breed to create folder
        if (!string.IsNullOrEmpty(shopName) && !string.IsNullOrEmpty(shopContact))
        {
            LBLErrorMsg.Text = string.Empty;
            string filePath_UploadFolderTemp = "~/uploadedFiles/temp/shop";
            string filePath_UploadFolderTempWithShopNameAndContact = string.Concat(filePath_UploadFolderTemp, "/", shopName.ToLower());
            bool filePathExist = Directory.Exists(Server.MapPath(filePath_UploadFolderTempWithShopNameAndContact));
            LogController.LogLine("Check directory: " + filePath_UploadFolderTempWithShopNameAndContact);
            LogController.LogLine("Check directory result: " + filePathExist);

            // check for folders path - Temp
            if (filePathExist)
            {
                // remove old files
                LogController.LogLine("Removing old files: " + filePath_UploadFolderTempWithShopNameAndContact);
                Array.ForEach(Directory.GetFiles(Server.MapPath(filePath_UploadFolderTempWithShopNameAndContact)), File.Delete);
            }
            else
            {
                // dont exists - create path
                Directory.CreateDirectory(Server.MapPath(filePath_UploadFolderTempWithShopNameAndContact));
            }
            LogController.LogLine("Temp directory: " + filePath_UploadFolderTempWithShopNameAndContact);
            // save post files to temp folder
            if (FileUpload1.HasFiles)
            {
                photoEntities = new List<PhotoEntity>();
                LogController.LogLine("Total files posted: " + FileUpload1.PostedFiles.Count);
                foreach (HttpPostedFile httpPostedFileInfo in FileUpload1.PostedFiles)
                {
                    string savePath = Path.Combine(Server.MapPath(filePath_UploadFolderTempWithShopNameAndContact), httpPostedFileInfo.FileName);
                    httpPostedFileInfo.SaveAs(savePath);
                    photoEntities.Add(new PhotoEntity(httpPostedFileInfo.FileName, savePath));
                }
            }
            // display images from temp folders - based on category and breed
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath(filePath_UploadFolderTempWithShopNameAndContact));
            photoPreview.InnerHtml = string.Empty;
            foreach (var file in dir.GetFiles("*.jpg"))
            {
                LogController.LogLine("File name: " + file.Name);
                LogController.LogLine(string.Concat(filePath_UploadFolderTempWithShopNameAndContact, "/", file.Name));
                photoPreview.InnerHtml += string.Concat(
                    "<img  src =\"",
                    string.Concat(filePath_UploadFolderTempWithShopNameAndContact, "/", file.Name).Replace("~/", ""),
                    "\" Height=\"100\" Width=\"100\"/>",
                    "<br>", file.Name, "<hr/>");
            }
        }
        else
        {
            MessageHandler.ErrorMessage(LBLErrorMsg, "Shop name and contact cannot be empty");
        }
        LogController.Log();
    }

    protected void BTNSave_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region Logical Methods
    /// <summary>
    /// Source from https://forums.asp.net/t/2000851.aspx?24+hours+time+format
    /// User: a2h
    /// Purpose: preload time interval and bind to drop down list
    /// </summary>
    private List<string> setupHourRange()
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);

        // defualt start time value
        DateTime start = DateTime.ParseExact("00:00", "HH:mm", null);
        // default end time value
        DateTime end = DateTime.ParseExact("23:59", "HH:mm", null);

        //set the interval time 
        int interval = 30;
        //list to hold the values of intervals
        List<string> listTimeIntervals = new List<string>();
        //populate the list with the interval values
        for (DateTime i = start; i <= end; i = i.AddMinutes(interval))
            listTimeIntervals.Add(i.ToString("HH:mm tt"));

        return listTimeIntervals;
        ////Assign the list to datasource of dropdownlist
        //DropDownList1.DataSource = listTimeIntervals;
        ////Databind the dropdownlist
        //DropDownList1.DataBind();
    }
    #endregion


    protected void BTNAddAddress_Click(object sender, EventArgs e)
    {
        var address = "pet lovers singapore";
        var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(address));

        var request = WebRequest.Create(requestUri);
        var response = request.GetResponse();
        var xdoc = XDocument.Load(response.GetResponseStream());

        var result = xdoc.Element("GeocodeResponse").Element("result");
        var locationElement = result.Element("geometry").Element("location");
        var lat = locationElement.Element("lat");
        var lng = locationElement.Element("lng");
        LBLErrorMsg.Text = lat + " " + lng;
    }


}