using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestePraticoModel.Model;

namespace TestePraticoRepository.Interface
{
    public interface IEstabelecimentoRepository
    {
        List<EstabelecimentoModel> GetAll();
        EstabelecimentoModel GetSingle(long id);
        EstabelecimentoModel FindByCnpj(string cnpj);
        bool Create(EstabelecimentoModel estabelecimento);
    }
}
