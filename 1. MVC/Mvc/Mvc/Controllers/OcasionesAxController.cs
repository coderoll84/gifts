using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc.Data.Context;
using Mvc.Data.Models;
using Mvc.Models;

namespace Mvc.Controllers
{
    public class OcasionesAxController : Controller
    {
        private readonly PvContext _context;

        public OcasionesAxController(PvContext context)
        {
            _context = context;
        }

        // GET: Ocasiones
        public async Task<IActionResult> Index()
        {
            var data = await _context.Ocasiones.ToListAsync();

            return View(data);
        }

        // GET: Ocasiones/Edit/5
        public async Task<IActionResult> Attach(int? id)
        {
            var model = new OcasionModel();
            if (id == null)
            {
                model.Id = 0;
                return PartialView("Attach", model);
            }
            var ocasione = await _context.Ocasiones.FindAsync(id);
            model.Id = ocasione.Id;
            model.Ocasion = ocasione.Ocasion;

            return PartialView("Attach", model);
        }

        // POST: Ocasiones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AttachConfirmed([Bind("Id,Ocasion")] OcasionModel model)
        {
            var entity = new Ocasione { Id = model.Id, Ocasion = model.Ocasion };

            if (ModelState.IsValid)
            {
                try
                {
                    if (entity.Id > 0)
                    {
                        var ocasion = await _context.Ocasiones.Where(o => o.Ocasion == model.Ocasion && o.Id != model.Id).FirstOrDefaultAsync();
                        if (ocasion != null) return BadRequest("Registro duplicado.");
                        _context.Update(entity);
                    }
                    else
                    {
                        var ocasion = await _context.Ocasiones.Where(o => o.Ocasion == model.Ocasion).FirstOrDefaultAsync();
                        if (ocasion != null) return BadRequest("Registro duplicado.");
                        _context.Add(entity);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OcasioneExists(entity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Json(entity);
        }

        // POST: Ocasiones/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ocasione = await _context.Ocasiones.FindAsync(id);
            _context.Ocasiones.Remove(ocasione);
            await _context.SaveChangesAsync();
            return Ok(ocasione);
        }

        private bool OcasioneExists(int id)
        {
            return _context.Ocasiones.Any(e => e.Id == id);
        }
    }
}