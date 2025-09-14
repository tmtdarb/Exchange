﻿using Exchange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Domain.Interfaces
{
    public interface IExchangeRatesRepository
    {
        Task<List<ExchangeRate>> GetAllExchangeRates();
    }
}
