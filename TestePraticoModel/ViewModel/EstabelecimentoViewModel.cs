using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestePraticoModel.Model;

namespace TestePraticoModel.ViewModel
{
    public class EstabelecimentoViewModel
    {
        public long id { get; set; }
        [Required]
        public string razao_social { get; set; }
        public string nome_fantasia { get; set; }
        [Required]
        public string cnpj { get; set; }
        public string email { get; set; }
        public string endereco { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public DateTime data_cadastro { get; set; }
        public long? cod_categoria { get; set; }
        public Int16? status { get; set; }
        public string agencia { get; set; }
        public string conta { get; set; }
    }
}
