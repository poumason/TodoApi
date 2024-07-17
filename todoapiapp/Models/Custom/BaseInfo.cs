namespace TodoApi.Models
{
    public abstract class BaseInfo
    {
        public string UserName { get; set; }

        public string Old { get; set; }
    }

    public class Student : BaseInfo
    {
        public string School { get; set; }

        public string Dept { get; set; }
    }

    public class Employee : BaseInfo
    {
        public string Company { get; set; }
        public string Salary { get; set; }
    }
}