using System;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Persistence2
{
    public class DBErrorLogging
    {
        //catch and record any database issues
        public void DumpDBException(DbEntityValidationException eve)
        {
            try
            {
                foreach (var ve in eve.EntityValidationErrors)
                {
                    using (DataScienceContext context = new DataScienceContext())
                    {
                        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        ve.Entry.Entity.GetType().Name, ve.Entry.State);

                        foreach (var eee in ve.ValidationErrors)
                        {
                            Debug.WriteLine("Entity of type: " + ve.Entry.Entity.GetType().Name + " has the following validation errors: " + ve.Entry.State, "Property: " + eee.PropertyName + ", Error: " + eee.ErrorMessage);

                            Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            eee.PropertyName, eee.ErrorMessage);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}
