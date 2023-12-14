using System;
using System.Linq;
using System.Timers;

namespace lab12 {
    class Writer
    {
        public string surname { get; set; }
        public string title { get; set; }
        public string genre { get; set; }

        public Writer(string Surname, string Title, string Genre)
        {
            surname = Surname;
            title = Title;
            genre = Genre;
        }

        public override string ToString()
        {
            return surname + " " + title + " " + genre;
        }
    }
    class Program
    {
        public static void Main(string[] args)
        {
            List<Writer> writers = new List<Writer>();
            string[] file = File.ReadAllLines("C:\\Users\\Acer\\source\\repos\\lab12\\text_for_lab12.txt");
             

            foreach (string element in file)
            {
                string[] part = element.Split(',');
                writers.Add(new Writer (part[0], part[1], part[2]));
            }

            //Фильтрация по детективам и романам с указанием писателя
            var w1 = writers.Where(wr => wr.genre == "Страшное" || wr.genre == "Веселое");
            //Проекция
            var w2 = writers
                .OrderByDescending(wr => wr.title)
                .Select(wr => wr.title);
            //Сортировка
            var w3 = writers
                .OrderBy(wr => wr.surname);
            //Группировка по жанру
            var w4 = writers
                .GroupBy(wr => wr.genre);
            //Агрегатные функции "Самое длинное название произведения"
            /*var w5 = from wr in writers select wr.title;
            var nw5 = w5.Max(wr => wr.Length);
            var nw6 = from wr in w5 where wr.Length == nw5 select wr;*/
            var w5 = writers
                .Where(wr => wr.title.Length == writers.Max(w => w.title.Length))
                .Select(t => t.title);
            //Извлечение 2/3 части элементов
            var w6 = writers
                .Take((writers.Count * 2 / 3));
            //Проверку на соответствие элементов списка условию согласно варианту задания.
            var w7 = writers
                .All(wr => wr.surname == "Толстой");
            var w8 = writers
                .Any(wr => wr.surname == "Толстой");
            //Объединения двух последовательностей(придумать вторую самостоятельно).
            List<int> secondSebsequence = new List<int> { 19, 19, 19, 19, 59, 19, 63, 37, 19, 19, 82, 19, 19, 61 };
            var w9 = writers
                .Zip(secondSebsequence, (writers, age) => new {Writer = writers, Age = age});

            Console.WriteLine("Фильтрация по веселому и страшному:\n");
            Console.WriteLine("| {0,20} | {1,50} | {2, 20} |", "Фамилия", "Название", "Жанр");
            Console.WriteLine(new string('-', 100));
            foreach (var el in w1)
            {
                Console.WriteLine("| {0,-20} | {1,-50} | {2,-20} |", el.surname, el.title, el.genre);
            }

            Console.WriteLine("\nПроекция названия в порядке от Я до А:\n");

            foreach (var el in w2)
            {
                Console.WriteLine($"{el}");
            }

            Console.WriteLine("\nСортировка по фамилии и названию произведения:\n");
            
            foreach(var el in w3)
            {
                Console.WriteLine($"{el.surname} {el.title}");
            }

            Console.WriteLine("\nГруппировка по жанру:");

            foreach(var el in w4)
            {
                Console.WriteLine($"\n{el.Key}:");
                
                foreach (var el1 in el)
                {
                    Console.WriteLine(el1.title);
                }
            }

            Console.WriteLine("\nАгрегатные функции Самое длинное название произведения:");
            
            foreach(var el in w5)
            {
                Console.WriteLine(el);
            }
            
            /*Console.WriteLine($"{nw5}");
            foreach(var el in nw6)
            {
                Console.WriteLine(el);
            }*/

            Console.WriteLine("\nИзвлечение 2/3 части элементов:");

            foreach(var el in w6)
            {
                Console.WriteLine($"{el}");
            }

            Console.WriteLine("\nПроверка на соответсвие:");
            Console.WriteLine("Все ли произведения написаны Толстым:");
            if(w7 == false)
            {
                Console.WriteLine("Неверно. Не все произведения написаны Толстым!");
            }
            else
            {
                Console.WriteLine("Верно. Все произведения написаны Толстым");
            }
            if(w8 == false)
            {
                Console.WriteLine("Неверно. Ни одно из произведений не было написано Толстым");
            }
            else
            {
                Console.WriteLine("Верно. Одно из произведений точно написано Толстым");
            }

            Console.WriteLine("\nОбъединение:");

            /*foreach(var el in writers)
            {
                foreach(var el1 in secondSebsequence)
                {
                    Console.WriteLine($"{el}, {el1}");
                    break;
                }
            }*/
            for(int i = 0; i < writers.Count - 1; i++)
            {
                Console.WriteLine($"{writers[i]}, {secondSebsequence[i]}");
            }
            

        }
    }
}