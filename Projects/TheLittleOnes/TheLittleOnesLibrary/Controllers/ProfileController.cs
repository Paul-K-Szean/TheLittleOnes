using System.Data;
using System.Data.OleDb;
using TheLittleOnesLibrary.DataAccessObject;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.EnumFolder;
namespace TheLittleOnesLibrary.Controllers
{
    public class ProfileController
    {
        private static ProfileController profileCtrl;
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
        // Default Constructor
        public ProfileController()
        {
            dao = DAO.getInstance();
        }
        // Create profile
        public ProfileEntity createProfile(ProfileEntity profileEntity, string accountID)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("INSERT INTO PROFILE (ACCOUNTID,PROFILENAME,PROFILECONTACT,PROFILEADDRESS)",
                                                         "VALUES (@PROFILEID,@PROFILENAME,@PROFILECONTACT, @PROFILEADDRESS);");
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTID", accountID);
                oleDbCommand.Parameters.AddWithValue("@PROFILENAME", profileEntity.ProfileName);
                oleDbCommand.Parameters.AddWithValue("@PROFILECONTACT", profileEntity.ProfileContact);
                oleDbCommand.Parameters.AddWithValue("@PROFILEADDRESS", profileEntity.ProfileAddress);
                int insertID = dao.createRecord(oleDbCommand);
                if (insertID > 0)
                {
                    profileEntity.ProfileID = insertID.ToString();
                    return profileEntity;
                }
                else
                {
                    return null;
                }
            }
        }
        // Create PetPhoto
        public ProfileEntity createPhoto(ProfileEntity profileEntity)
        {
            profileEntity.PhotoEntities = PhotoController.getInstance().createPhoto(profileEntity.PhotoEntities, profileEntity.ProfileID);
            return profileEntity;
        }
        // Retrieve profile
        public ProfileEntity getProfile(string accountID)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM PROFILE WHERE ACCOUNTID LIKE @ACCOUNTID");
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTID", string.Concat(accountID));
                dataSet = dao.getRecord(oleDbCommand);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    // instantial profile 
                    ProfileEntity profileEntity = new ProfileEntity(
                        dataSet.Tables[0].Rows[0]["profileID"].ToString(),
                        dataSet.Tables[0].Rows[0]["profileName"].ToString(),
                        dataSet.Tables[0].Rows[0]["profileContact"].ToString(),
                        dataSet.Tables[0].Rows[0]["profileAddress"].ToString(),
                        PhotoController.getInstance().getPhotoEntities(dataSet.Tables[0].Rows[0]["profileID"].ToString(), Enums.GetDescription(PhotoPurpose.ProfileInfo)));
                    return profileEntity;
                }
                else
                {
                    return null;
                }
            }
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
                    return profileEntity;
                }
                // return unedited profileEntity
                return null;
            }
        }
        // Update profile
        public ProfileEntity updateSystemProfile(ProfileEntity profileEntity)
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
                    return profileEntity;
                }
                // fail update
                return null;
            }
        }
    }
}