using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RIESGOS_Y_RESPUESTAS.Models;

namespace RIESGOS_Y_RESPUESTAS.Controllers
{
    public class Activoes1Controller : Controller
    {
        private readonly DbgestorContext _context;

        public Activoes1Controller(DbgestorContext context)
        {
            _context = context;
        }

        // GET: Activoes1
        public async Task<IActionResult> Index()
        {
            var dbgestorContext = _context.Activos.Include(a => a.IdmunicipalidadNavigation);
            var items = _context.Activos.Include(a => a.Dañosformularios).ToList();
            return View(await dbgestorContext.ToListAsync());
        }

        // GET: Activoes1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activo = await _context.Activos
                .Include(a => a.IdmunicipalidadNavigation)
                .Include(a => a.Dañosformularios)
                .FirstOrDefaultAsync(m => m.Idactivos == id);

            if (activo == null)
            {
                return NotFound();
            }

            return View(activo);
        }

    
        // GET: Activoes1/Create
        public IActionResult Create()
        {
            ViewData["Idmunicipalidad"] = new SelectList(_context.MunicipalidadIqqs, "Idmunicipalidad", "NombreDepartamento");
            return View();
        }

        // POST: Activoes1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Activoes1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idactivos,Implementos,TiposDeActivosIdtipos,CosteActivos,Cantidad,Idmunicipalidad")] Activo activo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(activo);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "El registro se ha guardado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "No se pudo guardar el activo. Inténtalo de nuevo. Detalles del error: " + ex.Message);
                }
                finally
                {
                    activo = null;
                }
            }

            // Si llegamos aquí, es porque hubo un error de validación o una excepción
            ViewData["Idmunicipalidad"] = new SelectList(_context.MunicipalidadIqqs, "Idmunicipalidad", "NombreDepartamento", activo.Idmunicipalidad);
            return View(activo);
        }
        // GET: Activoes1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activo = await _context.Activos.FindAsync(id);
            if (activo == null)
            {
                return NotFound();
            }
            ViewData["Idmunicipalidad"] = new SelectList(_context.MunicipalidadIqqs, "Idmunicipalidad", "NombreDepartamento", activo.Idmunicipalidad);
            return View(activo);
        }

        // POST: Activoes1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idactivos,Implementos,TiposDeActivosIdtipos,CosteActivos,Cantidad,Idmunicipalidad")] Activo activo)
        {
            if (id != activo.Idactivos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activo);
                    TempData["SuccessMessage"] = "El registro se ha modificado exitosamente.";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivoExists(activo.Idactivos))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idmunicipalidad"] = new SelectList(_context.MunicipalidadIqqs, "Idmunicipalidad", "NombreDepartamento", activo.Idmunicipalidad);
            return View(activo);
        }

        // GET: Activoes1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activo = await _context.Activos
                .Include(a => a.IdmunicipalidadNavigation)
                .FirstOrDefaultAsync(m => m.Idactivos == id);
            if (activo == null)
            {
                return NotFound();
            }

            return View(activo);
        }

        // POST: Activoes1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activo = await _context.Activos.FindAsync(id);
            if (activo == null)
            {
                return NotFound();
            }

            try
            {
                _context.Activos.Remove(activo);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "El registro se ha eliminado exitosamente.";
                return RedirectToAction(nameof(Index)); // Redirigir al índice si la eliminación fue exitosa
            }
            catch (DbUpdateException)
            {
                // Guardar un mensaje y el ID del activo en TempData si no se puede eliminar
                TempData["ErrorMessage"] = "No se puede eliminar este activo porque está relacionado con otros registros.";
                TempData["ErrorActivoId"] = id; // Guarda el ID del activo
                return RedirectToAction(nameof(Delete), new { id }); // Redirigir a la vista de eliminación con el ID del activo
            }
        }

        private bool ActivoExists(int id)
        {
            return _context.Activos.Any(e => e.Idactivos == id);
        }
    }
}
