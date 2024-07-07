using LendingPlatform.Services;

namespace LendingPlatform.Services.Tests;

public class ApplicationProcessorTest
{
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
        var applicationProcessor = new ApplicationProcessor();

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

        var applicationProcessor = new ApplicationProcessor();

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

        var applicationProcessor = new ApplicationProcessor();

        var result = applicationProcessor.Process(application);

        Assert.Equal(success, result);
    }

}