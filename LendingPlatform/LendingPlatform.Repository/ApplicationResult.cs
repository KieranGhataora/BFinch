using LendingPlatform.Models;

namespace LendingPlatform.Repository;

public class ApplicationResult
{
    public Application Application { get; set; }
    public bool Success { get; set; }
}