using Application.Features.UserOperationClaims.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Models
{
    public class UserOperationClaimListViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserOperationsClaimsListDto[] Claims { get; set; }
    }
}
