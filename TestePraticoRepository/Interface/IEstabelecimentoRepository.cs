using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestePraticoModel.Enum;
using TestePraticoModel.Model;
using TestePraticoModel.ViewModel;

namespace TestePraticoRepository.Interface
{
    public interface IEstabelecimentoRepository
    {
        List<EstabGridViewModel> GetAll();
        EstabelecimentoModel GetSingle(long id);
        EstabelecimentoModel FindByCnpj(string cnpj);
        bool Create(EstabelecimentoModel estabelecimento);
        bool Edit(EstabelecimentoModel estabelecimento);
        bool Delete(long id);
        bool EditStatus(long id, EStatus status);
    }
}
