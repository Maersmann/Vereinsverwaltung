using Vereinsverwaltung.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vereinsverwaltung.Data.Infrastructure.Base
{
    public class BaseRepository
    {
        protected readonly Repository repo;
        public BaseRepository()
        {
            repo = GlobalVariables.GetRepoBase();
        }
    }
}
