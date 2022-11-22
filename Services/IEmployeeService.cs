using Sqli.Models;

namespace Sqli.Services
{
    public interface IEmployeeService
    {
        EmployeeModel[] GetByName(string namePattern);
        void ResetDatabase();
    }
}