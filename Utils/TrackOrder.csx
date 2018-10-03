#r "$NuGet\System.Net.Http\4.3.3\lib\net46\System.Net.Http.dll"
#r "$NuGet\System.Security.Cryptography.Algorithms\4.3.0\lib\net46\System.Security.Cryptography.Algorithms.dll"
#r "$NuGet\System.Security.Cryptography.Encoding\4.3.0\lib\net46\System.Security.Cryptography.Encoding.dll"
#r "$NuGet\System.Security.Cryptography.Primitives\4.3.0\lib\net46\System.Security.Cryptography.Primitives.dll"
#r "$NuGet\System.Security.Cryptography.X509Certificates\4.3.0\lib\net46\System.Security.Cryptography.X509Certificates.dll"

using System.Diagnostics;
using System.Net.Http;

var result = String.Empty;
var client = new HttpClient();
var timeStamp = new Stopwatch();
var filePath = $@"{Path.GetTempPath()}\trackOrder.html";

var regexTrack = new Regex(@"(<table)(.|\n)+(table>)", RegexOptions.IgnoreCase);

var objects = new Dictionary<string, string>
{
    {"Carteira Àgora", "JT719439305BR"},
    {"SSD Kingston", "LP00109961041371"},
    //{"Teste", "AA100833276BR"},
};

foreach (var obj in objects)
{
    var content = new FormUrlEncodedContent(new Dictionary<string, string> 
    {
        {"acao", "track"},
        {"objetos", obj.Value},
        {"btnPesq", "Buscar"},
    });
    
    var response = await client.PostAsync("https://www2.correios.com.br/sistemas/rastreamento/ctrl/ctrlRastreamento.cfm?", content);
    var responseText = await response.Content.ReadAsStringAsync();
    
    var resultTrack = regexTrack.Match(responseText);
    var resultObj = String.Empty;

    if (resultTrack.Success)
    {
        resultObj = Regex.Replace(resultTrack.Value, @"<br.+?>", "", RegexOptions.IgnoreCase) ?? "Objeto não encontrado";
    }
    else
    {
        resultObj = "<p>Objeto não encontrado</p>";
    }
    
    
    result += $"<h3>{obj.Key}</h3>{resultObj}<hr />";
}

File.WriteAllText(filePath, result);
System.Diagnostics.Process.Start(filePath);
$"{timeStamp.Elapsed}".Dump();