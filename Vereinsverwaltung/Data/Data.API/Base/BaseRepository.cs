using Vereinsverwaltung.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vereinsverwaltung.Data.Repository.Base
{
    public class BaseRepository
    {
        protected readonly RepositoryBase repo;
        public BaseRepository()
        {
            repo = GlobalVariables.GetRepoBase();
        }
    }
}
