using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TheLittleOnesLibrary.Entities
{
    public class AdoptRequestEntity
    {
        private string adoptReqID;
        private AccountEntity accountEntity;
        private AdoptInfoEntity adoptInfoEntity;
        private DateTime adoptReqDateAppmt;
        private DateTime adoptReqDateCreated;
        private string adoptReqStatus;
        // Create record
        public AdoptRequestEntity(AccountEntity accountEntity, AdoptInfoEntity adoptInfoEntity, DateTime adoptReqDateAppmt, DateTime adoptReqDateCreated, string adoptReqStatus)
        {
            this.accountEntity = accountEntity;
            this.adoptInfoEntity = adoptInfoEntity;
            this.adoptReqDateAppmt = adoptReqDateAppmt;
            this.AdoptReqDateCreated = adoptReqDateCreated;
            this.AdoptReqStatus = adoptReqStatus;
        }
        // Retrieve/update records
        public AdoptRequestEntity(string adoptReqID, AccountEntity accountEntity, AdoptInfoEntity adoptInfoEntity, DateTime adoptReqDateAppmt, DateTime adoptReqDateCreated, string adoptReqStatus)
        {
            this.AdoptReqID = adoptReqID;
            this.accountEntity = accountEntity;
            this.adoptInfoEntity = adoptInfoEntity;
            this.adoptReqDateAppmt = adoptReqDateAppmt;
            this.AdoptReqDateCreated = adoptReqDateCreated;
            this.AdoptReqStatus = adoptReqStatus;
        }
        public string AdoptReqID
        {
            get
            {
                return adoptReqID;
            }
            set
            {
                adoptReqID = value;
            }
        }
        public AccountEntity AccountEntity
        {
            get
            {
                return accountEntity;
            }
            set
            {
                accountEntity = value;
            }
        }
        public AdoptInfoEntity AdoptInfoEntity
        {
            get
            {
                return adoptInfoEntity;
            }
            set
            {
                adoptInfoEntity = value;
            }
        }
        public DateTime AdoptReqDateTime
        {
            get
            {
                return adoptReqDateAppmt;
            }
            set
            {
                adoptReqDateAppmt = value;
            }
        }
        public DateTime AdoptReqDateCreated
        {
            get
            {
                return adoptReqDateCreated;
            }
            set
            {
                adoptReqDateCreated = value;
            }
        }
        public string AdoptReqStatus
        {
            get
            {
                return adoptReqStatus;
            }
            set
            {
                adoptReqStatus = value;
            }
        }
    }
}
