using System;
using System.Collections.Generic;

namespace Petuda.Model.DDD.Factories
{
    public class ScriptFactory : IScriptFactory
    {
        public Script Create(String name/*, String league*/, DateTime? gameDate)
        {
            return new Script()
                {
                    ID = Guid.NewGuid(),
                    Name = name,
                    //League = league,
                    GameDate = gameDate,
                    JokesIDs = new List<Guid>()
                };
        }
    }
}