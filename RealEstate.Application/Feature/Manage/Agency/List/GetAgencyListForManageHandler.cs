using Microsoft.EntityFrameworkCore;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Manage.Agency.List
{
    public class GetAgencyListForManageHandler : IRequestHandler<GetAgencyListForManageRequest, GetAgencyListForManageResponse>
    {
        readonly ApplicationDbContext _db;
        public GetAgencyListForManageHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<GetAgencyListForManageResponse> Handle(GetAgencyListForManageRequest request, CancellationToken cancellationToken)
        {
            var query = _db.Agencies.AsQueryable();

            if (request.AgencyId.HasValue)
                query = query.Where(a => a.AgencyId == request.AgencyId);

            if (request.AgencyType.HasValue)
                query = query.Where(a => a.AgencyType == request.AgencyType);

            if (!string.IsNullOrEmpty(request.Name))
                query = query.Where(a => a.Name.ToUpper().Contains(request.Name.ToUpper()));

            if (request.IsApproved.HasValue)
                query = query.Where(a => a.IsApproved == request.IsApproved);

            if (request.IsDeleted.HasValue)
                query = query.Where(a => a.IsDeleted == request.IsDeleted);

            if (!string.IsNullOrEmpty(request.IdentificationNumber))
                query = query.Where(a => a.IdentificationNumber.Contains(request.IdentificationNumber));

            if (!string.IsNullOrEmpty(request.Email))
                query = query.Where(a => a.Email.ToUpper().Contains(request.Email.ToUpper()));

            if (!string.IsNullOrEmpty(request.PhoneNumber))
                query = query.Where(a => a.PhoneNumber.Contains(request.PhoneNumber));

            if (request.UpdateDateFrom.HasValue)
                query = query.Where(a => a.UpdateDate >= request.UpdateDateFrom);
            if (request.UpdateDateTo.HasValue)
                query = query.Where(a => a.UpdateDate <= request.UpdateDateTo);

            if (request.DeleteDateFrom.HasValue)
                query = query.Where(a => a.DeleteDate >= request.DeleteDateFrom);

            if (request.DeleteDateTo.HasValue)
                query = query.Where(a => a.DeleteDate <= request.DeleteDateTo);

            if (request.CreateDateFrom.HasValue)
                query = query.Where(u => u.CreateDate >= request.CreateDateFrom);

            if (request.CreateDateTo.HasValue)
                query = query.Where(u => u.CreateDate <= request.CreateDateTo);

            var agenciesWithUsers = from agency in query
                                    join user in _db.Users on agency.UserId equals user.UserId
                                    select new
                                    {
                                        Agency = agency,
                                        User = user
                                    };

            int page = request.Page ?? 1;
            int pageSize = request.PageSize ?? 20;
            int skip = (page - 1) * pageSize;

            var agencies = await agenciesWithUsers
            .OrderBy(a => a.Agency.AgencyId)
            .Skip(skip)
            .Take(pageSize)
            .Select(a => new GetAgencyListForManageItemsResponse
            {
                AgencyId = a.Agency.AgencyId,
                AgencyType = a.Agency.AgencyType,
                Name = a.Agency.Name,
                IsApproved = a.Agency.IsApproved,
                IsDeleted = a.Agency.IsDeleted,
                IdentificationNumber = a.Agency.IdentificationNumber,
                Email = a.Agency.Email,
                PhoneNumber = a.Agency.PhoneNumber,
                UpdateDate = a.Agency.UpdateDate,
                DeleteDate = a.Agency.DeleteDate,
                UserId = a.Agency.UserId,
                UserPin = a.User.PIN,
                CreateDate = a.Agency.CreateDate
            })
            .ToListAsync(cancellationToken);

            return new GetAgencyListForManageResponse
            {
                TotalCount = agencies.Count,
                Pagination = new Common.Models.Pagination { Page = page, PageSize = pageSize },
                AgencyListForManage = agencies,
                StatusCode = 200,
                Success = true
            };
        }

    }
}
