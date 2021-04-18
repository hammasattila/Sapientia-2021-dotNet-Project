using System;
using System.Collections.Generic;

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
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string IdCardNr { get; set; }
        public bool IsDeleted { get; set; }
        public string Barcode { get; set; }
        public DateTime InsertedAt { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<ClientPass> ClientPasses { get; set; }
        public virtual ICollection<Entry> Entries { get; set; }
    }
}
