using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Feature.Manage.User.List;
using RealEstate.Infrastructure.Data;
using System.Text;

namespace RealEstate.Application.Feature.Manage.Export.User
{
    public class ExportUserListCsvHandler : IRequestHandler<ExportUserListCsvRequest, ExportUserListCsvResponse>
    {
        private readonly ApplicationDbContext _db;

        public ExportUserListCsvHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ExportUserListCsvResponse> Handle(ExportUserListCsvRequest request, CancellationToken cancellationToken)
        {
            var users = await _db.Users
                .Where(request.Filter.WhereClause())
                .Select(x => new
                {
                    x.UserId,
                    x.FirstName,
                    x.LastName,
                    x.PIN,
                    x.UserName,
                    x.Email,
                    x.PhoneNumber,
                    x.Balance,
                    x.IsBlocked,
                    x.BlockDate,
                    x.Gender,
                    x.Type,
                    x.RegisterDate
                })
                .ToListAsync(cancellationToken);

            var sb = new StringBuilder();
            sb.AppendLine("ID,FirstName,LastName,PIN,UserName,Email,PhoneNumber,Balance,IsBlocked,BlockDate,Gender,Type,RegisterDate");

            foreach (var user in users)
            {
                sb.AppendLine($"{user.UserId},{user.FirstName},{user.LastName},{user.PIN}," +
                              $"{user.UserName},{user.Email},{user.PhoneNumber},{user.Balance}," +
                              $"{user.IsBlocked},{user.BlockDate},{user.Gender},{user.Type},{user.RegisterDate}");
            }

            return new ExportUserListCsvResponse
            {
                FileContent = Encoding.UTF8.GetBytes(sb.ToString())
            };
        }
    }
}
