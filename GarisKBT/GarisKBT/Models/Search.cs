using System;
using System.Collections.Generic;
using System.Text;

namespace GarisKBT.Models
{
    public partial class Search
    {
        public int Id { get; set; }
        public string JSONString { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
