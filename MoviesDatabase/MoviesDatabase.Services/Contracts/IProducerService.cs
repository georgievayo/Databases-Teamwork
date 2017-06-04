﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesDatabase.Models;

namespace MoviesDatabase.Services.Contracts
{
    public interface IProducerService
    {
        void AddProducers();

        Producer CreateProducer(string name);

        Producer GetProducerBy(string name);
    }
}