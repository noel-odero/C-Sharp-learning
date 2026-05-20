// Question 1
using System.Diagnostics;

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


// Question 3 — Sequential vs concurrent
// Write two methods: BoilKettleAsync (waits 3 seconds) 
// and MakeToastAsync (waits 2 seconds). First call them 
// sequentially with two separate awaits and measure the total 
// time with Stopwatch. Then start both tasks before awaiting 
// either and measure again. What is the difference in time and why?

async Task BoilKettleAsync()
{
    await Task.Delay(3000);
    Console.WriteLine("Kettle boiled");
}

async Task MakeToastAsync()
{
    await Task.Delay(2000);
    Console.WriteLine("Toast made");
}
var sw = Stopwatch.StartNew();
await BoilKettleAsync();
await MakeToastAsync();
sw.Stop();

Console.WriteLine($"Sequential: {sw.ElapsedMilliseconds}ms");


sw.Restart();
Task kettle = BoilKettleAsync();
Task Toast = MakeToastAsync();
await Task.WhenAll(kettle, Toast);
sw.Stop();

Console.WriteLine($"Concurrent: {sw.ElapsedMilliseconds}ms");



// Question 4 — WhenAll with results
// Write three methods: FetchUsersAsync, FetchOrdersAsync, and FetchProductsAsync 
// — each simulates a database call with a different delay and returns a fake string 
// result. Use Task.WhenAll to run all three at the same time and print all three
//  results when they're done. How do you get each individual result out of WhenAll?

async Task<string> FetchUsersAsync()
{
    await Task.Delay(1000);
    
}
async Task<string> FetchOrdersAsync()
{
    await Task.Delay(2000);
    
}
async Task<string> FetchProductsAsync()
{
    await Task.Delay(3000);
    
}

// Question 5 — WhenAny for first response wins
// Write two methods: FetchFromServer1Async and FetchFromServer2Async. 
// Make one slower than the other. Use Task.WhenAny to take whichever 
// responds first and print its result. What happens to the slower task — does it stop running?



// Question 6 — Timeout pattern
// Using Task.WhenAny, write a method called FetchWithTimeoutAsync
//  that calls a slow operation but gives it only 2 seconds before 
// declaring "Timed out!". The slow operation should take 5 seconds. 
// How do you detect which one won — the real task or the timeout?


// Question 7 — Basic exception in async
// Write a method FetchDataAsync that throws an InvalidOperationException 
// with the message "Server is down". Wrap your await call in a try/catch 
// in Main. Does the exception surface where you await it, or somewhere else?


// Question 8 — WhenAll and multiple failures
// Write three tasks where two of them throw exceptions and one succeeds.
// Pass all three to Task.WhenAll and catch the exception. How many exceptions
//  do you see by default? How do you inspect each task individually to see all the failures?



// Question 9 — finally in async
// Write a method that opens a "connection" (just prints "Connection opened"), 
// does some async work, then throws an exception. Add a finally block that prints 
// "Connection closed". Does finally still run even when an exception is thrown inside an async method?


// Question 10 — Basic cancellation
// Write a method DoLongWorkAsync(CancellationToken token)
//  that loops 10 times, waits 1 second per iteration, and 
// prints the iteration number. Create a CancellationTokenSource 
// in Main, pass its token to the method, and cancel it after 3 seconds
//  using CancelAfter. What exception is thrown and where do you catch it?



// Question 11 — ThrowIfCancellationRequested
// Write a method that does several steps of work (just Task.Delay calls between them).
//  Between each step, call token.ThrowIfCancellationRequested() manually.
//  Cancel the token before the method is called. Which step does it stop at?



// Question 12 — Passing token through a chain
// Write three methods that call each other: StartProcessAsync 
// calls ProcessStepAsync which calls FetchDataAsync. Each one receives 
// and passes the CancellationToken through. Cancel from Main and observe 
// that the cancellation travels all the way down the chain. What happens 
// if you forget to pass the token to one of the middle methods?





// Question 13 — Retry with cancellation
// Write a method FetchWithRetryAsync(int maxRetries, 
// CancellationToken token) that tries to call a fake API. 
// Make the fake API fail the first two times and succeed on the third.
//  The method should retry up to maxRetries times, waiting 
// 1 second between attempts. If the token is cancelled mid-retry, 
// stop immediately. If all retries are exhausted, throw an exception.



// Question 14 — Concurrent work with cancellation and error handling
// Write 5 tasks that each simulate different jobs with different 
// durations. One of them should throw an exception. 
// Run all 5 with Task.WhenAll, handle the exception from 
// the failing one, and wrap everything in a timeout using Task.WhenAny. 
// If the timeout fires, cancel all remaining work using a CancellationTokenSource.