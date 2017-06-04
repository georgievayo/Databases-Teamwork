﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesDatabase.Models;

namespace MoviesDatabase.Services.Contracts
{
    public interface IStarService
    {
        void AddStars();

        Star CreateStar(string firstName, string lastName, int age, string address);
    }
}