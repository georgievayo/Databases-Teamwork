﻿using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.PostgreSQL
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext()
            : base("name=UsersConnection")
        {

        }

        public IDbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            this.OnUserModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void OnUserModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(user => user.Username)
                .HasColumnName("Username")
                .HasMaxLength(30)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_Username")
                        { IsUnique = true }
                        ));

            modelBuilder.Entity<User>()
                .Property(user => user.PassHash)
                .HasColumnName("PassHash")
                .HasMaxLength(60);

        }
    }
}
