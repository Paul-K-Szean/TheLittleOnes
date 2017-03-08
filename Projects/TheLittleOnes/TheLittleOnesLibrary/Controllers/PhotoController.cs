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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary.DataAccessObject;
using TheLittleOnesLibrary.Entities;

namespace TheLittleOnesLibrary.Controllers
{
    public class PhotoController
    {
        private static PhotoController photoCtrl;
        private List<PhotoEntity> photoEntities;

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

        // Create Photo
        public List<PhotoEntity> createPhoto(List<PhotoEntity> photoEntities, string ownerID)
        {
            foreach (PhotoEntity photoEntity in photoEntities)
            {
                using (oleDbCommand = new OleDbCommand())
                {
                    oleDbCommand.CommandType = CommandType.Text;
                    oleDbCommand.CommandText = string.Concat("INSERT INTO PHOTO (PHOTOOWNERID, PHOTOPURPOSE, PHOTONAME,PHOTOPATH)",
                                                             "VALUES (@PHOTOOWNERID,@PHOTOPURPOSE, @PHOTONAME,@PHOTOPATH);");
                    oleDbCommand.Parameters.AddWithValue("@PHOTOOWNERID", ownerID);
                    oleDbCommand.Parameters.AddWithValue("@PHOTOPURPOSE", photoEntity.PhotoPurpose);
                    oleDbCommand.Parameters.AddWithValue("@PHOTONAME", photoEntity.PhotoName);
                    oleDbCommand.Parameters.AddWithValue("@PHOTOPATH", photoEntity.PhotoPath);
                    int insertID = dao.createRecord(oleDbCommand);
                    if (insertID > 0)
                    {
                        photoEntity.PhotoID = insertID.ToString();
                    }
                }
            }
            return photoEntities;
        }

