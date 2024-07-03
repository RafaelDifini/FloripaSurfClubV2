using FloripaSurfClubCore.Enums;
using FloripaSurfClubCore.Requests.Account;
using FloripaSurfClubCore.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaSurfClubCore.Handlers
{
    public interface IAccountHandler
    {
        Task<Response<string>> LoginAsync(LoginRequest request);
        Task<Response<string>> RegisterAsync(RegistrarRequest request);
        Task LogoutAsync();
    }
}
