using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.VereinsmeisterschaftModels
{
    public class VereinsmeisterschaftModel
    {
        public int ID { get; set; }
        public DateTime Erstelldatum { get; set; }
        public DateTime Stichtag { get; set; }
        public int Jahr { get; set; }
    }
}
