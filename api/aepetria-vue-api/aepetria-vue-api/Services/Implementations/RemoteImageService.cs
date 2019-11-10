using aepetria_vue_api.DAL;
using aepetria_vue_api.DAL.Models;
using aepetria_vue_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aepetria_vue_api.Services.Implementations
{
    public class RemoteImageService : IRemoteImageService
    {
        private AepetriaDbContext _db;

        public RemoteImageService(AepetriaDbContext db)
        {
            _db = db;
        }

        public async Task<List<RemoteImage>> GetAll()
        {
            return await _db.RemoteImages.ToListAsync();
        }

        public async Task<RemoteImage> GetByID(int id)
        {
            return await _db.RemoteImages.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<RemoteImage> GetActiveImage()
        {
            return await _db.RemoteImages.Where(x => x.IsActive).FirstOrDefaultAsync();
        }

        public async Task<int> Add(RemoteImage remoteImage)
        {
            await _db.RemoteImages.AddAsync(remoteImage);
            await _db.SaveChangesAsync();
            return remoteImage.Id;
        }

        public async Task<string> Update(RemoteImage remoteImage)
        {
            _db.RemoteImages.Update(remoteImage);
            await _db.SaveChangesAsync();
            return "success";
        }

        public async Task<string> Delete(RemoteImage remoteImage)
        {
            _db.RemoteImages.Remove(remoteImage);
            await _db.SaveChangesAsync();
            return "success";
        }
    }
}
