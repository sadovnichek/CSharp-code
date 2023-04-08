using System;
using System.Collections.Generic;
using System.Linq;

class DisjointSetUnion
{
    private int[] parent;
    private int[] rank;
    
    public DisjointSetUnion(int size)
    {
        parent = new int[size + 1];
        for (var i = 1; i <= size; i++)
        {
            parent[i] = i;
        }
        rank = new int[size + 1];
    }

    public int Find(int element)
    {
        if (element != parent[element])
        {
            parent[element] = Find(parent[element]);
        }

        return parent[element];
    }

    public void Union(int first, int second)
    {
        var firstId = Find(first);
        var secondId = Find(second);
        if (firstId == secondId) return;
        if (rank[firstId] > rank[secondId])
        {
            parent[secondId] = firstId;
        }
        else
        {
            if (rank[firstId] == rank[secondId])
            {
                rank[secondId]++;
            }
            parent[firstId] = secondId;
        }
    }
}
