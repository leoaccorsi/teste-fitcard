using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using TestePraticoModel.Enum;

namespace TestePraticoServices.Helpers
{
    public static class RetornoHelper
    {
        public static string RetornoEstabelecimento(ERetornoEstabelecimento enumRetorno)
        {
            ResourceManager rm = RetornoResource.ResourceManager;
            return rm.GetString(enumRetorno.ToString());
        }
    }
}
