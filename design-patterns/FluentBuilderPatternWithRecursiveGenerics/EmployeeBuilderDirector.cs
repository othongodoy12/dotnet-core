namespace FluentBuilderPatternWithRecursiveGenerics
{
    public class EmployeeBuilderDirector : EmployeeSalaryBuilder<EmployeeBuilderDirector>
    {
        public static EmployeeBuilderDirector NewEmployee => 
            new EmployeeBuilderDirector();
    }
}