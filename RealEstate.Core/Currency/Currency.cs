namespace RealEstate.Core.Currency
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public string Text { get; set; }
        public bool IsCrypto { get; set; } = false;
        public bool IsTop { get; set; }
        public decimal Rate { get; set; }
    }
}
