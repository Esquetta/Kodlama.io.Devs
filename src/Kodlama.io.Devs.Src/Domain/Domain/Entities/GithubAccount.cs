using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GithubAccount : Entity
    {
        public string AccountLink { get; set; }

        public virtual List<Developer> Developers { get; set; }

        public GithubAccount()
        {

        }

        public GithubAccount(int id, string AccountLink)
        {
            this.AccountLink = AccountLink;
            this.Id = id;
        }
    }
}
