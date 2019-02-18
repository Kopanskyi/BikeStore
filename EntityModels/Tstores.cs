using System;
using System.Collections.Generic;

namespace EntityModels
{
    public partial class Tstores
    {
        public Tstores()
        {
            Tbikes = new HashSet<Tbikes>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Tbikes> Tbikes { get; set; }
    }
}
