using System;
/*
 * Please note that credit for the below program goes to Justin Rogers(http://weblogs.asp.net/justin_rogers). 
 * Specifically his post at http://weblogs.asp.net/justin_rogers/articles/151757.aspx
 * 
 * */


/// <summary>
/// Convert a given number to it's long form English description.
/// </summary>
/*
    The below program works pretty well, but there are some
    round-off precision errors in the double that are giving
    me hell.  I may or may not look into it.  Ideally, if
    given a string, I'd parse the string.  If given a number,
    then and only then would I parse the number.  Since the
    string representation of a double is quite relaxed, it
    would actually be easier to parse.
 
  
*/

public class NumberToEnglish
{
    private static string[] onesMapping =
        new string[] {
            "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine",
            "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
        };
    private static string[] tensMapping =
        new string[] {
            "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
        };
    private static string[] groupMapping =
        new string[] {
            "Hundred", "Thousand", "Million", "Billion", "Trillion", "Quadrillion", "Quintillion", "Sextillian",
            "Septillion", "Octillion", "Nonillion", "Decillion", "Undecillion", "Duodecillion", "Tredecillion",
            "Quattuordecillion", "Quindecillion", "Sexdecillion", "Septendecillion", "Octodecillion", "Novemdecillion",
            "Vigintillion", "Unvigintillion", "Duovigintillion", "10^72", "10^75", "10^78", "10^81", "10^84", "10^87",
            "Vigintinonillion", "10^93", "10^96", "Duotrigintillion", "Trestrigintillion"
        };

    // NOTE: 10^303 is approaching the limits of double, as ~1.7e308 is where we are going
    // 10^303 is a centillion and a 10^309 is a duocentillion

    public static string EnglishFromNumber(int number)
    {
        return EnglishFromNumber((long)number);
    }

    public static string EnglishFromNumber(long number)
    {
        return EnglishFromNumber((double)number);
    }

    public static string EnglishFromNumber(double number)
    {
        string sign = null;
        if (number < 0)
        {
            sign = "Negative";
            number = Math.Abs(number);
        }

        int decimalDigits = 0;
        Console.WriteLine(number);
        while (number < 1 || (number - Math.Floor(number) > 1e-10))
        {
            number *= 10;
            decimalDigits++;
        }
        Console.WriteLine("Total Decimal Digits: {0}", decimalDigits);

        string decimalString = null;
        while (decimalDigits-- > 0)
        {
            int digit = (int)(number % 10); number /= 10;
            decimalString = onesMapping[digit] + " " + decimalString;
        }

        string retVal = null;
        int group = 0;
        if (number < 1)
        {
            retVal = onesMapping[0];
        }
        else
        {
            while (number >= 1)
            {
                int numberToProcess = (number >= 1e16) ? 0 : (int)(number % 1000);
                number = number / 1000;

                string groupDescription = ProcessGroup(numberToProcess);
                if (groupDescription != null)
                {
                    if (group > 0)
                    {
                        retVal = groupMapping[group] + " " + retVal;
                    }
                    retVal = groupDescription + " " + retVal;
                }

                group++;
            }
        }

        return String.Format("{0}{4}{1}{3}{2}",
            sign,
            retVal,
            decimalString,
            (decimalString != null) ? " Point " : "",
            (sign != null) ? " " : "");
    }

    private static string ProcessGroup(int number)
    {
        int tens = number % 100;
        int hundreds = number / 100;

        string retVal = null;
        if (hundreds > 0)
        {
            retVal = onesMapping[hundreds] + " " + groupMapping[0];
        }
        if (tens > 0)
        {
            if (tens < 20)
            {
                retVal += ((retVal != null) ? " " : "") + onesMapping[tens];
            }
            else
            {
                int ones = tens % 10;
                tens = (tens / 10) - 2; // 20's offset

                retVal += ((retVal != null) ? " " : "") + tensMapping[tens];

                if (ones > 0)
                {
                    retVal += ((retVal != null) ? " " : "") + onesMapping[ones];
                }
            }
        }

        return retVal;
    }
}