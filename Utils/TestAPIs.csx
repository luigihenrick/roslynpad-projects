#r "$NuGet\System.Net.Http\4.3.3\lib\net46\System.Net.Http.dll"
#r "$NuGet\System.Security.Cryptography.Algorithms\4.3.0\lib\net46\System.Security.Cryptography.Algorithms.dll"
#r "$NuGet\System.Security.Cryptography.Encoding\4.3.0\lib\net46\System.Security.Cryptography.Encoding.dll"
#r "$NuGet\System.Security.Cryptography.Primitives\4.3.0\lib\net46\System.Security.Cryptography.Primitives.dll"
#r "$NuGet\System.Security.Cryptography.X509Certificates\4.3.0\lib\net46\System.Security.Cryptography.X509Certificates.dll"

using System.Diagnostics;
using System.Net.Http;

const string swaggerPrefix = "http://swagger:9999/api/v1/";
const string tonePath = @"C:\Users\YourUser\Desktop\ClassicTone1_EU.wav";
var client = new HttpClient();
var timeStamp = new Stopwatch();

while (true)
{
    timeStamp.Restart();
    
    TestaApi(new string[] 
    {
        "controller1/method1?param1=1234&param2=6789",
        "controller2/method2?param1=1234&param2=6789",
    });
    
    Thread.Sleep(30000);
}

public async void TestaApi(string[] urls) 
{
    List<Task<HttpResponseMessage>> reqs = new List<Task<HttpResponseMessage>>();

    foreach (var url in urls)
    {            
        reqs.Add(client.GetAsync(new Uri(Path.Combine(swaggerPrefix, url))));
    }
    
    while (reqs.Count > 0)
    {
        //$"ReqsCount: {reqs.Count}".Dump();
        var firstFinishedTask = await Task.WhenAny(reqs).Dump();
        
        reqs.Remove(firstFinishedTask);
        
        var res = await firstFinishedTask;
        
        $"[{DateTime.Now}] Status result: {((int)res.StatusCode)};TimeElapsed: {timeStamp.Elapsed}".Dump();
        
        if((int)(res.StatusCode) != 200)
        {
            $"URL: {res.RequestMessage.RequestUri}".Dump();
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(tonePath);
            player.Play();
        }
    }
}