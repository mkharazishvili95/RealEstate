using RealEstate.Common.Enums.User;
using RealEstate.Core.PaidService;

namespace RealEstate.Application.Helpers
{
    public static class TariffHelper
    {
        public static decimal CalculateTariffPrice(UserType userType, Tariff? individualTariff, Tariff? agentTariff)
        {
            return userType switch
            {
                UserType.Individual => individualTariff?.Price ?? 0.25m,
                UserType.Agent => agentTariff?.Price ?? 0.20m,
                _ => 0.25m
            };
        }
    }
}
