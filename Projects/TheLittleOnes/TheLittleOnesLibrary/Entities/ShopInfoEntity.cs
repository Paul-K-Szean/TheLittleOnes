using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLittleOnesLibrary.Entities
{
    public class ShopInfoEntity
    {
        private string shopInfoID;
        private string shopInfoName;
        private string shopInfoContact;
        private string shopInfoAddress;
        private bool shopInfoGrooming;
        private string shopInfoType;
        private string shopInfoDesc;
        private bool shopCloseOnPublicHoliday;
        private List<ShopTimeEntity> shopTimeEntities;
        private List<PhotoEntity> photoEntities;

        // Create record
        public ShopInfoEntity() { }

        public ShopInfoEntity(string shopInfoName, string shopInfoContact, string shopInfoAddress, bool shopInfoGrooming, string shopInfoType, string shopInfoDesc, bool shopCloseOnPublicHoliday, List<ShopTimeEntity> shopTimeEntities, List<PhotoEntity> photoEntities)
        {
            this.ShopInfoName = shopInfoName;
            this.ShopInfoContact = shopInfoContact;
            this.ShopInfoAddress = shopInfoAddress;
            this.ShopInfoGrooming = shopInfoGrooming;
            this.shopInfoType = shopInfoType;
            this.ShopInfoDesc = shopInfoDesc;
            this.ShopCloseOnPublicHoliday = shopCloseOnPublicHoliday;
            this.ShopTimeEntities = shopTimeEntities;
            this.PhotoEntities = photoEntities;
        }

        // Retrieve/Update record
        public ShopInfoEntity(string shopInfoID, string shopInfoName, string shopInfoContact, string shopInfoAddress, bool shopInfoGrooming, string shopInfoType, string shopInfoDesc, bool shopCloseOnPublicHoliday, List<ShopTimeEntity> shopTimeEntities, List<PhotoEntity> photoEntities)
        {
            this.ShopInfoID = shopInfoID;
            this.ShopInfoName = shopInfoName;
            this.ShopInfoContact = shopInfoContact;
            this.ShopInfoAddress = shopInfoAddress;
            this.ShopInfoGrooming = shopInfoGrooming;
            this.shopInfoType = shopInfoType;
            this.ShopInfoDesc = shopInfoDesc;
            this.ShopCloseOnPublicHoliday = shopCloseOnPublicHoliday;
            this.ShopTimeEntities = shopTimeEntities;
            this.PhotoEntities = photoEntities;
        }

        public string ShopInfoID
        {
            get
            {
                return shopInfoID;
            }

            set
            {
                shopInfoID = value;
            }
        }

        public string ShopInfoName
        {
            get
            {
                return shopInfoName;
            }

            set
            {
                shopInfoName = value;
            }
        }

        public string ShopInfoContact
        {
            get
            {
                return shopInfoContact;
            }

            set
            {
                shopInfoContact = value;
            }
        }

        public string ShopInfoAddress
        {
            get
            {
                return shopInfoAddress;
            }

            set
            {
                shopInfoAddress = value;
            }
        }

        public bool ShopInfoGrooming
        {
            get
            {
                return shopInfoGrooming;
            }

            set
            {
                shopInfoGrooming = value;
            }
        }
        public string ShopInfoType
        {
            get
            {
                return shopInfoType;
            }

            set
            {
                shopInfoType = value;
            }
        }

        public string ShopInfoDesc
        {
            get
            {
                return shopInfoDesc;
            }

            set
            {
                shopInfoDesc = value;
            }
        }

        public bool ShopCloseOnPublicHoliday
        {
            get
            {
                return shopCloseOnPublicHoliday;
            }

            set
            {
                shopCloseOnPublicHoliday = value;
            }
        }

        public List<ShopTimeEntity> ShopTimeEntities
        {
            get
            {
                return shopTimeEntities;
            }

            set
            {
                shopTimeEntities = value;
            }
        }

        public List<PhotoEntity> PhotoEntities
        {
            get
            {
                return photoEntities;
            }

            set
            {
                photoEntities = value;
            }
        }


    }
}
