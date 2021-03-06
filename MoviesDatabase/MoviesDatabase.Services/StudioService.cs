﻿using System;
using System.Collections.Generic;
using System.Linq;
using MoviesDatabase.Data.Contracts;
using MoviesDatabase.Factories;
using MoviesDatabase.Models;
using MoviesDatabase.Services.Contracts;

namespace MoviesDatabase.Services
{
    public class StudioService : IStudioService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Studio> studioRepository;
        private readonly IStudioFactory studioFactory;

        public StudioService(IRepository<Studio> studioRepository, IUnitOfWork unitOfWork, IStudioFactory studioFactory)
        {
            if (studioRepository == null)
            {
                throw new ArgumentNullException("Studio repository cannot be null!");
            }

            if (studioFactory == null)
            {
                throw new ArgumentNullException("Studio factory cannot be null!");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work cannot be null!");
            }

            this.studioRepository = studioRepository;
            this.unitOfWork = unitOfWork;
            this.studioFactory = studioFactory;
        }

        public void AddStudios(IList<Studio> studios)
        {
            foreach (var studio in studios)
            {
                this.studioRepository.Add(studio);
            }

            this.unitOfWork.Commit();
        }

        public Studio CreateStudio(string name, string address)
        {
            var studio = this.studioFactory.CreateStudio(name, address);
            this.studioRepository.Add(studio);
            this.unitOfWork.Commit();

            return studio;
        }

        public Studio GetStudioByName(string name)
        {
            var studio = this.studioRepository.Entities
                .FirstOrDefault(s => s.Name == name);

            return studio;
        }

        public void UpdateStudio(Studio studio)
        {
            this.studioRepository.Update(studio);
            this.unitOfWork.Commit();
        }

        public void DeleteStudio(string name)
        {
            var studio = this.studioRepository.Entities
                .FirstOrDefault(s => s.Name == name);

            if (studio == null)
            {
                throw new NullReferenceException("There is not studio with this name.");
            }

            this.studioRepository.Delete(studio);
            this.unitOfWork.Commit();
        }
    }
}
