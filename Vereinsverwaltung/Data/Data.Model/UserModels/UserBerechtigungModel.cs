using Data.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.UserModels
{
    public class UserBerechtigungModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public BerechtigungTypes Berechtigung { get; set; }
    }
}
