using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ApiImdb.Models
{
    public class Ator
    {
        [BindNever]
        public int AtorId { get; set; }
        public string Nome { get; set; }
    }
}
