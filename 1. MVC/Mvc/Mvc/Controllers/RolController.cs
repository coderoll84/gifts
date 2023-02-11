using AutoMapper;
using Mvc.Data.Models;
using Mvc.DataAccess.Interfaces;
using Mvc.Models;

namespace Mvc.Controllers
{
    public class RolController : BaseController<RolModel, Rol>
    {
        public RolController(IRepositoryAsync<Rol> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
