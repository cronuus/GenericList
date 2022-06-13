using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _List
{
    internal class Program
    {
        static void Main(string[] args)
        {
            _List<string> list1 = new _List<string>();
            List<string> list2 = new List<string> { "bb", "cc", "dd" };
            string[] arr = new string[] { "111", "22", "3333", "44", "5", "66" };
            list1.Add("aaa");
            list1.Add("h");
            list1.Add("fdjfh");
            foreach (var item in list1)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(list1.Contains("bbb"));
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
            list1.Insert(2, "mln");
            foreach (var item in list1)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(list1.LastIndexOf("111"));
            list1.Remove("bb");
            foreach (var item in list1)
            {
                Console.WriteLine(item);
            }
            list1.RemoveAt(1);
            foreach (var item in list1)
            {
                Console.WriteLine(item);
            }
            list1.Reverse();
            foreach (var item in list1)
            {
                Console.WriteLine(item);
            }
            for (int i = 0; i < list1.ToArray().Length; i++)
            {
                Console.WriteLine(list1.ToArray()[i]);
            }
        }
    }
}
