using Data.Model.KoenigschiessenModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Messages.KoenigschiessenMessages
{
    public class OpenKoenigschiessenBestaetigungMessage
    {
        public Action Command { get; set; }
        public KoenigschiessenAnmeldungUebersichtModel KoenigschiessenAnmeldung { get; set; }
    }
}
