using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web_Crawler.Models
{
    public class FrequencyTokenInDescriptions
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Token { get; set; }
        public int Frequency { get; set; }
    }
}