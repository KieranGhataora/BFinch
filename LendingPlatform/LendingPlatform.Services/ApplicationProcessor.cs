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

        if (application.LoanValue < 1000000)
            return ltv switch
            {
                < 0.6m => application.CreditScore >= 750,
                < 0.8m => application.CreditScore >= 800,
                < 0.9m => application.CreditScore >= 900,
                _ => false
            };            
        
        return true;
    }
}