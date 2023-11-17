using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module11._1
{
    interface IEmployee
    {
        string Position { get; }
        string FullName { get; }
        DateTime HireDate { get; }
        decimal Salary { get; }
        Gender Gender { get; }
    }
    enum Gender
    {
        Male,
        Female
    }
    struct Employee : IEmployee
    {
        public string Position { get; set; }
        public string FullName { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public Gender Gender { get; set; }

        public override string ToString()
        {
            return $"Position: {Position}, Name: {FullName}, Hire Date: {HireDate.ToShortDateString()}, Salary: {Salary:C}, Gender: {Gender}";
        }
    }

    class Program
    {
        static void Main()
        {
            Console.Write("Enter the number of employees: ");
            int numEmployees = int.Parse(Console.ReadLine());

            IEmployee[] employees = new IEmployee[numEmployees];

            for (int i = 0; i < numEmployees; i++)
            {
                Console.WriteLine($"Enter details for employee {i + 1}:");

                employees[i] = new Employee
                {
                    Position = GetInput("Position"),
                    FullName = GetInput("Full Name"),
                    HireDate = DateTime.Parse(GetInput("Hire Date (YYYY-MM-DD)")),
                    Salary = decimal.Parse(GetInput("Salary")),
                    Gender = (Gender)Enum.Parse(typeof(Gender), GetInput("Gender (Male/Female)"))
                };
            }

            Console.WriteLine("a. All employees:");
            PrintEmployees(employees);

            string selectedPosition = GetInput("b. Enter position to filter employees");
            PrintEmployeesByPosition(employees, selectedPosition);

            PrintManagersAboveClerkAverageSalary(employees);

            DateTime filterDate = DateTime.Parse(GetInput("d. Enter date to filter employees hired later (YYYY-MM-DD)"));
            PrintEmployeesHiredAfterDate(employees, filterDate);

            string selectedGender = GetInput("e. Enter gender to filter employees (Male/Female/All)");
            PrintEmployeesByGender(employees, selectedGender);

            Console.ReadLine();
        }

        static void PrintEmployees(IEmployee[] employees)
        {
            foreach (var employee in employees)
            {
                Console.WriteLine(employee);
            }
            Console.WriteLine();
        }

        static void PrintEmployeesByPosition(IEmployee[] employees, string position)
        {
            foreach (var employee in employees)
            {
                if (employee.Position.Equals(position, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(employee);
                }
            }
            Console.WriteLine();
        }

        static void PrintManagersAboveClerkAverageSalary(IEmployee[] employees)
        {
            var clerks = employees.Where(e => e.Position.Equals("clerk", StringComparison.OrdinalIgnoreCase)).ToList();
            decimal averageClerkSalary = clerks.Any() ? clerks.Average(c => c.Salary) : 0;

            var managers = employees.Where(e => e.Position.Equals("manager", StringComparison.OrdinalIgnoreCase) && e.Salary > averageClerkSalary)
                                    .OrderBy(e => e.FullName);

            Console.WriteLine("c. Managers with salary above average clerk salary:");
            foreach (var manager in managers)
            {
                Console.WriteLine(manager);
            }
            Console.WriteLine();
        }

        static void PrintEmployeesHiredAfterDate(IEmployee[] employees, DateTime filterDate)
        {
            var filteredEmployees = employees.Where(e => e.HireDate > filterDate).OrderBy(e => e.FullName);

            Console.WriteLine("d. Employees hired after specified date:");
            foreach (var employee in filteredEmployees)
            {
                Console.WriteLine(employee);
            }
            Console.WriteLine();
        }

        static void PrintEmployeesByGender(IEmployee[] employees, string gender)
        {
            var filteredEmployees = employees;

            if (!string.Equals(gender, "All", StringComparison.OrdinalIgnoreCase))
            {
                Gender selectedGender = (Gender)Enum.Parse(typeof(Gender), gender);
                filteredEmployees = employee.Where(e => e.Gender == selectedGender);
            }

            Console.WriteLine("e. Employees filtered by gender:");
            foreach (var employee in filteredEmployees)
            {
                Console.WriteLine(employee);
            }
        }

        static string GetInput(string prompt)
        {
            Console.Write($"{prompt}: ");
            return Console.ReadLine();
        }
    }


}
