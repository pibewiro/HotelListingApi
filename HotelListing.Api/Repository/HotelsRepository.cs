using HotelListing.Api.Contracts;
using HotelListing.Api.Data;

namespace HotelListing.Api.Repository
{
    public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
    {   
        private readonly HotelListingDbContext _context;

        public HotelsRepository(HotelListingDbContext context) : base(context)
        {
            _context = context;
        }
    }
}