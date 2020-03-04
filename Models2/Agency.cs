using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models2
{
    public class Agency
    {
        public Agency()
        {
            Added = DateTime.Now;
        }

        [Key]
        public int AgencyID { get; set; }

        [Required]
        public String AgencyName { get; set; }

        [Required]
        [StringLength(4)]
        public String AgencyCode { get; set; }

        [ForeignKey("Years")]
        public int YearID { get; set; }

        [Required]
        public DateTime Added { get; set; }

        public virtual Years Years { get; set; }
    }
}
