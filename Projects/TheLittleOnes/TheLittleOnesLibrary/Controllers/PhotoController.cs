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
        private static string filePath_UploadFolderTemp;
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
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
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
        public List<PhotoEntity> saveToTempFolder(string photoPurpose, FileUpload fileUpload)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            filePath_UploadFolderTemp = string.Concat("~/uploadedFiles/temp/", photoPurpose.ToLower(), "/ID000/");
            bool filePathExist = Directory.Exists(HttpContext.Current.Server.MapPath(filePath_UploadFolderTemp));
            LogController.LogLine("Check directory: " + filePath_UploadFolderTemp + " Result: " + filePathExist);

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

            LogController.LogLine("Total files posted: " + fileUpload.PostedFiles.Count);
            // save posted files to temp folder
            if (fileUpload.HasFiles)
            {
                photoEntities = new List<PhotoEntity>();
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

        public List<PhotoEntity> changePhotoPathToDatabaseFolder(List<PhotoEntity> photoEntities, string ownerID)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            if (string.IsNullOrEmpty(filePath_UploadFolderTemp))
            {
                return null;
            }
            else
            {
                // check for database folder path
                string filePath_UploadFolderDatabase = filePath_UploadFolderTemp.Replace("temp", "database").Replace("ID000", ownerID);
                bool isfilePath_UploadFolderDatabaseExists = Directory.Exists(filePath_UploadFolderDatabase);

                // exists = wont create
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath_UploadFolderDatabase));
                Array.ForEach(Directory.GetFiles(HttpContext.Current.Server.MapPath(filePath_UploadFolderDatabase)), File.Delete);

                // get files from temp folder into database folder
                DirectoryInfo dirTemp = new DirectoryInfo(HttpContext.Current.Server.MapPath(filePath_UploadFolderTemp));
                LogController.LogLine(dirTemp.FullName);
                foreach (var file in dirTemp.GetFiles("*.*"))
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
                    photoEntity.PhotoPath = string.Concat(filePath_UploadFolderDatabase, photoEntity.PhotoName);
                    LogController.LogLine(photoEntity.PhotoPath);
                }

                //DirectoryInfo dirDatabase = new DirectoryInfo(HttpContext.Current.Server.MapPath(filePath_UploadFolderDatabase));
                //LogController.LogLine(dirDatabase.FullName);
                return photoEntities;
            }
        }

        // Preview photos
        public void previewPhotos(HtmlGenericControl photoPreview)
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
