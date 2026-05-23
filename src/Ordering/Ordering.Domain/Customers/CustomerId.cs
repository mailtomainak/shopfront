using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.Customers
{
    public readonly record struct CustomerId(Guid Value)
    {
        public static CustomerId New() => new(Guid.NewGuid());
        public override string ToString() => Value.ToString();
    }
}
