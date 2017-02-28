using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLittleOnesLibrary.DataAccessObject;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.EnumFolder;

namespace TheLittleOnesLibrary.Controllers
{
    public class ProfileController
    {
        private static ProfileController profileCtrl;
        private static ProfileEntity loggedInProfile;
        private ProfileEntity profileEntity;
        public static ProfileController getInstance()
        {
            if (profileCtrl == null)
                profileCtrl = new ProfileController();
            return profileCtrl;
        }


        // Data Access Object
        private DAO dao;
        private OleDbCommand oleDbCommand;
        private DataSet dataSet;
        private static List<PhotoEntity> photoEntities;

        // Default Constructor
        public ProfileController()
        {
            dao = DAO.getInstance();
        }


        // Create profile
        public ProfileEntity createProfile(ProfileEntity profileEntity)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("INSERT INTO PROFILE (ACCOUNTID,PROFILENAME,PROFILECONTACT,PROFILEADDRESS)",
                                                         "VALUES (@PROFILEID,@PROFILENAME,@PROFILECONTACT, @PROFILEADDRESS);");
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTID", profileEntity.AccountID);
                oleDbCommand.Parameters.AddWithValue("@PROFILENAME", profileEntity.ProfileName);
                oleDbCommand.Parameters.AddWithValue("@PROFILECONTACT", profileEntity.ProfileContact);
                oleDbCommand.Parameters.AddWithValue("@PROFILEADDRESS", profileEntity.ProfileAddress);

                int insertID = dao.createRecord(oleDbCommand);
                if (insertID > 0)
                {
                    return new ProfileEntity(insertID.ToString(),
                        profileEntity.ProfileID,
                        profileEntity.ProfileName,
                        profileEntity.ProfileContact,
                        profileEntity.ProfileAddress,
                        photoEntities);
                }
                else
                {
                    return null;
                }
            }
        }

        // Create PetPhoto
        public ProfileEntity createPetPhoto(ProfileEntity profileEntity)
        {
            profileEntity.PhotoEntities = PhotoController.getInstance().createPhoto(profileEntity.PhotoEntities, profileEntity.ProfileID);
            return profileEntity;
        }

        // Sign in profile
        public ProfileEntity logInProfile(AccountEntity accountEntity)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM PROFILE WHERE ACCOUNTID LIKE @ACCOUNTID");
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTID", string.Concat("%", accountEntity.AccountID, "%"));

                dataSet = dao.getRecord(oleDbCommand);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    // instantial profile 
                    loggedInProfile = new ProfileEntity(
                        dataSet.Tables[0].Rows[0]["accountID"].ToString(),
                        dataSet.Tables[0].Rows[0]["profileID"].ToString(),
                        dataSet.Tables[0].Rows[0]["profileName"].ToString(),
                        dataSet.Tables[0].Rows[0]["profileContact"].ToString(),
                        dataSet.Tables[0].Rows[0]["profileAddress"].ToString(),
                        PhotoController.getInstance().getPhotoEntities(dataSet.Tables[0].Rows[0]["profileID"].ToString(), PhotoPurpose.ProfileInfo.ToString()));
                    return loggedInProfile;
                }
                else
                {
                    return null;
                }
            }
        }

        // Get logged in profile
        public ProfileEntity getLoggedInProfile()
        {
            return loggedInProfile;
        }

        // Sign out profile
        public void SignOut()
        {
            loggedInProfile = null;
            // signout profile
        }

        // Update profile
        public ProfileEntity updateProfile(ProfileEntity profileEntity)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("UPDATE PROFILE SET ",
                                                        "PROFILENAME = @PROFILENAME, PROFILECONTACT = @PROFILECONTACT, PROFILEADDRESS = @PROFILEADDRESS ",
                                                        "WHERE PROFILEID = @PROFILEID");
                oleDbCommand.Parameters.AddWithValue("@PROFILENAME", string.Concat(profileEntity.ProfileName));
                oleDbCommand.Parameters.AddWithValue("@PROFILECONTACT", string.Concat(profileEntity.ProfileContact));
                oleDbCommand.Parameters.AddWithValue("@PROFILEADDRESS", string.Concat(profileEntity.ProfileAddress));
                oleDbCommand.Parameters.AddWithValue("@PROFILEID", string.Concat(profileEntity.ProfileID));

                int insertID = dao.updateRecord(oleDbCommand);
                if (insertID > 0)
                {
                    // return edited profileEntity
                    loggedInProfile = profileEntity;
                }
                // return unedited profileEntity
                return loggedInProfile;
            }
        }



    }
}
