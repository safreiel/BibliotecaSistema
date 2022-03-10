using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Biblioteca.Models
{
    public class BibliotecaContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {                   
            optionsBuilder.UseMySql("Server=localhost;DataBase=Biblioteca;Uid=root;");
        }

        public DbSet<livros> Livros {get; set;}
        public DbSet<Emprestimos> Emprestimos {get; set;}
        public DbSet<Usuario> Usuario {get; set;}
    }
}
