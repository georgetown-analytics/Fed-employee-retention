﻿using Microsoft.VisualBasic.FileIO;
using Models2;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace Persistence2
{
    public class EmploymentParserUtils
    {
        public void PopulateEmploymentNumbers(string filePath, int yearNumber)
        {
            try
            {
                using (DataScienceContext context = new DataScienceContext())
                {
                    using (TextFieldParser parser = new TextFieldParser(filePath))
                    {
                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(",");

                        //add new year if there is one
                        if (!context.Years.Any(x => x.Year == yearNumber))
                        {
                            Years years = new Years()
                            {
                                Year = yearNumber
                            };
                            context.Years.Add(years);
                            context.SaveChanges();
                        }

                        int yearID = (from u in context.Years where u.Year == yearNumber select u.YearID).FirstOrDefault();

                        while (!parser.EndOfData)
                        {
                            //Process row
                            int[] indices = new int[] { 0, 1 };
                            IEnumerable<String> columns = parser.ReadFields().Select((field, index) => new { field, index }).Where(fi => indices.Contains(fi.index)).Select(fi => fi.field);

                            String agencyName = columns.ElementAtOrDefault(0).Trim().Remove(0, 5);
                            String agencyCode = columns.ElementAtOrDefault(0).Trim().Substring(0, 4);
                            String employees = columns.ElementAtOrDefault(1).Trim();
                            int employeeCount = 0;

                            if (!employees.Equals("NA") && !employees.Equals("Employment") && !employees.Equals("Total"))
                            {
                                employeeCount = Convert.ToInt32(employees);
                            }

                            //add agency code if not exists
                            if (!String.IsNullOrWhiteSpace(agencyCode) && !String.IsNullOrWhiteSpace(agencyName) && employeeCount != 0 && !agencyCode.Equals("Agen") && !agencyName.Equals("UNSPECIFIED"))
                            {
                                //prevent duplicates
                                var agencyCheck = (from u in context.Agency where u.AgencyName.Equals(agencyName) && u.AgencyCode.Equals(agencyCode) && u.YearID == yearID select u.AgencyID).FirstOrDefault();
                                
                                if (agencyCheck == 0)
                                {
                                    Agency agency = new Agency()
                                    {
                                        AgencyName = agencyName,
                                        AgencyCode = agencyCode,
                                        YearID = yearID,
                                    };
                                    context.Agency.Add(agency);
                                    context.SaveChanges();
                                }
                            }

                            int agencyID = (from u in context.Agency where u.AgencyName.Equals(agencyName) && u.YearID == yearID select u.AgencyID).FirstOrDefault();

                            if (agencyID != 0)
                            {
                                //prevent duplicates
                                var employmentCheck = (from u in context.Employment where u.AgencyID == agencyID && u.YearID == yearID && employeeCount != 0 select u.EmploymentID).FirstOrDefault();

                                if (employmentCheck == 0)
                                {
                                    Employment employment = new Employment()
                                    {
                                        AgencyID = agencyID,
                                        EmployeeCount = employeeCount,
                                        QuitCount = null,
                                        AttritionRate = null,
                                        AverageSalary = null,
                                        Education = null,
                                        AverageService = null,
                                        Sex = null,
                                        YearID = yearID
                                    };
                                    context.Employment.Add(employment);
                                    context.SaveChanges();
                                }
                                else
                                {
                                    var employment = (from u in context.Employment where u.AgencyID == agencyID && u.YearID == yearID select u).FirstOrDefault();

                                    if (employment != null)
                                    {
                                        employment.EmployeeCount += employeeCount;
                                    }
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
