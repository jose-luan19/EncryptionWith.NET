using Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataEncryptController : Controller
    {
        private readonly IDecryptService _decryptService; 
        public DataEncryptController(IDecryptService decryptService)
        {
            _decryptService = decryptService;
        }

        [HttpPost("AES")]
        public ActionResult PostAES([FromBody]string message)
        {
            Console.WriteLine("Requisição para descriptografia AES");

            var data = _decryptService.DecryptAES(message);

            return Ok(data);
        }

        [HttpPost("RSA")]
        public ActionResult PostRSA([FromBody]string message)
        {
            Console.WriteLine("Requisição para descriptografia RSA");

            var data = _decryptService.DecryptRSA(message);

            return Ok(data);
        }
    }
}
