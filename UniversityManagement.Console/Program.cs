using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using UniversityManagement.Common.Models;
using UniversityManagement.Common.Services;

namespace UniversityManagement.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            var service = new UniversityServiceAsync<Student>("students_async.json");

            var bag = new ConcurrentBag<Student>();

            Parallel.For(0, 1000, i =>
            {
                var student = Student.CreateNew();
                bag.Add(student);
            });

            foreach (var student in bag)
            {
                await service.CreateAsync(student);
            }

            var all = await service.ReadAllAsync();

            Console.WriteLine("Min age: " + all.Min(s => s.Age));
            Console.WriteLine("Max age: " + all.Max(s => s.Age));
            Console.WriteLine("Avg age: " + all.Average(s => s.Age));

            await service.SaveAsync();

            Console.WriteLine("Saved 1000+ students to students_async.json");

            var autoReset = new AutoResetEvent(false);
            var thread = new Thread(() =>
            {
                Console.WriteLine("Waiting...");
                Thread.Sleep(1000);
                autoReset.Set();
            });

            thread.Start();
            autoReset.WaitOne();

            Console.WriteLine("Signaled");
            Console.ReadLine();
        }
    }
}
