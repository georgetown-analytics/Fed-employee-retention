using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models2
{
    public class Survey
    {
        public Survey()
        {
            Added = DateTime.Now;
        }

        [Key]
        public int SurveyID { get; set; }

        [ForeignKey("Agency")]
        public int AgencyID { get; set; }

        [Required]
        public double ResponseValue { get; set; }

        [ForeignKey("Years")]
        public int YearID { get; set; }

        [Index]
        public int QuestionNumber { get; set; }

        [Required]
        public DateTime Added { get; set; }

        public virtual Agency Agency { get; set; }

        public virtual Years Years { get; set; }
    }
}
