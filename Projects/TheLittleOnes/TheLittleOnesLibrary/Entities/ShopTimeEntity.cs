namespace TheLittleOnesLibrary.Entities
{
    public class ShopTimeEntity
    {
        private string shopTimeID;
        private string shopDayOfWeek;
        private string shopOpenTime;
        private string shopCloseTime;
        // Create record
        public ShopTimeEntity(string shopDayOfWeek, string shopOpenTime, string shopCloseTime)
        {
            this.shopDayOfWeek = shopDayOfWeek;
            this.shopOpenTime = shopOpenTime;
            this.shopCloseTime = shopCloseTime;
        }
        // Retrieve/Update record
        public ShopTimeEntity(string shopTimeID, string shopDayOfWeek, string shopOpenTime, string shopCloseTime)
        {
            this.shopTimeID = shopTimeID;
            this.shopDayOfWeek = shopDayOfWeek;
            this.shopOpenTime = shopOpenTime;
            this.shopCloseTime = shopCloseTime;
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
        public string ShopDayOfWeek
        {
            get
            {
                return shopDayOfWeek;
            }
            set
            {
                shopDayOfWeek = value;
            }
        }
        public string ShopOpenTime
        {
            get
            {
                return shopOpenTime;
            }
            set
            {
                shopOpenTime = value;
            }
        }
        public string ShopCloseTime
        {
            get
            {
                return shopCloseTime;
            }
            set
            {
                shopCloseTime = value;
            }
        }
    }
}