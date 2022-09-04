namespace ConsoleApp1;

public static class Program
{
    public static int FindFirstFreeIndex(int[] arr, int left, int right)
    {
        if (left >= right)
        {
            return left + 1;
        }
        var mid = (left + right) / 2;
        if (arr[mid] == mid + 1)
            return FindFirstFreeIndex(arr, mid + 1, right);
        return FindFirstFreeIndex(arr, left, mid);
    }

    public static void Main()
    {
        var arr = new int[] {5};
        Console.WriteLine(FindFirstFreeIndex(arr, 0, arr.Length - 1));
    }
}
