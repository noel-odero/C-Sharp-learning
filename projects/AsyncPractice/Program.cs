// Question 1
async Task BrewTeaAsync()
{
    await Task.Delay(3000);
    Console.WriteLine("Tea is ready");

}

Console.WriteLine("Starting to brew...");
await BrewTeaAsync();
Console.WriteLine("Done");



// Question 2
async Task<string> BrewTeaAsync1()
{
    await Task.Delay(3000);
    return("Earl grey");

}

string tea = await BrewTeaAsync1();
Console.WriteLine($"Your tea is {tea}");