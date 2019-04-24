﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Base.Service;
using Inventory.Domain;
using Galaxy.Base.Domain.DAL;
using Inventory.Domain.InventoryServiceInterface;

namespace InventoryServis
{
    public class InventoryProductDocumentService : Service<InventoryProductDocument> , IInventoryProductDocumentService
    {
        public InventoryProductDocumentService(IRepository<InventoryProductDocument> repository) : base(repository)
        {
 
        }
    }
}
