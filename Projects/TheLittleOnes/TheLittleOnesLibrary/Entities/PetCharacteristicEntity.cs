namespace TheLittleOnesLibrary.Entities
{
    public class PetCharEntity
    {
        private string charID;
        private string charOverallAdaptability;
        private string charOverallFriendliness;
        private string charOverallGrooming;
        private string charOverallTrainability;
        private string charOverallExercise;
        // 5
        private string charAdaptToSurrounding;
        private string charAdaptToNovice;
        private string charAdaptToLoneliness;
        private string charAdaptToCold;
        private string charAdaptToHot;
        // 4
        private string charFriendWithFamily;
        private string charFriendWithKids;
        private string charFriendWithStranger;
        private string charFriendWithOtherPet;
        // 3
        private string charGroomLevel;
        private string charGroomSheddingLevel;
        private string charGroomDrooling;
        // 5
        private string charTrainLevel;
        private string charTrainIntelligenceLevel;
        private string charTrainMouthiness;
        private string charTrainPreyDrive;
        private string charTrainBarkHowl;
        // 3
        private string charExerciseEnergyLevel;
        private string charExerciseNeeds;
        private string charExercisePlayfullness;
        // Create pet characterstic
        public PetCharEntity(string charOverallAdaptability, string charOverallFriendliness, string charOverallGrooming, string charOverallTrainability, string charOverallExercise, string charAdaptToSurrounding, string charAdaptToNovice, string charAdaptToLoneliness, string charAdaptToCold, string charAdaptToHot, string charFriendWithFamily, string charFriendWithKids, string charFriendWithStranger, string charFriendWithOtherPet, string charGroomLevel, string charGroomSheddingLevel, string charGroomDrooling, string charTrainLevel, string charTrainIntelligenceLevel, string charTrainMouthiness, string charTrainPreyDrive, string charTrainBarkHowl, string charExerciseEnergyLevel, string charExerciseNeeds, string charExercisePlayfullness)
        {
            this.charOverallAdaptability = charOverallAdaptability;
            this.charOverallFriendliness = charOverallFriendliness;
            this.charOverallGrooming = charOverallGrooming;
            this.charOverallTrainability = charOverallTrainability;
            this.charOverallExercise = charOverallExercise;
            this.charAdaptToSurrounding = charAdaptToSurrounding;
            this.charAdaptToNovice = charAdaptToNovice;
            this.charAdaptToLoneliness = charAdaptToLoneliness;
            this.charAdaptToCold = charAdaptToCold;
            this.charAdaptToHot = charAdaptToHot;
            this.charFriendWithFamily = charFriendWithFamily;
            this.charFriendWithKids = charFriendWithKids;
            this.charFriendWithStranger = charFriendWithStranger;
            this.charFriendWithOtherPet = charFriendWithOtherPet;
            this.charGroomSheddingLevel = charGroomSheddingLevel;
            this.charGroomDrooling = charGroomDrooling;
            this.charGroomLevel = charGroomLevel;
            this.charTrainLevel = charTrainLevel;
            this.charTrainIntelligenceLevel = charTrainIntelligenceLevel;
            this.charTrainMouthiness = charTrainMouthiness;
            this.charTrainPreyDrive = charTrainPreyDrive;
            this.charTrainBarkHowl = charTrainBarkHowl;
            this.charExerciseEnergyLevel = charExerciseEnergyLevel;
            this.charExerciseNeeds = charExerciseNeeds;
            this.charExercisePlayfullness = charExercisePlayfullness;
        }
        // Retrive/Update pet characterstic
        public PetCharEntity(string charID, string charOverallAdaptability, string charOverallFriendliness, string charOverallGrooming, string charOverallTrainability, string charOverallExercise, string charAdaptToSurrounding, string charAdaptToNovice, string charAdaptToLoneliness, string charAdaptToCold, string charAdaptToHot, string charFriendWithFamily, string charFriendWithKids, string charFriendWithStranger, string charFriendWithOtherPet, string charGroomLevel, string charGroomSheddingLevel, string charGroomDrooling, string charTrainLevel, string charTrainIntelligenceLevel, string charTrainMouthiness, string charTrainPreyDrive, string charTrainBarkHowl, string charExerciseEnergyLevel, string charExerciseNeeds, string charExercisePlayfullness)
        {
            this.charID = charID;
            this.charOverallAdaptability = charOverallAdaptability;
            this.charOverallFriendliness = charOverallFriendliness;
            this.charOverallGrooming = charOverallGrooming;
            this.charOverallTrainability = charOverallTrainability;
            this.charOverallExercise = charOverallExercise;
            this.charAdaptToSurrounding = charAdaptToSurrounding;
            this.charAdaptToNovice = charAdaptToNovice;
            this.charAdaptToLoneliness = charAdaptToLoneliness;
            this.charAdaptToCold = charAdaptToCold;
            this.charAdaptToHot = charAdaptToHot;
            this.charFriendWithFamily = charFriendWithFamily;
            this.charFriendWithKids = charFriendWithKids;
            this.charFriendWithStranger = charFriendWithStranger;
            this.charFriendWithOtherPet = charFriendWithOtherPet;
            this.charGroomSheddingLevel = charGroomSheddingLevel;
            this.charGroomDrooling = charGroomDrooling;
            this.charGroomLevel = charGroomLevel;
            this.charTrainLevel = charTrainLevel;
            this.charTrainIntelligenceLevel = charTrainIntelligenceLevel;
            this.charTrainMouthiness = charTrainMouthiness;
            this.charTrainPreyDrive = charTrainPreyDrive;
            this.charTrainBarkHowl = charTrainBarkHowl;
            this.charExerciseEnergyLevel = charExerciseEnergyLevel;
            this.charExerciseNeeds = charExerciseNeeds;
            this.charExercisePlayfullness = charExercisePlayfullness;
        }
        public string CharID
        {
            get
            {
                return charID;
            }
            set
            {
                charID = value;
            }
        }
        public string CharOverallAdaptability
        {
            get
            {
                return charOverallAdaptability;
            }
            set
            {
                charOverallAdaptability = value;
            }
        }
        public string CharOverallFriendliness
        {
            get
            {
                return charOverallFriendliness;
            }
            set
            {
                charOverallFriendliness = value;
            }
        }
        public string CharOverallGrooming
        {
            get
            {
                return charOverallGrooming;
            }
            set
            {
                charOverallGrooming = value;
            }
        }
        public string CharOverallTrainability
        {
            get
            {
                return charOverallTrainability;
            }
            set
            {
                charOverallTrainability = value;
            }
        }
        public string CharOverallExercise
        {
            get
            {
                return charOverallExercise;
            }
            set
            {
                charOverallExercise = value;
            }
        }
        public string CharAdaptToSurrounding
        {
            get
            {
                return charAdaptToSurrounding;
            }
            set
            {
                charAdaptToSurrounding = value;
            }
        }
        public string CharAdaptToNovice
        {
            get
            {
                return charAdaptToNovice;
            }
            set
            {
                charAdaptToNovice = value;
            }
        }
        public string CharAdaptToLoneliness
        {
            get
            {
                return charAdaptToLoneliness;
            }
            set
            {
                charAdaptToLoneliness = value;
            }
        }
        public string CharAdaptToCold
        {
            get
            {
                return charAdaptToCold;
            }
            set
            {
                charAdaptToCold = value;
            }
        }
        public string CharAdaptToHot
        {
            get
            {
                return charAdaptToHot;
            }
            set
            {
                charAdaptToHot = value;
            }
        }
        public string CharFriendWithFamily
        {
            get
            {
                return charFriendWithFamily;
            }
            set
            {
                charFriendWithFamily = value;
            }
        }
        public string CharFriendWithKids
        {
            get
            {
                return charFriendWithKids;
            }
            set
            {
                charFriendWithKids = value;
            }
        }
        public string CharFriendWithStranger
        {
            get
            {
                return charFriendWithStranger;
            }
            set
            {
                charFriendWithStranger = value;
            }
        }
        public string CharFriendWithOtherPet
        {
            get
            {
                return charFriendWithOtherPet;
            }
            set
            {
                charFriendWithOtherPet = value;
            }
        }
        public string CharGroomSheddingLevel
        {
            get
            {
                return charGroomSheddingLevel;
            }
            set
            {
                charGroomSheddingLevel = value;
            }
        }
        public string CharGroomDrooling
        {
            get
            {
                return charGroomDrooling;
            }
            set
            {
                charGroomDrooling = value;
            }
        }
        public string CharGroomLevel
        {
            get
            {
                return charGroomLevel;
            }
            set
            {
                charGroomLevel = value;
            }
        }
        public string CharTrainLevel
        {
            get
            {
                return charTrainLevel;
            }
            set
            {
                charTrainLevel = value;
            }
        }
        public string CharTrainIntelligenceLevel
        {
            get
            {
                return charTrainIntelligenceLevel;
            }
            set
            {
                charTrainIntelligenceLevel = value;
            }
        }
        public string CharTrainMouthiness
        {
            get
            {
                return charTrainMouthiness;
            }
            set
            {
                charTrainMouthiness = value;
            }
        }
        public string CharTrainPreyDrive
        {
            get
            {
                return charTrainPreyDrive;
            }
            set
            {
                charTrainPreyDrive = value;
            }
        }
        public string CharTrainBarkHowl
        {
            get
            {
                return charTrainBarkHowl;
            }
            set
            {
                charTrainBarkHowl = value;
            }
        }
        public string CharExerciseEnergyLevel
        {
            get
            {
                return charExerciseEnergyLevel;
            }
            set
            {
                charExerciseEnergyLevel = value;
            }
        }
        public string CharExerciseNeeds
        {
            get
            {
                return charExerciseNeeds;
            }
            set
            {
                charExerciseNeeds = value;
            }
        }
        public string CharExercisePlayfullness
        {
            get
            {
                return charExercisePlayfullness;
            }
            set
            {
                charExercisePlayfullness = value;
            }
        }
    }
}