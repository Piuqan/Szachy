using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    [Table("UserScore")]
    public class UserScore : IUserScore
    {
        [Ignore]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("ID")]
        public int IDGame { get; set; }

        public int userScore { get; set; }

        public string userID { get; set; }
        public virtual Chess game { get; set; }
    }
}
