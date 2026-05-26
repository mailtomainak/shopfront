using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.Common
{
    /// <summary>
    /// Value object representing a monetary amount in a specific currency.
    /// Operations across mismatched currencies are rejected to prevent silent
    /// loss of meaning from implicit conversions.
    /// </summary>
    public readonly record struct Money
    {
        /// <summary>The monetary amount. Always non-negative.</summary>
        public decimal Amount { get; }

        /// <summary>ISO 4217 three-letter currency code, normalized to upper case.</summary>
        public string CurrencyCode { get; }

        /// <summary>
        /// Creates a new <see cref="Money"/> value.
        /// </summary>
        /// <param name="amount">Non-negative monetary amount.</param>
        /// <param name="currencyCode">Three-letter ISO 4217 currency code (case-insensitive).</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="currencyCode"/> is not a 3-letter code,
        /// or when <paramref name="amount"/> is negative.
        /// </exception>
        public Money(decimal amount,string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode) || currencyCode.Length != 3)
                throw new ArgumentException("Currency must be a 3-letter ISO code.", nameof(currencyCode));
            if (amount < 0)
                throw new ArgumentException("Money amount cannot be negative.", nameof(amount));

            Amount = amount;
            // Normalize so equality comparisons treat "usd" and "USD" as the same currency.
            CurrencyCode = currencyCode.ToUpperInvariant();
        }

        /// <summary>Returns a zero-valued <see cref="Money"/> in the given currency.</summary>
        public static Money Zero(string currencyCode) => new Money(0m, currencyCode);

        /// <summary>
        /// Returns the sum of this and <paramref name="other"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the two values have different currency codes.
        /// </exception>
        public Money Add(Money other)
        {
            if (CurrencyCode != other.CurrencyCode)
                throw new InvalidOperationException(
                    $"Cannot add {CurrencyCode} and {other.CurrencyCode}. Currency mismatch.");
            return new Money(Amount + other.Amount, CurrencyCode);
        }

        /// <summary>Operator form of <see cref="Add"/>.</summary>
        public static Money operator +(Money a, Money b) => a.Add(b);

        /// <summary>Formats the amount with two decimal places followed by the currency code (e.g. "9.99 USD").</summary>
        public override string ToString() => $"{Amount:0.00} {CurrencyCode}";
    }
}
