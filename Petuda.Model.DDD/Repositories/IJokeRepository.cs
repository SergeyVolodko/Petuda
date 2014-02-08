using System;
using System.Collections.Generic;

namespace Petuda.Model.DDD.Repositories
{
    public interface IJokeRepository: IReposytory<Joke>
    {
        IEnumerable<Joke> Filter(string text, string theme, DateTime? dateFrom, DateTime? dateTo);
        int GetJokesCount();
    }
}