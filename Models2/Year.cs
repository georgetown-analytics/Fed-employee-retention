using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models2
{
    public class Years
    {
        public Years()
        {
            Added = DateTime.Now;
        }

        [Key]
        public int YearID { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public int Year { get; set; }

        [Required]
        public DateTime Added { get; set; }
    }
}
