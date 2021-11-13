namespace FluentBuilderPatternWithRecursiveGenerics
{
    public class EmployeeInfoBuilder<T>: EmployeeBuilder where T: EmployeeInfoBuilder<T>
    {
        protected Employee employee = new Employee();

        public T SetName(string name)
        {
            employee.Name = name;

            return (T)this;
        }
    }
}