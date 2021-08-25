using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }

        public string User_Name { get; set; }

        public DateTime Created { get; set; }

        public int CollectionID { get; set; }

        public Collection Collection { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Like> Likes { get; set; }

        public Item()
        {

        }
    }
}
