using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using QrCode;

namespace Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QrCodeController : ControllerBase
    {
        [HttpGet("Generate/{message}")]
        public ActionResult Generate(string message)
        {
            var qrService = new QrCodeService();
            var qrCode = qrService.CreateQrCode(message);
            var byteArray = ImageToByteArray(qrCode);

            return File(byteArray, "image/jpeg");
        }

        private byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var memoryStream = new MemoryStream())
            {
                imageIn.Save(memoryStream, ImageFormat.Jpeg);
                return memoryStream.ToArray();
            }
        }
    }
}
