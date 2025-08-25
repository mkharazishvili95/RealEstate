//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Options;
//using RealEstate_Manage.Configuration;
//using System.Security.Cryptography;
//using System.Text;

//namespace RealEstate_Manage.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class FilesController : ControllerBase
//    {
//        private readonly ImageKitSettings _settings;

//        public FilesController(IOptions<ImageKitSettings> settings)
//        {
//            _settings = settings.Value;
//        }

//        [HttpGet("auth")]
//        public IActionResult GetImageKitAuth()
//        {
//            var token = Guid.NewGuid().ToString();
//            var expire = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 240; 

//            var signatureBytes = new HMACSHA1(Encoding.UTF8.GetBytes(_settings.PrivateKey))
//                .ComputeHash(Encoding.UTF8.GetBytes(token + expire));

//            var signature = BitConverter.ToString(signatureBytes).Replace("-", "").ToLower();

//            return Ok(new
//            {
//                token,
//                expire,
//                signature,
//                publicKey = _settings.PublicKey,
//                urlEndpoint = _settings.UrlEndpoint
//            });
//        }
//    }
//}
