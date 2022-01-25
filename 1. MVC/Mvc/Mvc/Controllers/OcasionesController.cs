#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mvc.Data.Context;
using Mvc.Data.Models;

namespace Mvc.Controllers
{
    public class OcasionesController : Controller
    {
        private readonly PvContext _context;

        public OcasionesController(PvContext context)
        {
            _context = context;
        }

        // GET: Ocasiones
        public async Task<IActionResult> Index()
        {
            var data = await _context.Ocasiones.ToListAsync();

            return View(data);
        }

        // GET: Ocasiones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocasione = await _context.Ocasiones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ocasione == null)
            {
                return NotFound();
            }

            return View(ocasione);
        }

        // GET: Ocasiones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ocasiones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ocasion")] Ocasione ocasione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ocasione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ocasione);
        }

        // GET: Ocasiones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocasione = await _context.Ocasiones.FindAsync(id);
            if (ocasione == null)
            {
                return NotFound();
            }
            return View(ocasione);
        }

        // POST: Ocasiones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ocasion")] Ocasione ocasione)
        {
            if (id != ocasione.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ocasione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OcasioneExists(ocasione.Id))
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
            return View(ocasione);
        }

        // GET: Ocasiones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocasione = await _context.Ocasiones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ocasione == null)
            {
                return NotFound();
            }

            return View(ocasione);
        }

        // POST: Ocasiones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ocasione = await _context.Ocasiones.FindAsync(id);
            _context.Ocasiones.Remove(ocasione);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OcasioneExists(int id)
        {
            return _context.Ocasiones.Any(e => e.Id == id);
        }
    }
}
