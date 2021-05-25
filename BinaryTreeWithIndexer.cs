using System;
using System.Collections;
using System.Collections.Generic;

public class Node<T>
{
    public T Value { get; set; }
    public int Count { get; set; }
    public int Index { get; set; }

    public Node<T> Left { get; set; }
    public Node<T> Right { get; set; }
    public Node<T> Parent { get; set; }

    public bool IsLeaf()
    {
        return Left == null && Right == null;
    }

    public int GetLeftCount()
    {
        if (Left != null)
            return Left.Count;
        return 0;
    }

    public int GetRightCount()
    {
        if (Right != null)
            return Right.Count;
        return 0;
    }
}

public class BinaryTree<T> : IEnumerable<T> where T : IComparable
{
    private Node<T> root;
    public int Size = 0;

    public void UpdateWeights(Node<T> node)
    {
        var current = node;
        while(current.Parent != null)
        {
            current = current.Parent;
            current.Count++;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        var used = new HashSet<Node<T>>();
        var current = root;
        if (root == null) yield break;
        while (used.Count != Size)
        {
            while (current.Left != null && !used.Contains(current.Left))
            {
                current = current.Left;
            }
            if (!used.Contains(current))
            {
                yield return current.Value;
                used.Add(current);
            }
            if (current.Right != null && !used.Contains(current.Right))
                current = current.Right;
            else
                current = current.Parent;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Node<T> TraverseTree(T value, Node<T> currentNode)
    {
        while (!currentNode.IsLeaf())
        {
            if (value.CompareTo(currentNode.Value) >= 0)
            {
                if (currentNode.Right == null) break;
                else
                    currentNode = currentNode.Right;
            }
            else
            {
                if (currentNode.Left == null) break;
                else
                    currentNode = currentNode.Left;
            }
        }
        return currentNode;
    }

    public void Add(T value)
    {
        if (root == null)
        {
            root = new Node<T>() { Value = value, Count = 1 };
            Size = 1;
        }
        else
        {
            var currentNode = TraverseTree(value, root);
            if (value.CompareTo(currentNode.Value) >= 0)
            {
                currentNode.Right = new Node<T>() 
                { Value = value, Parent = currentNode, Count = 1 };
                UpdateWeights(currentNode.Right);
            }
            else
            {
                currentNode.Left = new Node<T>() 
                { Value = value, Parent = currentNode, Count = 1 };
                UpdateWeights(currentNode.Left);
            }
            Size++;
        }
    }

    public bool Contains(T value)
    {
        var currentNode = root;
        while (currentNode != null)
        {
            if (value.CompareTo(currentNode.Value) > 0)
                currentNode = currentNode.Right;
            else if (value.CompareTo(currentNode.Value) < 0)
                currentNode = currentNode.Left;
            else
                return true;
        }
        return false;
    }

    public T Get(int index, int parentNumber, bool isLeft, Node<T> current)
    {
        int currentIndex;
        if (current == root) 
            currentIndex = root.GetLeftCount();
        else if (isLeft) 
            currentIndex = parentNumber - 1 - current.GetRightCount();
        else 
            currentIndex = current.GetLeftCount() + parentNumber + 1;

        if (currentIndex == index) 
            return current.Value;
        if (index > currentIndex) 
            return Get(index, currentIndex, false, current.Right);
        return Get(index, currentIndex, true, current.Left);
    }

    public T this[int index]
    {
        get
        {
            if (index < Size && index >= 0)
                return Get(index, 0, false, root);
            else
                throw new IndexOutOfRangeException();
        }
    }
    
    public static BinaryTree<T> Unit(BinaryTree<T> tree1, BinaryTree<T> tree2)
        {
            var node = tree1.root;
            var root2 = tree2.root;
            while (true)
            {
                if (root2.Value.CompareTo(node.Value) < 0)
                {
                    if (node.Left != null)
                        node = node.Left;
                    else
                    {
                        node.Left = root2;
                        root2.Parent = node;
                        break;
                    }
                }
                else
                {
                    if (node.Right != null)
                        node = node.Right;
                    else
                    {
                        node.Right = root2;
                        root2.Parent = node;
                        break;
                    }
                }
            }
            tree1.Size += tree2.Size;
            return tree1;
        }
}
