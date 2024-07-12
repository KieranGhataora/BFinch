// See https://aka.ms/new-console-template for more information

using LendingPlatform.Models;
using LendingPlatform.Repository;
using LendingPlatform.Services;

const string fileName = "applications.json";

var arguments = Environment.GetCommandLineArgs();

var loanValue = decimal.Parse(arguments[1]);
var assetValue = decimal.Parse(arguments[2]);
var creditScore = int.Parse(arguments[3]);

var applicationRepository = new ApplicationRepository(fileName);

var applicationProcessor = new ApplicationProcessor(applicationRepository);

if (creditScore is < 1 or > 999) Console.WriteLine("Please enter a valid credit score.");

var result = applicationProcessor.Process(new Application()
{
    LoanValue = loanValue,
    AssetValue = assetValue,
    CreditScore = creditScore
});

var applications = applicationRepository.GetAllApplications().ToArray();
var successfulApplications = applications.Where(a => a.Success).ToArray();

Console.WriteLine($"Application Result: {result}");
Console.WriteLine($"Total Successful Applications: {successfulApplications.Count()}");
Console.WriteLine($"Total Failed Applications: {applications.Count(a => !a.Success)}" );
Console.WriteLine($"Total Loan Value Written To Date: £{successfulApplications.Sum(ar => ar.Application.LoanValue)}");
Console.WriteLine($"Total Average Mean LTV Percentage: {(successfulApplications.DefaultIfEmpty().Average(ar => ar?.Application.LoanValue / ar?.Application.AssetValue)*100 ?? 0)}%");