using Backend.Infrastructure.Abstraction.Persistence;
using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace Backend.Core.Features.Vouchers.Controllers
{
    [Route("api/qrcode")]
    [ApiController]
    public class QrCodesController : ControllerBase
    {
        private readonly IWriter _writer;

        public QrCodesController(IWriter writer)
        {
            _writer = writer;
        }

        [HttpPost]
        public IActionResult Generate(string input)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(input, QRCodeGenerator.ECCLevel.Q);
            SvgQRCode qrCode = new SvgQRCode(qrCodeData);
            string qrCodeAsSvg = qrCode.GetGraphic(20);
            return Ok(qrCodeAsSvg);
        }
    }
}