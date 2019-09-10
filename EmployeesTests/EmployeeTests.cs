/*
 * Solution 2 
 * Employee DLL Tester
 *
 *
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Tests
{
    [TestClass()]
    public class EmployeeTests
    {

        
        Employee employees = new Employee();

        //Prefered for optimium performance on dataset
        StringBuilder csvString = new StringBuilder();

        //Tests for Successfully reading the csv file
        [TestMethod()]
        public void readCsvTest()
        {

            //Arrange
            string expected = "Success";
            string actual = "";
                
            csvString.AppendLine("Employee4,Employee2,500");
            csvString.AppendLine("Employee3,Employee1,500");
            csvString.AppendLine("Employee1,,1000");
            csvString.AppendLine("Employee5,Employee1,500");
            csvString.AppendLine("Employee2,Employee1,800");

            //Act
            actual = employees.validateCsv(csvString.ToString());

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void validSalaryTest()
        {
            csvString.AppendLine("Employee4,Employee2,500");
            csvString.AppendLine("Employee3,Employee1,500");
            csvString.AppendLine("Employee1,,1000");
            csvString.AppendLine("Employee5,Employee1,500");
            csvString.AppendLine("Employee2,Employee1,800");

            employees = new Employee(csvString.ToString());
        }

        //Tests to validate salary as integers
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void invalidSalaryTest()
        {
            csvString.AppendLine("Employee4,Employee2,500");
            csvString.AppendLine("Employee3,Employee1,500");
            csvString.AppendLine("Employee1,,1000");
            csvString.AppendLine("Employee5,Employee1,yui"); // <-non-integer salary entry
            csvString.AppendLine("Employee2,Employee1,800");

            employees = new Employee(csvString.ToString());
        }

        // Tests to validate one employee does not report to more than one manager.
        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void oneManagerPerEmployeeTest()
        {
            csvString.AppendLine("Employee4,Employee2,500");
            csvString.AppendLine("Employee3,Employee1,Employee2,500");
            csvString.AppendLine("Employee1,,1000");
            csvString.AppendLine("Employee5,Employee1,500");
            csvString.AppendLine("Employee2,Employee1,800");

            employees = new Employee(csvString.ToString());
            
        }

        //There is only one CEO, i.e. only one employee with no manager.
        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void oneCEOTest()
        {
            csvString.AppendLine("Employee4,Employee2,500");
            csvString.AppendLine("Employee3,Employee1,500");
            csvString.AppendLine("Employee1,,1000");
            csvString.AppendLine("Employee5,,500");
            csvString.AppendLine("Employee2,Employee1,800");

            employees = new Employee(csvString.ToString());
            
        }

        //Test to Validate all managers to be employees
        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void managerIsEmployeeTest()
        {
            expected = true;

            //need to check for existing id {Employee2} in validateEmployee func

            csvString.AppendLine("Employee4,Employee6,500");
            csvString.AppendLine("Employee3,Employee1,500");
            csvString.AppendLine("Employee1,,1000");
            csvString.AppendLine("Employee5,Employee1,500");
            csvString.AppendLine("Employee2,Employee1,800");

            employees = new Employee(csvString.ToString());

            //Assert.AreEqual(expected, actual);


        }

        //Test budget for a manager who is the CEO
        [TestMethod()]
        public void ceoBudgetTest()
        {
            long expected = 3800;
            long actual = 0;
            csvString.AppendLine("Employee4,Employee2,500");
            csvString.AppendLine("Employee3,Employee1,500");
            csvString.AppendLine("Employee1,,1000");
            csvString.AppendLine("Employee5,Employee1,500");
            csvString.AppendLine("Employee2,Employee1,800");
            csvString.AppendLine("Employee6,Employee2,500");


            employees = new Employee(csvString.ToString());

            actual = employees.managerBudget("Employee1");
            Assert.AreEqual(expected, actual);
            
        }

        //Test for a budget of a specific manager
        [TestMethod()]
        public void managerBudgetTest()
        {
            long expected = 1800;
            long actual = 0;
            csvString.AppendLine("Employee4,Employee2,500");
            csvString.AppendLine("Employee3,Employee1,500");
            csvString.AppendLine("Employee1,,1000");
            csvString.AppendLine("Employee5,Employee1,500");
            csvString.AppendLine("Employee2,Employee1,800");

            employees = new Employee(csvString.ToString());

            actual = employees.managerBudget("Employee2");
            Assert.AreEqual(expected, actual);
            
        }

        //Test for an employee who is not a manager
        [TestMethod()]
        public void juniourBudgetTest()
        {
            long expected = 500;
            long actual = 0;
            csvString.AppendLine("Employee4,Employee2,500");
            csvString.AppendLine("Employee3,Employee1,500");
            csvString.AppendLine("Employee1,,1000");
            csvString.AppendLine("Employee5,Employee1,500");
            csvString.AppendLine("Employee2,Employee1,800");

            employees = new Employee(csvString.ToString());

            actual = employees.managerBudget("Employee3");
            Assert.AreEqual(expected, actual);
        }
    }
}