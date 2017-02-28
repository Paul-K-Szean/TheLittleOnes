using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLittleOnesLibrary.Entities
{
    public class ProfileEntity
    {
        private string accountID;
        private string profileID;
        private string profileName;
        private string profileContact;
        private string profileAddress;
        private List<PhotoEntity> photoEntities;

        // Create record constructor
        public ProfileEntity(string accountID, string profileName, string profileContact, string profileAddress, List<PhotoEntity> photoEntities)
        {
            this.accountID = accountID;
            this.profileName = profileName;
            this.profileContact = profileContact;
            this.profileAddress = profileAddress;
            this.PhotoEntities = photoEntities;
        }
        // Retrieve record constructor
        public ProfileEntity(string accountID, string profileID, string profileName, string profileContact, string profileAddress, List<PhotoEntity> photoEntities)
        {
            this.accountID = accountID;
            this.profileID = profileID;
            this.profileName = profileName;
            this.profileContact = profileContact;
            this.profileAddress = profileAddress;
            this.PhotoEntities = photoEntities;
        }

        

        public string AccountID
        {
            get
            {
                return accountID;
            }

            set
            {
                accountID = value;
            }
        }

        public string ProfileID
        {
            get
            {
                return profileID;
            }

            set
            {
                profileID = value;
            }
        }

        public string ProfileName
        {
            get
            {
                return profileName;
            }

            set
            {
                profileName = value;
            }
        }

        public string ProfileContact
        {
            get
            {
                return profileContact;
            }

            set
            {
                profileContact = value;
            }
        }

        public string ProfileAddress
        {
            get
            {
                return profileAddress;
            }

            set
            {
                profileAddress = value;
            }
        }

        public List<PhotoEntity> PhotoEntities
        {
            get
            {
                return photoEntities;
            }

            set
            {
                photoEntities = value;
            }
        }
    }
}
