var repository = @"C:\User\your-user\";
var invoicePath = @"Desktop\file.pdf";

var path = Path.Combine(repository, invoicePath).Dump();
Path.GetDirectoryName(path).Dump();
Path.GetFileName(path).Dump();
Path.GetExtension(path).Dump();