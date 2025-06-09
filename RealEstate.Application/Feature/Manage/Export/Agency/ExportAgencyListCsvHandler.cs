using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Feature.Manage.Agency.List;
using RealEstate.Infrastructure.Data;
using System.Text;

namespace RealEstate.Application.Feature.Manage.Export.Agency
{
    public class ExportAgencyListCsvHandler : IRequestHandler<ExportAgencyListCsvRequest, ExportAgencyListCsvResponse>
    {
        private readonly ApplicationDbContext _db;

        public ExportAgencyListCsvHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ExportAgencyListCsvResponse> Handle(ExportAgencyListCsvRequest request, CancellationToken cancellationToken)
        {
            var agencies = _db.Agencies
                .Where(request.Filter.WhereClause())
                .Join(_db.Users,
                      agency => agency.UserId,
                      user => user.UserId,
                      (agency, user) => new { Agency = agency, User = user });

            if (!string.IsNullOrEmpty(request.Filter.OwnerPIN))
                agencies = agencies.Where(x => x.User.PIN.Contains(request.Filter.OwnerPIN));

            var list = await agencies
                .Select(x => new
                {
                    x.Agency.AgencyId,
                    x.Agency.AgencyType,
                    x.Agency.Name,
                    x.Agency.IsApproved,
                    x.Agency.IsDeleted,
                    x.Agency.IdentificationNumber,
                    x.User.PIN,
                    x.Agency.Email,
                    x.Agency.PhoneNumber,
                    x.Agency.UpdateDate,
                    x.Agency.DeleteDate,
                    x.Agency.CreateDate
                })
                .ToListAsync(cancellationToken);

            var sb = new StringBuilder();
            sb.AppendLine("AgencyId,Type,Name,IsApproved,IsDeleted,IdentificationNumber,OwnerPIN,Email,PhoneNumber,UpdateDate,DeleteDate,CreateDate");

            foreach (var item in list)
            {
                sb.AppendLine($"{item.AgencyId},{item.AgencyType},{item.Name},{item.IsApproved},{item.IsDeleted},{item.IdentificationNumber},{item.PIN},{item.Email},{item.PhoneNumber},{item.UpdateDate},{item.DeleteDate},{item.CreateDate}");
            }

            return new ExportAgencyListCsvResponse
            {
                FileContent = Encoding.UTF8.GetBytes(sb.ToString())
            };
        }
    }
}
