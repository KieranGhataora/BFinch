using System.Text.Json;
using LendingPlatform.Models;

namespace LendingPlatform.Repository;

public class ApplicationRepository: IApplicationRepository
{
    private readonly string _fileName;

    public ApplicationRepository(string fileName)
    {
        this._fileName = fileName;
        if (!File.Exists(_fileName))
        {
            File.Create(_fileName);
        }
    }

    public void InsertApplication(Application application, bool success)
    {
        var applications = GetAllApplications();
        
        applications = applications.Append(new ApplicationResult
        {
            Application = application,
            Success = success
        });

        File.WriteAllText(_fileName, JsonSerializer.Serialize(applications.ToArray()));
    }

    public IEnumerable<ApplicationResult> GetAllApplications()
    {
        try
        {
            return JsonSerializer.Deserialize<IEnumerable<ApplicationResult>>(File.ReadAllText(_fileName))!;
        }
        catch (Exception e)
        {
            return new List<ApplicationResult>();
        }
    }
}