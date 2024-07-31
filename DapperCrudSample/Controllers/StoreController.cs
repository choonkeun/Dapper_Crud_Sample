using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DapperCrudSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly SqlConnection _connection;

        public StoreController(SqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet]
        public async Task<ActionResult<List<Store>>> GetAllStores()
        {
            IEnumerable<Store> stores = await SelectAllStore();
            return Ok(stores);
        }

        [HttpGet("{storeId}")]
        public async Task<ActionResult<Store>> GetStore(int storeId)
        {
            var stores = await _connection.QueryFirstAsync<Store>(
                "select * from Store where store_id = @store_id", new { store_id = storeId });
            return Ok(stores);
        }

        [HttpPost]
        public async Task<ActionResult<List<Store>>> CreateStore(Store store)
        {
            string sql = "insert into Store (store_id, store_name, store_address, city, state, zip) values (@store_id, @store_name, @store_address, @city, @state, @zip)";
            await _connection.ExecuteAsync(sql, store);
            return Ok(await SelectAllStore());
        }

        [HttpPut]
        public async Task<ActionResult<List<Store>>> UpdateStore(Store store)
        {
            string sql = "update Store set @store_name = @store_name, store_address = @store_address, city = @city, state = @state, zip = @zip where store_id = @store_id";
            await _connection.ExecuteAsync(sql, store);
            return Ok(await SelectAllStore());
        }

        [HttpDelete("{storeId}")]
        public async Task<ActionResult<List<Store>>> DeleteStore(int storeId)
        {
            await _connection.ExecuteAsync("delete from Store where store_id = @store_id", new { store_id = storeId });
            return Ok(await SelectAllStore());
        }

        private async Task<IEnumerable<Store>> SelectAllStore()
        {
            return await _connection.QueryAsync<Store>("select * from Store");
        }
    }
}
