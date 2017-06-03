﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesDatabase.Models;
using Newtonsoft.Json;

namespace MoviesDatabase.Parsers
{
    public class JSONParser
    {
        private StreamReader reader;

        public List<T> Parse<T>(string filePath)
        {
            var collection = new List<T>();
            this.reader = new StreamReader(filePath);
            var json = this.reader.ReadToEnd();

            collection = JsonConvert.DeserializeObject<List<T>>(json);
            return collection;
        }
    }
}
