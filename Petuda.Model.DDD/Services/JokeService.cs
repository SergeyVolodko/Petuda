using System;
using System.Collections.Generic;
using System.Linq;
using Petuda.Model.DDD.Exceptions;
using Petuda.Model.DDD.Factories;
using Petuda.Model.DDD.Repositories;

namespace Petuda.Model.DDD.Services
{
    public class JokeService : IJokeService
    {
        private readonly IJokeFactory jokeFactory;
        private readonly IJokeRepository jokeRepository;

        public JokeService(IJokeRepository jokeRepository, IJokeFactory jokeFactory)
        {
            this.jokeFactory = jokeFactory;
            this.jokeRepository = jokeRepository;
        }

        public Joke Load(Guid id)
        {
            try
            {
                return this.jokeRepository.Load(id);
            }
            catch
            {
                throw;
            }
        }

        public ICollection<Joke> LoadAllJokes()
        {
            try
            {
                return this.jokeRepository.LoadAll();
            }
            catch
            {
                throw;
            }
        }

        public List<String> GetAllThemes()
        {
            var jokes = jokeRepository.LoadAll();
            var themes = new SortedSet<String>(){""};
            
            foreach (var joke in jokes)
            {
                themes.Add(joke.Theme);
            }

            return themes.ToList();
        }

        public IEnumerable<Joke> GetFiltredJokes(string text, string theme, DateTime? dateFrom, DateTime? dateTo)
        {
            return jokeRepository.Filter(text, theme, dateFrom, dateTo);
        }

        public Joke CreateJoke(string name, string theme, string text, List<string> tags)
        {
            var joke = this.jokeFactory.Create(name, theme, text, tags);

            try
            {
                this.jokeRepository.Save(joke);
            }
            catch (Exception)
            {
                throw new JokeCantBeCreatedException(joke);
            }

            return joke;
        }

        public void UpdateJoke(Joke joke)
        {
            try
            {
                this.jokeRepository.Save(joke);
            }
            catch
            {
                throw;
            }
            
        }

        public void DeleteJoke(Guid jokeID)
        {
            try
            {
                this.jokeRepository.Remove(jokeID);
            }
            catch
            {
                throw;
            }
        }

        public int GetJokesCount()
        {
            return this.jokeRepository.GetJokesCount();
        }

    }//class
}//namespace