using System.Drawing.Imaging;
using System.IO;
using CodeHunt.QrCode;
using Microsoft.AspNetCore.Mvc;

namespace CodeHunt.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class QrCodeController : ControllerBase
    {
        private readonly IQrCodeService _qrCodeService;

        public QrCodeController(IQrCodeService qrCodeService)
        {
            _qrCodeService = qrCodeService;
        }

        [HttpGet("Generate/{message}")]
        public ActionResult Generate(string message)
        {
            var qrCode = _qrCodeService.CreateQrCode(message);
            var byteArray = ImageToByteArray(qrCode);

            return File(byteArray, "image/jpeg");
        }

        private static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using var memoryStream = new MemoryStream();
            imageIn.Save(memoryStream, ImageFormat.Jpeg);

            return memoryStream.ToArray();
        }
    }
}
