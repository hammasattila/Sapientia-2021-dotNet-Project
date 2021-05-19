using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class Client
    {
        public Client()
        {
            ClientPasses = new HashSet<ClientPass>();
            Entries = new HashSet<Entry>();
        }

        public int Id { get; set; }

        [Required] public string Name { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Id Card's serial number")]
        public string IdCardNr { get; set; }
        public bool IsDeleted { get; set; }
        public string Barcode { get; set; }
        public DateTime InsertedAt { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<ClientPass> ClientPasses { get; set; }
        public virtual ICollection<Entry> Entries { get; set; }
    }
}
