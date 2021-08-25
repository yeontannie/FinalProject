using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Collection
    {
        [Key]
        public int CollectionID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Theme { get; set; }

        public string User_Name { get; set; }

        public int ItemsAmount { get; set; }

        public ICollection<Item> Item { get; set; }

        public Collection()
        {

        }
    }
}
