using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSRSUserPrivileges.Model
{
    public class ReportItem
    {
        public ReportItem()
        {

        }

        public string Path{get;set;}
        public string Name { get; set; }
        public bool IsFolder { get; set; }

        public List<ReportItem> Children { get; set; }
    }
}
