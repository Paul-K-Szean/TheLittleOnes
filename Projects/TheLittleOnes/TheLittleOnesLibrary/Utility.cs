using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.EnumFolder;
namespace TheLittleOnesLibrary
{
    public class Utility
    {
        // Message color
        public static Color getHexColor(string hexColor)
        {
            return ColorTranslator.FromHtml(hexColor);
        }
        public static Color getDefaultColor() { return ColorTranslator.FromHtml("#666"); }
        public static Color getErrorColor() { return ColorTranslator.FromHtml("#E72C3E"); }
        public static Color getErrorColorLight() { return ColorTranslator.FromHtml("#F08080"); }
        public static Color getSuccessColor() { return ColorTranslator.FromHtml("#2ECC71"); }
        public static Color getWarningColor() { return Color.Orange; }
        public static Color getColorLightBlue()
        {
            return ColorTranslator.FromHtml("#93b6f2");
        }
        public static Color getColorWhite()
        {
            return ColorTranslator.FromHtml("#FFFFFF");
        }
        public static Color getColorLightGray()
        {
            return ColorTranslator.FromHtml("#EFF3F8");
        }
        private static int randomNumber;
        private static string name;
        public static string getName()
        {
            randomNumber = new Random().Next(0, 30);
            return name = string.Concat("webuser", randomNumber.ToString("00"));
        }
        public static string getEmail() { return string.Concat(name, "@hotmail.com"); }
        public static string getPassword() { return string.Concat(name); }
        public static string getUIDateFormat()
        {
            return "ddd, dd MMM yyyy";
        }
        public static string getUIDateTimeFormat()
        {
            return "dd MM yyyy HH:mm:ss";
        }
        public static List<ShopInfoEntity> shopInfoPetClinic;
        public static List<ShopInfoEntity> shopInfoPetShop;
        public static void initialiseShopInfo()
        {
            string shopName;
            string shopContact;
            string shopAddress;
            string shopDesc;
            // Pet Clinic
            if (shopInfoPetClinic == null)
            {
                shopInfoPetClinic = new List<ShopInfoEntity>();
                shopName = "The Joyous Vet";
                shopContact = "6769 0304";
                shopAddress = "475, Choa Chu Kang Avenue 3, #01-30A, Sunshine Place, Singapore 680475";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetClinic.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, false, ShopType.PetClinic.ToString(), shopDesc, true, null, null));
                shopName = "Island Vet";
                shopContact = "6560 5991";
                shopAddress = "114, Jurong East Street 13, #01-404, Singapore 600114";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetClinic.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, false, ShopType.PetClinic.ToString(), shopDesc, true, null, null));
                shopName = "My Family Vet";
                shopContact = "6566 0448";
                shopAddress = "265, Bukit Batok East Avenue 4, #01-403, Singapore 650265";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetClinic.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, false, ShopType.PetClinic.ToString(), shopDesc, true, null, null));
                shopName = "Advanced VetCare Veterinary Centre";
                shopContact = "6636 1788";
                shopAddress = "18 Jalan Pari Burong, Picardy Gardens, Shophouses along Upper Changi Road, Singapore 488684";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetClinic.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, false, ShopType.PetClinic.ToString(), shopDesc, true, null, null));
                shopName = "Singapore Veterinary Animal Clinic";
                shopContact = "6365 0308";
                shopAddress = "768, Woodlands Ave 6, #01-11, Mart, Singapore 730768";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetClinic.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, false, ShopType.PetClinic.ToString(), shopDesc, true, null, null));
                shopName = "Passion Veterinary Clinic Pte Ltd";
                shopContact = "6635 8725";
                shopAddress = "111, Woodlands Street 13, #01-86, Singapore 730111";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetClinic.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, false, ShopType.PetClinic.ToString(), shopDesc, true, null, null));
            }
            // Pet Shop
            if (shopInfoPetShop == null)
            {
                shopInfoPetShop = new List<ShopInfoEntity>();
                shopName = "Pet Lovers Centre";
                shopContact = "6366 1751";
                shopAddress = "30, Woodlands Avenue 1, #01-16/17, The Woodgrove, Singapore 739065";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre";
                shopContact = "6362 0975";
                shopAddress = "888, Woodlands Drive 50, #01-735, Singapore 730888";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre - Sembawang Shopping Center";
                shopContact = "6483 0837";
                shopAddress = "604 Sembawang Rd, #B1-07, S.C, Singapore 758459";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre";
                shopContact = "6755 1655";
                shopAddress = "930, Yishun Ave 2, #B1-60/61, Northpoint Shopping Centre, Singapore 769098";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre";
                shopContact = "6743 9260";
                shopAddress = "11, Yishun Industrial Street, #07-91, North Spring Bizhub, Singapore 768089";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre";
                shopContact = "6766 0273";
                shopAddress = "1, Woodlands Road, #01-01, Junction 10, Singapore 677899";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre";
                shopContact = "6764 9927";
                shopAddress = "21, Choa Chu Kang Ave 4, #B1-19, Lot One, Singapore 689812";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre";
                shopContact = "6710 7217";
                shopAddress = "4, Hillview Rise, #02-16, HillV2, Singapore 667979";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre";
                shopContact = "6459 5152";
                shopAddress = "8, Jln Leban, Singapore 577550";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre";
                shopContact = "6457 7095";
                shopAddress = "51, Ang Mo Kio Ave 3, #01-02, Big Mac Centre, Singapore 569922";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre";
                shopContact = "6452 6032";
                shopAddress = "301, Upper Thomson Road,#01-75, Thomson Plaza, Singapore 574408";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre - - Beauty World";
                shopContact = "6467 2019";
                shopAddress = "10, Chun Tin Rd, Singapore 599597";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre - The Seletar Mall";
                shopContact = "6341 6124";
                shopAddress = "33, Sengkang West Ave, Raffles Medical, Singapore 797653";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre - IMM";
                shopContact = "6567 1519";
                shopAddress = "2, Jurong East Street 21, #02-31, IMM, Singapore 609601";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre - The Grandstand";
                shopContact = "6467 3063";
                shopAddress = "200, Turf Club Road, #01-15B, Singapore 287994";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre - Nex";
                shopContact = "6634 2210";
                shopAddress = "Serangoon Central, Singapore 556083";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre - Serangoon Gardens";
                shopContact = "6288 4287";
                shopAddress = "10, Kensington Park Rd, Singapore 557262";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre - Junction 8";
                shopContact = "6352 8469";
                shopAddress = "9, Bishan Place, #B1-14, Junction 8, Singapore 579837";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre";
                shopContact = "6684 5208";
                shopAddress = "Jurong East Central 1, #B1-17, JCube, JCube, Singapore 609731";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre - JEM";
                shopContact = "6694 4739";
                shopAddress = "50, Jurong Gateway Road, #B1-24/25, Jem, Singapore 608549";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre - Boon Lay Shopping Center";
                shopContact = "6694 4739";
                shopAddress = "221, Boon Lay Place, #02-164, Boon Lay Shopping Centre, Singapore 640221";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Lovers Centre - Royalville";
                shopContact = "6469 0389";
                shopAddress = "833, Bukit Timah Rd, #01-01, Royalville, Singapore 279887";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pet Essentials";
                shopContact = "6763 3749";
                shopAddress = "524A, Jelapang Rd, #03-09B Greenridge Shopping Centre Singapore 671524";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pets' Station";
                shopContact = "6763 3749";
                shopAddress = "1, Woodlands Square, #B1-42/43, Causeway Point, Singapore 738099";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pets' Station Holding Pte Ltd";
                shopContact = "6363 1121";
                shopAddress = "21, Marsiling Ind Estate Road 9, Singapore 739175";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pets' Station";
                shopContact = "6288 0973";
                shopAddress = "16, Maju Ave, Singapore 556692";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pets' Station";
                shopContact = "6280 9537";
                shopAddress = "153, Serangoon North Avenue 1, #01-508, Serangoon North Village, Singapore 550153";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pets' Station";
                shopContact = "6791 9989";
                shopAddress = "1, Jurong West Central 2, #01-17B, Jurong Point Shopping Centre, Jurong Point, Singapore 648886";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pets' Station";
                shopContact = "6260 8280";
                shopAddress = "10, Tampines Central 1, Singapore 529536";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pets' Station";
                shopContact = "6448 0633";
                shopAddress = " 99, Frankel Ave, Singapore 458223";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pets' Station";
                shopContact = "6280 9537";
                shopAddress = "153, Serangoon North Avenue 1, #01-508, Serangoon North Village, Singapore 550153";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
                shopName = "Pets' Station";
                shopContact = "6280 9537";
                shopAddress = "153, Serangoon North Avenue 1, #01-508, Serangoon North Village, Singapore 550153";
                shopDesc = shopName + " are located at " + shopAddress + "! Contact them at " + shopContact;
                shopInfoPetShop.Add(new ShopInfoEntity(shopName, shopContact, shopAddress, (getRandomNumber(0, 2) == 0) ? true : false, ShopType.PetShop.ToString(), shopDesc, true, null, null));
            }
        }
        public static ShopInfoEntity getShopInfoEntity()
        {
            initialiseShopInfo();
            Thread.Sleep(8);
            LogController.LogLine(shopInfoPetClinic.Count + " " + shopInfoPetShop.Count);
            int index = 0;
            switch (new Random().Next(0, 2) == 0 ? ShopType.PetClinic.ToString() : ShopType.PetShop.ToString())
            {
                case "PetShop":
                    Thread.Sleep(10);
                    return shopInfoPetShop[new Random().Next(shopInfoPetShop.Count)];
                case "PetClinic":
                    Thread.Sleep(6);
                    return shopInfoPetClinic[new Random().Next(shopInfoPetClinic.Count)];
                default:
                    Thread.Sleep(10);
                    return new Random().Next(0, 1) == 0 ? shopInfoPetShop[new Random().Next(shopInfoPetShop.Count - 1)] : shopInfoPetClinic[new Random().Next(shopInfoPetShop.Count - 1)];
            }
        }
        public static int getRandomNumber()
        {
            return new Random().Next(0, 100);
        }
        public static int getRandomNumber(int min, int max)
        {
            Thread.Sleep(10);
            // inclusive or min and max
            return new Random().Next(min, max);
        }
        public static double getRandomDouble(double min, double max)
        {
            return min + new Random().NextDouble() * (max - min);
        }
        public static string getRandomBreed(string petCategory)
        {
            Array values;
            switch (petCategory.ToLower().Trim())
            {
                case "cat":
                    values = Enum.GetValues(typeof(PetBreedCat));
                    return Enums.GetDescription((PetBreedCat)values.GetValue(new Random((int)DateTime.Now.Ticks).Next(0, values.Length)));
                case "dog":
                    values = Enum.GetValues(typeof(PetBreedDog));
                    return Enums.GetDescription((PetBreedDog)values.GetValue(new Random((int)DateTime.Now.Ticks).Next(0, values.Length)));
                default:
                    return "Breed";
            }
        }
        /// <summary>
        /// Source from https://forums.asp.net/t/2000851.aspx?24+hours+time+format
        /// User: a2h
        /// Purpose: preload time interval and bind to drop down list
        /// </summary>
        public static List<string> getTimeInterval()
        {
            // defualt start time value
            DateTime start = DateTime.ParseExact("00:00", "HH:mm", null);
            // default end time value
            DateTime end = DateTime.ParseExact("23:59", "HH:mm", null);
            //set the interval time 
            int interval = 30;
            //list to hold the values of intervals
            List<string> listTimeIntervals = new List<string>();
            //populate the list with the interval values
            for (DateTime i = start; i <= end; i = i.AddMinutes(interval))
                listTimeIntervals.Add(i.ToString("HH:mm tt"));
            return listTimeIntervals;
            ////Assign the list to datasource of dropdownlist
            //DropDownList1.DataSource = listTimeIntervals;
            ////Databind the dropdownlist
            //DropDownList1.DataBind();
        }
        public static List<string> getTimeInterval(string startTime, string endTime)
        {
            // defualt start time value
            DateTime start = DateTime.ParseExact(DateTime.Parse(startTime).ToString("HH:mm"), "HH:mm", null);
            // default end time value
            DateTime end = DateTime.ParseExact(DateTime.Parse(endTime).AddHours(-1).ToString("HH:mm"), "HH:mm", null);
            //set the interval time 
            int interval = 30;
            //list to hold the values of intervals
            List<string> listTimeIntervals = new List<string>();
            //populate the list with the interval values
            for (DateTime i = start; i <= end; i = i.AddMinutes(interval))
            {
                // TODO remove user select timing for lunch
                if (i.ToString("HH:mm").Contains("11:00") ||
                    i.ToString("HH:mm").Contains("11:30") ||
                    i.ToString("HH:mm").Contains("12:00") ||
                    i.ToString("HH:mm").Contains("12:30"))
                {
                }
                else
                {
                    listTimeIntervals.Add(i.ToString("HH:mm tt"));
                }
            }
            return listTimeIntervals;
        }
    }
    public static class SubstringExtensions
    {
        /// <summary>
        /// Get string value between [first] a and [last] b.
        /// </summary>
        public static string Between(this string value, string a, string b)
        {
            int posA = value.IndexOf(a);
            int posB = value.LastIndexOf(b);
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }
        /// <summary>
        /// Get string value after [first] a.
        /// </summary>
        public static string Before(this string value, string a)
        {
            int posA = value.IndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            return value.Substring(0, posA);
        }
        /// <summary>
        /// Get string value after [last] a.
        /// </summary>
        public static string After(this string value, string a)
        {
            int posA = value.LastIndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= value.Length)
            {
                return "";
            }
            return value.Substring(adjustedPosA);
        }
    }
}
