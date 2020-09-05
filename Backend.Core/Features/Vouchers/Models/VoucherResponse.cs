using System;
using Backend.Core.Features.Offers.Models;

namespace Backend.Core.Features.Vouchers.Models
{
    public class VoucherResponse
    {
        public VoucherResponse(Guid id, Offer offer, string publicTransportQrCode, string voucherQrCode, Guid customerId)
        {
            Id = id;
            Offer = offer;
            PublicTransportQrCode = publicTransportQrCode;
            VoucherQrCode = voucherQrCode;
            CustomerId = customerId;
        }

        public Guid Id { get; set; }

        public Offer Offer { get; }

        public string PublicTransportQrCode { get; }

        public string VoucherQrCode { get; }

        public Guid CustomerId { get; }
    }
}
