using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class DeveloperRepository : EfRepositoryBase<Developer, BaseDbContext>, IDeveloperRepository
    {
        public DeveloperRepository(BaseDbContext context) : base(context)
        {
        }

        public List<OperationClaim> GetOperationClaims(User user)
        {
            var claims = from OperationClaim in Context.OperationClaims
                         join UserOperationClaim in Context.UserOperationClaims
                         on OperationClaim.Id equals UserOperationClaim.Id
                         where UserOperationClaim.UserId == user.Id
                         select new OperationClaim { Id = OperationClaim.Id, Name = OperationClaim.Name };

            List<OperationClaim> operationClaims = claims.ToList();
            return operationClaims;
        }
    }
}
