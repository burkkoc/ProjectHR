using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Validators
{
    public static class ExternalMethodsForValidators
    {
        public static bool IsPhotoExtensionValid(string? fileName)
        {
            if (fileName == null)
                return false;

            string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(fileName)?.ToLower();
            return allowedExtensions.Contains(extension) ? true : false;
        }
        public static bool IsPhotoSizeValid(IFormFile? photo)
        {
            if (photo == null)
                return false;
            int MaxFileSizeInBytes = 25 * 1024 * 1024;
            return photo.Length <= MaxFileSizeInBytes;
        }

        public static bool IsPasswordValid(string? password)
        {
            if (password?.Length >= 8 && password != null)
            {
                char[] specialChars = { '+', '-', '!', '*', '?', '.', '$', '@', ',', '<', '#', '%', ';', '>', '_', '='};
                int upperCounter = 0;
                int lowerCounter = 0;
                int specialCounter = 0;
                int numberCounter = 0;
                foreach (char chr in password)
                {
                    if (chr >= 65 && chr <= 90 && upperCounter < 2)
                        upperCounter++;
                    else if (chr >= 97 && chr <= 122 && lowerCounter < 2)
                        lowerCounter++;
                    else if (chr >= '0' && chr <= '9')
                        numberCounter++;
                    else if (specialChars.Contains(chr) && specialCounter < 2)
                        specialCounter++;
                }

                if (upperCounter > 1 && lowerCounter > 1 && specialCounter > 1 && numberCounter > 1)
                    return true;

                return false;
            }
            return false;
        }

        public static bool IsDateValid(DateTime date)
        {
            return date >= DateTime.UtcNow.AddYears(-100) && date.Year <= DateTime.UtcNow.Year ? true : false;
        }
        public static bool IsOver18(DateTime date)
        {
            return date <= DateTime.UtcNow.AddYears(-18) ? true : false;
        }
        public static bool IsTCValid(string tc)
        {
            if (tc != null && tc.Length == 11)
            {

                int oddNumbers = 0;
                int evenNumbers = 0;
                int number;
                int counter = 1;

                if (tc.StartsWith("0"))
                    return false;


                string tcStringForLoop = tc.Substring(0, 9);
                foreach (char singleNumber in tcStringForLoop)
                {
                    number = int.Parse(singleNumber.ToString());
                    if (counter % 2 == 1)
                        oddNumbers += number;
                    else
                        evenNumbers += number;
                    counter++;
                }

                if (int.Parse(tc[tc.Length - 2].ToString()) == ((7 * oddNumbers - evenNumbers) % 10) && (oddNumbers + evenNumbers + int.Parse(tc[tc.Length - 2].ToString())) % 10 == int.Parse(tc[tc.Length - 1].ToString()))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public static bool IsMailValid(string? mail)
        {
            if (mail == null)
                return false;

            string[] allowedDomainExtensions = { ".com", ".net", ".org", ".com.tr", ".edu" };
            string[] splittedMail = mail.Trim().Split("@");

            if (splittedMail.Length != 2)
                return false;

            foreach (var domainExtension in allowedDomainExtensions)
            {
                if (splittedMail[1].EndsWith(domainExtension) && splittedMail[1].Split(domainExtension).Length == 2)
                    return true;
            }
            return false;
        }

        public static bool IsTaxNoValid(string vergiNo)
        {
            if (!string.IsNullOrEmpty(vergiNo) && vergiNo.Length == 10)
            {
                int sum = 0;
                int lastDigit = (int)char.GetNumericValue(vergiNo[9]);
                for (int i = 0; i < 9; i++)
                {
                    int digit = (int)char.GetNumericValue(vergiNo[i]);
                    int tmp = (digit + 10 - (i + 1)) % 10;
                    sum += (tmp == 9) ? tmp : ((tmp * (int)Math.Pow(2, 10 - (i + 1))) % 9);
                }
                return lastDigit == (10 - (sum % 10)) % 10;
            }
            return false;
        }

        public static bool CompareDates(DateTime startDate, DateTime endDate)
        {
            return startDate < endDate ? true : false;
        }

        
    }
}
