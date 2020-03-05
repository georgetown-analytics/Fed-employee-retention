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
            int year = Convert.ToInt32(ConfigurationManager.AppSettings["year"]);


            Console.WriteLine("Beginning employment parsing");

            //parse employees
            new EmploymentParserUtils().PopulateEmploymentNumbers(employmentFilePath, year);

            Console.WriteLine("Beginning attrition parsing");

            //parse attrition rates
            new QuitParserUtils().PopulateAttritionNumbers(quitFilePath, year);

            Console.WriteLine("Beginning survey parsing");

            //parse questions and responses
            new SurveyParserUtils().PopulateSurveyUtils(surveyFilePath, year);

            Console.WriteLine("Completed all parsing");

        }
    }
}
