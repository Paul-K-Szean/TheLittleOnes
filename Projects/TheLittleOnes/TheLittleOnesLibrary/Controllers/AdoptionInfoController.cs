using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary.DataAccessObject;
using TheLittleOnesLibrary.Entities;
namespace TheLittleOnesLibrary.Controllers
{
    public class AdoptInfoController
    {
        private static AdoptInfoController AdoptInfoCtrl;
        public static AdoptInfoController getInstance()
        {
            if (AdoptInfoCtrl == null)
                AdoptInfoCtrl = new AdoptInfoController();
            return AdoptInfoCtrl;
        }
        // Data Access Object
        private DAO dao;
        private OleDbCommand oleDbCommand;
        private DataSet dataSet;
        // Default Constructor
        public AdoptInfoController()
        {
            dao = DAO.getInstance();
        }
        // Check If AdoptInfo Exists
        public bool checkAdoptInfoExist(string adoptPetBreed, string adoptPetName)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT PETID FROM PET WHERE PETBREED LIKE @PETBREED AND PETNAME LIKE @PETNAME ");
                oleDbCommand.Parameters.AddWithValue("@PETBREED", string.Concat(adoptPetBreed));
                oleDbCommand.Parameters.AddWithValue("@PETNAME", string.Concat(adoptPetName));
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
        // Create AdoptInfo
        public AdoptInfoEntity createAdoptInfo(AdoptInfoEntity adoptInfoEntity)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("INSERT INTO ADOPTINFO (SHOPINFOID,PETID, ADOPTINFOSTATUS) ",
                                                         "VALUES            (@SHOPINFOID,@PETID,@ADOPTINFOSTATUS);");
                oleDbCommand.Parameters.AddWithValue("@SHOPINFOID", adoptInfoEntity.ShopInfoEntity.ShopInfoID);
                oleDbCommand.Parameters.AddWithValue("@PETID", adoptInfoEntity.PetEntity.PetID);
                oleDbCommand.Parameters.AddWithValue("@ADOPTINFOSTATUS", adoptInfoEntity.AdoptInfoStatus);
                int insertID = dao.createRecord(oleDbCommand);
                if (insertID > 0)
                {
                    adoptInfoEntity.AdoptInfoID = insertID.ToString();
                    return adoptInfoEntity;
                }
                else
                {
                    return null;
                }
            }
        }
        // Update AdoptInfo
        public AdoptInfoEntity updateAdoptInfo(AdoptInfoEntity adoptInfoEntity)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("UPDATE ADOPTINFO SET SHOPINFOID = @SHOPINFOID , ADOPTINFOSTATUS = @ADOPTINFOSTATUS WHERE(ADOPTINFOID = @ADOPTINFOID)");
                oleDbCommand.Parameters.AddWithValue("@SHOPINFOID", adoptInfoEntity.ShopInfoEntity.ShopInfoID);
                oleDbCommand.Parameters.AddWithValue("@ADOPTINFOSTATUS", adoptInfoEntity.AdoptInfoStatus);
                oleDbCommand.Parameters.AddWithValue("@ADOPTINFOID", adoptInfoEntity.AdoptInfoID);
                int insertID = dao.updateRecord(oleDbCommand);
                if (insertID > 0)
                {
                    return adoptInfoEntity;
                }
                else
                {
                    return null;
                }
            }
        }
        // Retrieve AdoptInfoInfoEntity
        public AdoptInfoEntity getAdoptInfoEntity(string adoptInfoID)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM ADOPTINFO WHERE ADOPTINFOID = @ADOPTINFOID");
                oleDbCommand.Parameters.AddWithValue("@ADOPTINFOID", string.Concat(adoptInfoID));
                dataSet = dao.getRecord(oleDbCommand);
                ShopInfoController shopInfoCtrler = new ShopInfoController();
                PetController petCtrler = new PetController();
                if (dataSet != null)
                {
                    return new AdoptInfoEntity(
                        shopInfoCtrler.getShopInfo(dataSet.Tables[0].Rows[0][0].ToString()), // shopInfoID
                        petCtrler.getPet(dataSet.Tables[0].Rows[0][1].ToString()),// petID
                        dataSet.Tables[0].Rows[0][2].ToString(),// adoptinfoID
                        dataSet.Tables[0].Rows[0][3].ToString());// Status
                }
                else
                {
                    return null;
                }
            }
        }
        // Retrieve AdoptInfoInfoEntities
        public List<AdoptInfoEntity> getAdoptInfoEntities()
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM ADOPTINFO WHERE ADOPTINFOSTATUS = 'AVAILABLE' ");
                dataSet = dao.getRecord(oleDbCommand);
                List<AdoptInfoEntity> adoptInfoEntities = new List<AdoptInfoEntity>();
                if (dataSet != null)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        adoptInfoEntities.Add(
                            new AdoptInfoEntity(
                        ShopInfoController.getInstance().getShopInfo(row["shopInfoID"].ToString()), // shopInfoID
                        PetController.getInstance().getPet(row["petID"].ToString()),// petID
                        row["adoptInfoID"].ToString(),// adoptinfoID
                        row["adoptInfoStatus"].ToString()));// Status
                    }
                    return adoptInfoEntities;
                }
                else
                {
                    return null;
                }
            }
        }
        // Filter Data
        public DataTable filterAdoptionInfoData(string userShopID, string filterGender, string filterSize, string filterStatus, string tbSearchValue, Label LBLSearchResultAdoptInfo)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            LBLSearchResultAdoptInfo.ForeColor = Utility.getColorWhite();
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                string sqlQuery = string.Concat("SELECT AdoptInfo.shopInfoID, AdoptInfo.petID, AdoptInfo.adoptInfoID, AdoptInfo.adoptInfoStatus, Pet.petID AS Expr1, Pet.petBreed, Pet.petName, Pet.petGender, Pet.petWeight, Pet.petSize, Pet.petDesc, Pet.petEnergy, Pet.petFriendlyWithPet, Pet.petFriendlyWithPeople, Pet.petToiletTrained, Pet.petHealthInfo, ShopInfo.shopInfoID AS Expr2, ShopInfo.shopInfoName, ShopInfo.shopInfoContact, ShopInfo.shopInfoAddress, ShopInfo.shopInfoGrooming, ShopInfo.shopInfoType, ShopInfo.shopInfoDesc, ShopInfo.shopInfoCloseOnPublicHoliday ",
                    " FROM((ADOPTINFO INNER JOIN PET ON ADOPTINFO.PETID = PET.PETID) INNER JOIN SHOPINFO ON ADOPTINFO.SHOPINFOID = SHOPINFO.SHOPINFOID) WHERE ",
                    "( PETBREED LIKE @SEARCHVALUE OR ",
                    "  PETNAME LIKE @SEARCHVALUE OR ",
                    "  SHOPINFONAME LIKE @SEARCHVALUE ) "
                    );
                LBLSearchResultAdoptInfo.Text = "Records for Adoption Info ";
                if (!string.IsNullOrEmpty(tbSearchValue))
                {
                    LBLSearchResultAdoptInfo.Text += string.Concat("\"", tbSearchValue, "\" ");
                }
                if (!string.IsNullOrEmpty(filterGender))
                {
                    LBLSearchResultAdoptInfo.Text += string.Concat("\"", filterGender, "\" ");
                    sqlQuery += string.Concat(" AND (PETGENDER LIKE '", filterGender, "') ");
                }
                if (!string.IsNullOrEmpty(filterSize))
                {
                    LBLSearchResultAdoptInfo.Text += string.Concat("\"", filterSize, "\" ");
                    sqlQuery += string.Concat(" AND (PETSIZE LIKE '", filterSize, "') ");
                }
                if (!string.IsNullOrEmpty(filterStatus))
                {
                    LBLSearchResultAdoptInfo.Text += string.Concat("\"", filterStatus, "\" ");
                    sqlQuery += string.Concat(" AND (ADOPTINFOSTATUS = '", filterStatus, "') ");
                }
                if (!string.IsNullOrEmpty(userShopID))
                {
                    LBLSearchResultAdoptInfo.Text += string.Concat("\"", userShopID, "\" ");
                    sqlQuery += string.Concat(" AND (ADOPTINFO.SHOPINFOID  = ", userShopID, ") ");
                }
                oleDbCommand.CommandText = string.Concat(sqlQuery, " ORDER BY AdoptInfo.adoptInfoID DESC ");
                oleDbCommand.Parameters.AddWithValue("@SEARCHVALUE", string.Concat("%", tbSearchValue, "%"));
                dataSet = dao.getRecord(oleDbCommand);
                return dataSet.Tables[0];
            }
        }
    }
}