using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TheLittleOnesLibrary.DataAccessObject;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.EnumFolder;

namespace TheLittleOnesLibrary.Controllers
{
    public class PetController
    {
        private static PetController petCtrl;
        
        public static PetController getInstance()
        {
            if (petCtrl == null)
                petCtrl = new PetController();
            return petCtrl;
        }

        // Data Access Object
        private DAO dao;
        private OleDbCommand oleDbCommand;
        private DataSet dataSet;

        // Default Constructor
        public PetController()
        {
            dao = DAO.getInstance();
        }

        // Create Pet
        public PetEntity createPet(PetEntity petEntity)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("INSERT INTO PET (PETBREED,PETNAME,PETGENDER,PETWEIGHT, PETSIZE, PETDESC,PETENERGY,PETFRIENDLYWITHPET,PETFRIENDLYWITHPEOPLE,PETTOILETTRAINED,PETHEALTHINFO) ",
                                                         "VALUES (@PETBREED,@PETNAME,@PETGENDER,@PETWEIGHT,@PETSIZE,@PETDESC,@PETENERGY,@PETFRIENDLYWITHPET,@PETFRIENDLYWITHPEOPLE,@PETTOILETTRAINED,@PETHEALTHINFO);");

                oleDbCommand.Parameters.AddWithValue("@PETBREED", petEntity.PetBreed);
                oleDbCommand.Parameters.AddWithValue("@PETNAME", petEntity.PetName);
                oleDbCommand.Parameters.AddWithValue("@PETGENDER", petEntity.PetGender);
                oleDbCommand.Parameters.AddWithValue("@PETWEIGHT", petEntity.PetWeight);
                oleDbCommand.Parameters.AddWithValue("@PETSIZE", petEntity.PetSize);
                oleDbCommand.Parameters.AddWithValue("@PETDESC", petEntity.PetDesc);
                oleDbCommand.Parameters.AddWithValue("@PETENERGY", petEntity.PetEnergy);
                oleDbCommand.Parameters.AddWithValue("@PETFRIENDLYWITHPET", petEntity.PetFriendlyWithPet);
                oleDbCommand.Parameters.AddWithValue("@PETFRIENDLYWITHPEOPLE", petEntity.PetFriendlyWithPeople);
                oleDbCommand.Parameters.AddWithValue("@PETTOILETTRAINED", petEntity.PetToiletTrained);
                oleDbCommand.Parameters.AddWithValue("@PETHEALTHINFO", petEntity.PetHealthInfo);
                int insertID = dao.createRecord(oleDbCommand);
                if (insertID > 0)
                {
                    petEntity.PetID = insertID.ToString();
                    return petEntity;
                }
                else
                {
                    return null;
                }
            }
        }

        // Create Photo
        public PetEntity createPhoto(PetEntity petEntity)
        {
            petEntity.PhotoEntities = PhotoController.getInstance().createPhoto(petEntity.PhotoEntities, petEntity.PetID);
            return petEntity;
        }

        // Update Pet
        public PetEntity updatePet(PetEntity petEntity)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("UPDATE PET SET PETBREED = @PETBREED, PETNAME = @PETNAME, ",
                                    "PETGENDER = @PETGENDER, PETWEIGHT = @PETWEIGHT, PETSIZE = @PETSIZE, PETDESC = @PETDESC, PETENERGY = @PETENERGY, ",
                                    "PETFRIENDLYWITHPET = @PETFRIENDLYWITHPET, PETFRIENDLYWITHPEOPLE = @PETFRIENDLYWITHPEOPLE, ",
                                    "PETTOILETTRAINED = @PETTOILETTRAINED, PETHEALTHINFO = @PETHEALTHINFO ",
                                    "WHERE(PETID = @PETID)");
                oleDbCommand.Parameters.AddWithValue("@PETBREED", petEntity.PetBreed);
                oleDbCommand.Parameters.AddWithValue("@PETNAME", petEntity.PetName);
                oleDbCommand.Parameters.AddWithValue("@PETGENDER", petEntity.PetGender);
                oleDbCommand.Parameters.AddWithValue("@PETWEIGHT", petEntity.PetWeight);
                oleDbCommand.Parameters.AddWithValue("@PETSIZE", petEntity.PetSize);
                oleDbCommand.Parameters.AddWithValue("@PETDESC", petEntity.PetDesc);
                oleDbCommand.Parameters.AddWithValue("@PETENERGY", petEntity.PetEnergy);
                oleDbCommand.Parameters.AddWithValue("@PETFRIENDLYWOTHPET", petEntity.PetFriendlyWithPet);
                oleDbCommand.Parameters.AddWithValue("@PETFRIENDLYWITHPEOPLE", petEntity.PetFriendlyWithPeople);
                oleDbCommand.Parameters.AddWithValue("@PETTOILETTRAINED", petEntity.PetToiletTrained);
                oleDbCommand.Parameters.AddWithValue("@PETHEALTHINFO", petEntity.PetHealthInfo);
                oleDbCommand.Parameters.AddWithValue("@PETID", petEntity.PetID);
                int insertID = dao.updateRecord(oleDbCommand);
                if (insertID > 0)
                {
                    return petEntity;
                }
                else
                {
                    return null;
                }
            }
        }

        // Delete Photo
        public PetEntity deletePhoto(PetEntity petEntity)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            PhotoController.getInstance().deletePhoto(petEntity.PetID, PhotoPurpose.ShopInfo.ToString());
            return petEntity;
        }

        // Retrieve Pet
        public PetEntity getPet(string petID)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM PET WHERE PETID = @PETID");
                oleDbCommand.Parameters.AddWithValue("@PETID", string.Concat(petID));
                dataSet = dao.getRecord(oleDbCommand);

                return new PetEntity(
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
                 PhotoController.getInstance().getPhotoEntities(petID, PhotoPurpose.Pet.ToString()));
            }
        }

        // Retrieve PetPhoto
        //public List<PhotoEntity> getPetPhoto(string petID)
        //{
        //    return PhotoController.getInstance().getPhotoEntities(petID);
        //}

    }
}
