using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RIESGOS_Y_RESPUESTAS.Models;

namespace RIESGOS_Y_RESPUESTAS.Controllers
{
    public class DañosformularioController : Controller
    {
        private readonly DbgestorContext _context;

        public DañosformularioController(DbgestorContext context)
        {
            _context = context;
        }

        // GET: Dañosformulario
        public async Task<IActionResult> Index()
        {
            var dbgestorContext = _context.Dañosformularios.Include(d => d.ActivosNavigation);
            return View(await dbgestorContext.ToListAsync());
        }

        // GET: Dañosformulario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dañosformulario = await _context.Dañosformularios
                .Include(d => d.ActivosNavigation)
                .FirstOrDefaultAsync(m => m.Iddañosimportantes == id);
            if (dañosformulario == null)
            {
                return NotFound();
            }

            return View(dañosformulario);
        }

        // GET: Dañosformulario/Create
        public IActionResult Create()
        {
            ViewData["Activos"] = new SelectList(_context.Activos, "Idactivos", "Implementos");
            return View();
        }

        // POST: Dañosformulario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Iddañosimportantes,AreasAfectadas,ValorDaños,Respuesta,Activos,EstadoDaño")] Dañosformulario dañosformulario)
        {
            try
            {
                // Verificar si ya existen los mismos datos en la base de datos
                bool datosDuplicados = await _context.Dañosformularios.AnyAsync(df =>
                    //df.AreasAfectadas == dañosformulario.AreasAfectadas &&
                    df.ValorDaños == dañosformulario.ValorDaños &&
                    df.Respuesta == dañosformulario.Respuesta &&
                    df.Activos == dañosformulario.Activos &&
                    df.EstadoDaño == dañosformulario.EstadoDaño
                );

                if (datosDuplicados)
                {
                    ModelState.AddModelError("", "Ya existe un registro con los mismos datos. ingrese al dato ya existente en editar y modificar si es necesario");
                }
                // Validación personalizada
                //if (string.IsNullOrEmpty(dañosformulario.AreasAfectadas))
                //{
                //    ModelState.AddModelError(nameof(dañosformulario.AreasAfectadas), "Las áreas afectadas son obligatorias.");
                //}
                if (string.IsNullOrEmpty(dañosformulario.ValorDaños))
                {
                    ModelState.AddModelError(nameof(dañosformulario.ValorDaños), "El valor de daños es obligatorio.");
                }
                if (!dañosformulario.Activos.HasValue)
                {
                    ModelState.AddModelError(nameof(dañosformulario.Activos), "El ID de activos es obligatorio.");
                }
                if (!dañosformulario.Respuesta.HasValue)
                {
                    ModelState.AddModelError(nameof(dañosformulario.Respuesta), "La respuesta es obligatoria.");
                }
                if (string.IsNullOrEmpty(dañosformulario.EstadoDaño))
                {
                    ModelState.AddModelError(nameof(dañosformulario.EstadoDaño), "El estado del daño es obligatorio.");
                }
                if (ModelState.IsValid)
                {
                    _context.Add(dañosformulario);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "El registro se ha guardado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "No se pudo guardar el activo. Inténtalo de nuevo. Detalles del error: " + ex.Message);
            }

            // Cargar datos para la vista en caso de error
            ViewData["Activos"] = new SelectList(_context.Activos, "Idactivos", "Implementos", dañosformulario.Activos);
            return View(dañosformulario);
        }

        // GET: Dañosformulario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dañosformulario = await _context.Dañosformularios.FindAsync(id);
            if (dañosformulario == null)
            {
                return NotFound();
            }
            ViewData["Activos"] = new SelectList(_context.Activos, "Idactivos", "Implementos", dañosformulario.Activos);
            return View(dañosformulario);
        }

        // POST: Dañosformulario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Iddañosimportantes,AreasAfectadas,ValorDaños,Respuesta,Activos,EstadoDaño")] Dañosformulario dañosformulario)
        {
            // Lógica de validación...

            if (id != dañosformulario.Iddañosimportantes)
            {
                return NotFound();
            }

            var existingEntity = await _context.Dañosformularios.FindAsync(id);
            if (existingEntity == null)
            {
                return NotFound();
            }
            // Verificar si los datos son los mismos que los actuales
            var existingDañosformulario = await _context.Dañosformularios.FindAsync(id);
            if (existingDañosformulario != null)
            {
                bool isSameData =
                    existingDañosformulario.AreasAfectadas == dañosformulario.AreasAfectadas &&
                    existingDañosformulario.ValorDaños == dañosformulario.ValorDaños &&
                    existingDañosformulario.Respuesta == dañosformulario.Respuesta &&
                    existingDañosformulario.Activos == dañosformulario.Activos &&
                    existingDañosformulario.EstadoDaño == dañosformulario.EstadoDaño;

                if (isSameData)
                {
                    ModelState.AddModelError("", "No se realizaron cambios; los datos son los mismos que los existentes.");
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(existingEntity).CurrentValues.SetValues(dañosformulario);
                    TempData["SuccessMessage"] = "El registro se ha modificado correctamente";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DañosformularioExists(dañosformulario.Iddañosimportantes))
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

            ViewData["Activos"] = new SelectList(_context.Activos, "Idactivos", "Implementos", dañosformulario.Activos);
            return View(dañosformulario);
        }

        // GET: Dañosformulario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dañosformulario = await _context.Dañosformularios
                .Include(d => d.ActivosNavigation)
                .FirstOrDefaultAsync(m => m.Iddañosimportantes == id);
            if (dañosformulario == null)
            {
                return NotFound();
            }

            return View(dañosformulario);
        }

        // POST: Dañosformulario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dañosformulario = await _context.Dañosformularios.FindAsync(id);
            if (dañosformulario != null)
            {
                _context.Dañosformularios.Remove(dañosformulario);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "El registro se ha eliminado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        private bool DañosformularioExists(int id)
        {
            return _context.Dañosformularios.Any(e => e.Iddañosimportantes == id);
        }
    }
}
