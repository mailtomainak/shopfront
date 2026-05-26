using System;
using System.Collections.Generic;
using System.Text;
using Ordering.Domain.Common;

namespace Ordering.Domain.Orders
{
    /// <summary>
    /// A single line item on an <see cref="Order"/>: which product, how many, and the unit price
    /// captured at the time the line was added. Product name and unit price are snapshotted
    /// onto the line so the order remains historically accurate if the catalog later changes.
    /// </summary>
    public sealed record OrderLine
    {
        /// <summary>Identifier of the product being ordered.</summary>
        public Guid ProductId { get; }

        /// <summary>Product name snapshotted at the time the line was created.</summary>
        public string ProductName { get; }

        /// <summary>Number of units ordered. Always positive.</summary>
        public int Quantity { get; }

        /// <summary>Per-unit price snapshotted at the time the line was created.</summary>
        public Money UnitPrice { get; }

        /// <summary>Total price for this line (<see cref="UnitPrice"/> × <see cref="Quantity"/>).</summary>
        public Money LineTotal => new(UnitPrice.Amount * Quantity, UnitPrice.CurrencyCode);

        /// <summary>
        /// Creates a new order line.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="productId"/> is empty, <paramref name="productName"/> is
        /// blank, or <paramref name="quantity"/> is not positive.
        /// </exception>
        public OrderLine(Guid productId, string productName, int quantity, Money unitPrice)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("Product id is required.", nameof(productId));
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("Product name is required.", nameof(productName));
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive.", nameof(quantity));

            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
