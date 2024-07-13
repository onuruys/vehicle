using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle.DataAccessLayer.Entities;
using Vehicle.DataAccessLayer.Repository;
using VehicleAPI.Models;

namespace VehicleAPI.Services
{
    public interface IModelService
    {
        Task<int> CreateModel(ModelModel modelModel);
        Task Update(ModelModel modelModel);
        Task Delete(int modelId);
        Task<List<ModelDetailModel>> ReadAll(ModelFilterModel filter);
        Task<ModelDetailModel> Read(int modelId);
        Task<List<ModelLovModel>> ReadLov();
    }

    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;

        public ModelService(IModelRepository modelRepository, IMapper mapper)
        {
            _mapper = mapper;
            _modelRepository = modelRepository;
        }

        public async Task<int> CreateModel(ModelModel model)
        {
            var entity = _mapper.Map<ModelEntity>(model);
            return await _modelRepository.Create(entity);
        }

        public async Task Delete(int modelId)
        {
            await _modelRepository.Delete(modelId);
        }

        public async Task<ModelDetailModel> Read(int modelId)
        {
            var result = await _modelRepository.Read(modelId);
            return _mapper.Map<ModelDetailModel>(result);
        }

        public async Task<List<ModelDetailModel>> ReadAll(ModelFilterModel filter)
        {
            var result = await _modelRepository.ReadAll(_mapper.Map<ModelFilterEntity>(filter));
            return _mapper.Map<List<ModelDetailModel>>(result);
        }

        public async Task<List<ModelLovModel>> ReadLov()
        {
            var result = await _modelRepository.ReadLov();
            return _mapper.Map<List<ModelLovModel>>(result);
        }

        public async Task Update(ModelModel modelModel)
        {
            var entity = _mapper.Map<ModelEntity>(modelModel);
            await _modelRepository.Update(entity);
        }
    }
}