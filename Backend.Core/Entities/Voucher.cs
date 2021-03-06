﻿using System;

namespace Backend.Core.Entities
{
    public class Voucher : Entity
    {
        public Voucher()
        {
        }

        public Voucher(Guid id, Offer offer, string publicTransportQrCode, string voucherQrCode, Guid customerId)
        {
            Id = id;
            Offer = offer;
            PublicTransportQrCode = publicTransportQrCode;
            VoucherQrCode = voucherQrCode;
            CustomerId = customerId;
        }

        public Offer Offer { get; set; }

        public string PublicTransportQrCode { get; set; }

        public string VoucherQrCode { get; set; }

        public Guid CustomerId { get; set; }

        public bool IsUsed { get; set; }
    }
}