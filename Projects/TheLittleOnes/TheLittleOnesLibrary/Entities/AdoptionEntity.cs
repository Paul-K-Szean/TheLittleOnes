using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLittleOnesLibrary.Entities
{
    public class AdoptInfoEntity
    {
        private string adoptInfoID;
        private ShopInfoEntity shopInfoEntity;
        private PetEntity petEntity;
        private string adoptInfoStatus;

        // Create record consrtuctor
        public AdoptInfoEntity(ShopInfoEntity shopInfoEntity, PetEntity petEntity, string adoptInfoStatus)
        {
            this.shopInfoEntity = shopInfoEntity;
            this.petEntity = petEntity;
            this.adoptInfoStatus = adoptInfoStatus;
        }
        // Retrieve/Update record consrtuctor
        public AdoptInfoEntity(ShopInfoEntity shopInfoEntity, PetEntity petEntity, string adoptInfoID, string adoptInfoStatus)
        {
            this.adoptInfoID = adoptInfoID;
            this.shopInfoEntity = shopInfoEntity;
            this.petEntity = petEntity;
            this.adoptInfoStatus = adoptInfoStatus;
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

        public PetEntity PetEntity
        {
            get
            {
                return petEntity;
            }

            set
            {
                petEntity = value;
            }
        }

        public string AdoptInfoStatus
        {
            get
            {
                return adoptInfoStatus;
            }

            set
            {
                adoptInfoStatus = value;
            }
        }

    }
}
