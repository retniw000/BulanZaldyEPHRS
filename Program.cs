using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagementSystem
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public decimal MonthlySalary { get; set; }
        public double TaxRate { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; } = string.Empty;
    }

    class Program
    {
        static List<Employee> employees = new List<Employee>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n--- EPHRS - MAIN BRANCH ---");
                Console.WriteLine("1. Add Employee (Create)");
                Console.WriteLine("2. View All (Read)");
                Console.WriteLine("3. Search/Filter");
                Console.WriteLine("4. Update Employee");
                Console.WriteLine("5. Delete Employee");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");
                
                string choice = Console.ReadLine() ?? string.Empty;
                switch (choice)
                {
                    case "1": CreateEmployee(); break;
                    case "2": ReadEmployees(); break;
                    case "3": SearchEmployees(); break;
                    case "4": UpdateEmployee(); break;
                    case "5": DeleteEmployee(); break;
                    case "6": return;
                    default: Console.WriteLine("Invalid option."); break;
                }
            }
        }

        static void CreateEmployee()
        {
            Employee emp = new Employee();
            emp.Id = employees.Count == 0 ? 1001 : employees.Max(e => e.Id) + 1;

            emp.FirstName = ReadString("First Name: ");
            emp.LastName = ReadString("Last Name: ");
            emp.Department = ReadString("Department: ");
            emp.JobTitle = ReadString("Job Title: ");
            emp.MonthlySalary = ReadDecimal("Monthly Salary: ");
            emp.TaxRate = ReadDouble("Tax Rate (e.g., 0.12): ");
            emp.HireDate = ReadDate("Hire Date (yyyy-mm-dd): ");
            emp.IsActive = ReadBool("Is Active (true/false): ");
            emp.Email = ReadString("Email: ");

            employees.Add(emp);
            Console.WriteLine($"Employee added successfully with ID: {emp.Id}");
        }

        static void ReadEmployees()
        {
            if (!employees.Any())
            {
                Console.WriteLine("No records found.");
                return;
            }
            foreach (var e in employees)
            {
                Console.WriteLine($"ID: {e.Id} | Name: {e.FirstName} {e.LastName} | Dept: {e.Department} | Title: {e.JobTitle} | Salary: {e.MonthlySalary} | Active: {e.IsActive}");
            }
        }

        static void SearchEmployees()
        {
            Console.Write("Enter Department to filter by (or leave blank): ");
            string dept = Console.ReadLine() ?? string.Empty;
            
            var results = employees.Where(e => string.IsNullOrEmpty(dept) || e.Department.Equals(dept, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!results.Any())
            {
                Console.WriteLine("No matching employees found.");
                return;
            }
            foreach (var e in results)
            {
                Console.WriteLine($"ID: {e.Id} | Name: {e.FirstName} {e.LastName} | Dept: {e.Department}");
            }
        }

        static void UpdateEmployee()
        {
            int id = ReadInt("Enter Employee ID to update: ");
            var emp = employees.FirstOrDefault(e => e.Id == id);

            if (emp == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            emp.JobTitle = ReadString("New Job Title: ");
            emp.MonthlySalary = ReadDecimal("New Monthly Salary: ");
            Console.WriteLine("Employee updated successfully.");
        }

        static void DeleteEmployee()
        {
            int id = ReadInt("Enter Employee ID to delete: ");
            var emp = employees.FirstOrDefault(e => e.Id == id);

            if (emp != null)
            {
                employees.Remove(emp);
                Console.WriteLine("Employee deleted successfully.");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }

        // ---- Safe input helpers: re-prompt until the input is valid ----
        static string ReadString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = (Console.ReadLine() ?? string.Empty).Trim();
                if (input.Length > 0) return input;
                Console.WriteLine("Value cannot be empty. Please try again.");
            }
        }

        static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value)) return value;
                Console.WriteLine("Invalid number. Please try again.");
            }
        }

        static decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out decimal value) && value >= 0) return value;
                Console.WriteLine("Invalid amount. Please enter a non-negative number.");
            }
        }

        static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double value) && value >= 0 && value <= 1) return value;
                Console.WriteLine("Invalid rate. Please enter a value between 0 and 1 (e.g., 0.12).");
            }
        }

        static DateTime ReadDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (DateTime.TryParse(Console.ReadLine(), out DateTime value)) return value;
                Console.WriteLine("Invalid date. Please use the format yyyy-mm-dd.");
            }
        }

        static bool ReadBool(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (bool.TryParse(Console.ReadLine(), out bool value)) return value;
                Console.WriteLine("Please enter true or false.");
            }
        }
    }
}