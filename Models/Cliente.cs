using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AFPCrecer.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [Required]
        public int Edad { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }
    }
}
