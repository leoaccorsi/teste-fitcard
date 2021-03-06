﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestePraticoModel.Enum;
using TestePraticoModel.Model;
using TestePraticoRepository.Interface;
using TestePraticoRepository.Repository;
using TestePraticoServices.Interface;

namespace TestePraticoServices.Service
{
    public class CategoriaService : ICategoriaService
    {
        private ICategoriaRepository _categoriaRepository;

        public CategoriaService() : this(new CategoriaRepository())
        {
        }

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public List<CategoriaModel> GetAll()
        {
            return _categoriaRepository.GetAll();
        }

        public ERetornoEstabelecimento Create(CategoriaModel categoria)
        {
            if (_categoriaRepository.FindByName(categoria.nome) != null)
            {
                return ERetornoEstabelecimento.CategoriaRepetida;
            }

            _categoriaRepository.Create(categoria);
            return ERetornoEstabelecimento.SucessoCadastro;
        }

        public CategoriaModel GetSingle(long id)
        {
            return _categoriaRepository.GetSingle(id);
        }
    }
}
