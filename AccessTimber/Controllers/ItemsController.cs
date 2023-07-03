using AccessTimber.DBContext;
using ATModel;
using ATServices;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AccessTimber.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IDapper _dapper;
        public readonly DatabaseContext _dbContext;
        const string _spName = "SP_SET_ITEMS";

        public ItemsController(DatabaseContext dbContext, IDapper dapper)
        {
            _dbContext = dbContext;
            _dapper = dapper;
        }

        [HttpPost]
        public void Post(ItemsDBModel _dbModel)
        {
            //string _spName = "PMS.SP_SET_ITEMS";
            var dbparams = new DynamicParameters();

            if (_dbModel.ItemsID > 0)
                dbparams.Add("QryOption", 2, DbType.Int32);
            else
                dbparams.Add("QryOption", 1, DbType.Int32);

            dbparams.Add("ItemsID", _dbModel.ItemsID, DbType.Int32);
            dbparams.Add("ItemName", _dbModel.ItemName, DbType.String);
            dbparams.Add("Price", _dbModel.Price, DbType.String);
            dbparams.Add("Active", _dbModel.Active, DbType.Int32);

            Task.FromResult(_dapper.Save<int>(_spName, dbparams, commandType: CommandType.StoredProcedure));
        }

        [HttpGet]
        public async Task<List<ItemsDBModel>> Get()
        {
            //string _spName = "PMS.SP_SET_ITEMS";
            var dbparams = new DynamicParameters();
            dbparams.Add("QryOption", 3, DbType.Int32);
            return await Task.FromResult(_dapper.GetAll<ItemsDBModel>(_spName, dbparams, commandType: CommandType.StoredProcedure));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //string _spName = "PMS.SP_SET_ROLE_MATRIX_OPINION";
            var dbparams = new DynamicParameters();

            dbparams.Add("QryOption", 4, DbType.Int32);
            dbparams.Add("ItemsID", id, DbType.Int32);

            var _data = _dapper.GetAll<ItemsDBModel>(_spName, dbparams, commandType: CommandType.StoredProcedure);
            if (_data != null)
            {
                return Ok(_data);
            }
            return NotFound();
        }

        [HttpGet("RemoveRow/{id}")]
        public IActionResult Delete(int id)
        {
            //string _spName = "PMS.SP_SET_ROLE_MATRIX_OPINION";
            var dbparams = new DynamicParameters();
            dbparams.Add("QryOption", 5, DbType.Int32);
            dbparams.Add("ItemsID", id, DbType.Int32);
            Task.FromResult(_dapper.Save<int>(_spName, dbparams, commandType: CommandType.StoredProcedure));
            return Ok();
        }

        [HttpGet("GetAllActiveOpinion")]
        public async Task<List<ItemsDBModel>> GetAllActiveOpinion()
        {
            //string _spName = "PMS.SP_SET_ROLE_MATRIX_OPINION";
            var dbparams = new DynamicParameters();
            dbparams.Add("QryOption", 6, DbType.Int32);
            return await Task.FromResult(_dapper.GetAll<ItemsDBModel>(_spName, dbparams, commandType: CommandType.StoredProcedure));
        }
    }
}
