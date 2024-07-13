namespace Vehicle.DataAccessLayer.Entities
{
    public class ModelEntity
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int BrandId { get; set; }
        public bool Active { get; set; }
    }

    // Detail Entity
    public class ModelDetailEntity : ModelEntity
    {
    }

    // Filter Entity
    public class ModelFilterEntity
    {
        public int? ModelId { get; set; }
        public string ModelName { get; set; }
        public int? BrandId { get; set; }
        public bool? Active { get; set; }
    }

    // List of Value Entity
    public class ModelLovEntity
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int BrandId { get; set; }
    }
}