using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLittleOnesLibrary.Entities
{
    public class PhotoEntity
    {
        private string photoID;
        private string photoName;
        private string photoPath;

        // Create record consrtuctor
        public PhotoEntity(string photoName, string photoPath)
        {

            this.photoName = photoName;
            this.photoPath = photoPath;
        }
        // Retrieve record consrtuctor
        public PhotoEntity(string photoOwnerID, string photoID, string photoName, string photoPath)
        {

            this.photoID = photoID;
            this.photoName = photoName;
            this.photoPath = photoPath;
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
    }
}
