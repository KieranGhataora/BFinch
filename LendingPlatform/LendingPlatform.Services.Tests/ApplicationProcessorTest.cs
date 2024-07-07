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
}