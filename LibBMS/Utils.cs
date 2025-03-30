using System;
using System.Text.RegularExpressions;

namespace LibBMS.Common
{

    public class Utilities
    {
        public static bool IsValidYear(string input)
        {

            int currentYear = DateTime.Now.Year;
            string pattern = @"^\d{4}$";

            if (Regex.IsMatch(input, pattern))
            {
                int year = int.Parse(input);

                if (year > 0 && year <= currentYear)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidISBN(string isbn)
        {

            if (string.IsNullOrWhiteSpace(isbn)) return false;
            isbn = isbn.Replace("-", "").Replace(" ", "");
            if (isbn.Length == 10) return IsValidISBN10(isbn);
            if (isbn.Length == 13) return IsValidISBN13(isbn);

            //ISBN is invalid if it is not 10digit or 13 digit after replacing hyphens and whitespaces
            return false;

        }

        public static bool IsValidISBN10(string isbn)
        {

            /* 
            * ISBN-10:
            * multiply each digit by a descending weight (10 to 1),
            * sum the results, 
            * and check if the sum is divisible by 11
            */

            int total = 0;

            for (int i = 0; i < 9; i++)
            {
                if (!char.IsDigit(isbn[i])) return false;
                total += (isbn[i] - '0') * (i + 1);
            }

            if (isbn[9] == 'X')
            {
                total += 10 * 10;
            }
            else if (char.IsDigit(isbn[9]))
            {
                total += (isbn[9] - '0') * 10;
            }
            else
            {
                return false;
            }

            // ISBN-10 is valid if sum is divisible by 11
            return total % 11 == 0;



        }

        public static bool IsValidISBN13(string isbn)
        {

            /* 
            * ISBN-13:
            * multiply each digit by alternating weights (1 and 3), 
            * sum the results, 
            * and check if the sum is divisible by 10
            */

            int total = 0;

            // Loop through each character in the ISBN-13 string
            for (int i = 0; i < 13; i++)
            {
                if (!char.IsDigit(isbn[i])) return false;

                // Alternate between multiplying by 1 and 3
                int digit = isbn[i] - '0';
                total += (i % 2 == 0) ? digit : digit * 3;
            }

            // ISBN-13 is valid if sum is divisible by 10
            return total % 10 == 0;
        }



    }


}