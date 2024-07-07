using LendingPlatform.Models;

namespace LendingPlatform.Repository;

public interface IApplicationRepository
{
    public void InsertApplication(Application application, bool success);
    public IEnumerable<ApplicationResult> GetAllApplications();
}