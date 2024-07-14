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
    public interface IVehicleRepository : IBaseRepository<int, VehicleEntity, VehicleDetailEntity, VehicleFilterEntity,
        VehicleLovEntity>
    {
    }

    public class VehicleRepository : IVehicleRepository
    {
        private readonly IDataAccess _dataAccess;

        public VehicleRepository(IDataAccess dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        public async Task<int> Create(VehicleEntity entity)
        {
            var sql = "dbo.DEF_Vehicle_Create";
            var sqlParams = new DynamicParameters();

            sqlParams.Add("VehicleID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            sqlParams.Add("Name", entity.Name);
            sqlParams.Add("ModelID", entity.ModelId);
            sqlParams.Add("Active", entity.Active);
            sqlParams.Add("Plate", (object)entity.Plate ?? DBNull.Value);
            sqlParams.Add("ModelYear", (object)entity.ModelYear ?? DBNull.Value);
            sqlParams.Add("Color", (object)entity.Color ?? DBNull.Value);
            try
            {
                using (var connection = _dataAccess.CreateConnection())
                {
                    await connection.ExecuteAsync(sql, sqlParams, commandType: CommandType.StoredProcedure);
                    return sqlParams.Get<int>("VehicleID");
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("INVALID_MODEL_YEAR"))
                {
                    throw new Exception("INVALID_MODEL_YEAR");
                }
                else if (ex.Message.Contains("INVALID_PLATE"))
                {
                    throw new Exception("INVALID_PLATE");
                }
                else
                {
                    Console.WriteLine(ex.Message);
                    throw new Exception("An unexpected error occurred.");
                }
            }

        }

        public async Task Delete(int id)
        {
            var sql = "dbo.DEF_Vehicle_Delete";
            var sqlParams = new DynamicParameters();
            sqlParams.Add("VehicleID", id);

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

        public async Task<List<VehicleDetailEntity>> ReadAll(VehicleFilterEntity? filter)
        {
            var sql = "dbo.DEF_Vehicle_ReadAll";
            var sqlParams = new DynamicParameters();
            sqlParams.Add("VehicleID", filter?.VehicleId);
            sqlParams.Add("Name", filter?.Name);
            sqlParams.Add("ModelID", filter?.ModelId);
            sqlParams.Add("Active", filter?.Active);
            sqlParams.Add("Plate", filter?.Plate);
            sqlParams.Add("ModelYear", filter?.ModelYear);
            sqlParams.Add("Color", filter?.Color);

            try
            {
                using (var con = _dataAccess.CreateConnection())
                {
                    var result = await con.QueryAsync<VehicleDetailEntity>(
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

        public async Task<VehicleDetailEntity> Read(int id)
        {
            var sql = "dbo.DEF_Vehicle_Read";
            var sqlParams = new DynamicParameters();
            sqlParams.Add("VehicleID", id);

            try
            {
                using (var con = _dataAccess.CreateConnection())
                {
                    var result = await con.QueryAsync<VehicleDetailEntity>(
                        sql: sql,
                        param: sqlParams,
                        commandType: CommandType.StoredProcedure
                    );
                    return result.SingleOrDefault();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task Update(VehicleEntity entity)
        {
            var sql = "dbo.DEF_Vehicle_Update";
            var sqlParams = new DynamicParameters();
            sqlParams.Add("VehicleID", entity.VehicleId);
            sqlParams.Add("Name", entity.Name);
            sqlParams.Add("ModelId", entity.ModelId);
            sqlParams.Add("Active", entity.Active);
            sqlParams.Add("Plate", entity.Plate);
            sqlParams.Add("ModelYear", entity.ModelYear);
            sqlParams.Add("Color", entity.Color);

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

        public async Task<List<VehicleLovEntity>> ReadLov()
        {
            var sql = "dbo.DEF_Vehicle_Lov";
            var sqlParams = new DynamicParameters();

            try
            {
                using (var con = _dataAccess.CreateConnection())
                {
                    var result = await con.QueryAsync<VehicleLovEntity>(
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
    }
}