/*
 * Solution 2 
 * Employee Hierarchy Parser - DLL
 * 
 * Run the Test explorer to review output
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace Employees
{
    public class Employee
    {
        string empId;
        string managerId;
        int ceoCount = 0;
        int salary;

        //Dictionary to store managers and employees 
        Dictionary<string, List<string>> managers = new Dictionary<string, List<string>>();
        Dictionary<string, Employee> employees = new Dictionary<string, Employee>();


        //Default constructor for Employer
        public Employee()
        {

        }

        //Employee constructor that takes the csv string
        public Employee(string csvString)
        {
            validateCsv(csvString);
        }

        //Method to validate csv string
        public string validateCsv(String csvString)
        {
            string result = "Fail";
            StringReader stringReader = new StringReader(csvString);

            // Given the CSV is in the format employeeId, managerId, salary
            while (stringReader.Peek() > -1)
            {
                String row = stringReader.ReadLine();
                String[] param = row.Split(',');
                int paramLength = param.Length;

                //Set Employee ID 
                empId = param[0];

                //One employee does not report to more than one manager.
                if (paramLength > 3)
                {
                    throw new Exception("Employees should not report to more than one manager");
                }

                //Validate salary as numerals
                salary = validateSalary(param[2]);

                //There is only one CEO, i.e. only one employee with no manager above.
                if (ceoCount > 1)
                {
                    throw new Exception("There should only be one CEO");
                }
                else if (param[1] == "" && ceoCount <= 2)
                {
                    ceoCount++;
                    managerId = param[1];
                }
                else
                {
                    managerId = param[1];
                }
                //Adding managers to managers dictionary
                if (managerId != "")
                {
                    addManager(managerId, empId);
                }

                employees.Add(empId, createEmployee(empId, managerId, salary));

                result = "Success";
            }
            return result;
        }

        //Validate salary to be integers
        public int validateSalary(String salaryString)
        {
            int salary;
            try
            {
                salary = int.Parse(salaryString);
                return salary;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message + "Salaries ought to be a number");
            }

        }

        //One employee does not report to more than one manager.
        //Create new employees
        public Employee createEmployee(String emplID, String manID, int sal)
        {
            Employee employee = new Employee();
            employee.empId = emplID;
            employee.managerId = manID;
            employee.salary = sal;
            return employee;
        }

        //Add manager to the manager dictionary
        public void addManager(String managerID, String employeeID)
        {
            if (managers.ContainsKey(managerID))
            {
                managers[managerID].Add(employeeID);
            }
            else
            {
                List<string> junior = new List<string>() { employeeID };
                managers.Add(managerID, junior);
            }
        }

        //Method to calculate budget for each manager
        public long managerBudget(String employeeID)
        {
            long budget = 0;
            //employee who is not a manager
            if (!managers.ContainsKey(employeeID))
            {
                //Base case
                budget = employees[employeeID].salary;
            }
            else
            {
                
                foreach (string manager in managers[employeeID])
                {
                    budget += managerBudget(manager);
                }
                budget += employees[employeeID].salary;
            }
            return budget;
        }

    }
}
