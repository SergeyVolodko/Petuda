using System;

namespace Petuda.Model.DDD.Factories
{
    public interface IScriptFactory
    {
        Script Create(String name/*, String league*/, DateTime? gameDate);
    }
}