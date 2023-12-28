using ProniaOnion.Application.DTOs.Tokens;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.Abstraction.Services
{
    public interface ITokenHandler
    {
        TokenResponseDto CreateJwt(AppUser user, int minutes);
    }
}
