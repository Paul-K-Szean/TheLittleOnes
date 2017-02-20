using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLittleOnesLibrary.Entities
{
    public class PetInfoEntity
    {
        private string petID;
        private string petCategory;
        private string petBreed;
        private string petLifeSpanMin;
        private string petHeightMin;
        private string petWeightMin;
        private string petLifeSpanMax;
        private string petHeightMax;
        private string petWeightMax;
        private string petDesc;
        private string petPersonality;
        private string petDisplayStatus;
        private PetCharEntity petCharEnt;
        private List<PhotoEntity> petPhoto;

        // Create record consrtuctor
        public PetInfoEntity(string petCategory, string petBreed, string petLifeSpanMin, string petHeightMin, string petWeightMin, string petLifeSpanMax, string petHeightMax, string petWeightMax, string petDesc, string petPersonality, string petDisplayStatus, PetCharEntity petCharEnt, List<PhotoEntity> petPhoto)
        {
            this.petCategory = petCategory;
            this.petBreed = petBreed;
            this.petLifeSpanMin = petLifeSpanMin;
            this.petHeightMin = petHeightMin;
            this.petWeightMin = petWeightMin;
            this.petLifeSpanMax = petLifeSpanMax;
            this.petHeightMax = petHeightMax;
            this.petWeightMax = petWeightMax;
            this.petDesc = petDesc;
            this.petPersonality = petPersonality;
            this.petDisplayStatus = petDisplayStatus;
            this.PetCharEntity = petCharEnt;
            this.PetPhoto = petPhoto;
        }

        // Retrieve record consrtuctor
        public PetInfoEntity(string petID, string petCategory, string petBreed, string petLifeSpanMin, string petHeightMin, string petWeightMin, string petLifeSpanMax, string petHeightMax, string petWeightMax, string petDesc, string petPersonality, string petDisplayStatus, PetCharEntity petCharEnt, List<PhotoEntity> petPhoto)
        {
            this.petID = petID;
            this.petCategory = petCategory;
            this.petBreed = petBreed;
            this.petLifeSpanMin = petLifeSpanMin;
            this.petHeightMin = petHeightMin;
            this.petWeightMin = petWeightMin;
            this.petLifeSpanMax = petLifeSpanMax;
            this.petHeightMax = petHeightMax;
            this.petWeightMax = petWeightMax;
            this.petDesc = petDesc;
            this.petPersonality = petPersonality;
            this.petDisplayStatus = petDisplayStatus;
            this.PetCharEntity = petCharEnt;
            this.PetPhoto = petPhoto;
        }

        // Update record consrtuctor
        public PetInfoEntity(string petID, string petCategory, string petBreed, string petLifeSpanMin, string petHeightMin, string petWeightMin, string petLifeSpanMax, string petHeightMax, string petWeightMax, string petDesc, string petPersonality, string petDisplayStatus)
        {
            this.petID = petID;
            this.petCategory = petCategory;
            this.petBreed = petBreed;
            this.petLifeSpanMin = petLifeSpanMin;
            this.petHeightMin = petHeightMin;
            this.petWeightMin = petWeightMin;
            this.petLifeSpanMax = petLifeSpanMax;
            this.petHeightMax = petHeightMax;
            this.petWeightMax = petWeightMax;
            this.petDesc = petDesc;
            this.petPersonality = petPersonality;
            this.petDisplayStatus = petDisplayStatus;
        }

        public string PetInfoID
        {
            get
            {
                return petID;
            }

            set
            {
                petID = value;
            }
        }

        public string PetCategory
        {
            get
            {
                return petCategory;
            }

            set
            {
                petCategory = value;
            }
        }

        public string PetBreed
        {
            get
            {
                return petBreed;
            }

            set
            {
                petBreed = value;
            }
        }

        public string PetLifeSpanMin
        {
            get
            {
                return petLifeSpanMin;
            }

            set
            {
                petLifeSpanMin = value;
            }
        }

        public string PetHeightMin
        {
            get
            {
                return petHeightMin;
            }

            set
            {
                petHeightMin = value;
            }
        }

        public string PetWeightMin
        {
            get
            {
                return petWeightMin;
            }

            set
            {
                petWeightMin = value;
            }
        }

        public string PetLifeSpanMax
        {
            get
            {
                return petLifeSpanMax;
            }

            set
            {
                petLifeSpanMax = value;
            }
        }

        public string PetHeightMax
        {
            get
            {
                return petHeightMax;
            }

            set
            {
                petHeightMax = value;
            }
        }

        public string PetWeightMax
        {
            get
            {
                return petWeightMax;
            }

            set
            {
                petWeightMax = value;
            }
        }

        public string PetDesc
        {
            get
            {
                return petDesc;
            }

            set
            {
                petDesc = value;
            }
        }

        public string PetPersonality
        {
            get
            {
                return petPersonality;
            }

            set
            {
                petPersonality = value;
            }
        }

        public string PetDisplayStatus
        {
            get
            {
                return petDisplayStatus;
            }

            set
            {
                petDisplayStatus = value;
            }
        }

        public PetCharEntity PetCharEntity
        {
            get
            {
                return petCharEnt;
            }

            set
            {
                petCharEnt = value;
            }
        }

        public List<PhotoEntity> PetPhoto
        {
            get
            {
                return petPhoto;
            }

            set
            {
                petPhoto = value;
            }
        }
    }
}
