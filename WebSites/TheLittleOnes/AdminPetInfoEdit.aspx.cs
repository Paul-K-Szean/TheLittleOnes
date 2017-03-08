using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.Handler;
using TheLittleOnesLibrary.EnumFolder;

public partial class AdminPetInfoEdit : BasePage
{
    private TextBox UICtrlTextbox;
    private DropDownList UICtrlDropdownlist;
    private UpdatePanel UICtrlUpdatePanel;

    // temp variables
    private static FileUpload fileupload;
    private static HtmlGenericControl photoPreview;
    private static DataTable dTablePhoto;
    private static DataTable dTableOld;
    private static DataTable dTableEdit;
    private static DataTable dTablePetInfo;
    private static DataListItemEventArgs events;

    // input variables
    private string petInfoID;
    private string charID;
    private string category;
    private string breed;
    private string lifeSpanMin;
    private string lifeSpanMax;
    private string heightMin;
    private string heightMax;
    private string weightMin;
    private string weightMax;
    private string desc;
    private string personality;
    private string displayStatus;

    private string charOverallAdaptability;

    private static int gvPageSize = 5; // default

    // Page load
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        gvPageSize = int.Parse(DDLDisplayRecordCount.SelectedValue);
        GVPetInfoOverview.PageSize = gvPageSize;
        if (IsPostBack)
        {
            // update search result label
            loadLabelSearchResult();
        }
        else
        {
            // clear static data
            clearStaticData();
        }
    }

    #region Button Clicks
    // Preview image uploaded
    protected void BTNPreview_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        MessageHandler.ClearMessage(LBLErrorMsg);
        // create temp files in temp foler
        photoEntities = photoCtrler.saveToTempFolder(PhotoPurpose.PetInfo.ToString(), fileupload);
        // preview photo
        photoCtrler.previewPhotos(photoPreview);
    }
    #endregion

    #region Datalist Controls - Update, Cancel Commands
    protected void DLPetInfoDetails_ItemCreated(object sender, DataListItemEventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        Control BTNPreview = e.Item.FindControl("BTNPreview");
        if (BTNPreview != null)
        {
            ScriptManager mgr = ScriptManager.GetCurrent(this.Page);
            mgr.RegisterPostBackControl(BTNPreview);

        }
        Control FUL = e.Item.FindControl("FileUpload1");
        if (FUL != null)
        {
            fileupload = (FileUpload)FUL;
        }
        Control divPhotoPreview = e.Item.FindControl("photoPreview");
        if (divPhotoPreview != null)
        {
            photoPreview = (HtmlGenericControl)divPhotoPreview;
        }
    }

    protected void DLPetInfoDetails_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);

        if (e.Item.ItemType == ListItemType.EditItem)
        {
            events = e;
            // store the old data
            dTableOld = new DataTable("dTableOld");
            dTableOld = ((DataView)SDSPetChar.Select(DataSourceSelectArguments.Empty)).Table;
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            events = e;
            DLPetInfoDetails.EditItemIndex = -1;

        }
    }

    protected void DLPetInfoDetails_EditCommand(object source, DataListCommandEventArgs e)
    {
        LogController.LogLine("DLPetInfoDetails_EditCommand");
        if (string.IsNullOrEmpty(DLPetInfoDetails.DataSourceID))
        {
            DLPetInfoDetails.DataSource = null;
            DLPetInfoDetails.DataSource = SDSPetChar;
        }
        DLPetInfoDetails.EditItemIndex = e.Item.ItemIndex;
        DLPetInfoDetails.DataBind();

    }

    protected void DLPetInfoDetails_UpdateCommand(object source, DataListCommandEventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        // retrieve the updated values
        charID = ((TextBox)e.Item.FindControl("TBCharID")).Text.Trim();
        petInfoID = ((TextBox)e.Item.FindControl("TBPetInfoID")).Text.Trim();
        category = ((DropDownList)e.Item.FindControl("DDLCategory")).SelectedValue.Trim();
        breed = ((TextBox)e.Item.FindControl("TBBreed")).Text.Trim();
        lifeSpanMin = ((TextBox)e.Item.FindControl("TBLifeSpanMin")).Text.Trim();
        lifeSpanMax = string.IsNullOrEmpty(lifeSpanMax = ((TextBox)e.Item.FindControl("TBLifeSpanMax")).Text.Trim()) ? "0" : lifeSpanMax;
        heightMin = ((TextBox)e.Item.FindControl("TBHeightMin")).Text.Trim();
        heightMax = ((TextBox)e.Item.FindControl("TBHeightMax")).Text.Trim();
        weightMin = ((TextBox)e.Item.FindControl("TBWeightMin")).Text.Trim();
        weightMax = ((TextBox)e.Item.FindControl("TBWeightMax")).Text.Trim();
        desc = ((TextBox)e.Item.FindControl("TBDesc")).Text.Trim();
        personality = ((TextBox)e.Item.FindControl("TBPersonality")).Text.Trim();
        displayStatus = ((DropDownList)e.Item.FindControl("DDLStatus")).SelectedValue.Trim();

        // create object to update
        petInfoEntity = new PetInfoEntity(petInfoID, category, breed, lifeSpanMin, heightMin, weightMin, lifeSpanMax, heightMax, weightMax, desc, personality, displayStatus,
            petCharEntity, photoEntities);
        // update pet info
        petInfoEntity = petInfoCtrler.updatePetInfo(petInfoEntity);
        // update pet characteristic
        if (petInfoEntity.PetCharEntity != null)
        {
            petInfoEntity.PetCharEntity = petInfoCtrler.updatePetChar(petCharEntity);
        }
        // update pet photo
        if (petInfoEntity.PhotoEntities != null)
        {
            // change photo path to database instead of using temp
            petInfoEntity.PhotoEntities = photoCtrler.changePhotoPathToDatabaseFolder(photoEntities, petInfoEntity.PetInfoID);
            // remove old photos from database
            photoCtrler.deletePhoto(petInfoEntity.PetInfoID, PhotoPurpose.PetInfo.ToString());
            // create new photos into database
            photoCtrler.createPhoto(photoEntities, petInfoEntity.PetInfoID);
        }
        if (petInfoEntity != null)
            MessageHandler.SuccessMessage(LBLErrorMsg, "Pet info successfully updated");
        else
            MessageHandler.SuccessMessage(LBLErrorMsg, "Pet info was not successfully updated");

        // disable the edit mode
        DLPetInfoDetails.EditItemIndex = -1;
        // update displays
        SDSPhoto.DataBind();
        DLPetInfoDetails.DataBind();
        GVPetInfoOverview.DataBind();
        // clear static data
        clearStaticData();
    }

    protected void DLPetInfoDetails_CancelCommand(object source, DataListCommandEventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        // restore old data
        if (string.IsNullOrEmpty(DLPetInfoDetails.DataSourceID))
        {
            DLPetInfoDetails.DataSource = null;
            DLPetInfoDetails.DataSource = SDSPetChar;
        }
        DLPetInfoDetails.EditItemIndex = -1;
        DLPetInfoDetails.DataBind();
        // clear static data
        clearStaticData();
    }
    #endregion

    #region Dropdownlist Controls
    protected void DDLDisplayRecordCount_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = GVPetInfoOverview.SelectedIndex;
        foreach (GridViewRow row in GVPetInfoOverview.Rows)
        {
            if (row.RowIndex == GVPetInfoOverview.SelectedIndex)
            {
                GVPetInfoOverview.Rows[GVPetInfoOverview.SelectedIndex].BackColor = Utility.getColorLightBlue();
                GVPetInfoOverview.Rows[GVPetInfoOverview.SelectedIndex].ForeColor = Utility.getColorWhite();
            }
            else
            {
                if (row.RowIndex % 2 == 0)
                {
                    // even rows
                    row.BackColor = Utility.getColorWhite();
                }
                else
                {
                    // odd rows
                    row.BackColor = Utility.getColorLightGray();
                }
                GVPetInfoOverview.Rows[GVPetInfoOverview.SelectedIndex].ForeColor = Utility.getDefaultColor();
            }
        }
    }

    protected void DDLCharAdaptToSurrounding_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name); getEditedValues_Characteristics(sender);
    }

    protected void DDLCharAdaptToNovice_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name); getEditedValues_Characteristics(sender);
    }

    protected void DDLCharAdaptToLoneliness_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name); getEditedValues_Characteristics(sender);
    }

    protected void DDLCharAdaptToCold_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name); getEditedValues_Characteristics(sender);
    }

    protected void DDLCharAdaptToHot_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name); getEditedValues_Characteristics(sender);
    }

    protected void DDLCharFriendWithFamily_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name); getEditedValues_Characteristics(sender);
    }

    protected void DDLCharFriendWithKids_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name); getEditedValues_Characteristics(sender);
    }

    protected void DDLCharFriendWithStrangers_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name); getEditedValues_Characteristics(sender);
    }

    protected void DDLCharFriendWithOtherPet_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name); getEditedValues_Characteristics(sender);
    }

    protected void DDLCharGroomLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name); getEditedValues_Characteristics(sender);
    }

    protected void DDLCharGroomShedding_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name); getEditedValues_Characteristics(sender);
    }

    protected void DDLCharGroomDrooling_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name); getEditedValues_Characteristics(sender);
    }

    protected void DDLCharTrainLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name); getEditedValues_Characteristics(sender);
    }

    protected void DDLCharTrainIntelligence_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name); getEditedValues_Characteristics(sender);
    }

    protected void DDLCharTrainMouthiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name); getEditedValues_Characteristics(sender);
    }

    protected void DDLCharTrainPreyDrive_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name); getEditedValues_Characteristics(sender);
    }

    protected void DDLCharTrainBarkHowl_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name); getEditedValues_Characteristics(sender);
    }

    protected void DDLCharExerciseEnergyLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name); getEditedValues_Characteristics(sender);
    }

    protected void DDLCharExerciseNeeds_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name); getEditedValues_Characteristics(sender);
    }

    protected void DDLCharExercisePlayfullness_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name); getEditedValues_Characteristics(sender);
    }
    #endregion

    #region Gridview Controls
    protected void GVPetInfoOverview_DataBound(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        if (dTablePetInfo == null)
            dTablePetInfo = ((DataView)SDSPetInfo.Select(DataSourceSelectArguments.Empty)).Table;
        updateEntryCount(dTablePetInfo, GVPetInfoOverview, LBLEntriesCount);
    }

    protected void GVPetInfoOverview_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        highlightSelectedRow(GVPetInfoOverview);
        MessageHandler.ClearMessage(LBLErrorMsg);
        if (string.IsNullOrEmpty(DLPetInfoDetails.DataSourceID))
        {
            DLPetInfoDetails.DataSource = null;
            DLPetInfoDetails.DataSource = SDSPetChar;
        }
        DLPetInfoDetails.EditItemIndex = -1;
        DLPetInfoDetails.DataBind();
    }
    #endregion

    #region Logical Methods
    // get characteristic new values 
    protected void getEditedValues_Characteristics(object sender)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        if (events != null)
        {
            // get the drop down list control that had changed value
            DropDownList ddlNewValue = (DropDownList)sender;
            // duplicate old data
            if (dTableOld != null)
            {
                if (dTableEdit == null)
                {
                    dTableEdit = new DataTable("dTableEdit");
                    dTableEdit = dTableOld;
                }
            }

            // get the new value of the drop down list
            foreach (Control ctrl in events.Item.Controls)
            {
                if (ctrl is DropDownList)
                {
                    UICtrlDropdownlist = (DropDownList)ctrl;
                    if (UICtrlDropdownlist.ID.Equals(ddlNewValue.ID))
                    {
                        UICtrlDropdownlist.SelectedValue = ddlNewValue.SelectedValue;
                        foreach (DataRow row in dTableEdit.Rows)
                        {
                            foreach (DataColumn column in dTableEdit.Columns)
                            {
                                // found the value of the control
                                if (UICtrlDropdownlist.ID.ToLower().Contains(column.ColumnName.ToLower()))
                                {
                                    row.SetField(column, UICtrlDropdownlist.SelectedValue);
                                    // break away, no need to search anymore
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            // calculate the overall rating
            calculateOverallRating(dTableEdit);
            // reset the data to preview current changes
            DLPetInfoDetails.DataSourceID = null;
            DLPetInfoDetails.DataSource = null;
            DLPetInfoDetails.DataSource = dTableEdit;
            DLPetInfoDetails.DataBind();
        }
        else
        {
            Response.Redirect("AadminPetInfoEdit.aspx");
        }
    }

    // tabulate overall rating
    protected void calculateOverallRating(DataTable dTableEdit)
    {
        double totalScoreAdaptability = 0.0;
        double totalScoreFriendliness = 0.0;
        double totalScoreGrooming = 0.0;
        double totalScoreTrainability = 0.0;
        double totalScoreExercise = 0.0;
        List<double> listOverallScore = new List<double>();
        List<double> listAdaptabilityScore = new List<double>();
        List<double> listFriendlinessScore = new List<double>();
        List<double> listGroomingScore = new List<double>();
        List<double> listTrainabilityScore = new List<double>();
        List<double> listExerciseScore = new List<double>();

        // loop through all drop down list and store the value to the corresponding list<double>
        foreach (Control ctrl in events.Item.Controls)
        {
            // LogController.LogLine(ctrl.ID);
            if (ctrl is DropDownList)
            {
                UICtrlDropdownlist = (DropDownList)ctrl;
                if (UICtrlDropdownlist.ID.Contains("Adapt"))
                {
                    totalScoreAdaptability += double.Parse(UICtrlDropdownlist.SelectedValue);
                    listAdaptabilityScore.Add(double.Parse(UICtrlDropdownlist.SelectedValue));
                }
                if (UICtrlDropdownlist.ID.Contains("Friend"))
                {
                    totalScoreFriendliness += double.Parse(UICtrlDropdownlist.SelectedValue);
                    listFriendlinessScore.Add(double.Parse(UICtrlDropdownlist.SelectedValue));
                }
                if (UICtrlDropdownlist.ID.Contains("Groom"))
                {
                    totalScoreGrooming += double.Parse(UICtrlDropdownlist.SelectedValue);
                    listGroomingScore.Add(double.Parse(UICtrlDropdownlist.SelectedValue));
                }
                if (UICtrlDropdownlist.ID.Contains("Train"))
                {
                    totalScoreTrainability += double.Parse(UICtrlDropdownlist.SelectedValue);
                    listTrainabilityScore.Add(double.Parse(UICtrlDropdownlist.SelectedValue));
                }
                if (UICtrlDropdownlist.ID.Contains("Exercise"))
                {
                    totalScoreExercise += double.Parse(UICtrlDropdownlist.SelectedValue);
                    listExerciseScore.Add(double.Parse(UICtrlDropdownlist.SelectedValue));
                }
            }
        }

        // calculate overall score for each characteristic
        listOverallScore.Add(Math.Round((totalScoreAdaptability / 5), 2));
        listOverallScore.Add(Math.Round((totalScoreFriendliness / 4), 2));
        listOverallScore.Add(Math.Round((totalScoreGrooming / 3), 2));
        listOverallScore.Add(Math.Round((totalScoreTrainability / 5), 2));
        listOverallScore.Add(Math.Round((totalScoreExercise / 3), 2));

        // convert new changes into object
        charID = ((TextBox)events.Item.FindControl("TBCharID")).Text.Trim();
        petCharEntity = new PetCharEntity(charID,
            listOverallScore[0].ToString(), listOverallScore[1].ToString(), listOverallScore[2].ToString(), listOverallScore[3].ToString(), listOverallScore[4].ToString(),
            listAdaptabilityScore[0].ToString(), listAdaptabilityScore[1].ToString(), listAdaptabilityScore[2].ToString(), listAdaptabilityScore[3].ToString(), listAdaptabilityScore[4].ToString(),
             listFriendlinessScore[0].ToString(), listFriendlinessScore[1].ToString(), listFriendlinessScore[2].ToString(), listFriendlinessScore[3].ToString(),
              listGroomingScore[0].ToString(), listGroomingScore[1].ToString(), listGroomingScore[2].ToString(),
               listTrainabilityScore[0].ToString(), listTrainabilityScore[1].ToString(), listTrainabilityScore[2].ToString(), listTrainabilityScore[3].ToString(), listTrainabilityScore[4].ToString(),
                listExerciseScore[0].ToString(), listExerciseScore[1].ToString(), listExerciseScore[2].ToString());

        // preview the new rating for each characteristic
        if (dTableEdit != null)
        {
            foreach (DataRow row in dTableEdit.Rows)
            {
                for (int i = 0; i < dTableEdit.Columns.Count; i++)
                {
                    LogController.LogLine(dTableEdit.Columns[i].ColumnName + " = " + row.ItemArray[i].ToString());
                    if (dTableEdit.Columns[i].ColumnName.Contains("OverallAdaptability"))
                    {
                        row.SetField(dTableEdit.Columns[i].ColumnName, listOverallScore[0]);
                    }
                    if (dTableEdit.Columns[i].ColumnName.Contains("OverallFriendliness"))
                    {
                        row.SetField(dTableEdit.Columns[i].ColumnName, listOverallScore[1]);
                    }
                    if (dTableEdit.Columns[i].ColumnName.Contains("OverallGrooming"))
                    {
                        row.SetField(dTableEdit.Columns[i].ColumnName, listOverallScore[2]);
                    }
                    if (dTableEdit.Columns[i].ColumnName.Contains("OverallTrainability"))
                    {
                        row.SetField(dTableEdit.Columns[i].ColumnName, listOverallScore[3]);
                    }
                    if (dTableEdit.Columns[i].ColumnName.Contains("OverallExercise"))
                    {
                        row.SetField(dTableEdit.Columns[i].ColumnName, listOverallScore[4]);
                    }
                }
            }
        }

    }

    // Update search result label
    private void loadLabelSearchResult()
    {
        if (string.IsNullOrEmpty(TBSearchPetInfo.Text))
        {
            MessageHandler.DefaultMessage(LBLSearchResult, string.Concat("Result for \"", TBSearchPetInfo.Text.Trim(), "\""));
        }
    }

    // Clear static data
    private void clearStaticData()
    {
        dTablePetInfo = null;
        dTableOld = null;
        dTableEdit = null;
        petCharEntity = null;
        photoEntities = null;
        // photoPreview.InnerHtml = string.Empty;
    }
    #endregion





}