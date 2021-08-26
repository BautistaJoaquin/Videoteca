using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace challenge_NET.Models
{
    public class PersonajePelicula
    {
       
        public int PeliculaId  {get; set;}
        public Pelicula pelicula {get; set;}

        public int PersonajeId {get;set;}
        public Personaje personaje {get; set;}
    }
}