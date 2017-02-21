using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLittleOnesLibrary.Entities
{
    public class ShopHourEntity
    {
        private string shopHourID;
        private string dayOfWeek;
        private string openTime;
        private string closeTime;

        // Create record
        public ShopHourEntity(string dayOfWeek, string openTime, string closeTime)
        {
            this.dayOfWeek = dayOfWeek;
            this.openTime = openTime;
            this.closeTime = closeTime;
        }
        // Retrieve/Update record
        public ShopHourEntity(string shopHourID, string dayOfWeek, string openTime, string closeTime)
        {
            this.shopHourID = shopHourID;
            this.dayOfWeek = dayOfWeek;
            this.openTime = openTime;
            this.closeTime = closeTime;
        }

        public string ShopHourID
        {
            get
            {
                return shopHourID;
            }

            set
            {
                shopHourID = value;
            }
        }

        public string DayOfWeek
        {
            get
            {
                return dayOfWeek;
            }

            set
            {
                dayOfWeek = value;
            }
        }

        public string OpenTime
        {
            get
            {
                return openTime;
            }

            set
            {
                openTime = value;
            }
        }

        public string CloseTime
        {
            get
            {
                return closeTime;
            }

            set
            {
                closeTime = value;
            }
        }
    }
}
