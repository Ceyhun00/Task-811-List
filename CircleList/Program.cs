using System;

namespace CircleList
{
    class Program
    {
        static void Main(string[] args)
        {
            //-----Создание списка из файла-----
            var list = new CircleList("In.txt");

            //-----Вывод содержимого файла на экзан-----
            list.Show();

            //-----Вставка нового участника в список-----
            var p = new Parcipiant { Name = "qwe", Gender = "male" };
            list.Insert(p);

            //-----Удаление участника из списка по имени-----
            list.Delete("qwe");

            //-----Сортировка списка начиная с выбранного имени-----
            list.Sort("Adam");

            //-----Удаление каждого k-ого и возврат последнего оставшегося-----
            list.Last(3);

            //-----Построение двух списков, 1 из мужчин, другой из женщин-----
            var genders = list.Gender();

            Console.ReadKey();
        }
    }
}
