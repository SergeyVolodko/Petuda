using System;
using System.Collections.Generic;

namespace Petuda.Model.DDD.Repositories
{
    public interface IScriptRepository: IReposytory<Script>
    {
        IEnumerable<Script> GetScriptsThatContainsJokeID(Guid id);
        void Save(IEnumerable<Script> scripts);
    }
}