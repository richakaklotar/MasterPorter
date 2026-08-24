using System.ComponentModel.DataAnnotations;

namespace AutoEntity
{
    public class ApiModel
    {
        public class Plant
        {
            [Key]
            public int PlantID { get; set; }

            [Required]
            public string PlantName { get; set; }

            [Required]
            public string PlantCode { get; set; }
        }
    }
}
