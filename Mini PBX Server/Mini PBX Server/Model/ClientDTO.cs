using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mini_PBX.Models
{
    class ClientDTO
    {
        public long Id { get; set; }
        [MaxLength(256)]
        [Required]
        public string userName { get; set; }
        [Required]
        [MaxLength(3)]
        public string phone_number { get; set; }
    }
}
