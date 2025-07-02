namespace RealEstate_Manage.Helpers
{
    public static class CurrencyHelper
    {
        public static string GetCurrencySymbol(int? currencyId)
        {
            return currencyId switch
            {
                1 => "₾",
                2 => "$",
                _ => ""  
            };
        }
    }
}
