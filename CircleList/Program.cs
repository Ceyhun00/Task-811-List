using System;

namespace CircleList
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new CircleList("In.txt");

            list.Show();

            var p = new Parcipiant { Name = "qwe", Gender = "male" };
            list.Insert(p);

            list.Delete("qwe");

            list.Sort("Adam");

            list.Last(3);

            var genders = list.Gender();

            Console.ReadKey();
        }
    }
}
