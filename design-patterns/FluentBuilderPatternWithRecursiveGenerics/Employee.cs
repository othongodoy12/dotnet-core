namespace FluentBuilderPatternWithRecursiveGenerics
{
    public class Employee
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }

        public override string ToString() => 
            $"Name: {Name}, Position: {Position}, Salary: {Salary}";
    }
}