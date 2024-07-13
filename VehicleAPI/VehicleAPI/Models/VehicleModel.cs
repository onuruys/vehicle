#nullable enable
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
    public class VehicleModel
    {
        public int? VehicleId { get; set; }
        public string Name { get; set; }
        public int? ModelId { get; set; }
        public string? Plate { get; set; }
        public int? ModelYear { get; set; }
        public string? Color { get; set; }
        public bool Active { get; set; }
    }

    public class VehicleDetailModel : VehicleModel
    {
    }

    public class VehicleFilterModel
    {
        public int? VehicleId { get; set; }
        public string Name { get; set; }
        public int? ModelId { get; set; }
        public string? Plate { get; set; }
        public int? ModelYear { get; set; }
        public string? Color { get; set; }
        public bool? Active { get; set; }
    }

    public class VehicleLovModel
    {
        public int VehicleId { get; set; }
        public string Name { get; set; }
        public int ModelId { get; set; }
        public string Plate { get; set; }
        public int ModelYear { get; set; }
        public string Color { get; set; }
    }
}