using LendingPlatform.Models;
using LendingPlatform.Repository;

namespace LendingPlatform.Services;

public class ApplicationProcessor(IApplicationRepository applicationRepository)
{
    private const decimal MaxLoan = 1500000m;
    private const decimal MinLoan = 100000m;
    private const decimal MinLtvAboveOneMillion = 0.6m;
    private const int MinCreditScoreAboveOneMillion = 950;

    public bool Process(Application application)
    {
        var applicationResult = true;
        
        if (application.LoanValue is > MaxLoan or < MinLoan) applicationResult = false;
        else
        {
            var ltv = application.LoanValue / application.AssetValue;
     
            switch (application.LoanValue)
            {
                case >= 1000000:
                {
                    if (ltv > MinLtvAboveOneMillion || application.CreditScore < MinCreditScoreAboveOneMillion)
                        applicationResult = false;
                    break;
                }
                case < 1000000:
                    applicationResult = ltv switch
                    {
                        < 0.6m => application.CreditScore >= 750,
                        < 0.8m => application.CreditScore >= 800,
                        < 0.9m => application.CreditScore >= 900,
                        _ => false
                    };
                    break;
            }
        }
        
        applicationRepository.InsertApplication(application, applicationResult);
        
        return applicationResult;
    }
}