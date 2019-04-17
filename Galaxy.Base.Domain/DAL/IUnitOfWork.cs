﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Base.Domain.DAL
{
    public interface IUnitOfWork:IDisposable
    {
        IProductRepository ProductRepository { get; }
        IMeasurementRepository MeasurementRepository { get; }
        IFactorRepository FactorRepository { get; }
        IPartyRepository PartyRepository { get; }
        IDocumentRepository DocumentRepository { get; }
        

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void CloseTransaction();
        void CloseSession();
    }
}
