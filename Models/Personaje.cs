using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;

namespace challenge_NET.Models
{
    public class Personaje
    {
        [Key]
        public int PersonajeId {get;set;}
        
        [Required(ErrorMessage = "Ingrese una imagen")]
        [Display(Name = "Imagen")]
        public string Imagen { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre")]
        public string Nombre {get; set;}

        
        public int Edad {get; set;}

        public double Peso {get; set;}

        [Required(ErrorMessage = "Ingrese una historia")]
        public string Historia {get; set;}

        public ICollection<PersonajePelicula> PersonajePelicula {get; set;}

    }
}