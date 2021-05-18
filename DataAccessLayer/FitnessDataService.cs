using System;
using System.Collections.Generic;
using System.Configuration;
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

        #region ClientMemberships
        public async Task<bool> TryAddClientMembershipAsync(ClientPass membership)
        {
            using (var db = new AppDbContext())
            {
                membership.Barcode = 0.ToString("D7");
                var res = await db.ClientPasses.AddAsync(membership);
                db.SaveChanges();
                membership.Barcode = membership.Id.ToString("D7");
                db.ClientPasses.Update(membership);
                db.SaveChanges();
                // db.ClientPasses.Update(membership);
            }

            return true;
        }

        public async Task<List<ClientPass>> GetValidClientMebershipsFor(int clientId)
        {
            using (var db = new AppDbContext())
            {
                return await db.ClientPasses
                    .Where(m => (m.ClientId == clientId) && (m.IsValid == true))
                    .Include(m => m.Pass)
                    .ToListAsync();
            }
        }
        #endregion

        #region Enteries
        public async Task<string> Enter(ClientPass membership)
        {
            int currentGym;
            try
            {
                currentGym = Int32.Parse(ConfigurationManager.AppSettings.Get("gym"));
            }
            catch (Exception)
            {
                currentGym = 0;
            }

            using (var db = new AppDbContext())
            {
                var membershipType = membership.Pass ?? await db.Passes.Where(p => p.Id == membership.PassId).FirstAsync();

                if (!membership.IsValid)
                {
                    return "Membership is not valid!";
                }

                if (membershipType.ValidForDays != 0 && membershipType.ValidForDays < (DateTime.Now - membership.DateOfPurchase).TotalDays)
                {
                    return "Membership expired!";
                }

                if (membershipType.ValidForEnteries != 0 && membershipType.ValidForEnteries < db.Entries.Count(e => e.ClientPassId == membership.Id))
                {
                    return "No more mebership enteries!";
                }

                if (membershipType.ValidPerDay != 0 && membershipType.ValidPerDay < db.Entries.Count(e => e.ClientPassId == membership.Id && e.EntryDate.Date == DateTime.Today))
                {
                    return "No more enteries for today!";
                }

                if (DateTime.Now.Hour < membershipType.ValidFrom)
                {
                    return $"Valid only after {membershipType.ValidFrom}!";
                }

                if (membershipType.ValidUntil < DateTime.Now.Hour)
                {
                    return $"Valid only after {membershipType.ValidUntil}!";
                }

                if (currentGym != 0 && membershipType.ValidForGymId != 0 && membershipType.ValidForGymId == currentGym)
                {
                    return "Mebership is not valid for this gym!";
                }

                var entery = new Entry()
                {
                    ClientId = membership.ClientId,
                    ClientPassId = membership.Id,
                    GymId = currentGym == 0 ? 1 : currentGym
                };

                await db.Entries.AddAsync(entery);
                await db.SaveChangesAsync();

                return String.Empty;
            }
        }
        #endregion
    }
}