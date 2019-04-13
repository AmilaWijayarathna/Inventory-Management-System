﻿using System.Collections.Generic;

namespace Galaxy.Base.Domain
{
    public class Product : BaseClass
    {
        public Product()
        {
        }
        public virtual string Name { get; set; }
        public virtual SubsidiaryUnit subsidiaryUnit { get; set; }
        public virtual Measurement measurement { get; set; }

        public virtual List<SubsidiaryUnit> SubsidiaryUnitList { get; set; }
    }
}
