using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Comment
    {
        public int CommentID { get; set; }

        public string User_Name { get; set; }

        public string Text { get; set; }

        public int ItemID { get; set; }

        public Item Item { get; set; }

        public Comment()
        {

        }
    }
}
