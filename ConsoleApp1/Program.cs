using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;

class Department
{
    public string ID_DE {  get; set; }
    public string Name { get; set; }

    public Department(string id_de,string name )
    {
        Name = name;
        ID_DE = id_de;
    }
}

class Employee
{
    public string Name { get; set; }
    public string ID_DE { get; set; }
    public int Age { get { return DateTime.Now.Year - Birthday.Year; } }
    public int Salary { get; set; }
    public DateOnly Birthday { get; set; }

    public Employee(string name, string id_de, int salary, DateOnly birthday)
    {
        Name = name;
        ID_DE = id_de;
        Salary = salary;
        Birthday = birthday;
    }

}

class Program
{
    static void Main()
    {
        List<Department> departments = new List<Department>
        {
            new Department ("HR","Sales" ),
            new Department ("IT","Main" ),
            new Department ("CP","Control Panel" ),
        };

        List<Employee> employees = new List<Employee>
        {
            new Employee("Nguyen Van A","HR",  5000, new DateOnly(1990, 1, 1)),
            new Employee("Tran Thi B","HR",  6000, new DateOnly(1985, 2, 2)),
            new Employee("Le Van C","CP",  7000, new DateOnly(1980, 3, 3))
        };

        // 1. In ra max, min, average của salary
        int maxSalary = employees.Max(e => e.Salary);
        int minSalary = employees.Min(e => e.Salary);
        double averageSalary = employees.Average(e => e.Salary);

        Console.WriteLine($"Max Salary: {maxSalary}");
        Console.WriteLine($"Min Salary: {minSalary}");
        Console.WriteLine($"Average Salary: {averageSalary}");

        // 2. Sử dụng câu lệnh left join
        var result = from e in employees
                     join d in departments on e.ID_DE equals d.ID_DE into deptEmp
                     from subDept in deptEmp.DefaultIfEmpty()
                     select new
                     {
                         EmployeeName = e.Name,
                         DepartmentName = subDept?.Name ?? string.Empty
                     };

        foreach (var item in result)
        {
            Console.WriteLine($"Employee: {item.EmployeeName}, Department: {item.DepartmentName}");
        }


        // 3. In ra nhân viên có tuổi cao nhất và thấp nhất dùng max, min
        int maxAge = employees.Max(e => e.Age);
        int minAge = employees.Min(e => e.Age);

        Employee oldestEmployee = employees.First(e => e.Age == maxAge);
        Employee youngestEmployee = employees.First(e => e.Age == minAge);

        Console.WriteLine($"Oldest Employee: {oldestEmployee.Name}, Age: {oldestEmployee.Age}");
        Console.WriteLine($"Youngest Employee: {youngestEmployee.Name}, Age: {youngestEmployee.Age}");
    }
}