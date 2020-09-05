using System;
using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Core.Features.Offers.Models;
using Backend.Core.Features.Vouchers.Models;
using Backend.Infrastructure.Abstraction.Persistence;
using Backend.Infrastructure.Abstraction.Security;
using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace Backend.Core.Features.Vouchers.Controllers
{
    [Route("api/vouchers")]
    [ApiController]
    public class VouchersController : ControllerBase
    {
        private readonly IWriter _writer;

        public VouchersController(IWriter writer) => _writer = writer;

        [HttpPost]
        public async Task<VoucherResponse> Create(Guid offerId)
        {
            var offerDbItem = await _writer.GetByIdOrThrowAsync<OfferDbItem>(offerId);

            Guid voucherId = Guid.NewGuid();

            string url = $"https://baernhaeckt2020.z19.web.core.windows.net/voucher/{voucherId}";

            var qrGenerator = new QRCodeGenerator();
            var voucherQrCode = new SvgQRCode(qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.L));
            string voucherQrSvg = voucherQrCode.GetGraphic(20);

            var publicTransportQrCode = new SvgQRCode(qrGenerator.CreateQrCode("sbb.ch", QRCodeGenerator.ECCLevel.L));
            string publicTransportQrSvg = publicTransportQrCode.GetGraphic(20);

            var voucher = await _writer.InsertAsync(new Voucher(voucherId, offerDbItem, publicTransportQrSvg, voucherQrSvg, HttpContext.User.Id()));

            Offer offer = new Offer();
            offer.From(offerDbItem);
            return new VoucherResponse(offer, voucher.PublicTransportQrCode, voucher.VoucherQrCode, voucher.CustomerId);
        }

        [HttpGet]
        public async Task<VoucherResponse> Get(Guid voucherId)
        {
            var voucher = await _writer.GetByIdOrThrowAsync<Voucher>(voucherId);
            Offer offer = new Offer();
            offer.From(voucher.Offer);
            return new VoucherResponse(offer, voucher.PublicTransportQrCode, voucher.VoucherQrCode, voucher.CustomerId);
        }
    }
}