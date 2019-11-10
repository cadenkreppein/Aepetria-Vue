using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aepetria_vue_api.DAL.Models;
using aepetria_vue_api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace aepetria_vue_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemoteController : ControllerBase
    {
        private readonly IRemoteImageService _remoteImages;
        private const int MAX_FILE_SIZE = 16777215; // MySQL mediumblob size limit (16 MB)
        private static readonly string[] PERMITTED_EXTENSIONS = { ".jpeg", ".jpg", ".png", ".gif" };
        private static readonly Dictionary<string, List<byte[]>> FILE_SIGNATURES =
            new Dictionary<string, List<byte[]>>
            {
                { ".jpeg", new List<byte[]>
                    {
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                    }
                },
                { ".jpg", new List<byte[]>
                    {
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                    }
                },
                { ".png", new List<byte[]>
                    {
                        new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }
                    }
                },
                { ".gif", new List<byte[]>
                    {
                        new byte[] { 0x47, 0x49, 0x46, 0x38 }
                    }
                }
            };

        public RemoteController(IRemoteImageService remoteImages)
        {
            _remoteImages = remoteImages;
        }

        // GET api/remote
        [HttpGet]
        public async Task<IEnumerable<RemoteImage>> GetAll()
        {
            var remoteImages = await _remoteImages.GetAll();
            return remoteImages.Any() ? remoteImages : new List<RemoteImage>();
        }

        // POST api/remote
        [HttpPost]
        public async Task<RemoteImage> UploadImage(IFormFile file)
        {
            if (file.Length > 0 && file.Length < MAX_FILE_SIZE)
            {
                var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(ext) || !PERMITTED_EXTENSIONS.Contains(ext))
                {
                    throw new ArgumentException("File's extension is not allowed");
                }
                using (var reader = new BinaryReader(file.OpenReadStream()))
                {
                    var signatures = FILE_SIGNATURES[ext];
                    var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

                    if (!signatures.Any(signature =>
                        headerBytes.Take(signature.Length).SequenceEqual(signature)))
                    {
                        throw new ArgumentException("File's signature is an not allowed");
                    }
                }
                var remoteImage = new RemoteImage();
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    remoteImage.Data = ms.ToArray();
                    remoteImage.Extension = ext.TrimStart('.');
                    remoteImage.IsActive = false;
                    await _remoteImages.Add(remoteImage);
                }
                return remoteImage;
            }
            else
            {
                throw new ArgumentException("File's size is not allowed (probably too big)");
            }
        }

        // PUT api/remote/5
        [HttpPut("{id}")]
        public async Task<string> SetActive(int id)
        {
            var remoteImages = await _remoteImages.GetAll();
            var activeImages = remoteImages.Where(x => x.IsActive);
            foreach (var image in activeImages)
            {
                image.IsActive = false;
                await _remoteImages.Update(image);
            }
            if (id != -1)
            {
                var remoteImage = await _remoteImages.GetByID(id);
                remoteImage.IsActive = true;
                await _remoteImages.Update(remoteImage);
            }
            return "success";
        }

        // DELETE api/remote/5
        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            await _remoteImages.Delete(await _remoteImages.GetByID(id));
            return "success";
        }

        [HttpGet("/api/[controller]/ws")]
        public async Task SendActiveImage()
        {
            var context = ControllerContext.HttpContext;

            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket socket = await context.WebSockets.AcceptWebSocketAsync();

                var activeImage = await _remoteImages.GetActiveImage();
                int? lastId = -1;
                do
                {
                    activeImage = await _remoteImages.GetActiveImage();
                    if (activeImage == null && lastId != null)
                    {
                        var blackout = new RemoteImage { Id = -1 };
                        var bytes = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(blackout, Formatting.None, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
                        var arraySegment = new ArraySegment<byte>(bytes);
                        await socket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
                        lastId = null;
                    }
                    else if (activeImage?.Id != lastId)
                    {
                        var bytes = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(activeImage, Formatting.None, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
                        var arraySegment = new ArraySegment<byte>(bytes);
                        await socket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
                        lastId = activeImage?.Id;
                    }
                    Thread.Sleep(500);
                }
                while (socket.State == WebSocketState.Open);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }
    }
}
