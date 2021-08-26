using Microsoft.AspNetCore.Http; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace challenge_NET.ViewModels
{
    public class PersonajeViewModel
    { 

        public int Id {get;set;} 

        [Required(ErrorMessage = "Ingrese un nombre")]
        public string Nombre {get; set;}

        public string Imagen { get; set; }

        public int Edad {get; set;}

        public double Peso {get; set;}

        [Required(ErrorMessage = "Ingrese una historia")]
        public string Historia {get; set;}

        [Required(ErrorMessage = "Ingrese una imagen")]
        [Display(Name = "Imagen")]
        public IFormFile CharacterImage { get; set; }
    
    }
}