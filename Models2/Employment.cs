﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models2
{
    public class Employment
    {
        public Employment()
        {
            Added = DateTime.Now;
        }

        [Key]
        public int EmploymentID { get; set; }

        [ForeignKey("Agency")]
        public int AgencyID { get; set; }

        [Required]
        public int EmployeeCount { get; set; }

        public int? QuitCount { get; set; }

        public int? AttritionRate { get; set; }

        public int? AverageSalary { get; set; }

        //years employed
        public int? AverageService { get; set; }

        //percent advanced degree
        public int? Education { get; set; }

        //percent female
        public int? Sex { get; set; }

        [ForeignKey("Years")]
        public int YearID { get; set; }

        [Required]
        public DateTime Added { get; set; }

        public virtual Agency Agency { get; set; }

        public virtual Years Years { get; set; }
    }
}
