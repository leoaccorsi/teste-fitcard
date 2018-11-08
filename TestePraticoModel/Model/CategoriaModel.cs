using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestePraticoModel.Model
{
    public class CategoriaModel
    {
        public long id { get; set; }
        [Required(ErrorMessage = "O campo 'Nome' é obrigatório")]
        public string nome { get; set; }
    }
}
