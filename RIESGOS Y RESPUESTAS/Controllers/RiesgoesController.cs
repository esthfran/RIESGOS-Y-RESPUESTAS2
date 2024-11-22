using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RIESGOS_Y_RESPUESTAS.Models;

namespace RIESGOS_Y_RESPUESTAS.Controllers
{
    public class RiesgoesController : Controller
    {
        private readonly DbgestorContext _context;

        public RiesgoesController(DbgestorContext context)
        {
            _context = context;
        }

        // GET: Riesgoes
        public async Task<IActionResult> Index()
        {
            var dbgestorContext = _context.Riesgos.Include(r => r.AreaMunicipalidadNavigation);
            return View(await dbgestorContext.ToListAsync());
        }

        // GET: Riesgoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var riesgo = await _context.Riesgos
                .Include(r => r.AreaMunicipalidadNavigation)
                //.Include(r=> r.AreaMunicipalidad)error aca
                .FirstOrDefaultAsync(m => m.Idreportes == id);
            if (riesgo == null)
            {
                return NotFound();
            }

            var viewModel = new
            {
                Riesgo = riesgo,
                NivelAmenaza = riesgo.GetNivelAmenazaDescription() // Adjust if method is elsewhere
            };

            return View(riesgo);
        }

        // GET: Riesgoes/Create
        public IActionResult Create()
        {
            ViewData["AreaMunicipalidad"] = new SelectList(_context.MunicipalidadIqqs, "Idmunicipalidad", "NombreDepartamento");
            return View();
        }

        // POST: Riesgoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idreportes,Nombre,NivelAmenaza,Detalles,AreaMunicipalidad,Daños,EstadoRiesgo,TipoRiesgo,Impacto,NivelAmenazaCopy1,ImpactoDescripcion,ImpactoNumerico")] Riesgo riesgo)
        {
            Debug.WriteLine("Inicio del método Create");

            try
            {
                // Verificar si ya existen los mismos datos en la base de datos
                bool datosDuplicados = await _context.Riesgos.AnyAsync(r =>
                    r.Nombre == riesgo.Nombre &&
                    r.NivelAmenaza == riesgo.NivelAmenaza &&
                    r.Detalles == riesgo.Detalles &&
                    r.AreaMunicipalidad == riesgo.AreaMunicipalidad &&
                    r.Daños == riesgo.Daños &&
                    r.EstadoRiesgo == riesgo.EstadoRiesgo &&
                    r.Impacto == riesgo.Impacto
                );

                Debug.WriteLine("Verificación de duplicados completada: " + datosDuplicados);

                if (datosDuplicados)
                {
                    ModelState.AddModelError("", "Ya existe un registro con los mismos datos. Por favor, edite el registro existente si es necesario.");
                    return View(riesgo);
                }

                // Ahora verifica la validación del ModelState
                if (!ModelState.IsValid)
                {
                    Debug.WriteLine("ModelState no es válido, revisando errores...");
                    foreach (var modelStateEntry in ModelState)
                    {
                        foreach (var error in modelStateEntry.Value.Errors)
                        {
                            Console.WriteLine($"Error en {modelStateEntry.Key}: {error.ErrorMessage}");
                        }
                    }
                    ViewBag.AreaMunicipalidad = new SelectList(_context.MunicipalidadIqqs, "Idmunicipalidad", "NombreDepartamento", riesgo.AreaMunicipalidad);
                    return View(riesgo);
                }

                // Si ModelState es válido, agrega el nuevo riesgo
                _context.Add(riesgo);
                await _context.SaveChangesAsync();

                // Agregar el mensaje a TempData
                TempData["SuccessMessage"] = "El registro se ha guardado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "No se pudo guardar el riesgo. Inténtalo de nuevo. Detalles del error: " + ex.Message);
                Debug.WriteLine("Error al guardar en la base de datos: " + ex.Message);
            }

            Debug.WriteLine("Se produjo un error al intentar guardar el registro.");

            // Si ocurre un error, vuelve a cargar la vista con el ViewBag y los datos actuales
            ViewBag.AreaMunicipalidad = new SelectList(_context.MunicipalidadIqqs, "Idmunicipalidad", "NombreDepartamento", riesgo.AreaMunicipalidad);
            return View(riesgo);
        }

        // GET: Riesgoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var model = _context.Riesgos.Find(id); // Adjust according to how you fetch data
            ViewBag.TipoRiesgoOptions = new SelectList(new[]
            {
            new { Value = "naturales", Text = "Amenazas Naturales" },
            new { Value = "humanas", Text = "Amenazas Humanas" },
            new { Value = "tecnologicas", Text = "Amenazas Tecnológicas" },
            new { Value = "organizativas", Text = "Amenazas Organizativas" },
            new { Value = "externas", Text = "Amenazas Externas" },
            new { Value = "internas", Text = "Amenazas Internas" },
            new { Value = "entorno", Text = "Amenazas Relacionadas con el Entorno" }
            }, "Value", "Text", model.TipoRiesgo);


            if (id == null)
            {
                return NotFound();
            }

            var riesgo = await _context.Riesgos.FindAsync(id);
            
            if (riesgo == null)
            {
                return NotFound();
            }
            ViewData["AreaMunicipalidad"] = new SelectList(_context.MunicipalidadIqqs, "Idmunicipalidad", "NombreDepartamento", riesgo.AreaMunicipalidad);
            return View(riesgo);
        }

        // POST: Riesgoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idreportes,Nombre,Causantes,NivelAmenaza,Detalles,AreaMunicipalidad,Daños,EstadoRiesgo,TipoRiesgo,Impacto,NivelAmenazaCopy1,ImpactoDescripcion,ImpactoNumerico")] Riesgo riesgo)
        {
            if (id != riesgo.Idreportes)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Verificar si 'Detalles' está vacío
                if (string.IsNullOrWhiteSpace(riesgo.Detalles))
                {
                    ModelState.AddModelError(nameof(riesgo.Detalles), "El Detalle es obligatorio.");
                    return View(riesgo); // Retorna la vista con el mensaje de error
                }

                try
                {
                    _context.Update(riesgo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RiesgoExists(riesgo.Idreportes))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewData["AreaMunicipalidad"] = new SelectList(_context.MunicipalidadIqqs, "Idmunicipalidad", "Idmunicipalidad", riesgo.AreaMunicipalidad);
                TempData["SuccessMessage"] = "El registro se ha modificado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(riesgo);
        }

        // GET: Riesgoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riesgo = await _context.Riesgos
                .Include(r => r.AreaMunicipalidadNavigation)
                .FirstOrDefaultAsync(m => m.Idreportes == id);
            if (riesgo == null)
            {
                return NotFound();
            }

            return View(riesgo);
        }

        // POST: Riesgoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var riesgo = await _context.Riesgos.FindAsync(id);
            if (riesgo != null)
            {
                _context.Riesgos.Remove(riesgo);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "El registro se ha eliminado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        private bool RiesgoExists(int id)
        {
            return _context.Riesgos.Any(e => e.Idreportes == id);
        }
    }
}
