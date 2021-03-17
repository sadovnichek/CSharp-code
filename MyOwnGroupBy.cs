using System;
using System.Collections.Generic;

namespace Program
{
    static class EnumerableExtention
    {
        public static Dictionary<TKey, List<TElement>> GroupBy<TElement, TKey>(this IEnumerable<TElement> source, Func<TElement, TKey> selector)
        {
            var dictionary = new Dictionary<TKey, List<TElement>>();
            foreach(var element in source)
            {
                if (!dictionary.ContainsKey(selector(element)))
                {
                    var list = new List<TElement>();
                    list.Add(element);
                    dictionary.Add(selector(element), list);
                }
                else
                {
                    dictionary[selector(element)].Add(element);
                }
            }
            return dictionary;
        }
    }

    class Document
    {
        public int ID { get; set; }
        public string month { get; set; }
    }

    class Program
    {
        public static void Main()
        {
            var documents = new Document[]
                {
                    new Document() {ID = 1, month = "June"},
                    new Document() {ID = 2, month = "November"},
                    new Document() {ID = 3, month = "June"},
                    new Document() {ID = 4, month = "August"},
                    new Document() {ID = 5, month = "November"}
                };
            var x = documents.GroupBy(d => d.month);
        }
    }
}
