using System;
using System.Collections.Generic;
using System.Linq;
using Petuda.Model.DDD.DALContracts;
using Petuda.Model.DDD.Helpers;

//using Petuda.Model.DDD.Exceptions;

namespace Petuda.Model.DDD.Repositories
{
    public class JokeRepository : IJokeRepository
    {
        private IJokeDao jokeDao;
        
        public JokeRepository(IJokeDao jokeDao)
        {
            this.jokeDao = jokeDao;
        }

        public void Save(Joke item)
        {
            this.jokeDao.Save(item);
        }

        public ICollection<Joke> LoadAll()
        {
            return this.jokeDao.GetAll();
        }

        public Joke Load(Guid id)
        {
            return this.jokeDao.GetByID(id);
        }

        public void Remove(Guid id)
        {
            this.jokeDao.Remove(id);
        }

        public IEnumerable<Joke> Filter(string text, string theme, DateTime? dateFrom, DateTime? dateTo)
        {
            var jokes = this.jokeDao.GetAll();
            var filtredJokes = jokes.Where(j => FilterHelper.TextFilterPassed(text, j) &&
                                                FilterHelper.ThemeFilterPassed(theme, j.Theme) &&
                                                FilterHelper.DateFromFilterPassed(dateFrom, j.Date) &&
                                                FilterHelper.DateToFilterPassed(dateTo, j.Date));
            
            return filtredJokes;
        }

        public int GetJokesCount()
        {
            return this.jokeDao.GetJokesCount();
        }
    }//class
}//namespace