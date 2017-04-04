using System;
namespace TheLittleOnesLibrary.Entities
{
    public class EventInfoEntity
    {
        private string eventID;
        private AccountEntity accountEntity;
        private string eventTitle;
        private string eventDesc;
        private string eventLocation;
        private string eventType;
        private DateTime eventDateTime;
        private DateTime eventDateCreated;
        private string eventStatus;
        // Create records
        public EventInfoEntity(AccountEntity accountEntity, string eventTitle, string eventDesc, string eventLocation, string eventType, DateTime eventDateTime, string eventStatus)
        {
            this.accountEntity = accountEntity;
            this.eventTitle = eventTitle;
            this.eventDesc = eventDesc;
            this.eventLocation = eventLocation;
            this.eventType = eventType;
            this.eventDateTime = eventDateTime;
            this.eventStatus = eventStatus;
        }
        // Retrieve/Update records
        public EventInfoEntity(string eventID, AccountEntity accountEntity, string eventTitle, string eventDesc, string eventLocation, string eventType, DateTime eventDateTime, DateTime eventDateCreated, string eventStatus)
        {
            this.eventID = eventID;
            this.accountEntity = accountEntity;
            this.eventTitle = eventTitle;
            this.eventDesc = eventDesc;
            this.eventLocation = eventLocation;
            this.eventType = eventType;
            this.eventDateTime = eventDateTime;
            this.eventDateCreated = eventDateCreated;
            this.eventStatus = eventStatus;
        }
        public string EventID { get => eventID; set => eventID = value; }
        public AccountEntity AccountEntity { get => accountEntity; set => accountEntity = value; }
        public string EventTitle { get => eventTitle; set => eventTitle = value; }
        public string EventDesc { get => eventDesc; set => eventDesc = value; }
        public string EventLocation { get => eventLocation; set => eventLocation = value; }
        public string EventType { get => eventType; set => eventType = value; }
        public DateTime EventDateTime { get => eventDateTime; set => eventDateTime = value; }
        public DateTime EventDateCreated { get => eventDateCreated; set => eventDateCreated = value; }
        public string EventStatus { get => eventStatus; set => eventStatus = value; }
    }
}
