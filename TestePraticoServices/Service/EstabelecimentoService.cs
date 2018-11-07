using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestePraticoModel.Enum;
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

        public ERetornoEstabelecimento Create(EstabelecimentoModel estabelecimento)
        {
            if (_estabelecimentoRepository.FindByCnpj(estabelecimento.cnpj) != null)
                return ERetornoEstabelecimento.CnpjJaUtilizado;

            if (!Helper.CnpjValido(estabelecimento.cnpj))
                return ERetornoEstabelecimento.CnpjInvalido;

            if (!Helper.ContaValida(estabelecimento.conta))
                return ERetornoEstabelecimento.ContaInvalida;

            if (!Helper.AgenciaValida(estabelecimento.agencia))
                return ERetornoEstabelecimento.AgenciaInvalida;

            if (!Helper.EmailValido(estabelecimento.email))
                return ERetornoEstabelecimento.EmailInvalido;

            var validaCategoria = this.ValidarCategoria(estabelecimento);
            if (validaCategoria != ERetornoEstabelecimento.Ok)
                return validaCategoria;

            _estabelecimentoRepository.Create(estabelecimento);
            return ERetornoEstabelecimento.Cadastrado;
        }

        private ERetornoEstabelecimento ValidarCategoria(EstabelecimentoModel estabelecimento)
        {
            if (estabelecimento.cod_categoria == null)
                return ERetornoEstabelecimento.Ok;

            var categoria = _categoriaService.GetSingle((long)estabelecimento.cod_categoria);

            switch (categoria.nome.ToUpper())
            {
                case "SUPERMERCADO":
                    return ((String.IsNullOrEmpty(estabelecimento.telefone)) ? ERetornoEstabelecimento.TelefoneObrigatorio : ERetornoEstabelecimento.Ok);
                default:
                    return ERetornoEstabelecimento.Ok;
            }
        }
    }
}
