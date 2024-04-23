using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModuleAPI.Models
{
    public class Cliente
    {
        public int Id { get; set; } = 0;

        public string Nome { get; set; } = string.Empty;

        public string Cpf_Cnpj { get; set; } = string.Empty;

        public DateTime UltimaAlteracaoDataHora {get; set;} = DateTime.Now;
    }
}