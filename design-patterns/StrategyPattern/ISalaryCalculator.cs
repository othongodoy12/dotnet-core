using System.Collections.Generic;

namespace StrategyPattern
{
    public interface ISalaryCalculator
    {
        double CalculateTotalSalary(IEnumerable<DeveloperReport> reports);
    }
}