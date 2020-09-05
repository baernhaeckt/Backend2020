using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Infrastructure.Abstraction.Persistence;
using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace Backend.Core.Features.Vouchers.Controllers
{
    [Route("api/qrcode")]
    [ApiController]
    public class VouchersController : ControllerBase
    {
        private readonly IWriter _writer;

        public VouchersController(IWriter writer) => _writer = writer;

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var voucher = await _writer.InsertAsync(new Voucher());

            string url = $"https://baernhaeckt2020.z19.web.core.windows.net/voucher/{voucher.Id}";

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.L);
            SvgQRCode qrCode = new SvgQRCode(qrCodeData);
            string qrCodeAsSvg = qrCode.GetGraphic(20);
            return Ok(qrCodeAsSvg);
        }
    }
}