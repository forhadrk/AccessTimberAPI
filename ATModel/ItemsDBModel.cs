using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATModel
{
    public class ItemsDBModel
    {
        public int ItemsID { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; } = 0;
        public bool Active { get; set; } = true;
    }
}
