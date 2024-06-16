using System.ComponentModel;
using System.ComponentModel.DataAnnotations; //etiquetas

namespace CarDealer.Models
{
    public class Make
    {
        [Key] //sea llave primaria
        public int Id { get; set; }

        
        [DisplayName("Make Name")]
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
       


    }
}
