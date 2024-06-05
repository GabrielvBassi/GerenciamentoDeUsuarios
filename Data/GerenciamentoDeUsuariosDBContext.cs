using GerenciamentoDeUsuarios.Data.Map;
using GerenciamentoDeUsuarios.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeUsuarios.Data
{
    public class GerenciamentoDeUsuariosDBContext : DbContext
    {
        public GerenciamentoDeUsuariosDBContext(DbContextOptions<GerenciamentoDeUsuariosDBContext> options): base(options) 
        {
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}

