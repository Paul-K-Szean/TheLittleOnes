﻿using System;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary.DataAccessObject;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.EnumFolder;
namespace TheLittleOnesLibrary.Controllers
{
    public class PetInfoController
    {
        private static PetInfoController petInfoCtrl;
        private static PetCharEntity petCharEntity;
        public static PetInfoController getInstance()
        {
            if (petInfoCtrl == null)
                petInfoCtrl = new PetInfoController();
            return petInfoCtrl;
        }
        // Data Access Object
        private DAO dao;
        private OleDbCommand oleDbCommand;
        private DataSet dataSet;
        // Default Constructor
        public PetInfoController()
        {
            dao = DAO.getInstance();
        }
        // Check if the same category and breed exist
        public bool checkPetInfoExist(string petCategory, string petBreed)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM PETINFO WHERE PETINFOCATEGORY LIKE @PETINFOCATEGORY AND PETINFOBREED LIKE @PETINFOBREED ");
                oleDbCommand.Parameters.AddWithValue("@PETINFOCATEGORY", string.Concat("%", petCategory, "%"));
                oleDbCommand.Parameters.AddWithValue("@PETINFOBREED", string.Concat("%", petBreed, "%"));
                if (string.IsNullOrEmpty(dao.getValue(oleDbCommand)))
                {
                    // empty = no record exists
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        // Create PetInfo
        public PetInfoEntity createPetInfo(PetInfoEntity petInfoEntity)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("INSERT INTO PETINFO (PETINFOCATEGORY,PETINFOBREED,PETINFOLIFESPANMIN,PETINFOHEIGHTMIN,PETINFOWEIGHTMIN, PETINFOLIFESPANMAX,PETINFOHEIGHTMAX,PETINFOWEIGHTMAX, PETINFODESC, PETINFOPERSONALITY, PETINFODISPLAYSTATUS)",
                                                         "VALUES (@PETINFOCATEGORY,@PETINFOBREED,@PETINFOLIFESPANMIN,@PETINFOHEIGHTMIN,@PETINFOWEIGHTMIN,@PETINFOLIFESPANMAX,@PETINFOHEIGHTMAX,@PETINFOWEIGHTMAX,@PETINFODESC,@PETINFOPERSONALITY,@PETINFODISPLAYSTATUS);");
                oleDbCommand.Parameters.AddWithValue("@PETINFOCATEGORY", petInfoEntity.PetCategory);
                oleDbCommand.Parameters.AddWithValue("@PETINFOBREED", petInfoEntity.PetBreed);
                oleDbCommand.Parameters.AddWithValue("@PETINFOLIFESPANMIN", petInfoEntity.PetLifeSpanMin);
                oleDbCommand.Parameters.AddWithValue("@PETINFOHEIGHTMIN", petInfoEntity.PetHeightMin);
                oleDbCommand.Parameters.AddWithValue("@PETINFOWEIGHTMIN", petInfoEntity.PetWeightMin);
                oleDbCommand.Parameters.AddWithValue("@PETINFOLIFESPANMAX", petInfoEntity.PetLifeSpanMax);
                oleDbCommand.Parameters.AddWithValue("@PETINFOHEIGHTMAX", petInfoEntity.PetHeightMax);
                oleDbCommand.Parameters.AddWithValue("@PETINFOWEIGHTMAX", petInfoEntity.PetWeightMax);
                oleDbCommand.Parameters.AddWithValue("@PETINFODESC", petInfoEntity.PetDesc);
                oleDbCommand.Parameters.AddWithValue("@PETINFOPERSONALITY", petInfoEntity.PetPersonality);
                oleDbCommand.Parameters.AddWithValue("@PETINFODISPLAYSTATUS", petInfoEntity.PetDisplayStatus);
                int insertID = dao.createRecord(oleDbCommand);
                if (insertID > 0)
                {
                    petInfoEntity.PetInfoID = insertID.ToString();
                    return petInfoEntity;
                }
                else
                {
                    return null;
                }
            }
        }
        // Create PetCharacteristic
        public PetInfoEntity createPetCharacteristic(PetInfoEntity petInfoEntity)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("INSERT INTO PETCHARACTERISTICS ",
                                                        "(PETINFOID,CHAROVERALLADAPTABILITY,CHAROVERALLFRIENDLINESS,CHAROVERALLGROOMING,CHAROVERALLTRAINABILITY,CHAROVERALLEXERCISE,",
                                                        "CHARADAPTTOSURROUNDING,CHARADAPTTONOVICE,CHARADAPTTOLONELINESS,CHARADAPTTOCOLD,CHARADAPTTOHOT,",
                                                        "CHARFRIENDWITHFAMILY,CHARFRIENDWITHKIDS,CHARFRIENDWITHSTRANGERS,CHARFRIENDWITHOTHERPET,",
                                                        "CHARGROOMLEVEL,CHARGROOMSHEDDING,CHARGROOMDROOLING,",
                                                        "CHARTRAINLEVEL,CHARTRAININTELLIGENCE,CHARTRAINMOUTHINESS,CHARTRAINPREYDRIVE, CHARTRAINBARKHOWL,",
                                                        "CHAREXERCISEENERGYLEVEL,CHAREXERCISENEEDS,CHAREXERCISEPLAYFULLNESS) ",
                                                        "VALUES (@PETINFOID,@CHAROVERALLADAPTABILITY,@CHAROVERALLFRIENDLINESS,@CHAROVERALLGROOMING,@CHAROVERALLTRAINABILITY,@CHAROVERALLEXERCISE,",
                                                        "@CHARADAPTTOSURROUNDING,@CHARADAPTTONOVICE,@CHARADAPTTOLONELINESS,@CHARADAPTTOCOLD,@CHARADAPTTOHOT,",
                                                        "@CHARFRIENDWITHFAMILY,@CHARFRIENDWITHKIDS,@CHARFRIENDWITHSTRANGERS,@CHARFRIENDWITHOTHERPET,",
                                                        "@CHARGROOMLEVEL,@CHARGROOMSHEDDING,@CHARGROOMDROOLING,",
                                                        "@CHARTRAINLEVEL,@CHARTRAININTELLIGENCE,@CHARTRAINMOUTHINESS,@CHARTRAINPREYDRIVE, @CHARTRAINBARKHOWL,",
                                                        "@CHAREXERCISEENERGYLEVEL,@CHAREXERCISENEEDS,@CHAREXERCISEPLAYFULLNESS) ");
                oleDbCommand.Parameters.AddWithValue("@PETINFOID", petInfoEntity.PetInfoID);
                oleDbCommand.Parameters.AddWithValue("@CHAROVERALLADAPTABILITY", petInfoEntity.PetCharEntity.CharOverallAdaptability);
                oleDbCommand.Parameters.AddWithValue("@CHAROVERALLFRIENDLINESS", petInfoEntity.PetCharEntity.CharOverallFriendliness);
                oleDbCommand.Parameters.AddWithValue("@CHAROVERALLGROOMING", petInfoEntity.PetCharEntity.CharOverallGrooming);
                oleDbCommand.Parameters.AddWithValue("@CHAROVERALLTRAINABILITY", petInfoEntity.PetCharEntity.CharOverallTrainability);
                oleDbCommand.Parameters.AddWithValue("@CHAROVERALLEXERCISE", petInfoEntity.PetCharEntity.CharOverallExercise);
                oleDbCommand.Parameters.AddWithValue("@CHARADAPTTOSURROUNDING", petInfoEntity.PetCharEntity.CharAdaptToSurrounding);
                oleDbCommand.Parameters.AddWithValue("@CHARADAPTTONOVICE", petInfoEntity.PetCharEntity.CharAdaptToNovice);
                oleDbCommand.Parameters.AddWithValue("@CHARADAPTTOLONELINESS", petInfoEntity.PetCharEntity.CharAdaptToLoneliness);
                oleDbCommand.Parameters.AddWithValue("@CHARADAPTTOCOLD", petInfoEntity.PetCharEntity.CharAdaptToCold);
                oleDbCommand.Parameters.AddWithValue("@CHARADAPTTOHOT", petInfoEntity.PetCharEntity.CharAdaptToHot);
                oleDbCommand.Parameters.AddWithValue("@CHARFRIENDWITHFAMILY", petInfoEntity.PetCharEntity.CharFriendWithFamily);
                oleDbCommand.Parameters.AddWithValue("@CHARFRIENDWITHKIDS", petInfoEntity.PetCharEntity.CharFriendWithKids);
                oleDbCommand.Parameters.AddWithValue("@CHARFRIENDWITHSTRANGERS", petInfoEntity.PetCharEntity.CharFriendWithStranger);
                oleDbCommand.Parameters.AddWithValue("@CHARFRIENDWITHOTHERPET", petInfoEntity.PetCharEntity.CharFriendWithOtherPet);
                oleDbCommand.Parameters.AddWithValue("@CHARGROOMLEVEL", petInfoEntity.PetCharEntity.CharGroomLevel);
                oleDbCommand.Parameters.AddWithValue("@CHARGROOMSHEDDING", petInfoEntity.PetCharEntity.CharGroomSheddingLevel);
                oleDbCommand.Parameters.AddWithValue("@CHARGROOMDROOLING", petInfoEntity.PetCharEntity.CharGroomDrooling);
                oleDbCommand.Parameters.AddWithValue("@CHARTRAINLEVEL", petInfoEntity.PetCharEntity.CharTrainLevel);
                oleDbCommand.Parameters.AddWithValue("@CHARTRAININTELLIGENCE", petInfoEntity.PetCharEntity.CharTrainIntelligenceLevel);
                oleDbCommand.Parameters.AddWithValue("@CHARTRAINMOUTHINESS", petInfoEntity.PetCharEntity.CharTrainMouthiness);
                oleDbCommand.Parameters.AddWithValue("@CHARTRAINPREYDRIVE", petInfoEntity.PetCharEntity.CharTrainPreyDrive);
                oleDbCommand.Parameters.AddWithValue("@CHARTRAINBARKHOWL", petInfoEntity.PetCharEntity.CharTrainBarkHowl);
                oleDbCommand.Parameters.AddWithValue("@CHAREXERCISEENERGYLEVEL", petInfoEntity.PetCharEntity.CharExerciseEnergyLevel);
                oleDbCommand.Parameters.AddWithValue("@CHAREXERCISENEEDS", petInfoEntity.PetCharEntity.CharExerciseNeeds);
                oleDbCommand.Parameters.AddWithValue("@CHAREXERCISEPLAYFULLNESS", petInfoEntity.PetCharEntity.CharExercisePlayfullness);
                int insertID = dao.createRecord(oleDbCommand);
                if (insertID > 0)
                {
                    petInfoEntity.PetCharEntity.CharID = insertID.ToString();
                    return petInfoEntity;
                }
                else
                {
                    return null;
                }
            }
        }
        // Create PetPhoto
        public PetInfoEntity createPetPhoto(PetInfoEntity petInfoEntity)
        {
            petInfoEntity.PhotoEntities = PhotoController.getInstance().createPhoto(petInfoEntity.PhotoEntities, petInfoEntity.PetInfoID);
            return petInfoEntity;
        }
        // Update PetInfo
        public PetInfoEntity updatePetInfo(PetInfoEntity petInfoEntity)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("UPDATE PETINFO SET PETINFOCATEGORY = @PETINFOCATEGORY, PETINFOBREED = @PETINFOBREED, ",
                                    "PETINFOLIFESPANMIN = @PETINFOLIFESPANMIN, PETINFOHEIGHTMIN = @PETINFOHEIGHTMIN, PETINFOWEIGHTMIN = @PETINFOWEIGHTMIN,",
                                    "PETINFOLIFESPANMAX = @PETINFOLIFESPANMAX, PETINFOWEIGHTMAX = @PETINFOWEIGHTMAX, PETINFOHEIGHTMAX = @PETINFOHEIGHTMAX,",
                                    "PETINFODESC = @PETINFODESC, PETINFOPERSONALITY = @PETINFOPERSONALITY, PETINFODISPLAYSTATUS = @PETINFODISPLAYSTATUS ",
                                    "WHERE(PETINFOID = @PETINFOID)");
                oleDbCommand.Parameters.AddWithValue("@PETINFOCATEGORY", petInfoEntity.PetCategory);
                oleDbCommand.Parameters.AddWithValue("@PETINFOBREED", petInfoEntity.PetBreed);
                oleDbCommand.Parameters.AddWithValue("@PETINFOLIFESPANMIN", petInfoEntity.PetLifeSpanMin);
                oleDbCommand.Parameters.AddWithValue("@PETINFOHEIGHTMIN", petInfoEntity.PetHeightMin);
                oleDbCommand.Parameters.AddWithValue("@PETINFOWEIGHTMIN", petInfoEntity.PetWeightMin);
                oleDbCommand.Parameters.AddWithValue("@PETINFOLIFESPANMAX", petInfoEntity.PetLifeSpanMax);
                oleDbCommand.Parameters.AddWithValue("@PETINFOHEIGHTMAX", petInfoEntity.PetHeightMax);
                oleDbCommand.Parameters.AddWithValue("@PETINFOWEIGHTMAX", petInfoEntity.PetWeightMax);
                oleDbCommand.Parameters.AddWithValue("@PETINFODESC", petInfoEntity.PetDesc);
                oleDbCommand.Parameters.AddWithValue("@PETINFOPERSONALITY", petInfoEntity.PetPersonality);
                oleDbCommand.Parameters.AddWithValue("@PETINFODISPLAYSTATUS", petInfoEntity.PetDisplayStatus);
                oleDbCommand.Parameters.AddWithValue("@PETINFOID", petInfoEntity.PetInfoID);
                int insertID = dao.updateRecord(oleDbCommand);
                if (insertID > 0)
                {
                    return petInfoEntity;
                }
                else
                {
                    return null;
                }
            }
        }
        // Update PetChar
        public PetCharEntity updatePetChar(PetCharEntity petCharEntity)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("UPDATE PETCHARACTERISTICS SET CHAROVERALLADAPTABILITY = @CHAROVERALLADAPTABILITY, CHAROVERALLFRIENDLINESS = @CHAROVERALLFRIENDLINESS, ",
                                    "CHAROVERALLGROOMING = @CHAROVERALLGROOMING, CHAROVERALLTRAINABILITY = @CHAROVERALLTRAINABILITY, CHAROVERALLEXERCISE = @CHAROVERALLEXERCISE, ",
                                    "CHARADAPTTOSURROUNDING = @CHARADAPTTOSURROUNDING, CHARADAPTTONOVICE = @CHARADAPTTONOVICE, CHARADAPTTOLONELINESS = @CHARADAPTTOLONELINESS, ",
                                    "CHARADAPTTOCOLD = @CHARADAPTTOCOLD, CHARADAPTTOHOT = @CHARADAPTTOHOT, ",
                                    "CHARFRIENDWITHFAMILY = @CHARFRIENDWITHFAMILY, CHARFRIENDWITHKIDS = @CHARFRIENDWITHKIDS, CHARFRIENDWITHSTRANGERS = @CHARFRIENDWITHSTRANGERS, ",
                                    "CHARFRIENDWITHOTHERPET = @CHARFRIENDWITHOTHERPET, CHARGROOMLEVEL = @CHARGROOMLEVEL, CHARGROOMSHEDDING = @CHARGROOMSHEDDING, CHARGROOMDROOLING = @CHARGROOMDROOLING,  ",
                                    "CHARTRAINLEVEL = @CHARTRAINLEVEL, CHARTRAININTELLIGENCE = @CHARTRAININTELLIGENCE,CHARTRAINMOUTHINESS = @CHARTRAINMOUTHINESS, ",
                                    "CHARTRAINPREYDRIVE = @CHARTRAINPREYDRIVE,CHARTRAINBARKHOWL = @CHARTRAINBARKHOWL, ",
                                    "CHAREXERCISEENERGYLEVEL = @CHAREXERCISEENERGYLEVEL,CHAREXERCISENEEDS = @CHAREXERCISENEEDS, CHAREXERCISEPLAYFULLNESS = @CHAREXERCISEPLAYFULLNESS ",
                                    "WHERE(CHARID = @CHARID)");
                oleDbCommand.Parameters.AddWithValue("@CHAROVERALLADAPTABILITY", petCharEntity.CharOverallAdaptability);
                oleDbCommand.Parameters.AddWithValue("@CHAROVERALLFRIENDLINESS", petCharEntity.CharOverallFriendliness);
                oleDbCommand.Parameters.AddWithValue("@CHAROVERALLGROOMING", petCharEntity.CharOverallGrooming);
                oleDbCommand.Parameters.AddWithValue("@CHAROVERALLTRAINABILITY", petCharEntity.CharOverallTrainability);
                oleDbCommand.Parameters.AddWithValue("@CHAROVERALLEXERCISE", petCharEntity.CharOverallExercise);
                oleDbCommand.Parameters.AddWithValue("@CHARADAPTTOSURROUNDING", petCharEntity.CharAdaptToSurrounding);
                oleDbCommand.Parameters.AddWithValue("@CHARADAPTTONOVICE", petCharEntity.CharAdaptToNovice);
                oleDbCommand.Parameters.AddWithValue("@CHARADAPTTOLONELINESS", petCharEntity.CharAdaptToLoneliness);
                oleDbCommand.Parameters.AddWithValue("@CHARADAPTTOCOLD", petCharEntity.CharAdaptToCold);
                oleDbCommand.Parameters.AddWithValue("@CHARADAPTTOHOT", petCharEntity.CharAdaptToHot);
                oleDbCommand.Parameters.AddWithValue("@CHARFRIENDWITHFAMILY", petCharEntity.CharFriendWithFamily);
                oleDbCommand.Parameters.AddWithValue("@CHARFRIENDWITHKIDS", petCharEntity.CharFriendWithKids);
                oleDbCommand.Parameters.AddWithValue("@CHARFRIENDWITHSTRANGERS", petCharEntity.CharFriendWithStranger);
                oleDbCommand.Parameters.AddWithValue("@CHARFRIENDWITHOTHERPET", petCharEntity.CharFriendWithOtherPet);
                oleDbCommand.Parameters.AddWithValue("@CHARGROOMLEVEL", petCharEntity.CharGroomLevel);
                oleDbCommand.Parameters.AddWithValue("@CHARGROOMSHEDDING", petCharEntity.CharGroomSheddingLevel);
                oleDbCommand.Parameters.AddWithValue("@CHARGROOMDROOLING", petCharEntity.CharGroomDrooling);
                oleDbCommand.Parameters.AddWithValue("@CHARTRAINLEVEL", petCharEntity.CharTrainLevel);
                oleDbCommand.Parameters.AddWithValue("@CHARTRAININTELLIGENCE", petCharEntity.CharTrainIntelligenceLevel);
                oleDbCommand.Parameters.AddWithValue("@CHARTRAINMOUTHINESS", petCharEntity.CharTrainMouthiness);
                oleDbCommand.Parameters.AddWithValue("@CHARTRAINPREYDRIVE", petCharEntity.CharTrainPreyDrive);
                oleDbCommand.Parameters.AddWithValue("@CHARTRAINBARKHOWL", petCharEntity.CharTrainBarkHowl);
                oleDbCommand.Parameters.AddWithValue("@CHAREXERCISEENERGYLEVEL", petCharEntity.CharExerciseEnergyLevel);
                oleDbCommand.Parameters.AddWithValue("@CHAREXERCISENEEDS", petCharEntity.CharExerciseNeeds);
                oleDbCommand.Parameters.AddWithValue("@CHAREXERCISEPLAYFULLNESS", petCharEntity.CharExercisePlayfullness);
                oleDbCommand.Parameters.AddWithValue("@CHARID", Convert.ToInt32(petCharEntity.CharID));
                int insertID = dao.updateRecord(oleDbCommand);
                if (insertID > 0)
                {
                    return petCharEntity;
                }
                else
                {
                    return null;
                }
            }
        }
        // Delete PetPhoto
        public PetInfoEntity deletePetPhoto(PetInfoEntity petInfoEntity)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            PhotoController.getInstance().deletePhoto(petInfoEntity.PetInfoID, Enums.GetDescription(PhotoPurpose.ShopInfo));
            return petInfoEntity;
        }
        // Retrieve PetInfo
        public PetInfoEntity getPetInfo(string petInfoID)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM PETINFO WHERE PETINFOID = @PETINFOID");
                oleDbCommand.Parameters.AddWithValue("@PETINFOID", string.Concat(petInfoID));
                dataSet = dao.getRecord(oleDbCommand);
                if (dataSet.Tables != null)
                {
                    return new PetInfoEntity(
                        dataSet.Tables[0].Rows[0][0].ToString(),
                        dataSet.Tables[0].Rows[0][1].ToString(),
                        dataSet.Tables[0].Rows[0][2].ToString(),
                        dataSet.Tables[0].Rows[0][3].ToString(),
                        dataSet.Tables[0].Rows[0][4].ToString(),
                        dataSet.Tables[0].Rows[0][5].ToString(),
                        dataSet.Tables[0].Rows[0][6].ToString(),
                        dataSet.Tables[0].Rows[0][7].ToString(),
                        dataSet.Tables[0].Rows[0][8].ToString(),
                        dataSet.Tables[0].Rows[0][9].ToString(),
                        dataSet.Tables[0].Rows[0][10].ToString(),
                        dataSet.Tables[0].Rows[0][11].ToString(),
                            petCharEntity = getPetChar(petInfoID), PhotoController.getInstance().getPhotoEntities(petInfoID, Enums.GetDescription(PhotoPurpose.PetInfo)));
                }
                else { return null; }
            }
        }
        // Retrieve PetChar
        public PetCharEntity getPetChar(string petInfoID)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM PETCHARACTERISTICS WHERE PETINFOID = @PETINFOID");
                oleDbCommand.Parameters.AddWithValue("@PETINFOID", string.Concat(petInfoID));
                dataSet = dao.getRecord(oleDbCommand);
                return petCharEntity = new PetCharEntity(
                    dataSet.Tables[0].Rows[0]["petinfoid"].ToString(),
                    dataSet.Tables[0].Rows[0]["charOverallAdaptability"].ToString(),
                    dataSet.Tables[0].Rows[0]["charOverallFriendliness"].ToString(),
                    dataSet.Tables[0].Rows[0]["charOverallGrooming"].ToString(),
                    dataSet.Tables[0].Rows[0]["charOverallTrainability"].ToString(),
                    dataSet.Tables[0].Rows[0]["charOverallExercise"].ToString(),
                    dataSet.Tables[0].Rows[0]["charAdaptToSurrounding"].ToString(),
                    dataSet.Tables[0].Rows[0]["charAdaptToNovice"].ToString(),
                    dataSet.Tables[0].Rows[0]["charAdaptToLoneliness"].ToString(),
                    dataSet.Tables[0].Rows[0]["charAdaptToCold"].ToString(),
                    dataSet.Tables[0].Rows[0]["charAdaptToHot"].ToString(),
                    dataSet.Tables[0].Rows[0]["charFriendWithFamily"].ToString(),
                    dataSet.Tables[0].Rows[0]["charFriendWithKids"].ToString(),
                    dataSet.Tables[0].Rows[0]["charFriendWithStrangers"].ToString(),
                    dataSet.Tables[0].Rows[0]["charFriendWithOtherPet"].ToString(),
                    dataSet.Tables[0].Rows[0]["charGroomLevel"].ToString(),
                    dataSet.Tables[0].Rows[0]["charGroomShedding"].ToString(),
                    dataSet.Tables[0].Rows[0]["charGroomDrooling"].ToString(),
                    dataSet.Tables[0].Rows[0]["charTrainLevel"].ToString(),
                    dataSet.Tables[0].Rows[0]["charTrainIntelligence"].ToString(),
                    dataSet.Tables[0].Rows[0]["charTrainMouthiness"].ToString(),
                    dataSet.Tables[0].Rows[0]["charTrainPreyDrive"].ToString(),
                    dataSet.Tables[0].Rows[0]["charTrainBarkHowl"].ToString(),
                    dataSet.Tables[0].Rows[0]["charExerciseEnergyLevel"].ToString(),
                    dataSet.Tables[0].Rows[0]["charExerciseNeeds"].ToString(),
                    dataSet.Tables[0].Rows[0]["charExercisePlayfullness"].ToString()
                    );
            }
        }
        // Filter Pet Info Data
        public DataTable filterPetInfoData(string ddlbreed, string tbSearchValue, Label searchResult)
        {
            searchResult.ForeColor = Utility.getColorWhite();
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                string sqlQuery = string.Concat("SELECT * FROM PETINFO WHERE (PETINFOCATEGORY LIKE @SEARCHVALUE OR ",
                                            "PETINFODESC LIKE @SEARCHVALUE OR ",
                                            "PETINFOPERSONALITY LIKE @SEARCHVALUE OR ",
                                            "PETINFODISPLAYSTATUS LIKE @SEARCHVALUE) ");
                searchResult.Text = "Records for Pet Info ";
                if (!string.IsNullOrEmpty(tbSearchValue))
                    searchResult.Text += string.Concat("\"", tbSearchValue, "\" ");
                if (!string.IsNullOrEmpty(ddlbreed))
                {
                    searchResult.Text += string.Concat("\"", ddlbreed, "\" ");
                    sqlQuery += string.Concat(" AND (PETINFOBREED LIKE '%", ddlbreed, "%') ");
                }
                oleDbCommand.CommandText = sqlQuery;
                oleDbCommand.Parameters.AddWithValue("@SEARCHVALUE", string.Concat("%", tbSearchValue, "%"));
                dataSet = dao.getRecord(oleDbCommand);
                return dataSet.Tables[0];
            }
        }
    }
}