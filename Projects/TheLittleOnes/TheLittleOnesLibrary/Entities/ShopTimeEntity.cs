namespace TheLittleOnesLibrary.Entities
{
    public class ShopTimeEntity
    {
        private string shopTimeID;
        private string dayOfWeek;
        private string openTime;
        private string closeTime;
        // Create record
        public ShopTimeEntity(string dayOfWeek, string openTime, string closeTime)
        {
            this.dayOfWeek = dayOfWeek;
            this.openTime = openTime;
            this.closeTime = closeTime;
        }
        // Retrieve/Update record
        public ShopTimeEntity(string shopTimeID, string dayOfWeek, string openTime, string closeTime)
        {
            this.shopTimeID = shopTimeID;
            this.dayOfWeek = dayOfWeek;
            this.openTime = openTime;
            this.closeTime = closeTime;
        }
        public string ShopTimeID
        {
            get
            {
                return shopTimeID;
            }
            set
            {
                shopTimeID = value;
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
