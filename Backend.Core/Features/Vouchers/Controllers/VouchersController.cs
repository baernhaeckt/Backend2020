using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

using Backend.Core.Entities;
using Backend.Core.Features.Newsfeed;
using Backend.Core.Features.Offers.Models;
using Backend.Core.Features.Vouchers.Models;
using Backend.Infrastructure.Abstraction.Persistence;
using Backend.Infrastructure.Abstraction.Security;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QRCoder;

namespace Backend.Core.Features.Vouchers.Controllers
{
    [Route("api/vouchers")]
    [ApiController]
    public class VouchersController : ControllerBase
    {
        private readonly IWriter _writer;

        private readonly IHubContext<NotificationHub> _notification;

        public VouchersController(IWriter writer, IHubContext<NotificationHub> notification)
        {
            _writer = writer;
            _notification = notification;
        }

        [HttpPost("{offerId}")]
        public async Task<VoucherResponse> Create(Guid offerId)
        {
            var offer = await _writer.GetByIdOrThrowAsync<Offer>(offerId);

            Guid voucherId = Guid.NewGuid();

            string url = $"https://baernhaeckt2020.z19.web.core.windows.net/voucher/{voucherId}";

            var qrGenerator = new QRCodeGenerator();
            var voucherQrCode = new SvgQRCode(qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.M));
            string voucherQrSvg = voucherQrCode.GetGraphic(new Size(300, 300), Color.Black, Color.White, false);

            var publicTransportQrCode = new SvgQRCode(qrGenerator.CreateQrCode("bls.ch", QRCodeGenerator.ECCLevel.M));
            string publicTransportQrSvg = publicTransportQrCode.GetGraphic(new Size(300, 300), Color.Black, Color.White, false);

            var voucher = await _writer.InsertAsync(new Voucher(voucherId, offer, publicTransportQrSvg, voucherQrSvg, HttpContext.User.Id()));

            OfferResponse offerResponse = new OfferResponse();
            offerResponse.From(offer);
            return new VoucherResponse(voucherId, offerResponse, voucher.PublicTransportQrCode, voucher.VoucherQrCode, voucher.CustomerId, voucher.IsUsed);
        }

        [HttpPut("{voucherId}")]
        public async Task<IActionResult> Use(Guid voucherId)
        {
            var voucher = await _writer.GetByIdOrThrowAsync<Voucher>(voucherId);
            if (voucher.Offer.GuideId != HttpContext.User.Id())
            {
                return Unauthorized();
            }

            if (voucher.IsUsed)
            {
                return BadRequest("Voucher already used.");
            }

            await _writer.UpdateAsync<Voucher>(voucherId, new { IsUsed = true });
            await _notification.Clients.Group(voucher.CustomerId.ToString())
                .SendAsync("newEvent", new { Variant = "Success", Title = "Voucher used", Message = voucher.Offer.Name});

            return NoContent();
        }

        [HttpGet]
        public async Task<IEnumerable<VouchersResponse>> Get()
            => await _writer.WhereAsync<Voucher, VouchersResponse>(v => v.CustomerId == HttpContext.User.Id(), v => new VouchersResponse(v.Id, v.Offer.Name));

        [HttpGet("{voucherId}")]
        public async Task<IActionResult> Get(Guid voucherId)
        {
            var voucher = await _writer.GetByIdOrThrowAsync<Voucher>(voucherId);
            if (voucher.CustomerId != HttpContext.User.Id() && voucher.Offer.GuideId != HttpContext.User.Id())
            {
                return Unauthorized();
            }

            OfferResponse offer = new OfferResponse();
            offer.From(voucher.Offer);
            return Ok(new VoucherResponse(voucherId, offer, voucher.PublicTransportQrCode, voucher.VoucherQrCode, voucher.CustomerId, voucher.IsUsed));
        }
    }
}