        // Delete Photo
        public void deletePhoto(string ownerID, string photoPurpose)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("DELETE FROM PHOTO WHERE PHOTOOWNERID = @PHOTOOWNERID AND PHOTOPURPOSE = @PHOTOPURPOSE");
                oleDbCommand.Parameters.AddWithValue("@PHOTOOWNERID", ownerID);
                oleDbCommand.Parameters.AddWithValue("@PHOTOPURPOSE", photoPurpose);
                dao.deleteRecord(oleDbCommand);
            }
        }

        // Create temp files in uploadfiles/temp foler
        public List<PhotoEntity> saveToTempFolder(string photoPurpose, FileUpload fileUpload, string filePath_UploadFolderTemp)
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
                string[] extensions = { ".jpg", ".jpeg", ".png", ".gif", ".tiff", ".bmp" };
                foreach (HttpPostedFile httpPostedFileInfo in fileUpload.PostedFiles)
                { 
                    if (extensions.Contains(Path.GetExtension(fileUpload.FileName).ToLower()))
                    {
                        string fileName = httpPostedFileInfo.FileName.Replace(" ", "");
                        string savePath = Path.Combine(HttpContext.Current.Server.MapPath(filePath_UploadFolderTemp), fileName);
                        httpPostedFileInfo.SaveAs(savePath);
                        photoEntities.Add(new PhotoEntity(fileName, savePath, photoPurpose));
                    }
                }
            }

            // return list<photoEntities> with temp path
            return photoEntities;
        }

        // Change photo path to database instead of using temp
        public List<PhotoEntity> changePhotoPathToDatabaseFolder(List<PhotoEntity> photoEntities, string filePath_UploadFolderTemp)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
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
                foreach (var file in dir.GetFiles("*.*"))
                {
                    if (file.Extension.Contains("jpg") || file.Extension.Contains("jpeg") || file.Extension.Contains("png") || file.Extension.Contains("gif") ||
                        file.Extension.Contains("tiff") || file.Extension.Contains("bmp"))
                    {
                        File.Copy(Path.Combine(HttpContext.Current.Server.MapPath(filePath_UploadFolderTemp), file.Name),
                        Path.Combine(HttpContext.Current.Server.MapPath(filePath_UploadFolderDatabase), file.Name), true);
                    }
                }

                // rename the file path from temp to database
                foreach (PhotoEntity photoEntity in photoEntities)
                {
                    photoEntity.PhotoPath = photoEntity.PhotoPath.Replace("temp", "database");
                }

                return photoEntities;
            }
        }

        // Change photo path to database instead of using temp
        public List<PhotoEntity> changePhotoPathToDatabaseFolder(List<PhotoEntity> photoEntities, string filePath_UploadFolderTemp, string ownerID)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            if (string.IsNullOrEmpty(filePath_UploadFolderTemp))
            {
                return null;
            }
            else
            {
                // check for database folder path
                string filePath_UploadFolderDatabase = filePath_UploadFolderTemp.Replace("temp", "database").Replace("000", ownerID);
                bool isfilePath_UploadFolderDatabaseExists = Directory.Exists(filePath_UploadFolderDatabase);

                // exists = wont create
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath_UploadFolderDatabase));
                Array.ForEach(Directory.GetFiles(HttpContext.Current.Server.MapPath(filePath_UploadFolderDatabase)), File.Delete);

                // get files from temp folder into database folder
                DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath(filePath_UploadFolderTemp));
                LogController.LogLine(dir.FullName);
                foreach (var file in dir.GetFiles("*.*"))
                {
                    if (file.Extension.Contains("jpg") || file.Extension.Contains("jpeg") || file.Extension.Contains("png") || file.Extension.Contains("gif") ||
                        file.Extension.Contains("tiff") || file.Extension.Contains("bmp"))
                    {
                        File.Copy(Path.Combine(HttpContext.Current.Server.MapPath(filePath_UploadFolderTemp), file.Name),
                        Path.Combine(HttpContext.Current.Server.MapPath(filePath_UploadFolderDatabase), file.Name), true);
                    }
                }

                // rename the file path from temp to database
                foreach (PhotoEntity photoEntity in photoEntities)
                {
                    photoEntity.PhotoPath = photoEntity.PhotoPath.Replace("temp", "database");
                }

                return photoEntities;
            }
        }


        // Preview photos
        public void previewPhotos(HtmlGenericControl photoPreview, string filePath_UploadFolderTemp)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            // display images from temp folders
            DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath(filePath_UploadFolderTemp));
            photoPreview.InnerHtml = string.Empty;
            photoPreview.Style.Add("Height", "300px");
       
            foreach (var file in dir.GetFiles())
            {
                LogController.LogLine(string.Concat(filePath_UploadFolderTemp, "/", file.Name.ToLower().Trim().Replace(" ", "")));

                photoPreview.InnerHtml += string.Concat(
                    "<img  src =\"",
                    string.Concat(filePath_UploadFolderTemp, "/", file.Name).Replace("~/", ""),
                    "\" Height=\"150\"/>",
                    "<br>", file.Name, "<hr/>");
            }
        }

        // Retrieve photoEntites
        public List<PhotoEntity> getPhotoEntities(string photoOwnerID, string photoPurpose)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM PHOTO WHERE PHOTOOWNERID = @PHOTOOWNERID AND PHOTOPURPOSE = @PHOTOPURPOSE");
                oleDbCommand.Parameters.AddWithValue("@PHOTOOWNERID", string.Concat(photoOwnerID));
                oleDbCommand.Parameters.AddWithValue("@PHOTOPURPOSE", string.Concat(photoPurpose));
                dataSet = dao.getRecord(oleDbCommand);
                photoEntities = new List<PhotoEntity>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    PhotoEntity photoEntity = new PhotoEntity(
                        row[0].ToString(),
                        row[1].ToString(),
                        row[2].ToString(),
                        row[3].ToString(),
                        row[3].ToString());
                    photoEntities.Add(photoEntity);
                }
                return photoEntities;
            }
        }
    }

}
