namespace LendingPlatform.Services;

public class ApplicationProcessor
{
    private const decimal MaxLoan = 1500000m;
    private const decimal MinLoan = 100000m;

    public bool Process(Application application)
    {
        if (application.LoanValue is > MaxLoan or < MinLoan) return false;

        return true;
    }
}