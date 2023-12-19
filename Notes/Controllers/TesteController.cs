using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Notes.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        [HttpGet("teste")]
        public string Teste()
        {
            return "TESTE BEM SUCEDIDO!";
        }
    }
}
