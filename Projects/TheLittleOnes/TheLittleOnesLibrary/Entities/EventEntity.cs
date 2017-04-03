using System;

namespace TheLittleOnesLibrary.Entities
{
    public class EventEntity
    {
        private string eventID;
        private AccountEntity accountEntity;
        private string eventTitle;
        private string eventDesc;
        private string eventLocation;
        private string eventType;
        private DateTime eventDateTime;
        private DateTime eventCreatedDate;
        private string eventStatus;
        // Create records
        public EventEntity(AccountEntity accountEntity, string eventTitle, string eventDesc, string eventLocation, string eventType, DateTime eventDateTime, DateTime eventCreatedDate, string eventStatus)
        {
            this.accountEntity = accountEntity;
            this.eventTitle = eventTitle;
            this.eventDesc = eventDesc;
            this.eventLocation = eventLocation;
            this.eventType = eventType;
            this.eventDateTime = eventDateTime;
            this.eventCreatedDate = eventCreatedDate;
            this.eventStatus = eventStatus;
        }
        // Retrieve/Update records
        public EventEntity(string eventID, AccountEntity accountEntity, string eventTitle, string eventDesc, string eventLocation, string eventType, DateTime eventDateTime, DateTime eventCreatedDate, string eventStatus)
        {
            this.eventID = eventID;
            this.accountEntity = accountEntity;
            this.eventTitle = eventTitle;
            this.eventDesc = eventDesc;
            this.eventLocation = eventLocation;
            this.eventType = eventType;
            this.eventDateTime = eventDateTime;
            this.eventCreatedDate = eventCreatedDate;
            this.eventStatus = eventStatus;
        }
        public string EventID { get => eventID; set => eventID = value; }
        public AccountEntity AccountEntity { get => accountEntity; set => accountEntity = value; }
        public string EventTitle { get => eventTitle; set => eventTitle = value; }
        public string EventDesc { get => eventDesc; set => eventDesc = value; }
        public string EventLocation { get => eventLocation; set => eventLocation = value; }
        public string EventType { get => eventType; set => eventType = value; }
        public DateTime EventDateTime { get => eventDateTime; set => eventDateTime = value; }
        public DateTime EventCreatedDate { get => eventCreatedDate; set => eventCreatedDate = value; }
        public string EventStatus { get => eventStatus; set => eventStatus = value; }
    }
}
