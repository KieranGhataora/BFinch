using LendingPlatform.Models;
using LendingPlatform.Repository;
using LendingPlatform.Services;
using Moq;

namespace LendingPlatform.Services.Tests;

public class ApplicationProcessorTest
{
    private ApplicationProcessor GetApplicationProcessor()
    {
        return new ApplicationProcessor(
            new Mock<IApplicationRepository>().Object);
    }
    
    [Theory]
    [InlineData(1500001, false)]
    [InlineData(1500000, true)]
    [InlineData(1499999, true)]
    [InlineData(99999, false)]
    [InlineData(100000, true)]
    [InlineData(100001, true)]
    public void ApplicationProcessor_ReturnsCorrectResult_IfLoanAmountIsTooBigOrSmall(decimal loanAmount,
        bool success)
    {
        var applicationProcessor = GetApplicationProcessor();

        var applicationResult = applicationProcessor.Process(new Application
        {
            LoanValue = loanAmount,
            AssetValue = loanAmount / 0.5m,
            CreditScore = 999
        });

        Assert.Equal(success, applicationResult);
    }
    
    [Theory]
    [InlineData(0.6, true)]
    [InlineData(0.59, true)]
    [InlineData(0.61, false)]
    public void ApplicationProcessor_ReturnsCorrectResult_IfLTVCriteriaNotMet_AboveOneMillion(decimal ltv, bool success)
    {
        const decimal loanAmount = 1000000;

        var application = new Application
        {
            LoanValue = loanAmount,
            AssetValue = loanAmount/ ltv,
            CreditScore = 950
        };

        var applicationProcessor = GetApplicationProcessor();

        var result = applicationProcessor.Process(application);

        Assert.Equal(success, result);
    }

    [Theory]
    [InlineData(950, true)]
    [InlineData(951, true)]
    [InlineData(949, false)]
    public void ApplicationProcessor_ReturnsCorrectResult_IfCreditScoreCriteriaNotMet_AboveOneMillion(int creditScore,
        bool success)
    {
        const decimal loanAmount = 1000000;

        var application = new Application
        {
            LoanValue = loanAmount,
            AssetValue = loanAmount / 0.5m,
            CreditScore = creditScore
        };

        var applicationProcessor = GetApplicationProcessor();

        var result = applicationProcessor.Process(application);

        Assert.Equal(success, result);
    }
    
    [Theory]
    [InlineData(0.59, 750, true)]
    [InlineData(0.59, 749, false)]
    [InlineData(0.6, 750, false)]
    [InlineData(0.79, 800, true)]
    [InlineData(0.79, 799, false)]
    [InlineData(0.80, 800, false)]
    [InlineData(0.89, 900, true)]
    [InlineData(0.89, 899, false)]
    [InlineData(0.90, 800, false)]
    public void ApplicationProcessor_ReturnsCorrectResult_IfLTVAndCreditScoreCriteriaAreNotMet_UnderOneMillion(
        decimal ltv, int creditScore, bool success)
    {
        const decimal loanAmount = 999999;

        var application = new Application
        {
            LoanValue = loanAmount,
            AssetValue = loanAmount / ltv,
            CreditScore = creditScore
        };

        var applicationProcessor = GetApplicationProcessor();

        var result = applicationProcessor.Process(application);

        Assert.Equal(success, result);
    }
}