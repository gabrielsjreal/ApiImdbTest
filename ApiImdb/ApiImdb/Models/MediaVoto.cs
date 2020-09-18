using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace ApiImdb.Models
{
    public class MediaVoto
    {
        public Filme Filme { get; set; }
        public double Pontuacao { get; set; }
    }
}
