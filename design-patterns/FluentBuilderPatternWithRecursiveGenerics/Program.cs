using System;

namespace FluentBuilderPatternWithRecursiveGenerics
{
    class Program
    {
        static void Main(string[] args)
        {
            var emp = EmployeeBuilderDirector
                .NewEmployee
                .SetName("Maks")
                .AtPosition("Software Developer")
                .WithSalary(3500)
                .Build();
                
            Console.WriteLine(emp);
        }
    }
}
