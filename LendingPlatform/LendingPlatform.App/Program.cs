// See https://aka.ms/new-console-template for more information

using LendingPlatform.Services;

var arguments = Environment.GetCommandLineArgs();

var loanValue = decimal.Parse(arguments[1]);
var assetValue = decimal.Parse(arguments[2]);
var creditScore = int.Parse(arguments[3]);
var applicationProcessor = new ApplicationProcessor();

if (creditScore is < 1 or > 999) Console.WriteLine("Please enter a valid credit score.");

var result = applicationProcessor.Process(new Application()
{
    LoanValue = loanValue,
    AssetValue = assetValue,
    CreditScore = creditScore
});

Console.WriteLine($"Application Result: {result}");
