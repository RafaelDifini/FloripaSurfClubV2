using FloripaSurfClubCore.Data;

namespace FloripaSurfClubAPI.Endpoints
{
    public abstract class BaseEndpoint
    {
        protected readonly FloripaSurfClubContextV2 _context;

        protected BaseEndpoint(FloripaSurfClubContextV2 context)
        {
            _context = context;
        }
    }
}
