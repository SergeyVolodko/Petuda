using System;
using System.Collections.Generic;

namespace Petuda.Model.DDD.Factories
{
    public interface IJokeFactory
    {
        Joke Create(String name, String theme, String text, List<String> tags);
    }
}