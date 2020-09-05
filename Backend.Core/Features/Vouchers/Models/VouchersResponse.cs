using System;

namespace Backend.Core.Features.Vouchers.Models
{
    public class VouchersResponse
    {
        public Guid Id { get; }

        public string Name { get; }

        public VouchersResponse(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}