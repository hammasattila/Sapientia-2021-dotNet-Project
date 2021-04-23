using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer{
    public class FitnessDataService 
    {
        public async Task<bool> TryAddClientAsync(Client client)
        {

            using (var db = new AppDbContext())
            {   
                client.Barcode = (db.Clients.Count() + 1).ToString("D7");
                var res = await db.Clients.AddAsync(client);
                db.SaveChanges();
            }

            return true;
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            using (var db = new AppDbContext())
            {
                return await db.Clients
                    .Where(client => client.IsDeleted == false)
                    .ToListAsync();
            }
        }
    }
}