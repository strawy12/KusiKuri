using System;
using System.Numerics;

namespace ExtenstionMethod
{
    public static class MainExtenstionMethod
    {
        public static BigInteger Multiply(this BigInteger num, decimal multiplier, MidpointRounding rounding = MidpointRounding.AwayFromZero)
        {
            int digits = GetScale(multiplier);      
            decimal decScale = Pow10Dec(digits); 
            decimal scaledDec = Math.Round(multiplier * decScale, 0, rounding);

            BigInteger scaled = new BigInteger(scaledDec);  
            BigInteger div = BigInteger.Pow(10, digits); 

            return (num * scaled) / div;
        }

    // decimal의 소수 자릿수
    private static int GetScale(decimal d)
    {
        int bits = decimal.GetBits(d)[3];
        return (bits >> 16) & 0xFF; // 0..28
    }

    // 10^n (decimal)
    private static decimal Pow10Dec(int n)
    {
        decimal r = 1m;
        for (int i = 0; i < n; i++) r *= 10m;
        return r;
    }

    }

}