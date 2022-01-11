using System;
using System.Collections.Generic;
using System.Text;

namespace GarisKBT.Models
{
    public class Marga
    {
        private List<MargaAlias> alias;
        private int id;
        private string name;
        private Marga superior;
        private int? superiorId;
        private string nim;

        public Marga(List<MargaAlias> alias, int id, string name, Marga superior, int? superiorId)
        {
            this.alias = alias;
            this.id = id;
            this.name = name;
            this.superior = superior;
            this.SuperiorId = superiorId;
        }
        public string NIMName
        {
            get
            {
                return string.Format("{0} - {1}", NIM, Name);
            }
        }
        public List<MargaAlias> Alias { get => alias; set => alias = value; }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public Marga Superior { get => superior; set => superior = value; }
        public int? SuperiorId { get => superiorId; set => superiorId = value; }
        public string NIM { get => nim; set => nim = value; }
    }
    public class MargaAlias
    {
        private int id;
        private string name;

        public MargaAlias(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
