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
    public class ShopInfoController
    {

        private static ShopInfoController shopInfoCtrl;
        private static List<ShopTimeEntity> shopTimeEntities;

        public static ShopInfoController getInstance()
        {
            if (shopInfoCtrl == null)
                shopInfoCtrl = new ShopInfoController();
            return shopInfoCtrl;
        }

        // Data Access Object
        private DAO dao;
        private OleDbCommand oleDbCommand;
        private DataSet dataSet;

        // Default Constructor
        public ShopInfoController()
        {
            dao = DAO.getInstance();
        }

        // Check if the same outlet exists
        public bool checkOutletExist(string address)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM SHOPINFO WHERE SHOPINFOADDRESS LIKE @SHOPINFOADDRESS");
                oleDbCommand.Parameters.AddWithValue("@SHOPINFOADDRESS", string.Concat("%", address, "%"));

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

        // Create ShopInfo 
        public ShopInfoEntity createShopInfo(ShopInfoEntity shopInfoEntity)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("INSERT INTO SHOPINFO (SHOPINFONAME,SHOPINFOCONTACT,SHOPINFOADDRESS,SHOPINFOGROOMING, SHOPINFOTYPE, SHOPINFODESC, SHOPINFOCLOSEONPUBLICHOLIDAY)",
                                                         "VALUES (@SHOPINFONAME,@SHOPINFOCONTACT,@SHOPINFOADDRESS,@SHOPINFOGROOMING,@SHOPINFOTYPE,@SHOPINFODESC,@SHOPINFOCLOSEONPUBLICHOLIDAY);");
                oleDbCommand.Parameters.AddWithValue("@SHOPINFONAME", shopInfoEntity.ShopInfoName);
                oleDbCommand.Parameters.AddWithValue("@SHOPINFOCONTACT", shopInfoEntity.ShopInfoContact);
                oleDbCommand.Parameters.AddWithValue("@SHOPINFOADDRESS", shopInfoEntity.ShopInfoAddress);
                oleDbCommand.Parameters.AddWithValue("@SHOPINFOGROOMING", shopInfoEntity.ShopInfoGrooming);
                oleDbCommand.Parameters.AddWithValue("@SHOPINFOTYPE", shopInfoEntity.ShopInfoType);
                oleDbCommand.Parameters.AddWithValue("@SHOPINFODESC", shopInfoEntity.ShopInfoDesc);
                oleDbCommand.Parameters.AddWithValue("@SHOPINFOCLOSEONPUBLICHOLIDAY", shopInfoEntity.ShopCloseOnPublicHoliday);

                int insertID = dao.createRecord(oleDbCommand);
                if (insertID > 0)
                {
                    shopInfoEntity.ShopInfoID = insertID.ToString();
                    return shopInfoEntity;
                }
                else
                {
                    return null;
                }
            }
        }

        // Create ShopTime
        public ShopInfoEntity createShopTime(ShopInfoEntity shopInfoEntity)
        {

            foreach (ShopTimeEntity shopTimeEntity in shopInfoEntity.ShopTimeEntities)
            {
                using (oleDbCommand = new OleDbCommand())
                {
                    oleDbCommand.CommandType = CommandType.Text;
                    oleDbCommand.CommandText = string.Concat("INSERT INTO SHOPTIME (SHOPINFOID,SHOPDAY,SHOPOPENTIME,SHOPCLOSETIME)",
                                                             "VALUES (@SHOPINFOID,@SHOPDAY,@SHOPOPENTIME,@SHOPCLOSETIME);");
                    oleDbCommand.Parameters.AddWithValue("@SHOPINFOID", shopInfoEntity.ShopInfoID);
                    oleDbCommand.Parameters.AddWithValue("@SHOPDAY", shopTimeEntity.DayOfWeek);
                    oleDbCommand.Parameters.AddWithValue("@SHOPOPENTIME", shopTimeEntity.OpenTime);
                    oleDbCommand.Parameters.AddWithValue("@SHOPCLOSETIME", shopTimeEntity.CloseTime);

                    int insertID = dao.createRecord(oleDbCommand);
                    if (insertID > 0)
                    {
                        shopTimeEntity.ShopTimeID = insertID.ToString();
                    }
                }
            }
            return shopInfoEntity;
        }

        // Create ShopPhoto
        public ShopInfoEntity createPhoto(ShopInfoEntity shopInfoEntity)
        {
            shopInfoEntity.PhotoEntities = PhotoController.getInstance().createPhoto(shopInfoEntity.PhotoEntities, shopInfoEntity.ShopInfoID);
            return shopInfoEntity;
        }

        // Update ShopInfo
        public ShopInfoEntity updateShopInfo(ShopInfoEntity shopInfoEntity)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("UPDATE SHOPINFO SET SHOPINFONAME = @SHOPINFONAME, SHOPINFOCONTACT = @SHOPINFOCONTACT, ",
                                    "SHOPINFOADDRESS = @SHOPINFOADDRESS, SHOPINFOGROOMING = @SHOPINFOGROOMING, SHOPINFOTYPE = @SHOPINFOTYPE,",
                                    "SHOPINFODESC = @SHOPINFODESC, SHOPINFOCLOSEONPUBLICHOLIDAY = @SHOPINFOCLOSEONPUBLICHOLIDAY ",
                                    "WHERE(SHOPINFOID = @SHOPINFOID)");
                oleDbCommand.Parameters.AddWithValue("@SHOPINFONAME", shopInfoEntity.ShopInfoName);
                oleDbCommand.Parameters.AddWithValue("@SHOPINFOCONTACT", shopInfoEntity.ShopInfoContact);
                oleDbCommand.Parameters.AddWithValue("@SHOPINFOADDRESS", shopInfoEntity.ShopInfoAddress);
                oleDbCommand.Parameters.AddWithValue("@SHOPINFOGROOMING", shopInfoEntity.ShopInfoGrooming);
                oleDbCommand.Parameters.AddWithValue("@SHOPINFOTYPE", shopInfoEntity.ShopInfoType);
                oleDbCommand.Parameters.AddWithValue("@SHOPINFODESC", shopInfoEntity.ShopInfoDesc);
                oleDbCommand.Parameters.AddWithValue("@SHOPINFOCLOSEONPUBLICHOLIDAY", shopInfoEntity.ShopCloseOnPublicHoliday);
                oleDbCommand.Parameters.AddWithValue("@SHOPINFOID", shopInfoEntity.ShopInfoID);
                int insertID = dao.updateRecord(oleDbCommand);
                if (insertID > 0)
                {
                    return shopInfoEntity;
                }
                else
                {
                    return null;
                }
            }
        }

        // Delete ShopPhoto
        public ShopInfoEntity deleteShopPhoto(ShopInfoEntity shopInfoEntity)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            PhotoController.getInstance().deletePhoto(shopInfoEntity.ShopInfoID, PhotoPurpose.ShopInfo.ToString());
            return shopInfoEntity;
        }
        // Delete ShopTime
        public ShopInfoEntity deleteShopTime(ShopInfoEntity shopInfoEntity)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("DELETE FROM SHOPTIME WHERE SHOPINFOID = @SHOPINFOID");
                oleDbCommand.Parameters.AddWithValue("@SHOPINFOID", shopInfoEntity.ShopInfoID);
                dao.deleteRecord(oleDbCommand);
            }
            return shopInfoEntity;
        }

        // Retrieve ShopInfo
        public ShopInfoEntity getShopInfo(string shopInfoID)
        {
            if (shopInfoID == "0" || string.IsNullOrEmpty(shopInfoID))
            {
                return null;
            }
            else
            {
                using (oleDbCommand = new OleDbCommand())
                {
                    oleDbCommand.CommandType = CommandType.Text;
                    oleDbCommand.CommandText = string.Concat("SELECT * FROM SHOPINFO WHERE SHOPINFOID = @SHOPINFOID");
                    oleDbCommand.Parameters.AddWithValue("@SHOPINFOID", string.Concat(shopInfoID));
                    dataSet = dao.getRecord(oleDbCommand);
                    return new ShopInfoEntity(
                        dataSet.Tables[0].Rows[0][0].ToString(),
                        dataSet.Tables[0].Rows[0][1].ToString(),
                        dataSet.Tables[0].Rows[0][2].ToString(),
                        dataSet.Tables[0].Rows[0][3].ToString(),
                        Convert.ToBoolean(dataSet.Tables[0].Rows[0][4]),
                        dataSet.Tables[0].Rows[0][5].ToString(),
                        dataSet.Tables[0].Rows[0][6].ToString(),
                        Convert.ToBoolean(dataSet.Tables[0].Rows[0][7]),
                            getShopTime(shopInfoID), PhotoController.getInstance().getPhotoEntities(shopInfoID, PhotoPurpose.ShopInfo.ToString()));
                }
            }

        }

        // Retrieve ShopTime
        public List<ShopTimeEntity> getShopTime(string shopInfoID)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM SHOPTIME WHERE SHOPINFOID = @SHOPINFOID");
                oleDbCommand.Parameters.AddWithValue("@SHOPINFOID", string.Concat(shopInfoID));
                dataSet = dao.getRecord(oleDbCommand);
                shopTimeEntities = new List<ShopTimeEntity>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    ShopTimeEntity shopTimeEntity = new ShopTimeEntity(
                    row.ItemArray[1].ToString(),
                    row.ItemArray[2].ToString(),
                    row.ItemArray[3].ToString(),
                    row.ItemArray[4].ToString());
                    shopTimeEntities.Add(shopTimeEntity);
                }
                return shopTimeEntities;
            }
        }

        // Retrieve ShopPhoto
        //public List<PhotoEntity> getShopPhoto(string shopInfoID)
        //{
        //    return PhotoController.getInstance().getPhotoEntities(shopInfoID);
        //}

        // Filter Shop Info Data
        public DataTable filterShopInfoData(bool chkbxPetShop, bool chkbxPetClinic, bool chkbxGrooming, string tbSearchValue, Label searchResult)
        {
            searchResult.ForeColor = Utility.getColorWhite();
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                string sqlQuery = string.Concat("SELECT * FROM SHOPINFO WHERE (SHOPINFONAME LIKE @SEARCHVALUE OR ",
                                            "SHOPINFOCONTACT LIKE @SEARCHVALUE OR ",
                                            "SHOPINFOADDRESS LIKE @SEARCHVALUE OR ",
                                            "SHOPINFODESC LIKE @SEARCHVALUE) ");
                searchResult.Text = "Records for Shop Info ";
                if (!string.IsNullOrEmpty(tbSearchValue))
                    searchResult.Text += string.Concat("\"", tbSearchValue, "\" ");
                if (chkbxPetShop)
                {
                    searchResult.Text += string.Concat("\"", "Pet Shop", "\" ");
                    sqlQuery += string.Concat(" AND (SHOPINFOTYPE LIKE '%Shop%') ");
                }
                if (chkbxPetClinic)
                {
                    searchResult.Text += string.Concat("\"", "Pet Clinic", "\" ");
                    sqlQuery += string.Concat(" AND (SHOPINFOTYPE LIKE '%Clinic%') ");
                }
                if (chkbxGrooming)
                {
                    searchResult.Text += string.Concat("\"", "Grooming", "\" ");
                    sqlQuery += string.Concat(" AND (SHOPINFOGROOMING = TRUE) ");
                }

                oleDbCommand.CommandText = string.Concat(sqlQuery, " ORDER BY [SHOPINFOID] DESC, [SHOPINFONAME] " );
                oleDbCommand.Parameters.AddWithValue("@SEARCHVALUE", string.Concat("%", tbSearchValue, "%"));

                dataSet = dao.getRecord(oleDbCommand);
                return dataSet.Tables[0];
            }
        }

    }
}
