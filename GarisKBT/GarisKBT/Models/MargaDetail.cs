using System;
using System.Collections.Generic;
using System.Text;

namespace GarisKBT.Models
{
    public class MargaDetail
    {
        private int id;
        private string name;
        private int? superiorId;
        private string nim;
        private string wikipediaLink;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int? SuperiorId { get => superiorId; set => superiorId = value; }
        public string Nim { get => nim; set => nim = value; }
        public string WikipediaLink { get => wikipediaLink; set => wikipediaLink = value; }
    }
}
