using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;

int[] numbers = new[] { 2, 3, 4, 5, 3, 6, 7 };
var result = from n in numbers where n < 5 select n;
var result2 = numbers.Where(n => n < 5).Select(n => n);

foreach (int i in result2)
{
    Console.WriteLine(i);
}

foreach (int i in result)
{
    Console.WriteLine(i);
}

Thread t = Thread.CurrentThread;
t.Name = "Main Thread";
Console.WriteLine($"Thread name - {t.Name}");
Console.WriteLine($"Current thread " + Thread.CurrentThread.Name);

static class Wheres
{
    // public static IEnumerable<int> Where(this int[] ints, Func<int, bool> gauntlet)
    // {
    //     Console.WriteLine("My Where extension");
    //     foreach (int i in ints)
    //     {
    //         if (gauntlet(i))
    //         {
    //             yield return i;
    //         }
    //     }
    // }




    public static IEnumerable<T> Where<T>(this IEnumerable<T> items, Func<T, bool> gauntlet)
    {
        Console.WriteLine("My Where extension");
        foreach(T item in items)
        {
            if (gauntlet(item))
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<R> Select<T,R>(this IEnumerable<T> items, Func<T, R> transform){
        Console.WriteLine("My Select extension");
        foreach(T item in items)
        {
            yield return transform(item);
        }
    }
}
