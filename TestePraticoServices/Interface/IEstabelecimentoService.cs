using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestePraticoModel.Enum;
using TestePraticoModel.Model;
using TestePraticoModel.ViewModel;

namespace TestePraticoServices.Interface
{
    public interface IEstabelecimentoService
    {
        EstabelecimentoModel GetSingle(long id);
        List<EstabGridViewModel> GetAll();
        ERetornoEstabelecimento Create(EstabelecimentoModel estabelecimento);
        ERetornoEstabelecimento Edit(EstabelecimentoModel estabelecimento);
        ERetornoEstabelecimento Delete(long id);
    }
}
