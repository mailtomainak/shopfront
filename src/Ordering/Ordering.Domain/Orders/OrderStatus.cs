using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.Orders
{
    /// <summary>
    /// Lifecycle states of an order. The happy path is
    /// Placed → Confirmed → Shipped → Completed; Cancelled and Failed are terminal off-ramps.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>Order has been submitted by the customer but not yet confirmed.</summary>
        Placed,

        /// <summary>Order has been validated and accepted for fulfilment.</summary>
        Confirmed,

        /// <summary>Order has left the warehouse and is in transit to the customer.</summary>
        Shipped,

        /// <summary>Order has been delivered and is considered closed successfully.</summary>
        Completed,

        /// <summary>Order was cancelled before completion (by customer or system).</summary>
        Cancelled,

        /// <summary>Order could not be processed due to an unrecoverable error.</summary>
        Failed
    }
}
