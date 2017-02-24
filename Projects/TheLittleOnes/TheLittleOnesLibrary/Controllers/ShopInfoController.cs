using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLittleOnesLibrary.DataAccessObject;
using TheLittleOnesLibrary.Entities;

namespace TheLittleOnesLibrary.Controllers
{
    public class ShopInfoController
    {

        private static ShopInfoController shopInfoCtrl;
        private static ShopInfoEntity shopInfoEntity;
        private static ShopTimeEntity shopTimeEntity;
        private static PhotoEntity photoEntity;

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

        // Create ShopInfo 
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
        public ShopInfoEntity createPetPhoto(ShopInfoEntity shopInfoEntity)
        {
            foreach (PhotoEntity photoEntity in shopInfoEntity.PhotoEntities)
            {
                using (oleDbCommand = new OleDbCommand())
                {
                    oleDbCommand.CommandType = CommandType.Text;
                    oleDbCommand.CommandText = string.Concat("INSERT INTO PHOTO (PHOTOOWNERID,PHOTONAME,PHOTOPATH)",
                                                             "VALUES (@PHOTOOWNERID,@PHOTONAME,@PHOTOPATH);");
                    oleDbCommand.Parameters.AddWithValue("@PHOTOOWNERID", shopInfoEntity.ShopInfoID);
                    oleDbCommand.Parameters.AddWithValue("@PHOTONAME", photoEntity.PhotoName);
                    oleDbCommand.Parameters.AddWithValue("@PHOTOPATH", photoEntity.PhotoPath);

                    int insertID = dao.createRecord(oleDbCommand);
                    if (insertID > 0)
                    {
                        photoEntity.PhotoID = insertID.ToString();
                    }
                }
            }
            return shopInfoEntity;
        }
    }
}
