﻿﻿using System;
using System.Collections.Generic;
using System.Reflection;
using MoviesDatabase.CLI.Commands.Contracts;
using MoviesDatabase.Models;
using MoviesDatabase.Services.Contracts;

namespace MoviesDatabase.CLI.Commands
{
    public class UpdateCommand : ICommand
    {
		private readonly IBookService BookService;
		private readonly IStarService StarService;
		private readonly IStudioService StudioService;

        public UpdateCommand(IBookService bookService, IStarService starService, IStudioService studioService)
        {
			if (bookService == null)
			{
				throw new ArgumentNullException("Book service cannot be null.");
			}

			if (starService == null)
			{
				throw new ArgumentNullException("Star service cannot be null.");
			}

			if (studioService == null)
			{
				throw new ArgumentNullException("Studio service cannot be null.");
			}

			this.BookService = bookService;
			this.StarService = starService;
			this.StudioService = studioService;
        }

        public string Execute(IList<string> parameters)
        {
            string model = parameters[0];
            string name = parameters[1].Replace('_', ' ');

			parameters.RemoveAt(0);
			parameters.RemoveAt(0);

			switch (model.ToLower())
			{
				case "book":
					Book currentBook = this.BookService.GetBookByTitle(name);

				    if (currentBook == null)
				    {
                        throw new NullReferenceException("There is not such book in database.");
				    }

					foreach (string parameter in parameters)
					{
						KeyValuePair<string, string> update = new KeyValuePair<string, string>(parameter.Split(':')[0], parameter.Split(':')[1]);

						PropertyInfo propertyInfo = currentBook.GetType().GetProperty(update.Key);
                        propertyInfo.SetValue(currentBook, Convert.ChangeType(update.Value, (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)) ? (Nullable.GetUnderlyingType(propertyInfo.PropertyType)) : (propertyInfo.PropertyType)), null);
					}

					this.BookService.UpdateBook(currentBook);

                    break;
                case "star":
                    string firstName = name.Split(' ')[0];
					string lastName = name.Split(' ')[1];

					Star currentStar = this.StarService.GetStarByName(firstName, lastName);
				    
                    if (currentStar == null)
				    {
                        throw new NullReferenceException("There is not such star in database.");
				    }

                    foreach (string parameter in parameters)
                    {
                        KeyValuePair<string, string> update = new KeyValuePair<string, string>(parameter.Split(':')[0], parameter.Split(':')[1]);

                        PropertyInfo propertyInfo = currentStar.GetType().GetProperty(update.Key);
                        propertyInfo.SetValue(currentStar, Convert.ChangeType(update.Value, (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)) ? (Nullable.GetUnderlyingType(propertyInfo.PropertyType)) : (propertyInfo.PropertyType)),null);
                    }

                    this.StarService.UpdateStar(currentStar);

                    break;
				case "studio":
					Studio currentStudio = this.StudioService.GetStudioByName(name);
				    
                    if (currentStudio == null)
				    {
                        throw new NullReferenceException("There is not such studio in database.");
				    }

                    foreach (string parameter in parameters)
					{
						KeyValuePair<string, string> update = new KeyValuePair<string, string>(parameter.Split(':')[0], parameter.Split(':')[1]);

						PropertyInfo propertyInfo = currentStudio.GetType().GetProperty(update.Key);
						propertyInfo.SetValue(currentStudio, Convert.ChangeType(update.Value, (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)) ? (Nullable.GetUnderlyingType(propertyInfo.PropertyType)) : (propertyInfo.PropertyType)), null);
					}

                    this.StudioService.UpdateStudio(currentStudio);

                    break;
				default:
                    throw new ArgumentException($"{model}s cannot be updated.");
			}

            return $"{name} was updated successfully.";
		}
    }
}
// Sample command:
// Update Star Ivan Ivanov age:20 address:Sofia