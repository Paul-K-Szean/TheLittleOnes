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
        

        // Create record constructor
        public ProfileEntity(string accountID, string profileName, string profileContact, string profileAddress)
        {
            this.accountID = accountID;
            this.profileName = profileName;
            this.profileContact = profileContact;
            this.profileAddress = profileAddress;
        }
        // Retrieve record constructor
        public ProfileEntity(string accountID, string profileID, string profileName, string profileContact, string profileAddress)
        {
            this.accountID = accountID;
            this.profileID = profileID;
            this.profileName = profileName;
            this.profileContact = profileContact;
            this.profileAddress = profileAddress;
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
    }
}
