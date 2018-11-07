using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestePraticoModel.Enum
{
    public enum ERetornoEstabelecimento
    {
        Ok,
        Cadastrado,
        CnpjJaUtilizado,
        CnpjInvalido,
        ContaInvalida,
        AgenciaInvalida,
        EmailInvalido,
        TelefoneObrigatorio
    }
}
