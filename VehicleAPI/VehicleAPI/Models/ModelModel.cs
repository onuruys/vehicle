using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Vehicle.DataAccessLayer.Entities;
using VehicleAPI.Utils;

namespace VehicleAPI.Models
{
    public class ModelModel
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int BrandId { get; set; }
        public bool Active { get; set; }
    }

    public class ModelDetailModel : ModelModel
    {
    }

    public class ModelFilterModel
    {
        public int? ModelId { get; set; }
        public string ModelName { get; set; }
        public int? BrandId { get; set; }
        public bool? Active { get; set; }
    }

    public class ModelLovModel
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int BrandId { get; set; }
    }
}