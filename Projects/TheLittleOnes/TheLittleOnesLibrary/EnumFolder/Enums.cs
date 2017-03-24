using System;
using System.ComponentModel;
using System.Reflection;
namespace TheLittleOnesLibrary.EnumFolder
{
    public class Enums
    {
        public static string GetDescription(Enum value)
        {
            // Get the Description attribute value for the enum value
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }
    public enum AccountType
    {
        [Description("Web Admin")]
        WebAdmin,
        [Description("Web Sponsor Group")]
        WebSponsorGroup,
        [Description("Web Shelter Group")]
        WebShelterGroup,
        [Description("Web User")]
        WebUser
    }
    public enum AppointmentType
    {
        [Description("Adoption")]
        Adoption,
        [Description("Clinic")]
        Clinic,
    }
    public enum ShopType
    {
        [Description("Pet Shop")]
        PetShop,
        [Description("Pet Clinic")]
        PetClinic,
        [Description("Pet Shelter")]
        PetShelter
    }
    public enum SystemStatus
    {
        Adopted,
        Available,
        Cancelled,
        Confirmed,
        Pending,
        Completed
    }
    public enum PhotoPurpose
    {
        Pet,
        [Description("Pet Info")]
        PetInfo,
        [Description("Shop Info")]
        ShopInfo,
        [Description("Profile Info")]
        ProfileInfo
    }
    public enum PetCategory
    {
        Dog, Cat
    }
    public enum PetBreedDog
    {
        // 
        [Description("Corgi")]
        Corgi,
        // G
        [Description("German Shepherd")]
        GermanShepherd,
        [Description("Golden Retriever")]
        GoldenRetriever,
        // L
        [Description("Labrador Retriever")]
        LabradorRetriever,
        // M 
        [Description("Maltese")]
        Maltese,
        // P
        [Description("Pomeranian")]
        Pomeranian,
        [Description("Poodle")]
        Poodle,
        // S
        [Description("Schnauzer")]
        Schnauzer,
        [Description("Shih Tzu")]
        ShihTzu,
        [Description("Siberian Husky")]
        SiberianHusky,
        [Description("Silky Terrier")]
        SilkyTerrier,
        // W
        [Description("West Highland White Terrier")]
        WestHighlandWhiteTerrier,
        // Y
        [Description("Yorkshire Terrier")]
        YorkshireTerrier,
    }
    public enum PetBreedCat
    {
        // A 
        [Description("American Shorthair")]
        AmericanShorthair,
        [Description("Abyssinian Cat")]
        AbyssinianCat,
        // B
        [Description("British Shorthair")]
        BritishShorthair,
        [Description("Burmese Cat")]
        BurmeseCat,
        // L
        [Description("Egyptian Mau")]
        EgyptianMau,
        // S
        [Description("Siamese Cat")]
        SiameseCat,
        [Description("Siberian Cat")]
        SiberianCat,
        // P
        [Description("Persian Cat")]
        PersianCat,
    }
}