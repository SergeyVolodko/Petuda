using System;
using System.Collections.Generic;

namespace Petuda.Model.DDD.Services
{
    public interface IJokeService
    {
        Joke Load(Guid id);
        ICollection<Joke> LoadAllJokes();
        List<String> GetAllThemes();
        IEnumerable<Joke> GetFiltredJokes(string text, string theme, DateTime? dateFrom, DateTime? dateTo);
        Joke CreateJoke(string name, string theme, string text, List<string> tags);
        void UpdateJoke(Joke joke);
        void DeleteJoke(Guid jokeID);
        int GetJokesCount();
    }
}
