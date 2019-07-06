using System;
using System.Collections.Generic;
using Entities;
using System.Linq;

namespace Secao_16__LINQ_With_Lambda_demo_c213
{
    class Program
    {
        static void Print<T>(string message, IEnumerable<T> collection)
        {
            Console.WriteLine(message);
            foreach(T obj in collection)
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            Category c1 = new Category() { Id = 1, Name="Tools",Tier=2};
            Category c2 = new Category() { Id = 2, Name="Computers",Tier=1};
            Category c3 = new Category() { Id = 3, Name="Electronics",Tier=1};

            List<Product> products = new List<Product>()
            {
                new Product() { Id = 1, Name = "Computer", Price = 1100.0, Category = c2 },
                new Product() { Id = 2, Name = "Hammer", Price = 90.0, Category = c1 },
                new Product() { Id = 3, Name = "TV", Price = 1700.0, Category = c3 },
                new Product() { Id = 4, Name = "Notebook", Price = 1300.0, Category = c2 },
                new Product() { Id = 5, Name = "Saw", Price = 80.0, Category = c1 },
                new Product() { Id = 6, Name = "Tablet", Price = 700.0, Category = c2 },
                new Product() { Id = 7, Name = "Camera", Price = 700.0, Category = c3 },
                new Product() { Id = 8, Name = "Printer", Price = 350.0, Category = c3 },
                new Product() { Id = 9, Name = "MacBook", Price = 1800.0, Category = c2 },
                new Product() { Id = 10, Name = "Sound Bar", Price = 700.0, Category = c3 },
                new Product() { Id = 11, Name = "Level", Price = 70.0, Category = c1 }
            };
            var r1 = products.Where(p => p.Category.Tier == 1 && p.Price < 900.0);
            Print("Tier 1 and price < 900", r1);

            Console.WriteLine("\n---------------------------------\n");

            var r2 = products.Where(p => p.Category.Name == "Tools").Select(p=>p.Name);
            Print("Names category tools", r2);

            var r3 = products.Where(p => p.Name[0] == 'C').Select(p => new { p.Name, p.Price, CategoryName = p.Category.Name });
            Print("Products with first letter C",r3);

            var r4 = products.Where(p => p.Category.Tier == 1).OrderBy(p => p.Price).ThenBy(p => p.Name);
            Print("Tier 1 order for price and name", r4);

            var r5 = r4.Skip(2).Take(4);
            Print("Skip and take", r5);

            var r6 = products.First();
            Print("First test 1 ", r5);

            var r7 = products.Where(p => p.Price > 3000.0).FirstOrDefault();
            Console.WriteLine("First test 2 First or default"+r7);

            var r8 = products.Where(p => p.Id == 3).SingleOrDefault();
            Console.WriteLine("Single Or Default: "+r8);

            var r9 = products.Where(p => p.Id == 30).SingleOrDefault();
            Console.WriteLine("\nSingle Or Default 2 (return none result): "+r9);

            var r10 = products.Max(p => p.Price);
            Console.WriteLine("Max Price: "+r10);

            var r11 = products.Min(p => p.Price);
            Console.WriteLine("Max Price: "+r11);

            var r12 = products.Where(p => p.Category.Id == 1).Sum(p => p.Price);
            Console.WriteLine("Category 1 sum price: "+r12);

            var r13 = products.Where(p => p.Category.Id == 1).Average(p => p.Price);
            Console.WriteLine("Category 1 average price: " + r13);

            var r14 = products.Where(p => p.Category.Id == 5).Select(p => p.Price).DefaultIfEmpty(0.0).Average(); //solução do problema caso tente realizar a media onde não contém valores
            Console.WriteLine("Solution average none value: "+r14);

            //montar operação agregada personalizada selecte e agregate

            var r15 = products.Where(p => p.Category.Id == 1)
                .Select(p => p.Price)
                .Aggregate(0.0,(x, y) => x + y); //inserindo ooperação criada para soma no agregate*para solucionar o problema caso não haja valor é fazer uma sobrecarga definindo um valor padrão
            Console.WriteLine("Category 1 agregate sum: "+r15+"\n");

            var r16 = products.GroupBy(p => p.Category);
            foreach(IGrouping<Category,Product> group in r16)
            {
                Console.WriteLine("Category: "+group.Key.Name+":");
                foreach(Product p in group){
                    Console.WriteLine(p);
                }
                Console.WriteLine();
            }
        }
    }
}
