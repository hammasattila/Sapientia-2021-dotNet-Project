using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class FitnessDataService
    {
        #region Clients
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
        #endregion

        #region Gyms
        public async Task<List<Gym>> GetGymsAsync()
        {
            using (var db = new AppDbContext())
            {
                return await db.Gyms
                    .OrderBy(gym => gym.IsDeleted)
                    .ThenBy(gym => gym.Name)
                    .ToListAsync();
            }
        }

        public async Task<bool> TryAddGymAsync(Gym gym)
        {

            using (var db = new AppDbContext())
            {
                var res = await db.Gyms.AddAsync(gym);
                db.SaveChanges();
            }

            return true;
        }

        public Task<bool> UpdateGymAsync(Gym gym)
        {

            using (var db = new AppDbContext())
            {
                var res = db.Gyms.Update(gym);
                db.SaveChanges();
            }

            return Task.FromResult(true);
        }

        #endregion

        #region Passes

        public async Task<List<Pass>> GetPassesAsync()
        {
            using (var db = new AppDbContext())
            {
                return await db.Passes
                    .OrderBy(pass => pass.IsDeleted)
                    .ThenBy(pass => pass.Name)
                    .ToListAsync();
            }
        }

        public async Task<bool> TryAddPassAsync(Pass pass)
        {

            using (var db = new AppDbContext())
            {
                var res = await db.Passes.AddAsync(pass);
                db.SaveChanges();
            }

            return true;
        }

        public async Task<bool> UpdatePassAsync(Pass pass)
        {

            using (var db = new AppDbContext())
            {
                var res = db.Passes.Update(pass);
                db.SaveChanges();
            }

            return true;
        }
        #endregion
    }
}