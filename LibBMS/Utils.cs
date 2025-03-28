using System;
using System.Text.RegularExpressions;

namespace LibBMS.Common
{

    public class Utilities {
        public static bool IsInvalidYear (string input) {

        int currentYear = DateTime.Now.Year;
        string pattern = @"^\d{4}$";

        if (Regex.IsMatch(input, pattern))
        {
            int year = int.Parse(input);

            if (year > 0 && year <= currentYear)
            {
                return false;
            }
            else
            {
                Console.WriteLine($"Year is not in the allowed range (1 to {currentYear})");
                return true;
            }
        }
        else
        {
            return true;
        }
    }
    }
}