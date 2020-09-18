
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace ApiImdb.Models
{
    public class Filme
    {
        [BindNever]
        public int FilmeId { get; set; }
        public string Nome { get; set; }
        public Genero Genero { get; set; }
        public Diretor Diretor { get; set; }
        public List<Ator> ListaDeAtores { get; set; }
    }
}
