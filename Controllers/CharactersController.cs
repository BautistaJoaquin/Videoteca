using System;
using System.IO;
using System.Linq; 
using System.Collections.Generic;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using challenge_NET.Models;
using challenge_NET.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace challenge_NET.Controllers
{
    public class CharactersController: Controller
    {
        private readonly AppDBContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        
        public CharactersController(AppDBContext context,IWebHostEnvironment hostEnvironment)
        {
            
            dbContext = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Personajes
        public async Task<IActionResult> Index()
        {
            
           var personaje = await dbContext.Personaje.ToListAsync();
        return View(personaje);
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            // var personaje = await dbContext.Personaje.FirstOrDefaultAsync(m => m.PersonajeId == id);
            Personaje personaje = await dbContext.Personaje.Include(pp => pp.PersonajePelicula).ThenInclude(p => p.pelicula).FirstOrDefaultAsync();
            // var query = dbContext.Personaje.Include(b => b.PersonajePelicula).ThenInclude(p => p.pelicula).ToList();
            
            System.Console.WriteLine(personaje);
            var personajeViewModel = new PersonajeViewModel()
            {
                
                Id = personaje.PersonajeId,
                Nombre = personaje.Nombre,
                Edad = personaje.Edad,
                Peso = personaje.Peso,
                Historia = personaje.Historia,
                Imagen = personaje.Imagen
            };

            if (personaje == null)
            {
                return NotFound();
            }

            return View(personaje);
        }
        // GET: Personajes/Create
        public IActionResult Create()
        {
            ViewData["ListaPeliculas"] = new SelectList(dbContext.Pelicula,"PeliculaId","Calificacion");
            return View();
        }

        // POST: Personajes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonajeViewModel model)
        {
          
            if (ModelState.IsValid)  
            
            {
               
                string uniqueFileName = ProcessUploadedFile(model);
                    Personaje personaje = new Personaje
                    {
                        PersonajeId  = 0,
                        Imagen = uniqueFileName,
                        Nombre = model.Nombre,
                        Edad = model.Edad,
                        Peso = model.Peso,
                        Historia = model.Historia
                    };
                    
                    dbContext.Add(personaje);  
                    await dbContext.SaveChangesAsync();  
                    return RedirectToAction(nameof(Index));  
            }
            return View();
        }

         // GET: Personajes/Delete
        public IActionResult Delete(int? id)
        {
            Personaje personaje = dbContext.Personaje.Find(id);

            if (personaje.Imagen != null)
            {
                //Borrar Imagen desde carpeta
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\Personajes", personaje.Imagen);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                //Borrar Data
                var del = (from Personaje in dbContext.Personaje where Personaje.PersonajeId == id select Personaje).FirstOrDefault();
                dbContext.Personaje.Remove(del);
                dbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                //Delete Data
                var del = (from Personaje in dbContext.Personaje where Personaje.PersonajeId == id select Personaje).FirstOrDefault();
                dbContext.Personaje.Remove(del);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
        }

         // GET: Personajes/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personaje = await dbContext.Personaje.FindAsync(id);

            if (personaje == null)
            {
                return NotFound();
            }
            
            var PersonajeViewModel = new PersonajeViewModel()
            {
                Id = personaje.PersonajeId,
                Nombre = personaje.Nombre,
                Edad = personaje.Edad,
                Peso = personaje.Peso,
                Historia = personaje.Historia,
                Imagen = personaje.Imagen
            };

            
            return View(PersonajeViewModel);
        }

        [HttpPost]  
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> Edit(int id, PersonajeViewModel pvm)  
        {  
            
            if (ModelState.IsValid)  
            {  
                var personaje = await dbContext.Personaje.FindAsync(pvm.Id);  
                personaje.Nombre = pvm.Nombre;  
                personaje.Edad = pvm.Edad;  
                personaje.Peso = pvm.Peso;  
                personaje.Historia = pvm.Historia;  
                  
                if (pvm.CharacterImage != null)  
                {  
                    if (pvm.Imagen != null)  
                    {  
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "img/Personajes/", pvm.Imagen);
                        System.IO.File.Delete(filePath);  
                    }    
                       
                    personaje.Imagen = ProcessUploadedFile(pvm);  
                }  
                dbContext.Update(personaje);  
                await dbContext.SaveChangesAsync();  
                return RedirectToAction(nameof(Index));  
            }  
            return View();  
        }  
        
         private string ProcessUploadedFile(PersonajeViewModel model)
        {
            string uniqueFileName = null;

            if (model.CharacterImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img/Personajes");
                
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.CharacterImage.FileName;
                Console.WriteLine(uniqueFileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CharacterImage.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}