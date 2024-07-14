namespace Vehicle.DataAccessLayer.Entities
{
    public class VehicleEntity
    {
        public int? VehicleId { get; set; }
        public string Name { get; set; }
        public int? ModelId { get; set; }
        public string? Plate { get; set; }
        public int? ModelYear { get; set; }
        public string? Color { get; set; }
        public bool Active { get; set; }
    }

    // Detail Entity
    public class VehicleDetailEntity : VehicleEntity
    {
    }

    // Filter Entity
    public class VehicleFilterEntity
    {
        public int? VehicleId { get; set; }
        public string Name { get; set; }
        public int? ModelId { get; set; }
        public string? Plate { get; set; }
        public int? ModelYear { get; set; }
        public string? Color { get; set; }
        public bool? Active { get; set; }
    }

    // List of Value Entity
    public class VehicleLovEntity
    {
        public int VehicleId { get; set; }
        public string Name { get; set; }
        public int ModelId { get; set; }
        public string Plate { get; set; }
        public int ModelYear { get; set; }
        public string Color { get; set; }
    }
}