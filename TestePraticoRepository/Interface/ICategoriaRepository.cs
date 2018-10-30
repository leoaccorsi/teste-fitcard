using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestePraticoModel.Model;

namespace TestePraticoRepository.Interface
{
    public interface ICategoriaRepository
    {
        List<CategoriaModel> GetAll();
        bool Create(CategoriaModel categoria);
    }
}
