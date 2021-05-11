using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class Pass
    {
        public Pass()
        {
            ClientPasses = new HashSet<ClientPass>();
            Entries = new HashSet<Entry>();
        }

        public int Id { get; set; }
        [Required] public string Name { get; set; }
        
        [Range(.0, Double.MaxValue, ErrorMessage = "Price must be a positive value!")]
        [Required] public decimal Price { get; set; }
        public int ValidForDays { get; set; }
        public int ValidForEnteries { get; set; }
        public int ValidForGymId { get; set; }
        public Gym ValidForGym  { get; set; }
        public int ValidFrom { get; set; }
        public int ValidUntil { get; set; }
        public int ValidPerDay { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<ClientPass> ClientPasses { get; set; }
        public virtual ICollection<Entry> Entries { get; set; }
    }
}
