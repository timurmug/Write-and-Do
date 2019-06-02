using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace WandD_nodate.ViewModels
{
    public class Addition
    {
        public static string ChooseImage()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 3);
            switch (randomNumber)
            {
                case 0: return "leftperson.png";
                case 1: return "centerperson.png";
                case 2: return "rightperson.png";
                default: return null;
            } 
        }

        //конвертировать в зачеркнутый текст
        public static string ConvertToStrikethrough(string stringToChange)
        {
            var newString = "";
            foreach (var character in stringToChange)
            {
                newString += $"{character}\u0336";
            }
            return newString;
        }

        //конвертировать в обычный текст
        public static string ConvertFromStrikethrough(string stringToChange)
        {
            //return Regex.Replace(stringToChange, @"[^\u0000-\u007F]+", stringToChange /*String.Empty*/);
            return Regex.Replace(stringToChange, @"\u0336", /*stringToChange*/ String.Empty);
        }

        //является ли текст зачеркнутым?
        public static bool IsStrikethrough(string stringtoCheck)
        {
            //string str = Regex.Replace(stringtoCheck, @"[^\u0000-\u007F]+", /*stringtoCheck*/ String.Empty);
            string str = Regex.Replace(stringtoCheck, @"\u0336", /*stringtoCheck*/ String.Empty);
            if (str == stringtoCheck)
                return false;
            else
                return true;
        }

        //конвертер hex to color
        public static String HexConverter(System.Drawing.Color c)
        {
            String rtn = String.Empty;
            try
            {
                rtn = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
            }
            catch (Exception ex)
            { }
            return rtn;
        }

        //стиль для кнопок повторения
        public static Style buttonStyle = new Style(typeof(Button))
        {
            Setters =
                {
                    new Setter
                    {
                         Property = Button.TextColorProperty,
                         Value=Color.FromHex("0D47A1")
                    },
                    new Setter
                    {
                         Property = Button.BackgroundColorProperty,
                         Value = Color.FromHex("E3F2FD")
                    },
                    new Setter
                    {
                        Property = Button.FontSizeProperty,
                        Value = Device.GetNamedSize(NamedSize.Micro, typeof(Button))
                    },
                    new Setter
                    {
                        Property=Button.WidthRequestProperty,
                        Value=36
                    },
                    new Setter
                    {
                        Property=Button.HeightRequestProperty,
                        Value=36
                    },
                    new Setter
                    {
                        Property=Button.BorderRadiusProperty,
                        Value=18
                    },
                    //new Setter
                    //{
                    //    Property=Button.BorderColorProperty,
                    //    Value=Color.FromHex("0D47A1")
                    //},
                    //new Setter
                    //{
                    //    Property=Button.BorderWidthProperty,
                    //    Value=0.5
                    //}
                }
        };
        //стиль для кнопок маркера
        public static Style buttonStyle2 = new Style(typeof(Button))
        {
            Setters =
                {
                    new Setter
                    {
                         Property = Button.TextColorProperty,
                         Value=Color.Gray
                    },
                    new Setter
                    {
                        Property = Button.FontSizeProperty,
                        Value = Device.GetNamedSize(NamedSize.Micro, typeof(Button))
                    },
                    new Setter
                    {
                        Property=Button.WidthRequestProperty,
                        Value=35
                    },
                    new Setter
                    {
                        Property=Button.HeightRequestProperty,
                        Value=35
                    },
                    new Setter
                    {
                        Property=Button.BorderRadiusProperty,
                        Value=17
                    },
                    new Setter
                    {
                        Property=Button.BorderColorProperty,
                        Value=Color.FromHex("e2e2e2")
                    },
                    new Setter
                    {
                        Property=Button.BorderWidthProperty,
                        Value=0.5
                    }
                }
        };
        public static Color choosedaysColor(int operation, bool isVisible)
        {
            if (operation == 1)
            {
                if (isVisible == true)
                {
                    return Color.FromHex("E3F2FD");
                }
                else
                {
                    return Color.LightGray;
                }
            }
            else
            {
                if (isVisible == true)
                {
                    return Color.FromHex("0D47A1");
                }
                else
                {
                    return Color.White;
                }
            }
        }
        
    }
}
