using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace TheLittleOnesLibrary.EnumFolder
{
    public class Enums
    {

        public static void EnumToDDL(Type EnumType, DropDownList TheListBox)
        {
            // Array listValues = Enum.GetValues(EnumType);
            // Array.Sort(listValues);
            List<string> listNames = Enum.GetNames(EnumType).ToList();
            listNames.Sort();
            TheListBox.DataSource = listNames;
            TheListBox.DataBind();
        }


    }

    public enum AccountType
    {
        WebUser,
        WebAdmin,
        WebSponsorGroup,
        WebShelterGroup
    }

    public enum ShopType
    {
        PetShop,
        PetClinic
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
        [Description("Pomeraiian")]
        Pomeraiian,
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
