using System;

namespace Petuda.Model.DDD.Exceptions
{
    public class ScriptAlreadyContainsJokeException: Exception
    {
        public int JokeInScriptIndex { get; set; }

        public ScriptAlreadyContainsJokeException(int jokeInScriptIndex)
        {
            this.JokeInScriptIndex = jokeInScriptIndex;
        }
    }
}