using System.Globalization;

var str = "Ações Internacionais";

str.ToLowerInvariant().Dump();
str.ToLower().Dump();


RemoveDiacritics(str).ToLower().Dump();

static string RemoveDiacritics(string text) 
{
    var normalizedString = text.Normalize(NormalizationForm.FormD);
    var stringBuilder = new StringBuilder();

    foreach (var c in normalizedString)
    {
        var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
        if (unicodeCategory != UnicodeCategory.NonSpacingMark)
        {
            stringBuilder.Append(c);
        }
    }

    return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
}