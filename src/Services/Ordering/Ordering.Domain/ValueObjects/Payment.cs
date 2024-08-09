namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string CardNumber { get; init; }
        public string CardHolderName { get; init; }
        public DateTime Expiration { get; init; }
        public string CVV { get; init; }
        
        protected Payment(string cardNumber, string cardHolderName, DateTime expiration, string cvv)
        {
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            Expiration = expiration;
            CVV = cvv;
        }
     
        public static Payment Of(string cardNumber, string cardHolderName, DateTime expiration, string cvv)
        {
            ArgumentException.ThrowIfNullOrEmpty(cardNumber, nameof(cardNumber));
            ArgumentException.ThrowIfNullOrEmpty(cardHolderName, nameof(cardHolderName));
            ArgumentException.ThrowIfNullOrEmpty(cvv, nameof(cvv));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);
            
            return new Payment(cardNumber, cardHolderName, expiration, cvv);
        }
    }
}
