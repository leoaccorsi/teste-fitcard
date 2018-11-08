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
        SucessoCadastro,
        SucessoEdicao,
        SucessoDelete,
        CnpjJaUtilizado,
        CnpjInvalido,
        ContaInvalida,
        AgenciaInvalida,
        EmailInvalido,
        TelefoneObrigatorio,
        ErroDesconhecido,
        CategoriaRepetida
    }
}
