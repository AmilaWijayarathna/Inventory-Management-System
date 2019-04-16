﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Base.Domain;
using Galaxy.Base.Domain.DAL;
using NHibernate;


namespace Galaxy.Base.Data.DAL
{
   public class DocumentRepository:Repository<Document>
    {
       public DocumentRepository(ISession session, ITransaction transaction) : base(session, transaction)
       {
           
       }

    }
}
