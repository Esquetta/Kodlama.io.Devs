using Application.Services.Repositories;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.JWT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
    public class AuthManager : IAuthService
    {
        private readonly ITokenHelper tokenHelper;
        private readonly IRefreshTokenRepository refreshTokenRepository;
        private readonly IUserOperationClaimRepository userOperationClaimRepository;

        public AuthManager(ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository, IUserOperationClaimRepository userOperationClaimRepository)
        {
            this.tokenHelper = tokenHelper;
            this.refreshTokenRepository = refreshTokenRepository;
            this.userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
        {
            RefreshToken addedToken = await refreshTokenRepository.AddAsync(refreshToken);
            return addedToken;
        }

        public async Task<AccessToken> CreateAccessToken(User user)
        {
            IPaginate<UserOperationClaim> userOperationClaims = await userOperationClaimRepository.GetListAsync(x => x.UserId == user.Id, include: x => x.Include(x => x.OperationClaim));

            IList<OperationClaim> operationClaims = userOperationClaims.Items.Select(x => new OperationClaim { Id =x.OperationClaim.Id,Name=x.OperationClaim.Name}).ToList();

            AccessToken accessToken = tokenHelper.CreateToken(user, operationClaims);
            return accessToken;
        }

        public async Task<RefreshToken> CreateRefreshToken(User user, string IpAddress)
        {
            RefreshToken refreshToken = tokenHelper.CreateRefreshToken(user, IpAddress);
            return refreshToken;
        }
    }
}
