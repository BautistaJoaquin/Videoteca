using Microsoft.EntityFrameworkCore;

namespace challenge_NET.Models

{
   
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<Personaje> Personaje {get;set;}
        public DbSet<Pelicula> Pelicula {get;set;}

        public DbSet<PersonajePelicula> PersonajePeliculas {get;set;}

        public DbSet<Userdetails> Userdetails {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        //Write Fluent API configurations here
        
        modelBuilder.Entity<PersonajePelicula>()
        .HasKey(pp => new { pp.PersonajeId, pp.PeliculaId });

        modelBuilder.Entity<PersonajePelicula>()
        .HasOne(pp => pp.pelicula)
        .WithMany(p => p.PersonajePelicula  )
        .HasForeignKey(pp => pp.PeliculaId);  
        
        modelBuilder.Entity<PersonajePelicula>()
        .HasOne(pp => pp.personaje)
        .WithMany(per => per.PersonajePelicula)
        .HasForeignKey(pp => pp.PersonajeId);

        modelBuilder.Entity<Userdetails>(entity =>
            {
                entity.ToTable("Userdetails");
 
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);
 
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
 
                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }    
    }
}