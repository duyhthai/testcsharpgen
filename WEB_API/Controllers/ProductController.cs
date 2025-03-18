using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly string _connectionString;
        public ProductController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnectionString");
        }
        // GET: api/Product
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            using(var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var result = await conn.QueryAsync<Product>("", null,null,null,System.Data.CommandType.Text);
                return result;
            }            
        }

        // GET api/Product/5
        [HttpGet("{id}")]
        public async Task<Product> Get(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                var result = await conn.QueryAsync<Product>("Get_Product_ById", parameters, null, null, System.Data.CommandType.StoredProcedure);
                return result.Single();
            }
        }

        public async Task<Product> Get(int id, string name)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                parameters.Add("@name", name);
                var result = await conn.QueryAsync<Product>("Search_Product", parameters, null, null, System.Data.CommandType.StoredProcedure);
                return result.Single();
            }
        }

        // POST api/Product
        [HttpPost]
        public async Task<int> Post([FromBody] Product product)
        {
            int newId = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@sku", product.Sku);
                parameters.Add("@content", product.Content);
                parameters.Add("@price", product.Price);
                parameters.Add("@isActive", product.IsActive);
                parameters.Add("@imageUrl", product.ImageUrl);
                parameters.Add("@id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                var result = await conn.ExecuteAsync("Create_Product", parameters, null, null, System.Data.CommandType.StoredProcedure);
                newId = parameters.Get<int>("@id");
            }
            return newId;
        }

        // PUT api/Product
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Product product)
        {
      
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                parameters.Add("@sku", product.Sku);
                parameters.Add("@content", product.Content);
                parameters.Add("@price", product.Price);
                parameters.Add("@isActive", product.IsActive);
                parameters.Add("@imageUrl", product.ImageUrl);
                await conn.ExecuteAsync("Update_Product", parameters, null, null, System.Data.CommandType.StoredProcedure);
            }
            
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                var result = await conn.ExecuteAsync("Delete_Product_ById", parameters, null, null, System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
