using System;
using System.Text;

public static class Extensions
{
    public static string ToRoman(this int num)
    {
        if (num < 0 || num > 3999)
        {
            throw new Exception($"ivalid input (num = {num}) should be between 0 and 3999 (inclusive)");
        }

        var val = num;

        var sb = new StringBuilder("");
        while (val > 0)
        {
            string numeral;
            int subtract;

            if (val >= 900)
            {
                if (val < 1000)
                {
                    subtract = 900;
                    numeral = "CM";
                }
                else
                {
                    subtract = 1000;
                    numeral = "M";
                }
            }
            else if (val >= 400)
            {
                if (val < 500)
                {
                    subtract = 400;
                    numeral = "CD";
                }
                else
                {
                    subtract = 500;
                    numeral = "D";
                }
            }
            else if (val >= 90)
            {
                if (val < 100)
                {
                    subtract = 90;
                    numeral = "XC";
                }
                else
                {
                    subtract = 100;
                    numeral = "C";
                }
            }
            else if (val >= 40)
            {
                if (val < 50)
                {
                    subtract = 40;
                    numeral = "XL";
                }
                else
                {
                    subtract = 50;
                    numeral = "L";
                }
            }
            else if (val >= 9)
            {
                if (val < 10)
                {
                    subtract = 9;
                    numeral = "IX";
                }
                else
                {
                    subtract = 10;
                    numeral = "X";
                }
            }
            else if (val >= 4)
            {
                if (val < 5)
                {
                    subtract = 4;
                    numeral = "IV";
                }
                else
                {
                    subtract = 5;
                    numeral = "V";
                }
            }
            else
            {
                subtract = 1;
                numeral = "I";
            }

            val -= subtract;
            sb.Append(numeral);
        }

        return sb.ToString();
    }
}

public class Solution
{
    public static void Main(string[] argv)
    {
        var val = int.Parse(Console.ReadLine());

        var roman = val.ToRoman();

        Console.WriteLine($"Roman is {roman}.");
    }
}
