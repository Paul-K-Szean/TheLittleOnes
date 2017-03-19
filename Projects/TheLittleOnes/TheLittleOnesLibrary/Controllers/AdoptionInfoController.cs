using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary.DataAccessObject;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.EnumFolder;

namespace TheLittleOnesLibrary.Controllers
{
    public class AdoptInfoController
    {
        private static AdoptInfoController AdoptInfoInfoCtrl;
        public static AdoptInfoController getInstance()
        {
            if (AdoptInfoInfoCtrl == null)
                AdoptInfoInfoCtrl = new AdoptInfoController();
            return AdoptInfoInfoCtrl;
        }
        private static List<AdoptRequestEntity> adoptReqEntities;
        // Data Access Object
        private DAO dao;
        private OleDbCommand oleDbCommand;
        private DataSet dataSet;
        private DataSet dataSetAdoptRequest;
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
        // Retrieve AdoptInfoInfo
        public AdoptInfoEntity getAdoptInfo(string adoptInfoID)
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
        // Filter Data
        public DataTable filterAdoptionInfoData(string filterGender, string filterSize, string filterStatus, string tbSearchValue, Label LBLSearchResultAdoptInfo)
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
                oleDbCommand.CommandText = string.Concat(sqlQuery, " ORDER BY AdoptInfo.adoptInfoID DESC ");
                oleDbCommand.Parameters.AddWithValue("@SEARCHVALUE", string.Concat("%", tbSearchValue, "%"));
                dataSet = dao.getRecord(oleDbCommand);
                return dataSet.Tables[0];
            }
        }
        #region Adopt Request
        // Automatically checks if adopt request date is over
        public void checkAdoptRequestStatus()
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            List<AdoptRequestEntity> adoptRequestEntities = new List<AdoptRequestEntity>();
            DateTime currentDateTime = DateTime.Now;
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM ADOPTREQUEST");
                dataSet = dao.getRecord(oleDbCommand);
                foreach (AdoptRequestEntity adoptRequestEntity in adoptRequestEntities)
                {
                    if (adoptRequestEntity.AdoptReqDateTime < currentDateTime)
                    {
                        LogController.LogLine(Enums.GetDescription(SystemStatus.Completed));
                    }
                }
            }
        }
        // Check if AdoptRequest exists
        public bool checkAdoptRequestExist(string adoptRequesterID, string adoptInfoID)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT ADOPTREQID FROM ADOPTREQUEST WHERE ADOPTREQUESTERID = @ADOPTREQUESTERID AND ADOPTINFOID = @ADOPTINFOID AND ADOPTREQSTATUS='Confirmed'");
                oleDbCommand.Parameters.AddWithValue("@ADOPTREQUESTERID", string.Concat(adoptRequesterID));
                oleDbCommand.Parameters.AddWithValue("@ADOPTINFOID", string.Concat(adoptInfoID));
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
        // Create AdoptRequest
        public AdoptRequestEntity createAdoptRequest(AdoptRequestEntity adoptRequestEntity)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("INSERT INTO ADOPTREQUEST(ADOPTREQUESTERID,ADOPTINFOID,ADOPTREQDATETIME,ADOPTREQDATECREATED, ADOPTREQSTATUS ) ",
                                                         "              VALUES (@ADOPTREQUESTERID,@ADOPTINFOID,@ADOPTREQDATETIME,NOW(),@ADOPTREQSTATUS);");
                oleDbCommand.Parameters.AddWithValue("@ADOPTREQUESTERID", adoptRequestEntity.AccountEntity.AccountID);
                oleDbCommand.Parameters.AddWithValue("@ADOPTINFOID", adoptRequestEntity.AdoptInfoEntity.AdoptInfoID);
                oleDbCommand.Parameters.AddWithValue("@ADOPTREQDATETIME", adoptRequestEntity.AdoptReqDateTime);
                oleDbCommand.Parameters.AddWithValue("@ADOPTREQSTATUS", Enums.GetDescription(SystemStatus.Confirmed));
                int insertID = dao.createRecord(oleDbCommand);
                if (insertID > 0)
                {
                    adoptRequestEntity.AdoptReqID = insertID.ToString();
                    return adoptRequestEntity;
                }
                else
                {
                    return null;
                }
            }
        }
        // Retrieve Total AdoptRequest 
        public List<AdoptRequestEntity> getAllAdoptRequestEntities(string adoptInfoID)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM ADOPTREQUEST WHERE ADOPTINFOID = @ADOPTINFOID");
                oleDbCommand.Parameters.AddWithValue("@ADOPTINFOID", string.Concat(adoptInfoID));
                dataSet = dao.getRecord(oleDbCommand);
                adoptReqEntities = new List<AdoptRequestEntity>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    AdoptRequestEntity adoptReqEntity = new AdoptRequestEntity(
                        row[0].ToString(),
                        row[1] as AccountEntity,
                        row[2] as AdoptInfoEntity,
                        DateTime.Parse(row[3].ToString()),
                        DateTime.Parse(row[4].ToString()),
                        row[5].ToString());
                    adoptReqEntities.Add(adoptReqEntity);
                }
                return adoptReqEntities;
            }
        }
        // Retrieve user AdoptRequest 
        public AdoptRequestEntity getUserAdoptRequestEntity(string adoptRequesterID, string adoptInfoID)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);

            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM ADOPTREQUEST WHERE ADOPTREQUESTERID = @ADOPTREQUESTERID AND ADOPTINFOID = @ADOPTINFOID");
                oleDbCommand.Parameters.AddWithValue("@ADOPTREQUESTERID", string.Concat(adoptRequesterID));
                oleDbCommand.Parameters.AddWithValue("@ADOPTINFOID", string.Concat(adoptInfoID));
                dataSetAdoptRequest = dao.getRecord(oleDbCommand);
                if (dataSetAdoptRequest != null)
                {
                    return new AdoptRequestEntity(
                       dataSetAdoptRequest.Tables[0].Rows[0]["ADOPTREQID"].ToString(),
                       AccountController.getInstance().getAccount(dataSetAdoptRequest.Tables[0].Rows[0]["ADOPTREQUESTERID"].ToString()),
                       getAdoptInfo(dataSetAdoptRequest.Tables[0].Rows[0]["ADOPTINFOID"].ToString()),
                       DateTime.Parse(dataSetAdoptRequest.Tables[0].Rows[0]["ADOPTREQDATETIME"].ToString()),
                       DateTime.Parse(dataSetAdoptRequest.Tables[0].Rows[0]["ADOPTREQDATECREATED"].ToString()),
                       dataSetAdoptRequest.Tables[0].Rows[0]["ADOPTREQSTATUS"].ToString()
                       );
                }
                else return null;
            }
        }
        // Retrieve all user AdoptRequest 
        public List<AdoptRequestEntity> getUserAdoptRequestEntities(string adoptRequesterID)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM ADOPTREQUEST WHERE ADOPTREQUESTERID = @ADOPTREQUESTERID");
                oleDbCommand.Parameters.AddWithValue("@ADOPTREQUESTERID", string.Concat(adoptRequesterID));
                dataSetAdoptRequest = dao.getRecord(oleDbCommand);
                adoptReqEntities = new List<AdoptRequestEntity>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    AdoptRequestEntity adoptReqEntity = new AdoptRequestEntity(
                        dataSetAdoptRequest.Tables[0].Rows[0]["ADOPTREQID"].ToString(),
                        AccountController.getInstance().getAccount(dataSetAdoptRequest.Tables[0].Rows[0]["ADOPTREQUESTERID"].ToString()),
                        getAdoptInfo(dataSetAdoptRequest.Tables[0].Rows[0]["ADOPTINFOID"].ToString()),
                        DateTime.Parse(dataSetAdoptRequest.Tables[0].Rows[0]["ADOPTREQDATETIME"].ToString()),
                        DateTime.Parse(dataSetAdoptRequest.Tables[0].Rows[0]["ADOPTREQDATECREATED"].ToString()),
                        dataSetAdoptRequest.Tables[0].Rows[0]["ADOPTREQSTATUS"].ToString()
                        );
                    adoptReqEntities.Add(adoptReqEntity);
                }
                return adoptReqEntities;
            }
        }
        // Update AdoptRequest
        public AdoptRequestEntity updateAdoptInfo(AdoptRequestEntity adoptRequestEntity)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("UPDATE ADOPTREQUEST SET ",
                    "ADOPTREQUESTERID = @ADOPTREQUESTERID , ADOPTINFOID = @ADOPTINFOID, ",
                    "ADOPTREQDATETIME = @ADOPTREQDATETIME, ADOPTREQDATECREATED = @ADOPTREQDATECREATED, ADOPTREQSTATUS = @ADOPTREQSTATUS",
                    " WHERE(ADOPTREQID = @ADOPTREQID)");
                oleDbCommand.Parameters.AddWithValue("@ADOPTREQUESTERID", adoptRequestEntity.AccountEntity.AccountID);
                oleDbCommand.Parameters.AddWithValue("@ADOPTINFOID", adoptRequestEntity.AdoptInfoEntity.AdoptInfoID);
                oleDbCommand.Parameters.AddWithValue("@ADOPTREQDATETIME", adoptRequestEntity.AdoptReqDateTime);
                oleDbCommand.Parameters.AddWithValue("@ADOPTREQDATECREATED", adoptRequestEntity.AdoptReqDateCreated);
                oleDbCommand.Parameters.AddWithValue("@ADOPTREQSTATUS", adoptRequestEntity.AdoptReqStatus);
                oleDbCommand.Parameters.AddWithValue("@ADOPTREQID", adoptRequestEntity.AdoptReqID);
                int insertID = dao.updateRecord(oleDbCommand);
                if (insertID > 0)
                {
                    return adoptRequestEntity;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion
    }
}
