var folderPath = @"C:\Users\your-user\Desktop\";

var text = File.ReadAllText(Path.Combine(folderPath, "input.txt"));

var regex = new Regex(@""".*""");

var matches = regex.Matches(text);

var output = String.Empty;

foreach (var match in matches)
{
    output += match.ToString().Replace('"', ' ') + "\n";
}

File.WriteAllText(Path.Combine(folderPath, "output.txt"), output);