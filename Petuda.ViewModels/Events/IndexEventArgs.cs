using System;

namespace Petuda.ViewModels.Events
{
    public class IndexEventArgs: EventArgs
    {
        private readonly int _index;

        public int Index
        {
            get { return _index; }
        }

        public IndexEventArgs(int index)
        {
            _index = index;
        }
    }
}