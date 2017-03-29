using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.EnumFolder;
using TheLittleOnesLibrary.Handler;
public partial class AdminPetInfoAdd : BasePageAdmin
{
    private Label UICtrlLabel;
    private TextBox UICtrlTextbox;
    private DropDownList UICtrlDropdownlist;
    
    // input variables
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
    // page load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
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
        // loop over for controls
        foreach (Control ctrl in UpdatePanel1.ContentTemplateContainer.Controls)
        {
            // set dropdownlist values
            if (ctrl is DropDownList)
                setDropdownlistValues(ctrl);
        }
    }
    // Initial dropdownlist values
    private void setDropdownlistValues(Control ctrl)
    {
        UICtrlDropdownlist = (DropDownList)ctrl;
        // category
        if (UICtrlDropdownlist.ID.Contains("Category"))
        {
            UICtrlDropdownlist.Items.Add(new ListItem("Dog", "Dog"));
            UICtrlDropdownlist.Items.Add(new ListItem("Cat", "Cat"));
        }
        // characteristics
        if (UICtrlDropdownlist.ID.Contains("Adapt") || UICtrlDropdownlist.ID.Contains("Friend") || UICtrlDropdownlist.ID.Contains("Grooming") ||
            UICtrlDropdownlist.ID.Contains("Train") || UICtrlDropdownlist.ID.Contains("Exercise"))
        {
            for (int index = 1; index <= 5; index++)
                UICtrlDropdownlist.Items.Add(new ListItem(index.ToString(), index.ToString()));
        }
    }
    #endregion
    #region Button Clicks
    // Preview image uploaded
    protected void BTNPreview_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        MessageHandler.ClearMessage(LBLErrorMsg);
        // create temp files in temp foler
        photoEntities = photoCtrler.saveToTempFolder(PhotoPurpose.PetInfo.ToString(), FileUpload1);
        // preview photo
        photoCtrler.previewPhotos(photoPreview);
    }
    // Create pet info
    protected void BTNAdd_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        // get inputs
        LBLErrorMsg.Text = string.Empty;
        category = DDLCategory.SelectedValue.Trim();
        breed = TBBreed.Text.Trim();
        lifeSpanMin = TBLifeSpanMin.Text.Trim();
        lifeSpanMax = TBLifeSpanMax.Text.Trim();
        heightMin = TBHeightMin.Text.Trim();
        heightMax = TBHeightMax.Text.Trim();
        weightMin = TBWeightMin.Text.Trim();
        weightMax = TBWeightMax.Text.Trim();
        desc = TBDesc.Text.Trim();
        personality = TBPersonality.Text.Trim();
        if (checkRequiredFields())
        {
            // check if pet info exists
            if (petInfoCtrler.checkPetInfoExist(category, breed))
            {
                // exists
                MessageHandler.ErrorMessage(LBLErrorMsg, "Pet info already exists");
            }
            else
            {
                // create pet info entity
                petInfoEntity = new PetInfoEntity(category, breed,
                    lifeSpanMin, heightMin, weightMin, lifeSpanMax, heightMax, weightMax, desc, personality, "Display",
                    petCharEntity, photoEntities);
                // add into database
                petInfoEntity = petInfoCtrler.createPetInfo(petInfoEntity);
                petInfoEntity = petInfoCtrler.createPetCharacteristic(petInfoEntity);
                // change photo path to database instead of using temp
                if (photoEntities != null)
                {
                    petInfoEntity.PhotoEntities = photoCtrler.changePhotoPathToDatabaseFolder(photoEntities, petInfoEntity.PetInfoID);
                    petInfoEntity = petInfoCtrler.createPetPhoto(petInfoEntity);
                }
                if (petInfoEntity != null)
                {
                    MessageHandler.SuccessMessage(LBLErrorMsg, "Pet info successfully added");
                }
                else
                {
                    MessageHandler.ErrorMessageAdmin(LBLErrorMsg, "Pet info was not successfully added");
                }
            }
        }
    }
    // Generate random values
    protected void BTNGenerate_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        MessageHandler.ClearMessage(LBLErrorMsg);
        Random rnd = new Random();
        // random ddl values
        foreach (Control ctrl in UpdatePanel1.ContentTemplateContainer.Controls)
        {
            if (ctrl is DropDownList)
            {
                UICtrlDropdownlist = (DropDownList)ctrl;
                if (UICtrlDropdownlist.Enabled == true)
                    UICtrlDropdownlist.SelectedIndex = rnd.Next(1, UICtrlDropdownlist.Items.Count);
            }
        }
        // random text box values
        category = DDLCategory.SelectedValue.Trim();
        breed = TBBreed.Text = Utility.getRandomBreed(category).Trim();
        lifeSpanMin = TBLifeSpanMin.Text = Utility.getRandomNumber(10, 13).ToString("0.0").Trim();
        lifeSpanMax = TBLifeSpanMax.Text = Utility.getRandomNumber(13, 16).ToString("0.0").Trim();
        heightMin = TBHeightMin.Text = Utility.getRandomDouble(12, 16).ToString("0.0").Trim();
        Thread.Sleep(12);
        heightMax = TBHeightMax.Text = Utility.getRandomDouble(16, 28).ToString("0.0").Trim();
        Thread.Sleep(14);
        weightMin = TBWeightMin.Text = Utility.getRandomDouble(12, 16).ToString("0.0").Trim();
        Thread.Sleep(18);
        weightMax = TBWeightMax.Text = Utility.getRandomDouble(16, 28).ToString("0.0").Trim();
        desc = TBDesc.Text = string.Concat("Little did you know about ", breed, "! The number of things to talk about them is just too many. Just put them all here!");
        personality = TBPersonality.Text = string.Concat("You and ", breed, " may or may not be the perfect match! Their interesting personalities are astonishing.");
        // calculate overall rating
        calculateOverallRating();
        photoPreview.InnerHtml = string.Empty;
    }
    #endregion
    #region Logical Methods
    protected void calculateOverallRating()
    {
        double factorValue = 5.0;
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
        foreach (Control ctrl in UpdatePanel1.ContentTemplateContainer.Controls)
        {
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
                if (UICtrlDropdownlist.ID.Contains("Grooming"))
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
        listOverallScore.Add(totalScoreAdaptability / 5);
        listOverallScore.Add(totalScoreFriendliness / 4);
        listOverallScore.Add(totalScoreGrooming / 3);
        listOverallScore.Add(totalScoreTrainability / 5);
        listOverallScore.Add(totalScoreExercise / 3);
        LBLOverallAdaptability.Text = string.Concat("Adaptability (", listOverallScore[0].ToString("0.0"), "/", factorValue, ")");
        LBLOverallFriendliness.Text = string.Concat("Friendliness (", listOverallScore[1].ToString("0.0"), "/", factorValue, ")");
        LBLOverallGrooming.Text = string.Concat("Grooming (", listOverallScore[2].ToString("0.0"), "/", factorValue, ")");
        LBLOverallTrainability.Text = string.Concat("Trainability (", listOverallScore[3].ToString("0.0"), "/", factorValue, ")");
        LBLOverallExercise.Text = string.Concat("Exercise (", listOverallScore[4].ToString("0.0"), "/", factorValue, ")");
        petCharEntity = new PetCharEntity(
            listOverallScore[0].ToString("0.0"), listOverallScore[1].ToString("0.0"), listOverallScore[2].ToString("0.0"), listOverallScore[3].ToString("0.0"), listOverallScore[4].ToString("0.0"),
            listAdaptabilityScore[0].ToString("0.0"), listAdaptabilityScore[1].ToString("0.0"), listAdaptabilityScore[2].ToString("0.0"), listAdaptabilityScore[3].ToString("0.0"), listAdaptabilityScore[4].ToString("0.0"),
             listFriendlinessScore[0].ToString("0.0"), listFriendlinessScore[1].ToString("0.0"), listFriendlinessScore[2].ToString("0.0"), listFriendlinessScore[3].ToString("0.0"),
              listGroomingScore[0].ToString("0.0"), listGroomingScore[1].ToString("0.0"), listGroomingScore[2].ToString("0.0"),
               listTrainabilityScore[0].ToString("0.0"), listTrainabilityScore[1].ToString("0.0"), listTrainabilityScore[2].ToString("0.0"), listTrainabilityScore[3].ToString("0.0"), listTrainabilityScore[4].ToString("0.0"),
                listExerciseScore[0].ToString("0.0"), listExerciseScore[1].ToString("0.0"), listExerciseScore[2].ToString("0.0")
            );
    }
    private bool checkRequiredFields()
    {
        bool isUICtrlDropdownlistValid = true;
        bool isUICtrlTextboxValid = true;
        foreach (Control ctrl in UpdatePanel1.ContentTemplateContainer.Controls)
        {
            // check all drop down lists
            if (ctrl is DropDownList)
            {
                UICtrlDropdownlist = (DropDownList)ctrl;
                if (UICtrlDropdownlist.SelectedIndex == 0 && UICtrlDropdownlist.Enabled == true)
                {
                    isUICtrlDropdownlistValid = false;
                    LogController.LogLine("Error control: " + UICtrlDropdownlist.ID);
                }
            }
            // check all text boxes
            if (ctrl is TextBox)
            {
                UICtrlTextbox = (TextBox)ctrl;
                if (string.IsNullOrEmpty(UICtrlTextbox.Text.Trim()))
                {
                    isUICtrlTextboxValid = false;
                    LogController.LogLine("Error control: " + UICtrlTextbox.ID);
                }
            }
        }
        // return condition
        if (isUICtrlDropdownlistValid && isUICtrlTextboxValid)
        {
            return true;
        }
        else
        {
            // display error message
            MessageHandler.ErrorMessage(LBLErrorMsg, "Please ensure that all the fields are not empty");
            return false;
        }
    }
    #endregion
    #region Drop Down List PostBack Control
    protected void DDLAdaptSurrounding_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    protected void DDLAdapt_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    protected void DDLAdaptLoneliness_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    protected void DDLAdaptCold_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    protected void DDLAdaptHot_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    protected void DDLFriendlinessFamily_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    protected void DDLFriendlinessKids_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    protected void DDLFriendlinessStrangers_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    protected void DDLFriendlinessOtherPets_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    protected void DDLGroomingLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    protected void DDLGroomingShedding_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    protected void DDLGroomingDrooling_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    protected void DDLTrainLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    protected void DDLTrainIntelligence_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    protected void DDLTrainMouthiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    protected void DDLTrainPreyDrive_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    protected void DDLTrainBarkHowl_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    protected void DDLExerciseLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    protected void DDLExerciseEnergy_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    protected void DDLExerciseNeeds_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    protected void DDLExercisePlayfullness_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateOverallRating();
    }
    #endregion
}