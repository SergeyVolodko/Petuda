using System;

namespace Petuda.Model.DDD.Exceptions
{
    public class JokeCantBeCreatedException: Exception
    {
        private Joke _joke;
        public Joke Joke {
            get { return _joke; }
        }

        public JokeCantBeCreatedException(Joke joke)
        {
            _joke = joke;
        }
    }
}