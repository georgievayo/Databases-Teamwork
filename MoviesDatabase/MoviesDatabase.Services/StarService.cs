﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesDatabase.Data.Contracts;
using MoviesDatabase.Factories;
using MoviesDatabase.Models;
using MoviesDatabase.Parsers.Contracts;
using MoviesDatabase.Services.Contracts;

namespace MoviesDatabase.Services
{
    public class StarService : IStarService
    {
        private const string filePath = "...";
        private readonly IRepository<Star> starRepository;
        private readonly IJSONParser jsonParser;
        private readonly IStarFactory starFactory;

        public StarService(IRepository<Star> starRepository, IJSONParser jsonParser, IStarFactory starFactory)
        {
            if (starRepository == null)
            {
                throw new ArgumentNullException("Star repository cannot be null!");
            }

            if (jsonParser == null)
            {
                throw new ArgumentNullException("Json parser cannot be null!");
            }

            if (starFactory == null)
            {
                throw new ArgumentNullException("Star factory cannot be null!");
            }

            this.starRepository = starRepository;
            this.jsonParser = jsonParser;
            this.starFactory = starFactory;
        }

        public void AddStars()
        {
            var stars = this.jsonParser.Parse<Star>(filePath);

            foreach (var star in stars)
            {
                this.starRepository.Add(star);
            }
        }

        public Star CreateStar(string firstName, string lastName, int age, string address)
        {
            var star = this.starFactory.CreateStar(firstName, lastName, age, address);
            this.starRepository.Add(star);

            return star;
        }
    }
}