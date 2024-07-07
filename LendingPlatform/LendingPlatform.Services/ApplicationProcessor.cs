namespace LendingPlatform.Services;

public class ApplicationProcessor
{
    private const decimal MaxLoan = 1500000m;
    private const decimal MinLoan = 100000m;
    private const decimal MinLtvAboveOneMillion = 0.6m;
    private const int MinCreditScoreAboveOneMillion = 950;

    public bool Process(Application application)
    {
        if (application.LoanValue is > MaxLoan or < MinLoan) return false;
        
        var ltv = application.LoanValue / application.AssetValue;
     
        if (application.LoanValue >= 1000000)
            if (ltv > MinLtvAboveOneMillion || application.CreditScore < MinCreditScoreAboveOneMillion)
                return false;

        return true;
    }
}