using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace Persistence2
{
    public class DemoParserUtils
    {
        public void PopulateSexRates(String filePath, int year)
        {
            try
            {
                using (DataScienceContext context = new DataScienceContext())
                {
                    using (TextFieldParser parser = new TextFieldParser(filePath))
                    {
                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(",");

                        int yearID = (from u in context.Years where u.Year == year select u.YearID).FirstOrDefault();

                        while (!parser.EndOfData)
                        {
                            //Process row
                            int[] indices = new int[] { 0, 1 };
                            IEnumerable<String> columns = parser.ReadFields().Select((field, index) => new { field, index }).Where(fi => indices.Contains(fi.index)).Select(fi => fi.field);

                            String agencyCode = columns.ElementAtOrDefault(0).Trim().Substring(0, 4);
                            String femaleCount = columns.ElementAtOrDefault(1).Replace("%","").Trim();
                            double percentFemale = 0;                           

                            int agencyID = (from u in context.Agency where u.AgencyCode.Equals(agencyCode) && u.YearID == yearID select u.AgencyID).FirstOrDefault();

                            if (agencyID != 0)
                            {
                                var employment = (from u in context.Employment where u.AgencyID == agencyID && u.YearID == yearID select u).FirstOrDefault();

                                if (employment != null)
                                {
                                    if (femaleCount.Equals("NA"))
                                    {
                                        percentFemale = 0;
                                    }
                                    else
                                    {
                                        percentFemale = Convert.ToDouble(femaleCount);
                                    }

                                    employment.Sex = Convert.ToInt32(percentFemale);
                                }
                            }
                        }
                    }
                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                new DBErrorLogging().DumpDBException(ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.WriteLine(ex.StackTrace);
            }
        }

        public void PopulateAverageService(String filePath, int year)
        {
            try
            {
                using (DataScienceContext context = new DataScienceContext())
                {
                    using (TextFieldParser parser = new TextFieldParser(filePath))
                    {
                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(",");

                        int yearID = (from u in context.Years where u.Year == year select u.YearID).FirstOrDefault();

                        while (!parser.EndOfData)
                        {
                            //Process row
                            int[] indices = new int[] { 0, 1 };
                            IEnumerable<String> columns = parser.ReadFields().Select((field, index) => new { field, index }).Where(fi => indices.Contains(fi.index)).Select(fi => fi.field);

                            String agencyCode = columns.ElementAtOrDefault(0).Trim().Substring(0, 4);
                            String serviceCount = columns.ElementAtOrDefault(1).Replace("%", "").Trim();
                            double percentService = 0;

                            int agencyID = (from u in context.Agency where u.AgencyCode.Equals(agencyCode) && u.YearID == yearID select u.AgencyID).FirstOrDefault();

                            if (agencyID != 0)
                            {
                                var employment = (from u in context.Employment where u.AgencyID == agencyID && u.YearID == yearID select u).FirstOrDefault();

                                if (employment != null)
                                {
                                    if (serviceCount.Equals("NA"))
                                    {
                                        percentService = 0;
                                    }
                                    else
                                    {
                                        percentService = Convert.ToDouble(serviceCount);
                                    }

                                    employment.AverageService = Convert.ToInt32(percentService);
                                }
                            }
                        }
                    }
                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                new DBErrorLogging().DumpDBException(ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.WriteLine(ex.StackTrace);
            }
        }

        public void PopulateAverageSalary(String filePath, int year)
        {
            try
            {
                using (DataScienceContext context = new DataScienceContext())
                {
                    using (TextFieldParser parser = new TextFieldParser(filePath))
                    {
                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(",");

                        int yearID = (from u in context.Years where u.Year == year select u.YearID).FirstOrDefault();

                        while (!parser.EndOfData)
                        {
                            //Process row
                            int[] indices = new int[] { 0, 1 };
                            IEnumerable<String> columns = parser.ReadFields().Select((field, index) => new { field, index }).Where(fi => indices.Contains(fi.index)).Select(fi => fi.field);

                            String agencyCode = columns.ElementAtOrDefault(0).Trim().Substring(0, 4);
                            String salaryFigure = columns.ElementAtOrDefault(1).Replace("%", "").Trim();
                            int salary = 0;

                            int agencyID = (from u in context.Agency where u.AgencyCode.Equals(agencyCode) && u.YearID == yearID select u.AgencyID).FirstOrDefault();

                            if (agencyID != 0)
                            {
                                var employment = (from u in context.Employment where u.AgencyID == agencyID && u.YearID == yearID select u).FirstOrDefault();

                                if (employment != null)
                                {
                                    if (!salaryFigure.Equals("NA"))
                                    {
                                        salary = Convert.ToInt32(salaryFigure);
                                    }

                                    employment.AverageSalary = salary;
                                }
                            }
                        }
                    }
                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                new DBErrorLogging().DumpDBException(ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.WriteLine(ex.StackTrace);
            }
        }

        public void PopulateAverageEducation(String filePath, int year)
        {
            try
            {
                using (DataScienceContext context = new DataScienceContext())
                {
                    using (TextFieldParser parser = new TextFieldParser(filePath))
                    {
                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(",");

                        int yearID = (from u in context.Years where u.Year == year select u.YearID).FirstOrDefault();

                        while (!parser.EndOfData)
                        {
                            //Process row
                            int[] indices = new int[] { 0, 1 };
                            IEnumerable<String> columns = parser.ReadFields().Select((field, index) => new { field, index }).Where(fi => indices.Contains(fi.index)).Select(fi => fi.field);

                            String agencyCode = columns.ElementAtOrDefault(0).Trim().Substring(0, 4);
                            String educationCount = columns.ElementAtOrDefault(1).Replace("%", "").Trim();
                            double percentEducation = 0;

                            int agencyID = (from u in context.Agency where u.AgencyCode.Equals(agencyCode) && u.YearID == yearID select u.AgencyID).FirstOrDefault();

                            if (agencyID != 0)
                            {
                                var employment = (from u in context.Employment where u.AgencyID == agencyID && u.YearID == yearID select u).FirstOrDefault();

                                if (employment != null)
                                {
                                    if (educationCount.Equals("NA"))
                                    {
                                        percentEducation = 0;
                                    }
                                    else
                                    {
                                        percentEducation = Convert.ToDouble(educationCount);
                                    }

                                    employment.Education = Convert.ToInt32(percentEducation);
                                }
                            }
                        }
                    }
                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                new DBErrorLogging().DumpDBException(ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}
