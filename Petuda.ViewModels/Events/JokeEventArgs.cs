using System;

namespace Petuda.ViewModels.Events
{
    public class JokeEventArgs : EventArgs
    {
        private readonly Guid _jokeID;

        public Guid JokeID
        {
            get { return _jokeID; }
        }

        public JokeEventArgs(Guid jokeID)
        {
            _jokeID = jokeID;
        }
    }
}