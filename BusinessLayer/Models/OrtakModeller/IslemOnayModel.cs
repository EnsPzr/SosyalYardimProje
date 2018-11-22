using System;
using System.Collections.Generic;

namespace BusinessLayer.Models.OrtakModeller
{
    public class IslemOnayModel
    {
        public bool? TamamlandiMi { get; set; }

        public List<String> HataMesajlari { get; set; }

        public IslemOnayModel()
        {
            HataMesajlari = new List<string>();
        }
    }
}
