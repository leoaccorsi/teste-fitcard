using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestePraticoModel.Model;
using TestePraticoRepository.Interface;
using Dapper;

namespace TestePraticoRepository.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private SqlConnection _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalDbConnection"].ConnectionString);

        public List<CategoriaModel> GetAll()
        {
            var query = "SELECT * FROM Categoria";
            return _conn.Query<CategoriaModel>(query).ToList();
        }

        public CategoriaModel GetSingle(long id)
        {
            var query = "SELECT * FROM Categoria WHERE Id = @id";
            return _conn.Query<CategoriaModel>(query, new { id }).FirstOrDefault();
        }

        public CategoriaModel FindByName(string nome)
        {
            var query = "SELECT * FROM Categoria WHERE nome = @nome";
            return _conn.Query<CategoriaModel>(query, new { nome }).FirstOrDefault();
        }

        public bool Create(CategoriaModel categoria)
        {
            var query = "INSERT INTO CATEGORIA(nome) values(@nome)";
            return _conn.Execute(query, categoria) > 0;
        }
    }
}
