using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Tag
    {
        [Key]
        public string Text { get; set; }

        public int Count { get; set; }

        public Tag()
        {
            
        }
    }
}
