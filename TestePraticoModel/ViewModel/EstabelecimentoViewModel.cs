using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestePraticoModel.Model;

namespace TestePraticoModel.ViewModel
{
    public class EstabelecimentoViewModel
    {
        public long id { get; set; }
        [Display(Name = "Razão Social")]
        [Required(ErrorMessage = "O campo 'Razão Social' é obrigatório")]
        public string razao_social { get; set; }

        [Display(Name = "Nome Fantasia")]
        public string nome_fantasia { get; set; }

        [Display(Name = "CNPJ")]
        [Required(ErrorMessage = "O campo 'CNPJ' é obrigatório")]
        public string cnpj { get; set; }

        [Display(Name = "E-mail")]
        public string email { get; set; }

        [Display(Name = "Endereço")]
        public string endereco { get; set; }

        [Display(Name = "Cidade")]
        public string cidade { get; set; }

        [Display(Name = "Estado")]
        public string estado { get; set; }

        [Display(Name = "Telefone")]
        public string telefone { get; set; }

        [Display(Name = "Data Cadastro")]
        public DateTime data_cadastro { get; set; }

        [Display(Name = "Categoria")]
        public long? cod_categoria { get; set; }

        [Display(Name = "Status")]
        public Int16? status { get; set; }

        [Display(Name = "Agência")]
        public string agencia { get; set; }

        [Display(Name = "Conta")]
        public string conta { get; set; }
        
        public SelectList Categorias { get; set; }
    }
}
