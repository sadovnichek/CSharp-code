using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using KdTree;
using KdTree.Math;

class Program
{
    public static void Main()
    {
        /*var rndm = new Random();
        for (var i = 0; i < 90000; i++)
        {
            File.AppendAllText("input.txt", 
                $"{rndm.Next(-5, 5) + rndm.NextDouble()} {rndm.Next(-5, 5) + rndm.NextDouble()} " +
                $"{rndm.Next(-5, 5) + rndm.NextDouble()}\n");
        }*/

        var points = new List<float[]>();
        var tree = new KdTree<float, float>(3, new FloatMath());
        using var fs = new StreamReader("input.txt");
        while (!fs.EndOfStream)
        {
            var line = fs.ReadLine();
            var coordinates = line.Split().Select(float.Parse).ToArray();
            tree.Add(new[] {coordinates[0], coordinates[1], coordinates[2]}, 0);
            points.Add(coordinates);
        }

        foreach (var point in points)
        {
            var nodes = tree.RadialSearch(point, 1);
        }
    }
}
