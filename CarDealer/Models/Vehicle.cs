using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealer.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [YearValidation("Year not valid")]
        public int Year { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; }

        [DisplayName("Picture")]
        public string? PictureUrl { get; set; }


        [DisplayName("Make/Model")]
        public int ModelId { get; set; }


        [Required]
        [ForeignKey("ModelId")]
        public VehicleModel Model { get; set; }

    }


    [AttributeUsage(AttributeTargets.Field |
        AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    class YearValidation : ValidationAttribute
    {
        public YearValidation(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        public override bool IsValid(object value)
        {
            if ((int)value >= 1950 && (int)value <= DateTime.Now.Year + 1)
            {
                return true;
            }
            return false;
        }
    }

}
