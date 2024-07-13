using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.DataAccessLayer.Entities;
using Vehicle.DataAccessLayer.Utils;

namespace Vehicle.DataAccessLayer.Repository
{
    public interface
        IModelRepository : IBaseRepository<int, ModelEntity, ModelDetailEntity, ModelFilterEntity, ModelLovEntity>
    {
    }

    public class ModelRepository : IModelRepository
    {
        private readonly IDataAccess _dataAccess;

        public ModelRepository(IDataAccess dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        public async Task<int> Create(ModelEntity entity)
        {
            var sql = "dbo.DEF_Model_Create";
            var sqlParams = new DynamicParameters();
            sqlParams.Add("ModelID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            sqlParams.Add("ModelName", entity.ModelName);
            sqlParams.Add("BrandID", entity.BrandId);
            sqlParams.Add("Active", entity.Active);

            try
            {
                using (var con = _dataAccess.CreateConnection())
                {
                    await con.ExecuteAsync(
                        sql: sql,
                        param: sqlParams,
                        commandType: CommandType.StoredProcedure
                    );
                    var modelId = sqlParams.Get<int>("ModelID");
                    return modelId;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task Delete(int id)
        {
            var sql = "dbo.DEF_Model_Delete";
            var sqlParams = new DynamicParameters();
            sqlParams.Add("ModelID", id);

            try
            {
                using (var con = _dataAccess.CreateConnection())
                {
                    await con.ExecuteAsync(
                        sql: sql,
                        param: sqlParams,
                        commandType: CommandType.StoredProcedure
                    );
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<ModelDetailEntity> Read(int id)
        {
            var sql = "dbo.DEF_Model_Read";
            var sqlParams = new DynamicParameters();
            sqlParams.Add("ModelId", id);

            try
            {
                using (var con = _dataAccess.CreateConnection())
                {
                    var result = await con.QueryAsync<ModelDetailEntity>(
                        sql: sql,
                        param: sqlParams,
                        commandType: CommandType.StoredProcedure
                    );
                    return result.FirstOrDefault();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<ModelLovEntity>> ReadLov()
        {
            var sql = "dbo.DEF_Model_Lov";
            var sqlParams = new DynamicParameters();

            try
            {
                using (var con = _dataAccess.CreateConnection())
                {
                    var result = await con.QueryAsync<ModelLovEntity>(
                        sql: sql,
                        param: sqlParams,
                        commandType: CommandType.StoredProcedure
                    );
                    return result.AsList();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<ModelDetailEntity>> ReadAll(ModelFilterEntity filter)
        {
            var sql = "dbo.DEF_Model_ReadAll";
            var sqlParams = new DynamicParameters();
            sqlParams.Add("ModelID", filter.ModelId);
            sqlParams.Add("ModelName", filter.ModelName);
            sqlParams.Add("BrandID", filter.BrandId);
            sqlParams.Add("Active", filter.Active);

            try
            {
                using (var con = _dataAccess.CreateConnection())
                {
                    var result = await con.QueryAsync<ModelDetailEntity>(
                        sql: sql,
                        param: sqlParams,
                        commandType: CommandType.StoredProcedure
                    );
                    return result.ToList();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task Update(ModelEntity entity)
        {
            var sql = "dbo.DEF_Model_Update";
            var sqlParams = new DynamicParameters();
            sqlParams.Add("ModelID", entity.ModelId);
            sqlParams.Add("ModelName", entity.ModelName);
            sqlParams.Add("BrandID", entity.BrandId);
            sqlParams.Add("Active", entity.Active);

            try
            {
                using (var con = _dataAccess.CreateConnection())
                {
                    await con.ExecuteAsync(
                        sql: sql,
                        param: sqlParams,
                        commandType: CommandType.StoredProcedure
                    );
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}