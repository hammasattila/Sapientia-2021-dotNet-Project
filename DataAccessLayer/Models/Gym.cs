using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class Gym
    {
        public Gym()
        {
            Entries = new HashSet<Entry>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Entry> Entries { get; set; }
    }
}
