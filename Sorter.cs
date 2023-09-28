using System;
using System.Collections.Generic;

namespace Program
{
    class Program
    {
         public static IEnumerable<Point3D> GetPointsInLocalSurfaceCoordinates(List<Point3D> source)
        {
            var localO = source[0];
            var localX = source[1].Sub(source[0]);
            var normal = Vector.CrossProd(localX, source[2].Sub(localO));
            var localY = Vector.CrossProd(normal, localX);

            var Xaxis = Vector.GetVectorNorm(localX);
            var Yaxis = Vector.GetVectorNorm(localY);

            foreach (var point in source)
            {
                var x = Vector.DotProd(point.Sub(localO), Xaxis);
                var y = Vector.DotProd(point.Sub(localO), Yaxis);
                yield return new Point3D(x, y, 0);
            }
        }

        private static IEnumerable<Tuple<int, int>> GetPairs(int start, int end)
        {
            for (var i = start; i < end - 1; i++)
            {
                for (var j = i + 1; j < end; j++)
                {
                    yield return Tuple.Create(i, j);
                }
            }
        }

        public static int GetSignPointToLine(Point3D p, Line l)
        {
            var A = l.P1._y - l.P0._y;
            var B = -l.P1._x + l.P0._x;
            var C = -l.P0._x * A - l.P0._y * B;
            return Math.Sign(A * p._x + B * p._y + C);
        }

        public static IEnumerable<Tuple<int, int>> GetEdges(List<Point3D> surfacePoints)
        {
            if (surfacePoints.Count == 3)
            {
                yield return Tuple.Create(0, 1);
                yield return Tuple.Create(1, 2);
                yield return Tuple.Create(2, 0);
                yield break;
            }

            foreach (var pair in GetPairs(0, surfacePoints.Count))
            {
                var i = pair.Item1;
                var j = pair.Item2;
                var line = new Line(surfacePoints[i], surfacePoints[j]);
                var otherPoints = surfacePoints.Where(p => surfacePoints.IndexOf(p) != i
                                                           && surfacePoints.IndexOf(p) != j).ToList();
                var isBoundLine = true;
                for (int k = 0; k < otherPoints.Count - 1; k++)
                {
                    var signFirst = (GetSignPointToLine(otherPoints[k], line) >= 0);
                    var signSecond = (GetSignPointToLine(otherPoints[k + 1], line) >= 0);
                    if (signFirst != signSecond)
                    {
                        isBoundLine = false;
                        break;
                    }
                }
                if (isBoundLine)
                    yield return Tuple.Create(i, j);
            }
        }

        public static IEnumerable<Node> GetContour(Node[] source)
        {
            var points = source.Select(n => n.Position).ToList();
            var surfacePoints = GetPointsInLocalSurfaceCoordinates(points)
                .ToList();
            var sides = GetEdges(surfacePoints).ToList();
            var edges = new List<Tuple<int, int>>();
            sides.ForEach(t =>
            {
                edges.Add(Tuple.Create(t.Item2, t.Item1));
                edges.Add(Tuple.Create(t.Item1, t.Item2));
            });

            var start = 0;
            var stack = new Stack<int>();
            var used = new HashSet<int>();
            stack.Push(start);
            used.Add(start);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                var neighbors = edges.Where(t => t.Item1 == current)
                    .SelectMany(t => new[] { t.Item2 });
                foreach (var vertex in neighbors)
                {
                    if (!used.Contains(vertex))
                    {
                        used.Add(vertex);
                        stack.Push(vertex);
                        break;
                    }
                }
            }

            if (used.Count != source.Length)
            {
                Console.WriteLine("Here");
            }

            foreach (var index in used)
            {
                yield return source[index];
            }
        }
    }
}
