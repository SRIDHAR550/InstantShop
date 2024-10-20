using InstantShop.Application.AuthClass;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantShop.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IEnumerable<IdentityError>> Reister(Register register);
        Task<object> Login(Login login);
    }
}
