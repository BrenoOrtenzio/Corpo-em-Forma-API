using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using ModuleAPI.Entities;

namespace ModuleAPI.Context
{
    // Context Indica nosso banco
    public class PrincipalContext : DbContext
    {
        public PrincipalContext(DbContextOptions<PrincipalContext> options) : base(options)
        {

        }

        // DbSet Indica nossa tabela
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Ficha> Fichas { get; set; }
        public DbSet<Exercicio> Exercicios { get; set; }
        public DbSet<ExercicioAplicado> ExerciciosAplicados { get; set; }
    }
}