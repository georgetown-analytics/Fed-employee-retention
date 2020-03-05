using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace Persistence2
{
    public class QuitParserUtils
    {
        public void PopulateAttritionNumbers(string filePath, int year)
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
                            int[] indices = new int[] { 0, 1, 2 };
                            IEnumerable<String> columns = parser.ReadFields().Select((field, index) => new { field, index }).Where(fi => indices.Contains(fi.index)).Select(fi => fi.field);

                            String agencyCode = columns.ElementAtOrDefault(0).Trim().Substring(0, 4);
                            String transfer = columns.ElementAtOrDefault(1).Trim();
                            String quit = columns.ElementAtOrDefault(2).Trim();
                            int transferCount = 0;
                            int quitCount = 0;
                            double totalLeaveCount = 0;
                            double attritionRate = 0;

                            if (!agencyCode.Equals("Coun") && !transfer.Equals("Transfer Out - Individual Transfer") && !quit.Equals("Quit"))
                            {
                                transferCount = Convert.ToInt32(transfer);
                                quitCount = Convert.ToInt32(quit);
                                totalLeaveCount = (transferCount += quitCount);
                            }

                            int agencyID = (from u in context.Agency where u.AgencyCode.Equals(agencyCode) && u.YearID == yearID select u.AgencyID).FirstOrDefault();

                            if (agencyID != 0)
                            {

                                var employment = (from u in context.Employment where u.AgencyID == agencyID && u.YearID == yearID select u).FirstOrDefault();

                                if (employment != null)
                                {
                                    if (totalLeaveCount != 0)
                                    {
                                        double employeeCount = employment.EmployeeCount;
                                        attritionRate = (totalLeaveCount / employeeCount) * 100;

                                        employment.QuitCount = Convert.ToInt32(totalLeaveCount);
                                        employment.AttritionRate = Convert.ToInt32(attritionRate);
                                    }
                                    else
                                    {
                                        employment.QuitCount = 0;
                                        employment.AttritionRate = 0;
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
