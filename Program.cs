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
            String quitFilePath = ConfigurationManager.AppSettings["quits"];
            String surveyFilePath = ConfigurationManager.AppSettings["survey"];
            String sexFilePath = ConfigurationManager.AppSettings["sex"];
            String educationFilePath = ConfigurationManager.AppSettings["education"];
            String serviceFilePath = ConfigurationManager.AppSettings["service"];
            String salaryFilePath = ConfigurationManager.AppSettings["salary"];
            int year = Convert.ToInt32(ConfigurationManager.AppSettings["year"]);


            Console.WriteLine("Beginning employment parsing");

            //parse employees
            new EmploymentParserUtils().PopulateEmploymentNumbers(employmentFilePath, year);

            Console.WriteLine("Beginning attrition parsing");

            //parse attrition rates
            new QuitParserUtils().PopulateAttritionNumbers(quitFilePath, year);

            Console.WriteLine("Beginning demographics parsing");

            new DemoParserUtils().PopulateSexRates(sexFilePath, year);
            new DemoParserUtils().PopulateAverageService(serviceFilePath, year);
            new DemoParserUtils().PopulateAverageSalary(salaryFilePath, year);
            new DemoParserUtils().PopulateAverageEducation(educationFilePath, year);

            Console.WriteLine("Beginning survey parsing");

            //parse questions and responses
            new SurveyParserUtils().PopulateSurveyUtils(surveyFilePath, year);

            Console.WriteLine("Completed all parsing");

        }
    }
}
