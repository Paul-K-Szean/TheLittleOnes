using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TheLittleOnesLibrary.Entities
{
    public class PetEntity
    {
        private string petID;
        private string petBreed;
        private string petName;
        private string petGender;
        private string petWeight;
        private string petSize;
        private string petDesc;
        private string petEnergy;
        private string petFriendlyWithpet;
        private string petFriendlyWithPeople;
        private string petToiletTrained;
        private string petHealthInfo;
        private List<PhotoEntity> photoEntities;
        // Create record
        public PetEntity(string petID, string petBreed, string petName, string petGender, string petWeight, string petSize, string petDesc, string petEnergy, string petFriendlyWithpet, string petFriendlyWithPeople, string petToiletTrained, string petHealthInfo, List<PhotoEntity> photoEntities)
        {
            this.petID = petID;
            this.petBreed = petBreed;
            this.petName = petName;
            this.petGender = petGender;
            this.petWeight = petWeight;
            this.petSize = petSize;
            this.petDesc = petDesc;
            this.petEnergy = petEnergy;
            this.petFriendlyWithpet = petFriendlyWithpet;
            this.petFriendlyWithPeople = petFriendlyWithPeople;
            this.petToiletTrained = petToiletTrained;
            this.petHealthInfo = petHealthInfo;
            this.photoEntities = photoEntities;
        }
        // Retrieve/Update record
        public PetEntity(string petBreed, string petName, string petGender, string petWeight, string petSize, string petDesc, string petEnergy, string petFriendlyWithpet, string petFriendlyWithPeople, string petToiletTrained, string petHealthInfo, List<PhotoEntity> photoEntities)
        {
            this.petBreed = petBreed;
            this.petName = petName;
            this.petGender = petGender;
            this.petWeight = petWeight;
            this.petSize = petSize;
            this.petDesc = petDesc;
            this.petEnergy = petEnergy;
            this.petFriendlyWithpet = petFriendlyWithpet;
            this.petFriendlyWithPeople = petFriendlyWithPeople;
            this.petToiletTrained = petToiletTrained;
            this.petHealthInfo = petHealthInfo;
            this.photoEntities = photoEntities;
        }
        public string PetID
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
        public string PetName
        {
            get
            {
                return petName;
            }
            set
            {
                petName = value;
            }
        }
        public string PetGender
        {
            get
            {
                return petGender;
            }
            set
            {
                petGender = value;
            }
        }
        public string PetWeight
        {
            get
            {
                return petWeight;
            }
            set
            {
                petWeight = value;
            }
        }
        public string PetSize
        {
            get
            {
                return petSize;
            }
            set
            {
                petSize = value;
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
        public string PetEnergy
        {
            get
            {
                return petEnergy;
            }
            set
            {
                petEnergy = value;
            }
        }
        public string PetFriendlyWithPet
        {
            get
            {
                return petFriendlyWithpet;
            }
            set
            {
                petFriendlyWithpet = value;
            }
        }
        public string PetFriendlyWithPeople
        {
            get
            {
                return petFriendlyWithPeople;
            }
            set
            {
                petFriendlyWithPeople = value;
            }
        }
        public string PetToiletTrained
        {
            get
            {
                return petToiletTrained;
            }
            set
            {
                petToiletTrained = value;
            }
        }
        public string PetHealthInfo
        {
            get
            {
                return petHealthInfo;
            }
            set
            {
                petHealthInfo = value;
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
