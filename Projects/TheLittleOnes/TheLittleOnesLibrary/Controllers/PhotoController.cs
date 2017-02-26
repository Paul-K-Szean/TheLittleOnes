using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary.DataAccessObject;
using TheLittleOnesLibrary.Entities;

namespace TheLittleOnesLibrary.Controllers
{
    public class PhotoController
    {
        private static PhotoController photoCtrl;
        private static PhotoEntity photoEntity;
        private static List<PhotoEntity> photoEntities;

        public static PhotoController getInstance()
        {
            if (photoCtrl == null)
                photoCtrl = new PhotoController();
            return photoCtrl;
        }
        // Data Access Object
        private DAO dao;
        private OleDbCommand oleDbCommand;
        private DataSet dataSet;
        // Default Constructor
        public PhotoController()
        {
            dao = DAO.getInstance();
        }

        // create temp files in uploadfiles/temp foler
        public List<PhotoEntity> previewPhotos(FileUpload fileUpload, string filePath_UploadFolderTemp)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            bool filePathExist = Directory.Exists(HttpContext.Current.Server.MapPath(filePath_UploadFolderTemp));
            LogController.LogLine("Check directory: " + filePath_UploadFolderTemp + ". Result: " + filePathExist);

            // check for folder paths - Temp
            if (filePathExist)
            {
                // remove old files
                LogController.LogLine("Removing old files: " + filePath_UploadFolderTemp);
                Array.ForEach(Directory.GetFiles(HttpContext.Current.Server.MapPath(filePath_UploadFolderTemp)), File.Delete);
            }
            else
            {
                // dont exists - create path
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath_UploadFolderTemp));
            }

            // save posted files to temp folder
            if (fileUpload.HasFiles)
            {
                photoEntities = new List<PhotoEntity>();
                LogController.LogLine("Total files posted: " + fileUpload.PostedFiles.Count);
                foreach (HttpPostedFile httpPostedFileInfo in fileUpload.PostedFiles)
                {
                    string fileName = httpPostedFileInfo.FileName.Replace(" ", "");
                    string savePath = Path.Combine(HttpContext.Current.Server.MapPath(filePath_UploadFolderTemp), fileName);
                    httpPostedFileInfo.SaveAs(savePath);
                    photoEntities.Add(new PhotoEntity(fileName, savePath));
                }
            }
            // return list<photoEntities> with temp path
            return photoEntities;
        }

        // change photo path to database instead of using temp
        public List<PhotoEntity> changePhotoPathToDatabaseFolder(List<PhotoEntity> photoEntities, string filePath_UploadFolderTemp)
        {

            if (string.IsNullOrEmpty(filePath_UploadFolderTemp))
            {
                return null;
            }
            else
            {
                // check for database folder path
                string filePath_UploadFolderDatabase = filePath_UploadFolderTemp.Replace("temp", "database");
                bool isfilePath_UploadFolderDatabaseExists = Directory.Exists(filePath_UploadFolderDatabase);

                // check for database folder path
                if (isfilePath_UploadFolderDatabaseExists)
                {
                    // remove old files
                    Array.ForEach(Directory.GetFiles(HttpContext.Current.Server.MapPath(filePath_UploadFolderDatabase)), File.Delete);
                }
                else
                {
                    // dont exists - create path
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath_UploadFolderDatabase));
                }

                // get files from temp folder into database folder
                DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath(filePath_UploadFolderTemp));
                LogController.LogLine(dir.FullName);
                foreach (var file in dir.GetFiles("*.jpg"))
                {
                    File.Copy(Path.Combine(HttpContext.Current.Server.MapPath(filePath_UploadFolderTemp), file.Name),
                       Path.Combine(HttpContext.Current.Server.MapPath(filePath_UploadFolderDatabase), file.Name), true);
                }

                // rename the file path from temp to database
                foreach (PhotoEntity photoEntity in photoEntities)
                {
                    photoEntity.PhotoPath = photoEntity.PhotoPath.Replace("temp", "database");
                }

                return photoEntities;
            }
        }

        // get photoEntites
        public List<PhotoEntity> getPhotoEntities()
        {
            if (photoEntities != null)
                return photoEntities;
            else
                return null;
        }
    }

}
