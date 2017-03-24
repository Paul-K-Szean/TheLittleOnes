namespace TheLittleOnesLibrary.Entities
{
    public class PhotoEntity
    {
        private string photoID;
        private string photoName;
        private string photoPath;
        private string photoPurpose;
        // Create record consrtuctor
        public PhotoEntity(string photoName, string photoPath, string photoPurpose)
        {
            this.photoName = photoName;
            this.photoPath = photoPath;
            this.PhotoPurpose = photoPurpose;
        }
        // Retrieve record consrtuctor
        public PhotoEntity(string photoOwnerID, string photoID, string photoName, string photoPath, string photoPurpose)
        {
            this.photoID = photoID;
            this.photoName = photoName;
            this.photoPath = photoPath;
            this.PhotoPurpose = photoPurpose;
        }
        public string PhotoID
        {
            get
            {
                return photoID;
            }
            set
            {
                photoID = value;
            }
        }
        public string PhotoName
        {
            get
            {
                return photoName;
            }
            set
            {
                photoName = value;
            }
        }
        public string PhotoPath
        {
            get
            {
                return photoPath;
            }
            set
            {
                photoPath = value;
            }
        }
        public string PhotoPurpose
        {
            get
            {
                return photoPurpose;
            }
            set
            {
                photoPurpose = value;
            }
        }
    }
}