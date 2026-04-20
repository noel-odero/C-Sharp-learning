int [] numbers = new[]{2, 3, 4, 5, 3, 6, 7};
var result = from n in numbers where n < 5 select n;

var result2 = numbers.Where(n => n < 5).Select(n => n);

foreach(int i in result2)
{
    Console.WriteLine(i);
}

foreach(int i in result)
{
    Console.WriteLine(i);
}