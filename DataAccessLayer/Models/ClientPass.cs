using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class ClientPass
    {

        public ClientPass()
        {
            Entries = new HashSet<Entry>();
        }

        public int Id { get; set; }
        public int ClientId { get; set; }
        [Range(1, Int16.MaxValue, ErrorMessage = "Membership must be selected!")]
        [Required] public int PassId { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public string Barcode { get; set; }
        public decimal SellingPrice { get; set; }
        public int NumberOfEnteries { get; set; }
        public DateTime? FirstUsedAt { get; set; }
        public bool IsValid { get; set; }

        public virtual Client Client { get; set; }
        public virtual Pass Pass { get; set; }
        public virtual ICollection<Entry> Entries { get; set; }
    }
}
