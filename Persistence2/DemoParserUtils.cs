﻿using Microsoft.VisualBasic.FileIO;
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
                            String percentFemale = columns.ElementAtOrDefault(1).Replace("%","").Trim();

                            if(percentFemale.Equals("NA"))
                            {
                                percentFemale = null;
                            }

                            int agencyID = (from u in context.Agency where u.AgencyCode.Equals(agencyCode) && u.YearID == yearID select u.AgencyID).FirstOrDefault();

                            if (agencyID != 0)
                            {
                                var employment = (from u in context.Employment where u.AgencyID == agencyID && u.YearID == yearID select u).FirstOrDefault();

                                if (employment != null)
                                {
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

        public void PopulateAverageService(String filePath)
        {

        }

        public void PopulateAverageSalary(String filePath)
        {

        }

        public void PopulateAverageEducation(String filePath)
        {

        }
    }
}
