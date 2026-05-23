using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.Orders
{
   public readonly record struct OrderId(Guid Value)
    {
        public static OrderId New() => new(Guid.NewGuid());
        public override string ToString() => Value.ToString();        
    }
}
