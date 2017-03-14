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
        private string adoptRequesterID;
        private string adoptInfoID;
        private DateTime adoptReqDateAppmt;
        private DateTime adoptReqDateCreated;
        // Create record
        public AdoptRequestEntity(string adoptRequesterID, string adoptInfoID, DateTime adoptReqDateAppmt, DateTime adoptReqDateCreated)
        {
            this.AdoptRequesterID = adoptRequesterID;
            this.AdoptInfoID = adoptInfoID;
            this.adoptReqDateAppmt = adoptReqDateAppmt;
            this.AdoptReqDateCreated = adoptReqDateCreated;
        }
        // Retrieve/update records
        public AdoptRequestEntity(string adoptReqID, string adoptRequesterID, string adoptInfoID, DateTime adoptReqDateAppmt, DateTime adoptReqDateCreated)
        {
            this.AdoptReqID = adoptReqID;
            this.AdoptRequesterID = adoptRequesterID;
            this.AdoptInfoID = adoptInfoID;
            this.adoptReqDateAppmt = adoptReqDateAppmt;
            this.AdoptReqDateCreated = adoptReqDateCreated;
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

        public string AdoptRequesterID
        {
            get
            {
                return adoptRequesterID;
            }

            set
            {
                adoptRequesterID = value;
            }
        }

        public string AdoptInfoID
        {
            get
            {
                return adoptInfoID;
            }

            set
            {
                adoptInfoID = value;
            }
        }

        public DateTime AdoptReqDateAppmt
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
    }
}
