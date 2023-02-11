using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Mvc.DataAccess.Interfaces;

namespace Mvc.Controllers
{
    public abstract class BaseController<Model, Entity> : Controller where Entity : class where Model : Models.Model, new()
    {
        protected readonly IRepositoryAsync<Entity> _repository;
        protected readonly IMapper _mapper;

        protected BaseController(IRepositoryAsync<Entity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _repository.GetAll();
            return View(data);
        }

        public async Task<IActionResult> Get(int? id)
        {
            var model = new Model();
            if (id == null)
            {
                model.Id = 0;
                return PartialView("Get", model);
            }
            Entity entity = await _repository.GetByID(id);
            model = _mapper.Map<Model>(entity);
            
            return PartialView("Get", model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Save(Model model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            Entity entity;
            try
            {
                entity = _mapper.Map<Entity>(model);

                if (model.Id > 0)
                {
                    /*var ocasion = await _repository.Find(o => o.Ocasion == model.Ocasion && o.Id != model.Id);
                    if (ocasion != null) return BadRequest("Registro duplicado.");*/
                    await _repository.Update(entity);
                }
                else
                {
                    /*var ocasion = await _repository.Find(o => o.Ocasion == model.Ocasion);
                    if (ocasion != null) return BadRequest("Registro duplicado.");*/
                    await _repository.Insert(entity);
                }
            }
            catch (DbUpdateException e)
            when (e.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                return Conflict("Registro duplicado.");
            }
            catch (Exception e) { 
                return Conflict(e.Message);
            }

            return Json(entity);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _repository.Delete(id);
            return Ok(deleted);
        }
    }
}