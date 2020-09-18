using Microsoft.EntityFrameworkCore;

namespace ApiImdb.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Diretor> Diretores { get; set; }
        public DbSet<Ator> Atores { get; set; }
        public DbSet<Voto> Votos { get; set; }

        public Contexto (DbContextOptions<Contexto> opcoes) : base(opcoes) { }
    }
}
