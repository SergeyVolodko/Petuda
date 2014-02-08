using System;
using System.Collections.Generic;

namespace Petuda.Model.DDD.Services
{
    public interface IScriptService
    {
        ICollection<Script> LoadAllScripts();
        Script CreateScript(string name, DateTime? gameDate);
        void UpdateScript(Script script);
        void RemoveScript(Guid scriptID);
        void AddJokeToScript(Guid scriptId, Guid jokeId, int? index = null);
        void RemoveJokeFromScripts(Guid jokeId);
        void RemoveJokeFromScript(Guid scriptId, Guid jokeId);
        void MoveJokeInScriptIndex(Guid scriptId, int prevIndex, int? newIndex);
    }
}