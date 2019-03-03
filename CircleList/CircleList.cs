using System;
using System.IO;

namespace CircleList
{
    class Parcipiant : IComparable<Parcipiant> //Интерфейс для более удобного сравнивания
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public Parcipiant Next { get; set; }

        public int CompareTo(Parcipiant par) //Совственно его реализация
        {
            return Name.CompareTo(par.Name);
        }

        public override string ToString()
        {
            return Name + " " + Gender;
        }
    }

    class CircleList
    {
        Parcipiant Temp { get; set; }
        private string Filename;
        private int Count = 0;

        public CircleList() { }
        public CircleList(string filename)
        {
            Filename = filename;
            var file = File.ReadAllLines(filename);
            foreach (var row in file)
            {
                var splited = row.Split(' ');
                if (Temp == null)
                {
                    Temp = new Parcipiant { Name = splited[0], Gender = splited[1], Next = null };
                    Temp.Next = Temp;
                    Count++;
                    continue;
                }
                var par = new Parcipiant { Name = splited[0], Gender = splited[1], Next = Temp.Next };
                Temp.Next = par;
                Temp = Temp.Next;
                Count++;
            }
        }

        public void Show() //Вывод из файла построчно
        {
            var file = File.ReadAllLines(Filename);
            foreach (var row in file)
            {
                Console.WriteLine(row);
            }
        }

        public void Insert(Parcipiant p) //Вставка в список на текущее место
        {
            p.Next = Temp.Next;
            Temp.Next = p;
            Count++;
        }
    
        public void Delete(string name)  //Поиск имени и удаление эл-а с этим именем
        {
            var temp = Temp;
            for (int i = 0; i < Count + 1; i++)
            {
                if (temp.Next.Name == name)
                {
                    temp.Next = temp.Next.Next;
                    Count--;
                    return;
                }
                temp = temp.Next;
            }
        }

        public void Sort(string name)
        {
            var temp = Temp;
            var find = false;
            for (int i = 0; i < Count + 1; i++)
            {
                if (temp.Name == name)
                {
                    find = true;
                    break;
                }
                temp = temp.Next;
            }
            if (!find)
                throw new ArgumentException();
            var result = new CircleList();
            var par = new Parcipiant { Name = temp.Name, Gender = temp.Gender };
            par.Next = par;
            result.Temp = par;
            temp = temp.Next;
            for (int i = 1; i < Count; i++)
            {
                var p = new Parcipiant { Name = temp.Name, Gender = temp.Gender };
                var t = result.Temp;
                for (int j = 1; j < Count; j++)
                {
                    if (p.CompareTo(t.Next) < 0 || t.CompareTo(p) < 0 && t.CompareTo(t.Next) >= 0)
                    {
                        p.Next = t.Next;
                        t.Next = p;
                        break;
                    }
                    t = t.Next;
                }
                temp = temp.Next;
            }
            Temp = result.Temp;
        }

        public Parcipiant Last(int k)
        {
            var temp = Temp;
            while (Count != 1)
            {
                for (int i = 0; i < k - 1; i++) //Отсчитываем k-ый эл-т
                {
                    temp = temp.Next;
                }
                Delete(temp.Name); //Удаляем его
            }
            return temp.Next;
        }

        public CircleList[] Gender() //Создаём 2 списка и просто сортируем, мужчин в 1, женщин во 2
        {
            var temp = Temp;
            var male = new CircleList();
            var female = new CircleList();
            for (int i = 0; i < Count; i++)
            {
                var par = new Parcipiant { Name = temp.Name, Gender = temp.Gender };
                if (par.Gender == "male")
                {
                    AddOrCreate(male, par); //Вспомогательный метод, чтобы код был чище (написан чуть ниже) 
                }
                else
                {
                    AddOrCreate(female, par);
                }
                temp = temp.Next;
            }
            return new CircleList[] { male, female };
        }

        private void AddOrCreate(CircleList list, Parcipiant p) //Если список пуст - создаём его, иначе добавляем эл-т
        {
            if (list.Temp == null)
            {
                p.Next = p;
                list.Temp = p;
            }
            else
                list.Insert(p);
        }
    }
}
