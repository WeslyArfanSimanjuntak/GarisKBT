using System;
using System.Collections.Generic;
using System.Text;

namespace GarisKBT.Models
{
    public class GetSilsilahResult
    {
        public string nim { get; set; }
        public int id { get; set; }
        public int order { get; set; }
        public int? ParentId { get; set; }
        public string Nama { get; set; }
        public bool isAncestor { get; set; }
        public int rank { get; set; }
    }
}
