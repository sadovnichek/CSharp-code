using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class Program
{
    const int p = 1000000007;
    const int x = 263;

    public static long Binpow(int a, int n)
    {
        if (n == 0)
            return 1;
        if (n % 2 == 1)
            return (Binpow(a, n - 1) % p) * (a % p) % p;
        var b = Binpow(a, n / 2) % p;
        return b * b % p;
    }

    public static long GetHash(string text)
    {
        var hash = 0L;
        for (var i = 0; i < text.Length; i++)
        {
            hash += text[i] * Binpow(x, i) % p;
        }
        return hash % p;
    }

    public static void RabinCarpAlgorithm(string pattern, string text)
    {
        var patternSize = pattern.Length;
        var hashes = new BigInteger[text.Length - patternSize + 1];
        var patternHash = GetHash(pattern);
        var initialWord = text.Substring(text.Length - patternSize, patternSize);
        hashes[text.Length - patternSize] = GetHash(initialWord);
        var lastPow = Binpow(x, patternSize - 1);
        for (var i = text.Length - patternSize - 1; i >= 0; i--)
        {
            hashes[i] = ((hashes[i + 1] - text[i + patternSize] * lastPow) * x % p + text[i] + p) % p;
        } 
        for (int i = 0; i < text.Length - patternSize + 1; i++)
        {
            if (hashes[i] == patternHash)
                Console.Write(i + " ");
        }
    }

    public static void Main()
    {
        var pattern = Console.ReadLine();
        var text = Console.ReadLine();
        RabinCarpAlgorithm(pattern, text);
    }
}
