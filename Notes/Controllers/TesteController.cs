using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Notes.Controllers
{
    [Authorize]
    [Route("[controller]")]
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
