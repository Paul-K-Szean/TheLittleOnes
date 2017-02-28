using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.Handler;

public partial class AdminDashboard : BasePage
{
    private static int GVRowIDPetInfo;
    private static int GVRowIDShopInfo;
    private static int gvPageSizePetInfo = 10; // default
    private static int gvPageSizeShopInfo = 10; // default

    private DataTable dTable;
    // Page load
    protected void Page_Load(object sender, EventArgs e)
    {
        // Set page sizes of Gridviews
        gvPageSizePetInfo = int.Parse(DDLDisplayRecordCountPetInfo.SelectedValue);
        GVPetInfoOverview.PageSize = gvPageSizePetInfo;
        gvPageSizeShopInfo = int.Parse(DDLDisplayRecordCountPetInfo.SelectedValue);
        GVShopInfoOverview.PageSize = gvPageSizeShopInfo;

        if (IsPostBack)
        {

        }
        else
        {

        }


        // dynamic text for search result
        loadSearchResultLabel();
    }

    private void loadSearchResultLabel()
    {
        // pet info
        if (!string.IsNullOrEmpty(TBSearchPetInfo.Text))
        {
            LBLSearchResultPetInfo.Text = "Result for \"" + TBSearchPetInfo.Text + "\"";
        }
        else
        {
            LBLSearchResultPetInfo.Text = "Result for Pet Info";
        }
    }


    #region Checkbox Control
    // Shop info filter pet shop
    protected void CHKBXFilterShop_CheckedChanged(object sender, EventArgs e)
    {
        if (CHKBXFilterPetShop.Checked)
        {
            CHKBXFilterPetClinic.Checked = false;
        }
        //filter data
        filterData();
    }
    // Shop info filter pet clinic
    protected void CHKBXFilterClinic_CheckedChanged(object sender, EventArgs e)
    {
        if (CHKBXFilterPetClinic.Checked)
        {
            CHKBXFilterGrooming.Checked = false;
            CHKBXFilterPetShop.Checked = false;
        }
        //filter data
        filterData();
    }
    // Shop info filter grooming service
    protected void CHKBXFilterGrooming_CheckedChanged(object sender, EventArgs e)
    {
        if (CHKBXFilterGrooming.Checked)
        {
            CHKBXFilterPetClinic.Checked = false;
        }
        //filter data
        filterData();
    }
    #endregion

