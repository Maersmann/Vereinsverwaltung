﻿using Vereinsverwaltung.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vereinsverwaltung.Data.Repository.Base
{
    public class Database
    {
    
        public void OpenConnection()
        {
            GlobalVariables.CreateRepoBase();
        }
    }
}
