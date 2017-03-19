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
        private ProfileEntity profileEntity;
        private ShopInfoEntity shopInfoEntity;
        private DateTime dateJoined;
        // Create record consrtuctor
        public AccountEntity(string accountEmail, string accountPassword, string accountType, ProfileEntity profileEntity, ShopInfoEntity shopInfoEntity)
        {
            this.accountEmail = accountEmail;
            this.accountPassword = accountPassword;
            this.accountType = accountType;
            this.ProfileEntity = profileEntity;
            this.ShopInfoEntity = shopInfoEntity;
        }
        // Retrieve/Update record constructor
        public AccountEntity(string accountID, string accountEmail, string accountPassword, string accountType, ProfileEntity profileEntity, ShopInfoEntity shopInfoEntity, DateTime dateJoined)
        {
            this.accountID = accountID;
            this.accountEmail = accountEmail;
            this.accountPassword = accountPassword;
            this.accountType = accountType;
            this.ProfileEntity = profileEntity;
            this.ShopInfoEntity = shopInfoEntity;
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
         public ProfileEntity ProfileEntity
        {
            get
            {
                return profileEntity;
            }
            set
            {
                profileEntity = value;
            }
        }
        public ShopInfoEntity ShopInfoEntity
        {
            get
            {
                return shopInfoEntity;
            }
            set
            {
                shopInfoEntity = value;
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
