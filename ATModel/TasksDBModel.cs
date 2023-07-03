using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATModel
{
    public class TasksDBModel
    {
        public int TasksID { get; set; }
        public string TaskName { get; set; }
        public int DefaultAssignee { get; set; }
        public bool Active { get; set; } = true;
    }
}
