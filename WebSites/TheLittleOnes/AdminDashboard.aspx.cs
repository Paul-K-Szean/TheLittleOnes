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

public partial class AdminDashboard : BasePage
{
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

        if (!string.IsNullOrEmpty(TBSearchShopInfo.Text))
        {
            LBLSearchResultShopInfo.Text = "Result for \"" + TBSearchPetInfo.Text + "\"";
        }
        else
        {
            LBLSearchResultShopInfo.Text = "Result for Shop Info";
        }

    }


    #region Checkbox Control
    // Shop info filter clinic
    protected void CHKBXFilterClinic_CheckedChanged(object sender, EventArgs e)
    {
        if (CHKBXFilterClinic.Checked)
        {
            CHKBXFilterGrooming.Checked = false;
            SDSShopInfo.SelectCommand = "SELECT * FROM SHOPINFO WHERE SHOPINFOTYPE LIKE '%CLINIC%' ";
            SDSShopInfo.DataBind();
        }
    }
    // Shop info filter grooming service
    protected void CHKBXFilterGrooming_CheckedChanged(object sender, EventArgs e)
    {
        if (CHKBXFilterGrooming.Checked)
        {
            CHKBXFilterClinic.Checked = false;
            SDSShopInfo.SelectCommand = "SELECT * FROM SHOPINFO WHERE SHOPINFOGROOMING = TRUE ";
            SDSShopInfo.DataBind();
        }
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

    protected void GVPetInfoOverview_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        PNLPetInfoDetails.Visible = true;
        dTable = ((DataView)SDSPetChar.Select(DataSourceSelectArguments.Empty)).Table;
        petInfoEntity = petInfoCtrler.getPetInfo(dTable.Rows[0]["petInfoID"].ToString());
        loadPieChartPetInfo(dTable);
        loadPetInfo(petInfoEntity);
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

    protected void GVShopInfoOverview_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        PNLShopInfoDetails.Visible = true;
        dTable = ((DataView)SDSPetChar.Select(DataSourceSelectArguments.Empty)).Table;
        shopInfoEntity = shopInfoCtrler.getShopInfo(dTable.Rows[0]["petInfoID"].ToString());
        loadPieChartPetInfo(dTable);
        loadPetInfo(petInfoEntity);
    }
    #endregion


    #region Logical Methods
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

    private void loadPetInfo(PetInfoEntity petInfoEntity)
    {
        LBLCategory.Text = petInfoEntity.PetCategory;
        LBLBreed.Text = petInfoEntity.PetBreed;
        LBLDesc.Text = petInfoEntity.PetDesc;
        LBLPersonality.Text = petInfoEntity.PetPersonality;
    }
    #endregion




}