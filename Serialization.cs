using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Program
{
    public class LabelAttribute : Attribute
    {
        public string LabelText { get; set; }
        public LabelAttribute(string labelText)
        {
            LabelText = labelText;
        }
    }

    public class Person
    {
        [Label("Имя")]
        public string FirstName { get; set; }

        [Label("Фамилия")]
        public string LastName { get; set; }
    }

    public class Program
    {
        public static void Print(object obj)
        {
            var type = obj.GetType();
            foreach (var property in type.GetProperties())
            {
                var attribute = property.GetCustomAttributes(true).OfType<LabelAttribute>().First();
                Console.WriteLine(attribute.LabelText + ": " + property.GetValue(obj));
            }    
        }

        public static void Main()
        {
            var person = new Person() { FirstName = "Victor", LastName = "Kapkaev" };
            var str = JsonConvert.SerializeObject(person);
            Console.WriteLine(str);
            Print(person);
        }   
    }
}
