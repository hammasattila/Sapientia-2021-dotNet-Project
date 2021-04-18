using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class ClientPass
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int PassId { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public string Barcode { get; set; }
        public decimal SellingPrice { get; set; }
        public int NumberOfEnteries { get; set; }
        public int? FirstUsedAt { get; set; }
        public bool? IsValid { get; set; }

        public virtual Client Client { get; set; }
        public virtual Pass Pass { get; set; }
    }
}
