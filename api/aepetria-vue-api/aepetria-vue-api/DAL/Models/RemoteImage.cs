using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aepetria_vue_api.DAL.Models
{
    public class RemoteImage : BaseEntity
    {
        [Required]
        public byte[] Data { get; set; }
        [Required]
        public string Extension { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
