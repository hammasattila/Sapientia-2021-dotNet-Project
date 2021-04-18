using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class Entry
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int PassId { get; set; }
        public int GymId { get; set; }
        public DateTime EntryDate { get; set; }

        public virtual Client Client { get; set; }
        public virtual Gym Gym { get; set; }
        public virtual Pass Pass { get; set; }
    }
}
