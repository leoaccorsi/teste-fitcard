﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestePraticoModel.Enum;
using TestePraticoModel.Model;

namespace TestePraticoServices.Interface
{
    public interface ICategoriaService
    {
        List<CategoriaModel> GetAll();
        ERetornoEstabelecimento Create(CategoriaModel categoria);
        CategoriaModel GetSingle(long id);
    }
}
