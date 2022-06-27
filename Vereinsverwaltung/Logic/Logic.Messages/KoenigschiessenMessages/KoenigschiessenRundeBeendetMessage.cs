using Data.Model.KoenigschiessenModels.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Messages.KoenigschiessenMessages
{
    public class KoenigschiessenRundeBeendetMessage
    {
        public KoenigschiessenAbschlussDTO KoenigschiessenAbschluss { get; set; }
    }
}
