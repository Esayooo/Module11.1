using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module11._2
{
    class Student
    {
        public string FullName { get; set; }
        public string Group { get; set; }
        public double AverageGrade { get; set; }
        public double FamilyIncomePerPerson { get; set; }
        public int FamilyMembers { get; set; }
        public List<string> GenderList { get; set; }
        public string TrainingForm { get; set; }

        public override string ToString()
        {
            return $"Name: {FullName}, Group: {Group}, Average Grade: {AverageGrade}, Family Income per Person: {FamilyIncomePerPerson}, Family Members: {FamilyMembers}, Gender: {string.Join(", ", GenderList)}, Training Form: {TrainingForm}";
        }
    }

    class Program
    {
        static void Main()
        {
            List<Student> students = GetStudentData();

            // 1. 提供宿舍的学生列表
            var dormitoryList = students
                .Where(s => s.FamilyIncomePerPerson < 2 * MinimumWageStandard)
                .OrderByDescending(s => s.AverageGrade)
                .ToList();

            Console.WriteLine("1. Dormitory list for students with family income below 2 times the minimum wage:");
            PrintStudentList(dormitoryList);

            // 2. 宿舍提供宿位的优先顺序列表
            var priorityDormitoryList = students
                .OrderByDescending(s => s.AverageGrade)
                .Select((student, index) => new { Student = student, Index = index + 1 })
                .GroupBy(item => (item.Index - 1) / 10)
                .SelectMany(group => group.Select(item => new { Student = item.Student, Priority = group.Key + 1 }))
                .ToList();

            Console.WriteLine("\n2. Priority dormitory list:");
            

            // 3. 拥有单一家庭的学生列表
            var singleFamilyStudents = students
                .Where(s => s.FamilyMembers == 1)
                .ToList();

            Console.WriteLine("\n3. Students with a single-family:");
            PrintStudentList(singleFamilyStudents);

            // 4. 成绩最高的10名学生
            var top10Students = students
                .OrderByDescending(s => s.AverageGrade)
                .Take(10)
                .ToList();

            Console.WriteLine("\n4. Top 10 outstanding students:");
            PrintStudentList(top10Students);

            // 5. 成绩最低的10名学生
            var bottom10Students = students
                .OrderBy(s => s.AverageGrade)
                .Take(10)
                .ToList();

            Console.WriteLine("\n5. Bottom 10 outstanding students:");
            PrintStudentList(bottom10Students);

            // 6. 剔除家庭收入最低、家庭不健全的3名学生
            var filteredStudents = students
                .OrderByDescending(s => s.AverageGrade)
                .Where((s, index) => s.FamilyIncomePerPerson >= 2 * MinimumWageStandard || s.FamilyMembers > 1 || index < 3)
                .ToList();

            Console.WriteLine("\n6. Filtered students (Excluding lowest income and incomplete family):");
            PrintStudentList(filteredStudents);

            Console.ReadLine();
        }

        const double MinimumWageStandard = 1000; 

        static List<Student> GetStudentData()
        {
            return new List<Student>
        {
            new Student { FullName = "John", Group = "A", AverageGrade = 85, FamilyIncomePerPerson = 1500, FamilyMembers = 4, GenderList = new List<string>{"Male"}, TrainingForm = "Full-Time" },
            new Student { FullName = "Jane", Group = "B", AverageGrade = 92, FamilyIncomePerPerson = 1200, FamilyMembers = 1, GenderList = new List<string>{"Female"}, TrainingForm = "Full-Time" },
            new Student { FullName = "Anna", Group = "A", AverageGrade = 90, FamilyIncomePerPerson = 1200, FamilyMembers = 1, GenderList = new List<string>{"Female"}, TrainingForm = "Full-Time" },
            new Student { FullName = "Peter", Group = "A", AverageGrade = 82, FamilyIncomePerPerson = 1200, FamilyMembers = 1, GenderList = new List<string>{"Male"}, TrainingForm = "Full-Time" },
            new Student { FullName = "Emi", Group = "C", AverageGrade = 80, FamilyIncomePerPerson = 1200, FamilyMembers = 1, GenderList = new List<string>{"Female"}, TrainingForm = "Full-Time" },
            new Student { FullName = "Roxy", Group = "A", AverageGrade = 72, FamilyIncomePerPerson = 1200, FamilyMembers = 1, GenderList = new List<string>{"Female"}, TrainingForm = "Full-Time" },
            new Student { FullName = "Mika", Group = "C", AverageGrade = 82, FamilyIncomePerPerson = 1200, FamilyMembers = 1, GenderList = new List<string>{"Female"}, TrainingForm = "Full-Time" },
            new Student { FullName = "Bob", Group = "D", AverageGrade = 88, FamilyIncomePerPerson = 1200, FamilyMembers = 1, GenderList = new List<string>{"Male"}, TrainingForm = "Full-Time" },
            new Student { FullName = "Candy", Group = "C", AverageGrade = 95, FamilyIncomePerPerson = 1200, FamilyMembers = 1, GenderList = new List<string>{"Female"}, TrainingForm = "Full-Time" },
            new Student { FullName = "Lisa", Group = "D", AverageGrade = 92, FamilyIncomePerPerson = 1200, FamilyMembers = 1, GenderList = new List<string>{"Female"}, TrainingForm = "Full-Time" },

        };
        }

        static void PrintStudentList(List<Student> students)
        {
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine();
        }

        static void PrintPriorityDormitoryList(List<dynamic> dormitoryList)
        {
            foreach (var item in dormitoryList)
            {
                var color = item.Priority <= 2 ? ConsoleColor.Green : (item.Priority <= 4 ? ConsoleColor.Yellow : ConsoleColor.Red);
                Console.ForegroundColor = color;
                Console.WriteLine($"Priority: {item.Priority}, {item.Student}");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }

}
