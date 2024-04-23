using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModuleAPI.Entities
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        
        public string  Documento { get; set; }

        public bool Ativo { get; set; }
    }
}