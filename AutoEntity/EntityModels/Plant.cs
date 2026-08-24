using System;
using System.Collections.Generic;

namespace AutoEntity.EntityModels
{
    public partial class Plant
    {
        public int PlantId { get; set; }
        public string PlantName { get; set; } = null!;
        public string PlantCode { get; set; } = null!;
        public bool? Isactive { get; set; }
    }
}
