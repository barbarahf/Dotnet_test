using System;
using System.Collections.Generic;

namespace isolutions.GrillMaster.Entities
{
    public class GrillMenu
    {
        public Guid Id { get; set; }
        public string Menu { get; set; }
        public IEnumerable<GrillItem> Items { get; set; }


        public override string ToString()
        {
            return "Id: " + Id + " Menu: " + Menu + " Items: " + Items;
        }
    }
}