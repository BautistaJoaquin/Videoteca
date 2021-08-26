using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace challenge_NET.Models
{
    public class Pelicula
    {
        [Key]
        public int PeliculaId {get;set;}

        public Byte[] Imagen {get; set;}

        public string Historia {get; set;}

        public DateTime FechaCreacion {get;set;}

        public int Calificacion {get;set;}

        public ICollection<PersonajePelicula> PersonajePelicula {get; set;}
        public ICollection<Genero> GeneroLink {get; set;}

    }
}