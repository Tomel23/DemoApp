using Demo.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoAppController : ControllerBase
    {
        private readonly IDemoDataService _demoDataService;
        public DemoAppController(IDemoDataService demoDataService)
        {
            _demoDataService = demoDataService;
        }

        [HttpGet("PathData")]
        public async Task<ICollection<string>> GetPathData() =>
             await _demoDataService.GetDemoPath();
    }
}