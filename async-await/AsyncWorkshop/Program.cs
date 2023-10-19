await AsyncTimeout(1000);

var five1 = AsyncTimeout(5000);
var five2 = AsyncTimeout(5000);

await five1;
await five2;

var ten1 = AsyncTimeout(10000);
var ten2 = AsyncTimeout(10000);
var ten3 = AsyncTimeout(10000);

await Task.WhenAll([ten1, ten2, ten3]);

var three = AsyncTimeout(3000);
var six = AsyncTimeout(6000);

await Task.WhenAny([three, six]);

var sync1 = SyncTimeout(5000);
var sync2 = SyncTimeout(5000);

await sync1;
await sync2;

var sync3 = SyncTimeoutYield(5000);
var sync4 = SyncTimeoutYield(5000);

await sync3;
await sync4;

Console.WriteLine("Done");

async Task AsyncTimeout(int timeout)
{
    Console.WriteLine($"Waiting {timeout}ms");
    await Task.Delay(timeout);
    Console.WriteLine($"Waited {timeout}ms");
}

async Task SyncTimeout(int timeout)
{
    Console.WriteLine($"Waiting {timeout}ms");
    Thread.Sleep(timeout);
    Console.WriteLine($"Waited {timeout}ms");
}

async Task SyncTimeoutYield(int timeout)
{
    await Task.Yield();

    Console.WriteLine($"Waiting {timeout}ms");
    Thread.Sleep(timeout);
    Console.WriteLine($"Waited {timeout}ms");
}