    #region Drop Down List Control
    // Pet info
    protected void DDLDisplayRecordCountPetInfo_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvPageSizePetInfo = int.Parse(DDLDisplayRecordCountPetInfo.SelectedValue);
        GVPetInfoOverview.PageSize = gvPageSizePetInfo;
    }
    // Shop info
    protected void DDLDisplayRecordCountShopInfo_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvPageSizeShopInfo = int.Parse(DDLDisplayRecordCountShopInfo.SelectedValue);
        GVShopInfoOverview.PageSize = gvPageSizeShopInfo;
    }
    #endregion

    #region Gridview Control
    // Pet info controls
    protected void GVPetInfoOverview_DataBound(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        DataView dataView = (DataView)SDSPetInfo.Select(DataSourceSelectArguments.Empty);
        int totalSize = dataView.Count;
        int currentPageIndex = GVPetInfoOverview.PageIndex + 1;
        int pageSize = GVPetInfoOverview.PageSize * currentPageIndex;
        int rowSize = GVPetInfoOverview.Rows.Count;

        if (pageSize > totalSize)
            pageSize = totalSize;
        LBLEntriesCountPetInfo.Text = string.Concat("Showing ", currentPageIndex, " to ", pageSize, " of ", totalSize, " entries");


    }
    // Pet info controls
    protected void GVPetInfoOverview_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        PNLPetInfoDetails.Visible = true;

        GridViewRow row = GVPetInfoOverview.Rows[e.NewSelectedIndex];
        GVRowIDPetInfo = Convert.ToInt32(GVPetInfoOverview.DataKeys[row.RowIndex].Values[0]);
        petInfoEntity = petInfoCtrler.getPetInfo(GVRowIDPetInfo.ToString());
   
        loadPetInfo(petInfoEntity);
    }
    // Pet info controls
    protected void GVPetInfoOverview_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        highlightSelectedRow(GVPetInfoOverview);
        dTable = ((DataView)SDSPetChar.Select(DataSourceSelectArguments.Empty)).Table as DataTable;
        loadPieChartPetInfo(dTable);
    }

    // Shop info controls
    protected void GVShopInfoOverview_DataBound(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        DataView dataView = (DataView)SDSShopInfo.Select(DataSourceSelectArguments.Empty);
        int totalSize = dataView.Count;
        int currentPageIndex = GVShopInfoOverview.PageIndex + 1;
        int pageSize = GVShopInfoOverview.PageSize * currentPageIndex;
        int rowSize = GVShopInfoOverview.Rows.Count;

        if (pageSize > totalSize)
            pageSize = totalSize;
        LBLEntriesCountShopInfo.Text = string.Concat("Showing ", currentPageIndex, " to ", pageSize, " of ", totalSize, " entries");

    }
    // Shop info controls
    protected void GVShopInfoOverview_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        GridViewRow row = GVShopInfoOverview.Rows[e.NewSelectedIndex];
        GVRowIDShopInfo = Convert.ToInt32(GVShopInfoOverview.DataKeys[row.RowIndex].Values[0]);
        shopInfoEntity = shopInfoCtrler.getShopInfo(GVRowIDShopInfo.ToString());
        loadShopInfo(shopInfoEntity);
        PNLShopInfoDetails.Visible = true;
    }
    // Shop info controls
    protected void GVShopInfoOverview_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        highlightSelectedRow(GVShopInfoOverview);
    }
    #endregion

    #region Logical Methods
    // Pet Info Chart
    private void loadPieChartPetInfo(DataTable dTable)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        List<string> dataLabel = new List<string>();
        List<string> dataValue = new List<string>();
        Font fontLabel = new Font(new FontFamily("Arial"), 15);
        Font fontLegend = new Font(new FontFamily("Arial"), 10);

        foreach (DataColumn column in dTable.Columns)
        {
            if (column.ColumnName.Contains("charOverall"))
            {
                dataLabel.Add(column.ColumnName.After("charOverall"));
                dataValue.Add(dTable.Rows[0][column.ColumnName].ToString());
            }
        }

        ChartArea chartArea = new ChartArea();

        chartArea.AxisX.MajorGrid.LineColor = Color.Black;
        chartArea.AxisY.MajorGrid.LineColor = Color.Black;
        // label settings

        // 3D settings
        chartArea.Area3DStyle.Enable3D = true;

        chartArea.Area3DStyle.Inclination = 45;
        chartArea.Area3DStyle.IsRightAngleAxes = false;
        chartArea.Position = new ElementPosition(0, 12, 100, 100);
        chart1.ChartAreas.Add(chartArea);

        Series series = new Series();
        series.Name = "Series1";
        series.ChartType = SeriesChartType.Pie;
        series.IsValueShownAsLabel = true;
        series.Font = fontLabel;
        chart1.Series.Add(series);

        chart1.Legends.Add(new Legend());
        chart1.Legends[0].Font = fontLegend;
        chart1.Legends[0].Enabled = true;
        chart1.Legends[0].Position.Auto = false;
        chart1.Legends[0].Position = new ElementPosition(0, 0, 100, 20);

        chart1.Palette = ChartColorPalette.Pastel;

        chart1.Width = 400;

        // bind the datapoints
        chart1.Series["Series1"].Points.DataBindXY(dataLabel, dataValue);

        // draw!
        chart1.DataBind();

        // write out a file
        // chart1.SaveImage(Server.MapPath("chart.png"), ChartImageFormat.Png);
    }
    // Pet Info Details
    private void loadPetInfo(PetInfoEntity petInfoEntity)
    {
        LBLCategory.Text = petInfoEntity.PetCategory;
        LBLBreed.Text = petInfoEntity.PetBreed;
        LBLDesc.Text = petInfoEntity.PetDesc;
        LBLPersonality.Text = petInfoEntity.PetPersonality;
    }
    // Shop Info
    private void loadShopInfo(ShopInfoEntity shopInfoEntity)
    {
        // shop info
        LBLShopName.Text = shopInfoEntity.ShopInfoName;
        LBLShopInfoContact.Text = shopInfoEntity.ShopInfoContact;
        LBLShopInfoAddress.Text = shopInfoEntity.ShopInfoAddress;
        LBLShopInfoDesc.Text = shopInfoEntity.ShopInfoDesc;

        List<string> dayOfWeek = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        // operating hours
        foreach (ShopTimeEntity shopTimeEntity in shopInfoEntity.ShopTimeEntities)
        {
            if (DateTime.Now.DayOfWeek.ToString().Equals(shopTimeEntity.DayOfWeek.ToString()))
            {

                if (DateTime.Now.TimeOfDay > (DateTime.Parse(shopTimeEntity.OpenTime)).TimeOfDay &&
                    DateTime.Now.TimeOfDay < (DateTime.Parse(shopTimeEntity.CloseTime)).TimeOfDay)
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

    // Filter data for shop info
    private void filterData()
    {
        bool chkbxPetShop = CHKBXFilterPetShop.Checked;
        bool chkbxPetClinic = CHKBXFilterPetClinic.Checked;
        bool chkbxGrooming = CHKBXFilterGrooming.Checked;
        string tbSearchValue = TBSearchShopInfo.Text;

        GVShopInfoOverview.DataSourceID = null;
        GVShopInfoOverview.DataSource = null;
        GVShopInfoOverview.DataSource = shopInfoCtrler.filterData(chkbxPetShop, chkbxPetClinic, chkbxGrooming, tbSearchValue, LBLSearchResultShopInfo);
        GVShopInfoOverview.DataBind();
    }
    #endregion

    #region Textbox Control
    protected void TBSearchShopInfo_TextChanged(object sender, EventArgs e)
    {
        // Filter data for shop info
        filterData();
    }
    #endregion








}