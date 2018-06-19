﻿using System.Collections.Generic;
using Domain.Entities;
using Domain.Logic;

namespace Domain.Interface
{
    public interface IDecorativeColumnRepository
    {
        void Add(Grid grid, DecorativeColumn decorativeColumn);
        List<DecorativeColumn> GetList(Grid grid);
        void Remove(Grid grid, DecorativeColumn decorativeColumn);
        bool Exist(Grid grid, DecorativeColumn decorativeColumn);
        int Count(Grid grid);
    }
}