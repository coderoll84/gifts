using AutoMapper;
using Mvc.Data.Models;
using Mvc.DataAccess.Interfaces;
using Mvc.Models;

namespace Mvc.Controllers
{
    public class OcasionController : BaseController<OcasionModel, Ocasione>
    {
        public OcasionController(IRepositoryAsync<Ocasione> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
