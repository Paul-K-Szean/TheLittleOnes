//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.OleDb;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TheLittleOnesLibrary.DataAccessObject;
//using TheLittleOnesLibrary.Entities;

//namespace TheLittleOnesLibrary.Controllers
//{
//    public class PhotoController
//    {
//        private static PhotoController photoCtrl;
//        private static PhotoEntity photoEntity;
//        public static PhotoController getInstance()
//        {
//            if (photoCtrl == null)
//                photoCtrl = new PhotoController();
//            return photoCtrl;
//        }
//        // Data Access Object
//        private DAO dao;
//        private OleDbCommand oleDbCommand;
//        private DataSet dataSet;
//        // Default Constructor
//        public PhotoController()
//        {
//            dao = DAO.getInstance();
//        }
        
//        // Create Photo
//        public PhotoEntity createPhoto(PhotoEntity photoEntity)
//        {
//            using (oleDbCommand = new OleDbCommand())
//            {
//                oleDbCommand.CommandType = CommandType.Text;
//                oleDbCommand.CommandText = string.Concat("INSERT INTO PHOTO (PHOTOOWNERID,PHOTONAME,PHOTOPATH)",
//                                                         "VALUES (@PHOTOOWNERID,@PHOTONAME,@PHOTOPATH);");
//                oleDbCommand.Parameters.AddWithValue("@PHOTOOWNERID", photoEntity.PhotoOwnerID);
//                oleDbCommand.Parameters.AddWithValue("@PHOTONAME", photoEntity.PhotoName);
//                oleDbCommand.Parameters.AddWithValue("@PHOTOPATH", photoEntity.PhotoPath);

//                int insertID = dao.createRecord(oleDbCommand);
//                if (insertID > 0)
//                {
//                    photoEntity.PhotoID = insertID.ToString();
//                    return photoEntity;
//                }
//                else
//                {
//                    return null;
//                }
//            }
//        }

//    }
//}
