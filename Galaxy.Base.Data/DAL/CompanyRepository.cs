﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galaxy.Base.Domain.DAL;
using Galaxy.Base.Domain;
using NHibernate;

namespace Galaxy.Base.Data.DAL
{
    public class CompanyRepository : IRepository<Company> , ICompanyRepository 
    {
        public CompanyRepository(ISession session, ITransaction transaction) : base(session, transaction)
        {

        }
    }
}
