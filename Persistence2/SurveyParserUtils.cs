using Microsoft.VisualBasic.FileIO;
using Models2;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace Persistence2
{
    public class SurveyParserUtils
    {
        public void PopulateSurveyUtils(String filePath, int yearNumber)
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

                        int[] indices = new int[74];

                        //account for all columns
                        for (int i = 1; i < 74; i++)
                        {
                            indices[i] = i;
                        }

                        while (!parser.EndOfData)
                        {
                            IEnumerable<String> columns = parser.ReadFields().Select((field, index) => new { field, index }).Where(fi => indices.Contains(fi.index)).Select(fi => fi.field);

                            String agencyCode = columns.ElementAtOrDefault(1).Trim();

                            int agencyID = (from u in context.Agency where u.AgencyCode.Equals(agencyCode) select u.AgencyID).FirstOrDefault();

                            if (agencyID != 0)
                            {
                                //prevent duplicates
                                if (!context.Survey.Any(x => x.AgencyID == agencyID && x.YearID == yearID))
                                {
                                    for (int i = 3; i < 74; i++)
                                    {
                                        int questionNumber = i - 2;

                                        //fill in Survey table with responses
                                        Survey survey = new Survey()
                                        {
                                            QuestionNumber = questionNumber,
                                            AgencyID = agencyID,
                                            YearID = yearID,
                                            ResponseValue = Convert.ToDouble(columns.ElementAtOrDefault(i).Trim())
                                        };
                                        context.Survey.Add(survey);
                                    }

                                    //save survey data per agency
                                    context.SaveChanges();
                                }
                            }
                        }
                    }
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
