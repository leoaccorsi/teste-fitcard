using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestePraticoModel.Enum;
using TestePraticoModel.Model;

namespace TestePraticoServices.Interface
{
    public interface IEstabelecimentoService
    {
        List<EstabelecimentoModel> GetAll();
        ERetornoEstabelecimento Create(EstabelecimentoModel estabelecimento);
    }
}
