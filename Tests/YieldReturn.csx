using System.Diagnostics;

public IEnumerable<int> V1()
{
    for (var i = 0; i < 10000; i++)
    {
        yield return i;
    }
}

public IEnumerable<int> V2()
{
    var list = new List<int>();

    for (var i = 0; i < 10000; i++)
    {
        list.Add(i);        
    }
    
    return list;
}

var sw = Stopwatch.StartNew();

V1().ToList();

sw.Stop();

sw.Elapsed.Dump();

sw.Reset();
sw.Start();

V2().ToList();

sw.Stop();

sw.Elapsed.Dump();


public IEnumerable<int> Ola()
{
    for (var i = 0; i < 100; i++)
    {
        System.Threading.Thread.Sleep(10);
        Console.WriteLine($"Fornecido número: {i}");
        yield return i;
    }
}

var a = Ola().ToList();

foreach (var i in a.Where(w =>
{
    Console.WriteLine($"Validando numero {w}");
    return w % 2 == 0; 
})
.Take(3))
{
    Console.WriteLine(i);
}