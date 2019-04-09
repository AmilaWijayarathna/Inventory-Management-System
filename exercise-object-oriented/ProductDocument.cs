﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercise_object_oriented
{
    public abstract class ProductDocument : BaseClass
    {

        public virtual int OrginalPrice { get; set; }
        public virtual int Piece { get; set; }
        public virtual int Profit { get; set; }
        public virtual float SellingPrice { get; set; }
        public virtual Product product { get; set; }
        public virtual FactorsList FactorsList { get; set; }

      
        
        public decimal Calculate(decimal Price)
        {
            Factor factor;
            for (int i = 1; i <= FactorsList.factorsList.Count(); i++)
            {
                factor = FactorsList.factorsList.Find(x => x.Priority == i);
                decimal QuantityFactor=CalculateFactor(factor, Price);
                Price += QuantityFactor;
            }

            return Price;
        }

        public decimal CalculateFactor(Factor factor, decimal Price)
        {
            decimal tmp=0;
            switch (factor.operation)
            {
                case Operation.Percentage:
                    tmp = ((factor.Quantity / 100) * Price);
                    if (factor.Increasing == false)
                    {
                        tmp = -tmp;
                    }
                    break;
                case Operation.Unit:
                    tmp = factor.Quantity;
                    if (factor.Increasing == false)
                    {
                        tmp = -tmp;
                    }
                    break;
                case Operation.Amount:
                    if (factor.Increasing == true)
                    {
                        /****************************************************************************************************/
                        //
                    }
                    else
                    {
                        /****************************************************************************************************/
                        //
                    }
                    break;
                default:
                    break;

            }
            return tmp;
        }


    }
    public  abstract class ProductDocument<T> :ProductDocument where T: Document
    {
        public virtual T Document { get; set; }
    }
}
