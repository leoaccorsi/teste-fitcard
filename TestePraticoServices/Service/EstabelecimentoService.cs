using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestePraticoModel.Model;
using TestePraticoRepository.Interface;
using TestePraticoRepository.Repository;
using TestePraticoServices.Helpers;
using TestePraticoServices.Interface;

namespace TestePraticoServices.Service
{
    public class EstabelecimentoService : IEstabelecimentoService
    {
        private IEstabelecimentoRepository _estabelecimentoRepository;
        private CategoriaService _categoriaService;

        public EstabelecimentoService() : this(new EstabelecimentoRepository(), new CategoriaService())
        {
        }

        public EstabelecimentoService(IEstabelecimentoRepository estabelecimentoRepository, CategoriaService categoriaService)
        {
            _estabelecimentoRepository = estabelecimentoRepository;
            _categoriaService = categoriaService;
        }

        public List<EstabelecimentoModel> GetAll()
        {
            return _estabelecimentoRepository.GetAll();
        }

        public bool Create(EstabelecimentoModel estabelecimento)
        {
            if (_estabelecimentoRepository.FindByCnpj(estabelecimento.cnpj) != null)
                return false;

            if (!Helper.CnpjValido(estabelecimento.cnpj))
                return false;

            if (!Helper.ContaValida(estabelecimento.conta))
                return false;

            if (!Helper.AgenciaValida(estabelecimento.agencia))
                return false;

            if (!Helper.EmailValido(estabelecimento.email))
                return false;

            if (!this.ValidarCategoria(estabelecimento))
                return false;

            _estabelecimentoRepository.Create(estabelecimento);
            return true;
        }

        private bool ValidarCategoria(EstabelecimentoModel estabelecimento)
        {
            if (estabelecimento.cod_categoria == null)
                return true;

            var categoria = _categoriaService.GetSingle((long)estabelecimento.cod_categoria);

            switch (categoria.nome.ToUpper())
            {
                case "SUPERMERCADO":
                    return !String.IsNullOrEmpty(estabelecimento.telefone);
                default:
                    return true;
            }
        }
    }
}
