﻿using System;
using System.Collections.Generic;
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

        [HttpPost("{offerId}")]
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
            return new VoucherResponse(voucherId, offer, voucher.PublicTransportQrCode, voucher.VoucherQrCode, voucher.CustomerId, voucher.IsUsed);
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

            Offer offer = new Offer();
            offer.From(voucher.Offer);
            return Ok(new VoucherResponse(voucherId, offer, voucher.PublicTransportQrCode, voucher.VoucherQrCode, voucher.CustomerId, voucher.IsUsed));
        }
    }
}