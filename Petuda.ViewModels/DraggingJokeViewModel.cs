using System;
using Petuda.Model.DDD;

namespace Petuda.ViewModels
{
    public class DraggingJokeViewModel : BaseViewModel
    {
        private Joke _joke;
        
        private int? _prevIndex;
        private bool _isDragging;
        
        public bool IsDragging
        {
            get { return _isDragging; }
            set
            {
                if (value == _isDragging)
                    return;

                _isDragging = value;
                NotifyPropertChanged("IsDragging");
            }
        }

        public Joke Joke
        {
            get { return _joke; }
            set
            {
                if (value == _joke)
                    return;

                _joke = value;
                NotifyPropertChanged("Joke");
            }
        }

        public int? PrevIndex
        {
            get { return _prevIndex; }
            set
            {
                if (value == _prevIndex)
                    return;

                _prevIndex = value;
                NotifyPropertChanged("PrevIndex");
            }
        }

        public DraggingJokeViewModel(Joke joke, int? prevIndex = null)
        {
            this.Joke = joke;
            this.PrevIndex = prevIndex;
        }

    }//class
}//namespace