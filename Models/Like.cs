using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Like
    {
        public int LikeID { get; set; }

        public int ItemID { get; set; }

        public Item Item { get; set; }

        public string User_Name { get; set; }

        public Like()
        {

        }
    }
}
