using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ApiImdb.Models
{
    public class Diretor
    {
        [BindNever]
        public int DiretorId { get; set; }
        public string Nome { get; set; }
    }
}
