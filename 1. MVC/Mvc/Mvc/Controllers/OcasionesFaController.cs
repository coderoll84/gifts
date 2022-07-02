using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc.Data.Context;
using Mvc.Data.Models;
using Mvc.DataAccess.Interfaces;
using Mvc.Models;

namespace Mvc.Controllers
{
    public class OcasionesFaController : Controller
    {
        private readonly IRepositoryAsync<Ocasione> _repository;

        public OcasionesFaController(IRepositoryAsync<Ocasione> repository)
        {
            _repository = repository;
        }

        // GET: Ocasiones
        public async Task<IActionResult> Index()
        {
            var data = await _repository.GetAll();

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
            var ocasione = await _repository.GetByID(id);
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
                        var ocasion = await _repository.Find(o => o.Ocasion == model.Ocasion && o.Id != model.Id);
                        if (ocasion != null) return BadRequest("Registro duplicado.");
                        await _repository.Update(entity);
                    }
                    else
                    {
                        var ocasion = await _repository.Find(o => o.Ocasion == model.Ocasion);
                        if (ocasion != null) return BadRequest("Registro duplicado.");
                        await _repository.Insert(entity);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await OcasioneExists(entity.Id))
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
            var ocasione = await _repository.Delete(id);
            return Ok(ocasione);
        }

        private async Task<bool> OcasioneExists(int id)
        {
            var entity = await _repository.GetByID(id);
            return (entity is null);
        }
    }
}