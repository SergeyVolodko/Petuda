using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Petuda.Model.DDD;
using Petuda.Model.DDD.DALContracts;
using PetudaDAL.XML.Exceptions;

namespace PetudaDAL.XML
{
    public class JokeXmlDao: IJokeDao
    {
        private readonly XMLSerializer<Joke> jokeSerializer;
        private ObservableCollection<Joke> jokes;
        private const String _fileName = "Jokes.xml";

        public JokeXmlDao()
        {
            jokeSerializer = new XMLSerializer<Joke>(_fileName);
        }

        public Joke GetByID(Guid id)
        {
            if (this.jokes == null)
            {
                GetAll();
            }

            var joke = this.jokes.FirstOrDefault(j => j.ID == id);

            return joke;
        }

        public ICollection<Joke> GetAll()
        {
            try
            {
                jokeSerializer.Read(out this.jokes);
            }
            catch
            {
                throw new ReadFileException(_fileName);
            }

            return jokes ?? (jokes = new ObservableCollection<Joke>());
        }

        public void Remove(Guid id)
        {
            if (this.jokes == null)
            {
               GetAll();
            }

            var joke = this.jokes.FirstOrDefault(j => j.ID == id);

            this.jokes.Remove(joke);

            SaveCollection();
        }

        public int GetJokesCount()
        {
            return this.jokes != null ? this.jokes.Count : 0;
        }

        public void Save(Joke item)
        {
            if (this.jokes == null)
            {
                GetAll();
            }

            var oldJoke = this.jokes.FirstOrDefault(j => j.ID == item.ID);

            // update joke if it already exists
            if (oldJoke != null)
            {
                var oldIndex = jokes.IndexOf(oldJoke);
                this.jokes[oldIndex] = item;
            }
            else
            {
                this.jokes.Add(item);
            }

            SaveCollection();
        }

        private void SaveCollection()
        {
            try
            {
                 jokeSerializer.Save(this.jokes);
            }
            catch
            {
                throw new SaveFileException(_fileName);
            }
        }

    }//class
}//namespace