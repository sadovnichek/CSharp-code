using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    class Edge
    {
        public int Start { get; set; }
        public int End { get; set; }
        public int Price { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            var input = Console.ReadLine().Split();
            var n = int.Parse(input[0]);
            var k = int.Parse(input[1]);
            var powerVertexes = Console.ReadLine().Split().Select(x => int.Parse(x)).ToList();
            var edges = new List<Edge>();
            for(int i = 0; i < n; i++)
            {
                var f = Console.ReadLine().Split().Select(x => int.Parse(x)).ToArray();
                for (int j = 0; j < f.Length; j++)
                {
                    if(i != j && !(powerVertexes.Contains(i + 1) && powerVertexes.Contains(j + 1)))
                    {
                        var edge = new Edge { Start = i + 1, End = j + 1, Price = f[j] };
                        edges.Add(edge);
                    }
                }
            }
            var minCost = 0;
            while(powerVertexes.Count != n)
            {
                var reacheable = edges.Where(x => powerVertexes.Contains(x.Start))
                    .Where(x => !powerVertexes.Contains(x.End))
                    .OrderBy(x => x.Price);
                var minEdge = reacheable.First();
                minCost += minEdge.Price;
                powerVertexes.Add(minEdge.End);
            }
            Console.WriteLine(minCost);
        }
    }
}
