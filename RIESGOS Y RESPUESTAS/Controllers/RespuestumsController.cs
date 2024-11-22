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
    public class RespuestumsController : Controller
    {
        private readonly DbgestorContext _context;

        public RespuestumsController(DbgestorContext context)
        {
            _context = context;
        }

        // GET: Respuestums
        public async Task<IActionResult> Index()
        {
            return View(await _context.Respuesta.ToListAsync());
        }

        // GET: Respuestums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respuestum = await _context.Respuesta
                .FirstOrDefaultAsync(m => m.Idrespuesta == id);
            if (respuestum == null)
            {
                return NotFound();
            }

            return View(respuestum);
        }

        // GET: Respuestums/Create
        public IActionResult Create()
        {
            // Opciones para porcentaje de mitigación (más detalladas)
            var porcentajeMitigacionOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "0%" },
                new SelectListItem { Value = "5", Text = "5%" },
                new SelectListItem { Value = "10", Text = "10%" },
                new SelectListItem { Value = "15", Text = "15%" },
                new SelectListItem { Value = "20", Text = "20%" },
                new SelectListItem { Value = "25", Text = "25%" },
                new SelectListItem { Value = "30", Text = "30%" },
                new SelectListItem { Value = "35", Text = "35%" },
                new SelectListItem { Value = "40", Text = "40%" },
                new SelectListItem { Value = "45", Text = "45%" },
                new SelectListItem { Value = "50", Text = "50%" },
                new SelectListItem { Value = "55", Text = "55%" },
                new SelectListItem { Value = "60", Text = "60%" },
                new SelectListItem { Value = "65", Text = "65%" },
                new SelectListItem { Value = "70", Text = "70%" },
                new SelectListItem { Value = "75", Text = "75%" },
                new SelectListItem { Value = "80", Text = "80%" },
                new SelectListItem { Value = "85", Text = "85%" },
                new SelectListItem { Value = "90", Text = "90%" },
                new SelectListItem { Value = "95", Text = "95%" },
                new SelectListItem { Value = "100", Text = "100%" }
            };

            // Pasar las opciones al ViewBag para ser utilizadas en la vista
            ViewBag.PorcentajeMitigacionOptions = new SelectList(porcentajeMitigacionOptions, "Value", "Text");

            // Opciones para tiempo de respuesta en minutos
            var tiempoRespuestaOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "15", Text = "Crítico (0-15 minutos)" },
                new SelectListItem { Value = "30", Text = "Alto (15-30 minutos)" },
                new SelectListItem { Value = "60", Text = "Moderado (30-60 minutos)" },
                new SelectListItem { Value = "240", Text = "Bajo (1-4 horas)" },
                new SelectListItem { Value = "480", Text = "Muy Bajo (4-8 horas)" }
            };

            // Pasar las opciones al ViewBag para ser utilizadas en la vista
            ViewBag.TiempoRespuestaOptions = new SelectList(tiempoRespuestaOptions, "Value", "Text");

            // Obtener los datos de los tiempos de respuesta desde la base de datos
            var datosTiempoRespuesta = _context.Respuesta
                .Select(r => new { r.Nombre, r.TiempoRespuesta })
                .ToList();

            // Pasar los datos al ViewBag o Model para ser utilizados en la vista
            ViewBag.DatosTiempoRespuesta = datosTiempoRespuesta;

            return View();

        }

        // POST: Respuestums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idrespuesta,Nombre,Descripción,PorcentajeMtigación,ValorMejorasMonetario,TiempoRespuesta")] Respuestum respuestum)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Verificar si 'Descripción' está vacío
                    if (string.IsNullOrWhiteSpace(respuestum.Descripción))
                    {
                        ModelState.AddModelError(nameof(respuestum.Descripción), "El campo Descripción es obligatorio.");
                        return View(respuestum);
                    }

                    // Verificar que 'TiempoRespuesta' tenga un valor válido
                    if (respuestum.TiempoRespuesta <= 0)
                    {
                        ModelState.AddModelError(nameof(respuestum.TiempoRespuesta), "El campo Tiempo de Respuesta debe ser un valor positivo.");
                        return View(respuestum);
                    }

                    // Agregar la nueva respuesta a la base de datos
                    _context.Add(respuestum);
                    await _context.SaveChangesAsync();

                    // Mostrar mensaje de éxito
                    TempData["SuccessMessage"] = "El registro se ha guardado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                // Manejar la excepción de actualización de la base de datos
                ModelState.AddModelError("", "No se pudo guardar los cambios. Inténtelo nuevamente. " + ex.Message);
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                ModelState.AddModelError("", "Ocurrió un error inesperado. " + ex.Message);
            }

            return View(respuestum);
        }

        // GET: Respuestums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respuestum = await _context.Respuesta.FindAsync(id);
            if (respuestum == null)
            {
                return NotFound();
            }
            return View(respuestum);
        }

        // POST: Respuestums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idrespuesta,Nombre,Descripción,PorcentajeMtigación,ValorMejorasMonetario,TiempoRespuesta")] Respuestum respuestum)
        {
            if (id != respuestum.Idrespuesta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(respuestum);
                    TempData["SuccessMessage"] = "el registro se ha modificado correctamente";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RespuestumExists(respuestum.Idrespuesta))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessMessage"] = "registro configurado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(respuestum);
        }

        // GET: Respuestums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respuestum = await _context.Respuesta
                .FirstOrDefaultAsync(m => m.Idrespuesta == id);
            if (respuestum == null)
            {
                return NotFound();
            }

            return View(respuestum);
        }

        // POST: Respuestums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var respuestum = await _context.Respuesta.FindAsync(id);
            if (respuestum != null)
            {
                _context.Respuesta.Remove(respuestum);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "El registro se ha eliminado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        private bool RespuestumExists(int id)
        {
            return _context.Respuesta.Any(e => e.Idrespuesta == id);
        }
    }
}
