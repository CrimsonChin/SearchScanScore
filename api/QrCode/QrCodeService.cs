using System.Drawing;
using QRCoder;

namespace QrCode
{
    public class QrCodeService
    {
        public Bitmap CreateQrCode(string plainText)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(plainText, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20);

            return qrCodeImage;
        }
    }
}
