using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ApiImdb.Models
{
    public class Voto
    {
        [BindNever]
        public int VotoId { get; set; }
        public Filme Filme { get; set; }
        public Classificacao Classificacao { get; set; }
        public Usuario Usuario { get; set; }
    }
}
