using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLittleOnesLibrary.Entities
{
    public class AccountEntity
    {
        private string accountID;
        private string accountEmail;
        private string accountPassword;
        private string accountType;
        private DateTime dateJoined;


        // Create record consrtuctor
        public AccountEntity(string accountEmail, string accountPassword, string accountType)
        {
            this.accountEmail = accountEmail;
            this.accountPassword = accountPassword;
            this.accountType = accountType;
        }

        // Retrieve record constructor
        public AccountEntity(string accountID, string accountEmail, string accountPassword, string accountType, DateTime dateJoined)
        {
            this.accountID = accountID;
            this.accountEmail = accountEmail;
            this.accountPassword = accountPassword;
            this.accountType = accountType;
            this.dateJoined = dateJoined;
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

        public string AccountEmail
        {
            get
            {
                return accountEmail;
            }

            set
            {
                accountEmail = value;
            }
        }

        public string AccountPassword
        {
            get
            {
                return accountPassword;
            }

            set
            {
                accountPassword = value;
            }
        }

        public string AccountType
        {
            get
            {
                return accountType;
            }

            set
            {
                accountType = value;
            }
        }

        public DateTime DateJoined
        {
            get
            {
                return dateJoined;
            }

            set
            {
                dateJoined = value;
            }
        }
    }
}
