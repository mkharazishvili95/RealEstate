using Microsoft.Extensions.Configuration;
using RealEstate.Common.Enums.Apartment;
using RealEstate.Infrastructure.Queries.Models.Apartment;

namespace RealEstate.Infrastructure.Queries.Apartment
{
    public class ApartmentQueries : QueriesBase, IApartmentQueries
    {
        public ApartmentQueries(IConfiguration configuration) : base(configuration) { }

        public async Task<GetApartmentDetailsModel?> GetApartment(int apartmentId)
        {
            var query = @$"
                SELECT 
                    ApartmentId,
                    Title,
                    Description,
                    Status,
                    CreateDate,
                    EndDate,
                    UpdateDate,
                    DeleteDate,
                    UserId,
                    Price,
                    CurrencyId,
                    AgencyId
                    FROM Apartments
                    WHERE ApartmentId = {apartmentId} ";

            return await Get(query, reader => new GetApartmentDetailsModel
            {
                ApartmentId = reader.GetInt32(reader.GetOrdinal("ApartmentId")),
                Title = reader.IsDBNull(reader.GetOrdinal("Title")) ? null : reader.GetString(reader.GetOrdinal("Title")),
                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : (ApartmentStatus)reader.GetInt32(reader.GetOrdinal("Status")),
                CreateDate = reader.IsDBNull(reader.GetOrdinal("CreateDate")) ? null : reader.GetDateTime(reader.GetOrdinal("CreateDate")),
                EndDate = reader.IsDBNull(reader.GetOrdinal("EndDate")) ? null : reader.GetDateTime(reader.GetOrdinal("EndDate")),
                UpdateDate = reader.IsDBNull(reader.GetOrdinal("UpdateDate")) ? null : reader.GetDateTime(reader.GetOrdinal("UpdateDate")),
                DeleteDate = reader.IsDBNull(reader.GetOrdinal("DeleteDate")) ? null : reader.GetDateTime(reader.GetOrdinal("DeleteDate")),
                UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? null : reader.GetString(reader.GetOrdinal("UserId")),
                Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? null : reader.GetDecimal(reader.GetOrdinal("Price")),
                CurrencyId = reader.IsDBNull(reader.GetOrdinal("CurrencyId")) ? null : reader.GetInt32(reader.GetOrdinal("CurrencyId")),
                AgencyId = reader.IsDBNull(reader.GetOrdinal("AgencyId")) ? null : reader.GetInt32(reader.GetOrdinal("AgencyId"))
            });
        }

        public Task<GetFiltredApartmentsModel> GetFiltredApartments()
        {
            throw new NotImplementedException();
        }

        public async Task<GetNewApartmentsModel> GetNewApartments()
        {
            var query = @"
                SELECT DISTINCT TOP 10
                ApartmentId,
                Title,
                Description,
                Status,
                CreateDate,
                EndDate,
                UpdateDate,
                DeleteDate,
                UserId,
                Price,
                CurrencyId,
                AgencyId
            FROM Apartments
            ORDER BY CreateDate DESC ";

            var apartments = await GetMany(query, reader => new GetNewApartmentsItemModel
            {
                ApartmentId = reader.GetInt32(reader.GetOrdinal("ApartmentId")),
                Title = reader.IsDBNull(reader.GetOrdinal("Title")) ? null : reader.GetString(reader.GetOrdinal("Title")),
                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : (ApartmentStatus)reader.GetInt32(reader.GetOrdinal("Status")),
                CreateDate = reader.IsDBNull(reader.GetOrdinal("CreateDate")) ? null : reader.GetDateTime(reader.GetOrdinal("CreateDate")),
                EndDate = reader.IsDBNull(reader.GetOrdinal("EndDate")) ? null : reader.GetDateTime(reader.GetOrdinal("EndDate")),
                UpdateDate = reader.IsDBNull(reader.GetOrdinal("UpdateDate")) ? null : reader.GetDateTime(reader.GetOrdinal("UpdateDate")),
                DeleteDate = reader.IsDBNull(reader.GetOrdinal("DeleteDate")) ? null : reader.GetDateTime(reader.GetOrdinal("DeleteDate")),
                UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? null : reader.GetString(reader.GetOrdinal("UserId")),
                Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? null : reader.GetDecimal(reader.GetOrdinal("Price")),
                CurrencyId = reader.IsDBNull(reader.GetOrdinal("CurrencyId")) ? null : reader.GetInt32(reader.GetOrdinal("CurrencyId")),
                AgencyId = reader.IsDBNull(reader.GetOrdinal("AgencyId")) ? null : reader.GetInt32(reader.GetOrdinal("AgencyId"))
            });

            return new GetNewApartmentsModel
            {
                TotalCount = apartments.Count,
                ApartmentList = apartments
            };
        }

    }
}

