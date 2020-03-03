using Persistence2;
using System;
using System.Configuration;

namespace DataScienceProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Beginning all parsing");


            String employmentFilePath = ConfigurationManager.AppSettings["employment"];
            int year = Convert.ToInt32(ConfigurationManager.AppSettings["year"]);


            Console.WriteLine("Beginning employment parsing");
            //parse employees
            new EmploymentParserUtils().PopulateEmploymentNumbers(employmentFilePath, year);

            Console.WriteLine("Completed employment parsing");

            Console.WriteLine("Completed all parsing");

        }
    }
}
