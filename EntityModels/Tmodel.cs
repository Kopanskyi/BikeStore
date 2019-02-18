using System;
using System.Collections.Generic;

namespace EntityModels
{
    public partial class Tmodel
    {
        public Tmodel()
        {
            Tbikes = new HashSet<Tbikes>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        public virtual ICollection<Tbikes> Tbikes { get; set; }
    }
}
