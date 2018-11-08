using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestePraticoModel.Enum;
using TestePraticoModel.Model;
using TestePraticoModel.ViewModel;
using TestePraticoRepository.Interface;

namespace TestePraticoRepository.Repository
{
    public class EstabelecimentoRepository : IEstabelecimentoRepository
    {
        private SqlConnection _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalDbConnection"].ConnectionString);

        public List<EstabGridViewModel> GetAll()
        {
            var query = @"SELECT e.*, c.nome 'categoria' FROM Estabelecimento e
                         LEFT JOIN Categoria c on e.cod_categoria = c.id 
                         WHERE status <> -1";
            return _conn.Query<EstabGridViewModel>(query).ToList();
        }

        public EstabelecimentoModel GetSingle(long id)
        {
            var query = "SELECT * FROM Estabelecimento WHERE Id = @id and status <> -1";
            return _conn.Query<EstabelecimentoModel>(query, new { id }).FirstOrDefault();
        }

        public EstabelecimentoModel FindByCnpj(string cnpj)
        {
            var query = "SELECT * FROM Estabelecimento WHERE cnpj = @cnpj and status <> -1";
            return _conn.Query<EstabelecimentoModel>(query, new { cnpj }).FirstOrDefault();
        }

        public bool Create(EstabelecimentoModel estabelecimento)
        {
            var query = @"INSERT INTO Estabelecimento(razao_social, nome_fantasia, cnpj, email, endereco, cidade, estado, telefone, data_cadastro, cod_categoria, status, agencia, conta) 
                                VALUES(@razao_social, @nome_fantasia, @cnpj, @email, @endereco, @cidade, @estado, @telefone, GETDATE(), @cod_categoria, 1, @agencia, @conta)";
            return _conn.Execute(query, estabelecimento) > 0;
        }

        public bool Edit(EstabelecimentoModel estabelecimento)
        {
            var query = @"UPDATE Estabelecimento SET razao_social = @razao_social, nome_fantasia = @nome_fantasia, 
                                                     email = @email, endereco = @endereco, cidade = @cidade, estado = @estado, 
                                                     telefone = @telefone, cod_categoria = @cod_categoria, agencia = @agencia, conta = @conta
                          WHERE ID = @ID";
            return _conn.Execute(query, estabelecimento) > 0;
        }

        public bool EditStatus(long id, EStatus status)
        {
            var query = @"UPDATE Estabelecimento SET status = @status
                          WHERE ID = @ID";
            return _conn.Execute(query, new { status, id }) > 0;
        }

        public bool Delete(long id)
        {
            var query = @"UPDATE Estabelecimento SET status = -1
                          WHERE ID = @ID";
            return _conn.Execute(query, new { id }) > 0;
        }
    }
}
