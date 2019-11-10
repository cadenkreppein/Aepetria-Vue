using aepetria_vue_api.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aepetria_vue_api.Services.Interfaces
{
    public interface IRemoteImageService
    {
        Task<List<RemoteImage>> GetAll();
        Task<RemoteImage> GetByID(int id);
        Task<RemoteImage> GetActiveImage();
        Task<int> Add(RemoteImage remoteImage);
        Task<string> Update(RemoteImage remoteImage);
        Task<string> Delete(RemoteImage remoteImage);
    }
}
