using System;
using System.Collections.Generic;

namespace EntityModels
{
    public partial class Tbikes
    {
        public int Id { get; set; }
        public int? StoreId { get; set; }
        public int ModelId { get; set; }
        public decimal? Price { get; set; }

        public virtual Tmodel Model { get; set; }
        public virtual Tstores Store { get; set; }
    }
}
