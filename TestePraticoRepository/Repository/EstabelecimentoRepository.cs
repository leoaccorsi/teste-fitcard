using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestePraticoModel.Model;
using TestePraticoRepository.Interface;

namespace TestePraticoRepository.Repository
{
    public class EstabelecimentoRepository : IEstabelecimentoRepository
    {
        private SqlConnection _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalDbConnection"].ConnectionString);

        public List<EstabelecimentoModel> GetAll()
        {
            var query = "SELECT * FROM Estabelecimento";
            return _conn.Query<EstabelecimentoModel>(query).ToList();
        }

        public EstabelecimentoModel GetSingle(long id)
        {
            var query = "SELECT * FROM Estabelecimento WHERE Id = @id";
            return _conn.Query<EstabelecimentoModel>(query, new { id }).FirstOrDefault();
        }

        public EstabelecimentoModel FindByCnpj(string cnpj)
        {
            var query = "SELECT * FROM Estabelecimento WHERE cnpj = @cnpj";
            return _conn.Query<EstabelecimentoModel>(query, new { cnpj }).FirstOrDefault();
        }

        public bool Create(EstabelecimentoModel estabelecimento)
        {
            var query = @"INSERT INTO Estabelecimento(razao_social, nome_fantasia, cnpj, email, endereco, cidade, estado, telefone, data_cadastro, cod_categoria, status, agencia, conta) 
                                VALUES(@razao_social, @nome_fantasia, @cnpj, @email, @endereco, @cidade, @estado, @telefone, GETDATE(), @cod_categoria, 1, @agencia, @conta)";
            return _conn.Execute(query, estabelecimento) > 0;
        }
    }
}
