using System.Diagnostics;

var folder = Path.GetDirectoryName(@"C:\dev\your-repo\");
var filePatten = @"\.((cs)|(ts)|(js)|(html))$";
var searchPatten = @"(<table)(.|\n)+(table>)";

var fileRegex = new Regex(filePatten, RegexOptions.Compiled);
var searchRegex = new Regex(searchPatten, RegexOptions.Compiled | RegexOptions.IgnoreCase);
var timer = new Stopwatch();

timer.Start();

foreach (string file in Directory
    .GetFiles(folder, "*.*", SearchOption.AllDirectories)
    .Where(file => fileRegex.IsMatch(file))
    .Where(fileName => searchRegex.IsMatch(File.ReadAllText(Path.Combine(folder, fileName)))))
{
    var lines = File.ReadAllLines(file);
    
    var linesMatches = Enumerable.Range(0, lines.Count())
             .Where(i => searchRegex.IsMatch(lines[i]))
             .ToList();
    
    foreach (var lineMatch in linesMatches)
    {
        $"{file}:{lineMatch + 1}".Dump();
    }
}

$"Time elapsed: {timer.Elapsed}".Dump();