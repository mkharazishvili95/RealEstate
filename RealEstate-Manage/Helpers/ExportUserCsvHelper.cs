using RealEstate.Application.Feature.Manage.User.List;
using RealEstate.Application.Feature.Manage.Agency.List;
using System.Text;
using RealEstate.Application.Feature.Manage.Apartment.List;

namespace RealEstate_Manage.Helpers
{
    public static class ExportCsvHelper
    {
        public static byte[] GenerateUserCsv(IEnumerable<GetUserListForManageItemsResponse> users)
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("FirstName,LastName,PIN,UserName,Email,PhoneNumber,Balance,IsBlocked,RegisterDate");

            foreach (var user in users)
            {
                csvBuilder.AppendLine($"{user.FirstName},{user.LastName},{user.PIN},{user.UserName},{user.Email},{user.PhoneNumber},{user.Balance},{user.IsBlocked},{user.RegisterDate:yyyy-MM-dd}");
            }

            return Encoding.UTF8.GetBytes(csvBuilder.ToString());
        }

        public static byte[] GenerateAgencyCsv(IEnumerable<GetAgencyListForManageItemsResponse> agencies)
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("AgencyId,Name,AgencyType,Email,IdentificationNumber,OwnerPIN,PhoneNumber,IsDeleted,IsApproved,CreateDate,UpdateDate,DeleteDate");

            foreach (var agency in agencies)
            {
                csvBuilder.AppendLine(string.Join(",",
                    agency.AgencyId.ToString(),
                    EscapeForCsv(agency.Name ?? ""),
                    EscapeForCsv(agency.AgencyType?.ToString() ?? ""),
                    EscapeForCsv(agency.Email ?? ""),
                    EscapeForCsv(agency.IdentificationNumber ?? ""),
                    EscapeForCsv(agency.OwnerPIN ?? ""),
                    EscapeForCsv(agency.PhoneNumber ?? ""),
                    agency.IsDeleted.ToString(),
                    agency.IsApproved.ToString(),
                    agency.CreateDate?.ToString("yyyy-MM-dd") ?? "",
                    agency.UpdateDate?.ToString("yyyy-MM-dd") ?? "",
                    agency.DeleteDate?.ToString("yyyy-MM-dd") ?? ""
                ));
            }

            return Encoding.UTF8.GetBytes(csvBuilder.ToString());
        }

        public static byte[] GenerateApartmentCsv(IEnumerable<GetApartmentListForManageItemsResponse> apartments)
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Id,OwnerPIN,Title,Price,Status,CreateDate,UpdateDate,AgencyId");

            foreach (var apt in apartments)
            {
                string currencyName = apt.CurrencyId.HasValue
                    ? (apt.CurrencyId == 1 ? " Gel" : " Usd")
                    : "";

                csvBuilder.AppendLine(string.Join(",",
                    apt.ApartmentId,
                    apt.UserPin,
                    EscapeForCsv(apt.Title ?? ""),
                    EscapeForCsv((apt.Price?.ToString() ?? "") + currencyName),
                    EscapeForCsv(apt.Status?.ToString() ?? ""),
                    apt.CreateDate?.ToString("yyyy-MM-dd") ?? "",
                    apt.UpdateDate?.ToString("yyyy-MM-dd") ?? "",
                    apt.AgencyId?.ToString() ?? ""
                ));
            }

            return Encoding.UTF8.GetBytes(csvBuilder.ToString());
        }


        private static string EscapeForCsv(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            if (input.Contains(",") || input.Contains("\"") || input.Contains("\n"))
                return $"\"{input.Replace("\"", "\"\"")}\"";
            return input;
        }
    }
}
