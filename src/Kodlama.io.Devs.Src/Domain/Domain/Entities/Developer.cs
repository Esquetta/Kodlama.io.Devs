using Core.Security.Entities;
using Core.Security.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Developer : User
    {
        public int? GithubAccountId { get; set; }
        public virtual GithubAccount? GithubAccount { get; set; }

        public Developer(int id, string firstName, string lastName, string email, byte[] passwordSalt, byte[] passwordHash,
                bool status, AuthenticatorType authenticatorType, int GithubAccountId) : this()
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            Status = status;
            AuthenticatorType = authenticatorType;
            GithubAccountId = GithubAccountId;
        }

        public Developer()
        {
        }
    }
}
