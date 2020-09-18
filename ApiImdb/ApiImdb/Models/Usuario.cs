using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace ApiImdb.Models
{
    public class Usuario
    {
        [BindNever]
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
    }
}
