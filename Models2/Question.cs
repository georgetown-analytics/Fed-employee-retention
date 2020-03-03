using System;
using System.ComponentModel.DataAnnotations;

namespace Models2
{
    public class Question
    {
        public Question()
        {
            Added = DateTime.Now;
        }

        [Key]
        public int QuestionID { get; set; }

        [Required]
        public int QuestionNumber { get; set; }

        [Required]
        public DateTime Added { get; set; }
    }
}
