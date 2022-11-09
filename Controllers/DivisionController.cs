using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using WebApi.Base;
using WebApi.Models;
using WebApi.Repositories.Data;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionController : BaseController<DivisionRepository,Division>
    {
        DivisionRepository repository;

        public DivisionController(DivisionRepository repository) : base(repository)
        {
            this.repository = repository;
        }

    }
}
