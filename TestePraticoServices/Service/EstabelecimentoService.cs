using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestePraticoModel.Model;
using TestePraticoRepository.Interface;
using TestePraticoRepository.Repository;
using TestePraticoServices.Interface;

namespace TestePraticoServices.Service
{
    public class EstabelecimentoService : IEstabelecimentoService
    {
        private IEstabelecimentoRepository _estabelecimentoRepository;

        public EstabelecimentoService() : this(new EstabelecimentoRepository())
        {
        }

        public EstabelecimentoService(IEstabelecimentoRepository estabelecimentoRepository)
        {
            _estabelecimentoRepository = estabelecimentoRepository;
        }

        public List<EstabelecimentoModel> GetAll()
        {
            return _estabelecimentoRepository.GetAll();
        }

        public bool Create(EstabelecimentoModel estabelecimento)
        {
            if (_estabelecimentoRepository.FindByCnpj(estabelecimento.cnpj) != null)
            {
                return false;
            }

            _estabelecimentoRepository.Create(estabelecimento);
            return true;
        }
    }
}
