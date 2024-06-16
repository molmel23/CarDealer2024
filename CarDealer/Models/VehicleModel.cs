using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarDealer.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Model Name")]
        public string Name { get; set; }


        [Display(Name = "Make")]
        public int MakeId { get; set; }

        [ForeignKey("MakeId")]
        public Make Make { get; set; }
    }

}
