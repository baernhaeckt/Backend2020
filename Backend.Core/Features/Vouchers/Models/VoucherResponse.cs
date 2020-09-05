using System;
using Backend.Core.Features.Offers.Models;

namespace Backend.Core.Features.Vouchers.Models
{
    public class VoucherResponse
    {
        public VoucherResponse(Guid id, OfferResponse offer, string publicTransportQrCode, string voucherQrCode, Guid customerId, bool isUsed)
        {
            Id = id;
            Offer = offer;
            PublicTransportQrCode = publicTransportQrCode;
            VoucherQrCode = voucherQrCode;
            CustomerId = customerId;
            IsUsed = isUsed;
        }

        public Guid Id { get; set; }

        public OfferResponse Offer { get; }

        public string PublicTransportQrCode { get; }

        public string VoucherQrCode { get; }

        public Guid CustomerId { get; }

        public bool IsUsed { get; set; }
    }
}
