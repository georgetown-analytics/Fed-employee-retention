using Microsoft.VisualBasic.FileIO;
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
                            int[] indices = new int[] { 0, 1, 2, 3, 4, 5 };
                            IEnumerable<String> columns = parser.ReadFields().Select((field, index) => new { field, index }).Where(fi => indices.Contains(fi.index)).Select(fi => fi.field);

                            String agencyName = columns.ElementAtOrDefault(0).Trim().Remove(0, 5);
                            String agencyCode = columns.ElementAtOrDefault(0).Trim().Substring(0, 4);
                            int DomesticUSCount = 0;
                            int USTerritoriesCount = 0;
                            int ForeignCount = 0;
                            int UnspecifiedCount = 0;
                            int locationAll = 0;

                            if (columns.ElementAtOrDefault(1).Trim().Equals("NA"))
                            {
                                DomesticUSCount = 0;
                            }
                            else
                            {
                                DomesticUSCount = Convert.ToInt32(columns.ElementAtOrDefault(1).Trim());
                            }

                            if (columns.ElementAtOrDefault(2).Trim().Equals("NA"))
                            {
                                USTerritoriesCount = 0;
                            }
                            else
                            {
                                USTerritoriesCount = Convert.ToInt32(columns.ElementAtOrDefault(2).Trim());
                            }

                            if (columns.ElementAtOrDefault(3).Trim().Equals("NA"))
                            {
                                ForeignCount = 0;
                            }
                            else
                            {
                                ForeignCount = Convert.ToInt32(columns.ElementAtOrDefault(3).Trim());
                            }

                            if (columns.ElementAtOrDefault(4).Trim().Equals("NA"))
                            {
                                UnspecifiedCount = 0;
                            }
                            else
                            {
                                UnspecifiedCount = Convert.ToInt32(columns.ElementAtOrDefault(4).Trim());
                            }

                            if (columns.ElementAtOrDefault(5).Trim().Equals("NA"))
                            {
                                locationAll = 0;
                            }
                            else
                            {
                                locationAll = Convert.ToInt32(columns.ElementAtOrDefault(5).Trim());
                            }

                            //add agency code if not exists
                            if (!String.IsNullOrWhiteSpace(agencyCode) && !String.IsNullOrWhiteSpace(agencyName))
                            {
                                if (!context.Agency.Any(x => x.AgencyName.Equals(agencyName)) && !context.Agency.Any(x => x.AgencyCode.Equals(agencyCode)))
                                {
                                    Agency agency = new Agency()
                                    {
                                        AgencyName = agencyName,
                                        AgencyCode = agencyCode,
                                        YearID = yearID,
                                        PartnershipCode = null
                                    };
                                    context.Agency.Add(agency);
                                    context.SaveChanges();
                                }
                            }

                            int agencyID = (from u in context.Agency where u.AgencyName.Equals(agencyName) select u.AgencyID).FirstOrDefault();

                            int employeeCount = 0;

                            if (!context.Employment.Any(x => x.AgencyID == agencyID))
                            {
                                Employment employment = new Employment()
                                {
                                    AgencyID = agencyID,
                                    EmployeeCount = employeeCount,
                                    QuitCount = null,
                                    AttritionRate = null,
                                    YearID = yearID
                                };
                                context.Employment.Add(employment);
                                context.SaveChanges();
                            }
                            else
                            {
                                var employment = (from u in context.Employment where u.AgencyID == agencyID select u).FirstOrDefault();
                                employment.EmployeeCount += employeeCount;
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
