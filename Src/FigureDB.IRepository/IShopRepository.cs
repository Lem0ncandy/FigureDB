﻿using FigureDB.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FigureDB.IRepository
{
    public interface IShopRepository : IGenericRepository<Shop, Guid>
    {
    }
}
