using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ApiImdb.Models
{
    public class Paginacao
    {
        public int TamanhoMaximoDaPagina { get; set; }
        public int NumeroDaPagina { get; set; }
        public int TamanhoPagina { get; set; }

        [BindNever]
        public int TamanhoDaPagina
        {
            get { return TamanhoPagina; }
            set
            {
                TamanhoPagina = (value > TamanhoMaximoDaPagina) ? TamanhoMaximoDaPagina : value;
            }
        }
    }
}
