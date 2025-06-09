using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Feature.Manage.Apartment.List;
using RealEstate.Infrastructure.Data;
using System.Text;

namespace RealEstate.Application.Feature.Manage.Export.Apartment
{
    public class ExportApartmentListCsvHandler : IRequestHandler<ExportApartmentListCsvRequest, ExportApartmentListCsvResponse>
    {
        private readonly ApplicationDbContext _db;

        public ExportApartmentListCsvHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ExportApartmentListCsvResponse> Handle(ExportApartmentListCsvRequest request, CancellationToken cancellationToken)
        {
            var apartments = await _db.Apartments
                .Include(a => a.User)
                .Where(request.Filter.WhereClause())
                .Select(a => new
                {
                    a.ApartmentId,
                    a.Title,
                    a.Status,
                    a.CreateDate,
                    a.UpdateDate,
                    a.DeleteDate,
                    UserPin = a.User != null ? a.User.PIN : "",
                    a.Price,
                    a.CurrencyId,
                    a.AgencyId
                })
                .ToListAsync(cancellationToken);

            var sb = new StringBuilder();
            sb.AppendLine("ApartmentId,Title,Status,CreateDate,UpdateDate,DeleteDate,UserPin,Price,CurrencyId,AgencyId");

            foreach (var apt in apartments)
            {
                sb.AppendLine($"{apt.ApartmentId},{apt.Title},{apt.Status},{apt.CreateDate},{apt.UpdateDate},{apt.DeleteDate},{apt.UserPin},{apt.Price},{apt.CurrencyId},{apt.AgencyId}");
            }

            return new ExportApartmentListCsvResponse
            {
                FileContent = Encoding.UTF8.GetBytes(sb.ToString())
            };
        }
    }
}
