// See https://aka.ms/new-console-template for more information

using System.Text.Json;

var arguments = Environment.GetCommandLineArgs();

var loanValue = decimal.Parse(arguments[1]);
var assetValue = decimal.Parse(arguments[2]);
var creditScore = int.Parse(arguments[3]);

Console.WriteLine(loanValue);
Console.WriteLine(assetValue);
Console.WriteLine(creditScore);