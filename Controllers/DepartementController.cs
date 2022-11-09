using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Base;
using WebApi.Models;
using WebApi.Repositories.Data;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class DepartementController : BaseController<DepartementRepository, Departement>
    {
        DepartementRepository repository;

        public DepartementController(DepartementRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
