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

public partial class AdminDashboard : BasePage
{
    private static int gvPageSize = 10; // default


    private DataTable dTable;
    // Page load
    protected void Page_Load(object sender, EventArgs e)
    {
        gvPageSize = int.Parse(DDLDisplayRecordCount.SelectedValue);
        GVPetInfoOverview.PageSize = gvPageSize;
        if (IsPostBack)
        {

        }
        else
        {

        }
    }

    #region Drop Down List PostBack Control
    protected void DDLDisplayRecordCount_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvPageSize = int.Parse(DDLDisplayRecordCount.SelectedValue);
        GVPetInfoOverview.PageSize = gvPageSize;
    }
    #endregion

    protected void GVPetInfoOverview_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        PNLPetInfoDetails.Visible = true;
        dTable = ((DataView)SDSPetChar.Select(DataSourceSelectArguments.Empty)).Table;
        loadPieChart(dTable);
        loadPetInfo(dTable);
    }

    void loadPieChart(DataTable dTable)
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
                LogController.LogLine(column.ColumnName.After("charOverall"));
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


    private void loadPetInfo(DataTable dTable)
    {

    }

}