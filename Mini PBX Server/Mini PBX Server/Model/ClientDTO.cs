using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mini_PBX.Models
{
    public class ClientDTO
    {
        public long Id { get; set; }
        public string userName { get; set; }
        public string phone_number { get; set; }
    }
}
