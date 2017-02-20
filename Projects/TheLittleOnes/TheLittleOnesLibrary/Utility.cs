﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private static int randomNumber;
        private static string name;

        public static string getName()
        {
            randomNumber = new Random().Next(0, 30);
            return name = string.Concat("Thelittleones", randomNumber.ToString("00"));
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
        
        public static int getRandomNumber()
        {
            return new Random().Next(0, 100);
        }
        public static int getRandomNumber(int min, int max)
        {
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
                    return ((PetBreedCat)values.GetValue(new Random((int)DateTime.Now.Ticks).Next(0, values.Length))).ToString();
                case "dog":
                    values = Enum.GetValues(typeof(PetBreedDog));
                    return ((PetBreedDog)values.GetValue(new Random((int)DateTime.Now.Ticks).Next(0, values.Length))).ToString();
                default:
                    return "Breed";
            }

        }

    }
}
