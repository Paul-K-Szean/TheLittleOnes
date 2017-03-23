using System;
namespace TheLittleOnesLibrary.Entities
{
    public class AppointmentEntity
    {
        private string appmtID;
        private AccountEntity accountEntity;
        private string appmtToID;
        private DateTime appmtDateAppmt;
        private DateTime appmtDateCreated;
        private string appmtStatus;
        private string appmtType;
        // Create record
        public AppointmentEntity(AccountEntity accountEntity, string appmtToID, DateTime appmtDateAppmt, DateTime appmtDateCreated, string appmtStatus, string appmtType)
        {
            this.accountEntity = accountEntity;
            this.appmtToID = appmtToID;
            this.appmtDateAppmt = appmtDateAppmt;
            this.appmtDateCreated = appmtDateCreated;
            this.appmtStatus = appmtStatus;
            this.appmtType = appmtType;
        }
        // Retrieve/update records
        public AppointmentEntity(string appmtID, AccountEntity accountEntity, string appmtToID, DateTime appmtDateAppmt, DateTime appmtDateCreated, string appmtStatus, string appmtType)
        {
            this.appmtID = appmtID;
            this.accountEntity = accountEntity;
            this.appmtToID = appmtToID;
            this.appmtDateAppmt = appmtDateAppmt;
            this.appmtDateCreated = appmtDateCreated;
            this.appmtStatus = appmtStatus;
            this.appmtType = appmtType;
        }
        public string AppmtID { get => appmtID; set => appmtID = value; }
        public AccountEntity AccountEntity { get => accountEntity; set => accountEntity = value; }
        public string AppmtToID { get => appmtToID; set => appmtToID = value; }
        public DateTime AppmtDateTime { get => appmtDateAppmt; set => appmtDateAppmt = value; }
        public DateTime AppmtDateCreated { get => appmtDateCreated; set => appmtDateCreated = value; }
        public string AppmtStatus { get => appmtStatus; set => appmtStatus = value; }
        public string AppmtType { get => appmtType; set => appmtType = value; }
    }
}
