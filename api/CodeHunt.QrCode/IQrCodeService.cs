using System.Drawing;

namespace CodeHunt.QrCode
{
    public interface IQrCodeService
    {
        Bitmap CreateQrCode(string plainText);
    }
}