using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace challenge_NET.Models
{
    public class Genero
    {
        [Key]
        public int GeneroId {get;set;}

        public Byte[] Imagen {get;set;}

        [ForeignKey("FK_Pelicula")]
        public int PeliculaId {get;set;}
    }
}